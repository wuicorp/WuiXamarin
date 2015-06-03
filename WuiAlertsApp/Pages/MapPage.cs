using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Labs;
using WuiAlertsApp.ViewModels;
using WuiAlertsApp.Models;
using System.Threading;
using WuiAlertsApp.Helpers;


//using Xamarin.Forms.Labs.Services.Geolocation;

namespace WuiAlertsApp.Pages
{
	public class MapPage : ContentPage
	{
		private MapViewModel ViewModel
		{
			get { return BindingContext as MapViewModel; }
		}

		// TODO: Uncomment once Xamarin.Forms supports this, hopefully w/ version 1.1.
		//IDictionary<Pin,T> PinMap;

		public MapPage(MapViewModel viewModel)
		{
			BindingContext = viewModel;

            //this.SetBinding(Page.TitleProperty, "Title");
            //this.SetBinding(Page.IconProperty, "Icon");

			Map map = MakeMap();
			var stack = new StackLayout { Spacing = 0 };

			//#if __ANDROID__ || __IOS__
			var searchAddress = new SearchBar { Placeholder = "Search Address", BackgroundColor = Color.White };

			searchAddress.SearchButtonPressed += async (e, a) =>
			{
				var addressQuery = searchAddress.Text;
				searchAddress.Text = "";
				searchAddress.Unfocus();

				var positions = (await (new Geocoder()).GetPositionsForAddressAsync(addressQuery)).ToList();
				if (!positions.Any())
				return;

				var position = positions.First();
				map.MoveToRegion(MapSpan.FromCenterAndRadius(position,
				Distance.FromMiles(0.1)));
				map.Pins.Add(new Pin
				{
				Label = addressQuery,
				Position = position,
				Address = addressQuery
				});
			};

			stack.Children.Add(searchAddress);

//			ToolbarItems.Add(new ToolbarItem("Filter", "filter.png", async () =>
//			{
//			var page = new ContentPage();
//			var result = await page.DisplayAlert("Title", "Message", "Accept", "Cancel");
//			Debug.WriteLine("success: {0}", result);
//			}));



//			#elif WINDOWS_PHONE
//			ToolbarItems.Add(new ToolbarItem("filter", "filter.png", async () =>
//			{
//			var page = new ContentPage();
//			var result = await page.DisplayAlert("Title", "Message", "Accept", "Cancel");
//			Debug.WriteLine("success: {0}", result);
//			}));
//
//			ToolbarItems.Add(new ToolbarItem("search", "search.png", () =>
//			{
//			}));
//			#endif

			map.VerticalOptions = LayoutOptions.FillAndExpand;
			map.HeightRequest = 100;
			map.WidthRequest = 960;

			stack.Children.Add(map);

			var pinNewLocation = new Button ();
			pinNewLocation.Text = "Pin location";

			pinNewLocation.Clicked += async (e, a) => {
				var position = GetMapCenter(map);
				var label = string.Empty;
				var address = string.Empty;
				var addresses = (await (new Geocoder()).GetAddressesForPositionAsync(position)).ToList();
				if (addresses.Any())
				{
					var currentAddress = addresses.First();
					label = currentAddress;
					address = currentAddress;
				}
				
				var newPin = new Pin {
					Position = position,
					Type = PinType.Place,
					Label = label,
					Address = address
				};
				map.Pins.Add (newPin);
			};
			stack.Children.Add (pinNewLocation);

			var currentLocationBtn = new Button ();
			currentLocationBtn.Text = "GetCurrentLocation";

			currentLocationBtn.Clicked += async (e, a) => {
                //var helper = new GeolocationHelper2();
                //await helper.GetPosition();
                //currentLocationBtn.Text = helper.PositionLatitude;

			    var tt = await GeolocationHelper.GetCurrentPosition(10000);

//				var tt = await GetPositionAsync();
//				currentLocationBtn.Text = "Lat: " + tt.Latitude;
//				var currentLocation = GetCurrentPosition();
//				currentLocationBtn.Text = "Lat: " + currentLocation.Result.Position.Latitude ;
			};
			stack.Children.Add (currentLocationBtn);
			Content = stack;
		}

		public Map MakeMap()
		{
			List<Pin> pins = ViewModel.LoadPins();

			// TODO: Uncomment once Xamarin.Forms supports this, hopefully w/ version 1.1.
			//var dict = pins.Zip(ViewModel.Models, (p, m)=>new KeyValuePair<Pin,T>(p, m)).ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
			//PinMap = dict;

			//var currentLocation = GeolocationHelper.GetCurrentPosition();

			Map map;
//			if (currentLocation.Result != null) {
//				map = new Map(MapSpan.FromCenterAndRadius (currentLocation.Result.Position, Distance.FromMiles (0.3)));
//			} 
//			if (currentLocation != null) {
//				map = new Map(MapSpan.FromCenterAndRadius (currentLocation.Position, Distance.FromMiles (0.3)));
//			} 
//			else {
				map = pins.Count == 0
				? new Map ()
				: new Map (MapSpan.FromCenterAndRadius (pins [0].Position, Distance.FromMiles (0.3)));
//			}
			map.IsShowingUser = true;

			foreach (var p in pins)
			{
				map.Pins.Add(p);
			}

			// TODO: Uncomment once Xamarin.Forms supports this, hopefully w/ version 1.1.
//			            map.PinSelected += (sender, args)=>
//			            {
//			                var pin = args.SelectedItem as Pin;
//			                var details = PinMap[pin];
//			                var page = new DetailPage<T>(details);
//			                Navigation.PushAsync(page);
//			            };

			return map;
		}

		private Position GetMapCenter(Map map)
		{
			return map.VisibleRegion.Center;
		}



		public async Task<Pin> GetCurrentPosition ()
		{
			TaskScheduler _scheduler = TaskScheduler.FromCurrentSynchronizationContext();
			CancellationTokenSource _cancelSource;
			string PositionStatus;
			string PositionLatitude;
			string PositionLongitude;
			Pin CurrentPosition;

			Xamarin.Forms.Labs.Services.Geolocation.IGeolocator Geolocator= DependencyService.Get<Xamarin.Forms.Labs.Services.Geolocation.IGeolocator>();;

			if (Geolocator == null)
			{
				Geolocator = DependencyService.Get<Xamarin.Forms.Labs.Services.Geolocation.IGeolocator>();
				//				Geolocator.PositionError += OnListeningError;
				//				Geolocator.PositionChanged += OnPositionChanged;
			}

			_cancelSource = new CancellationTokenSource();

			PositionStatus = String.Empty;
			PositionLatitude = String.Empty;
			PositionLongitude = String.Empty;

			TaskScheduler scheduler = TaskScheduler.FromCurrentSynchronizationContext();
			CancellationTokenSource cancelSource = new CancellationTokenSource();
			await Geolocator.GetPositionAsync(1000, cancelSource.Token, false)
				.ContinueWith(t => 
					{
						//IsBusy = false;
						if (t.IsFaulted)
						{
							PositionStatus = ((Xamarin.Forms.Labs.Services.Geolocation.GeolocationException)t.Exception.InnerException).Error.ToString();
						}
						else if (t.IsCanceled)
						{
							PositionStatus = "Canceled";
						}
						else
						{
//							PositionStatus = t.Result.Timestamp.ToString("G");
//							CurrentPosition = new Xamarin.Forms.Maps.Position(t.Result.Latitude, t.Result.Longitude);
						}
					}, scheduler);
			return null;
			//return CurrentPosition;
		}

		public async Task<Xamarin.Forms.Labs.Services.Geolocation.Position> GetPositionAsync() 
		{ 
			Xamarin.Forms.Labs.Services.Geolocation.IGeolocator locator = DependencyService.Get<Xamarin.Forms.Labs.Services.Geolocation.IGeolocator>();;;
			if (locator.IsGeolocationAvailable && locator.IsGeolocationEnabled) 
			{
				var position = await locator.GetPositionAsync(10);
				return position;
				//return new Position {Latitude = position.Latitude, Longitude = position.Longitude}; 
			} 
			return null; 
		}
	}
}
