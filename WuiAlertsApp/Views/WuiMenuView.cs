using System;
using System.Linq;
using System.Collections.Generic;
using Xamarin.Forms;
using WuiAlertsApp.Resx;
using WuiAlertsApp.Models;
using WuiAlertsApp.Repository;
using WuiAlertsApp.Pages;
using WuiAlertsApp.Views;
using WuiAlertsApp.ViewModels;
using System.Threading.Tasks;

namespace WuiAlertsApp.Views
{
	public class WuiMenuView : ContentPage
	{
		static readonly int NUM = 3;

		StackLayout stackLayout;
		AbsoluteLayout absoluteLayout;
		double menuItemSize;
		bool isBusy;

		public WuiMenuView ()
		{
			Title = AppResources.ApplicationTitle;

			LoadToolbarItems ();

			absoluteLayout = new AbsoluteLayout()
			{
				HorizontalOptions = LayoutOptions.Center,
				VerticalOptions = LayoutOptions.Center
			};

			int index = 0;

			for (int row = 0; row < NUM; row++)
			{
				for (int col = 0; col < NUM; col++)
				{
					MenuOption menuOption = new MenuOption(index)
					{
						Row = row,
						Col = col
					};

					// Add tap recognition
					var tapGestureRecognizer = new TapGestureRecognizer
					{
						Command = new Command(OnMenuTapped),
						CommandParameter = menuOption,
						NumberOfTapsRequired=2
					};

					menuOption.GestureRecognizers.Add(tapGestureRecognizer);
					absoluteLayout.Children.Add(menuOption);
					index++;
				}
			}

			// Put everything in a StackLayout.
			stackLayout = new StackLayout
			{
				BackgroundColor = Color.White,
				Children = 
				{
					new StackLayout
					{
						Padding = new Thickness(10, 70, 10, 0),
						HorizontalOptions = LayoutOptions.FillAndExpand,
						Children = 
						{

						}
					},
					absoluteLayout
				}
				};
			stackLayout.SizeChanged += OnStackSizeChanged;

			// And set that to the content of the page.
			this.Padding = new Thickness(0, Device.OnPlatform(20, 0, 0), 0, 0);
			this.Content = stackLayout;
		}

		private Frame GetFrame(string img)
		{
			var image = new Image()
			{
				Source = ImageSource.FromResource(img),
				Aspect = Aspect.AspectFit
			};

			return new Frame
			{
				HorizontalOptions = LayoutOptions.Center,
				BackgroundColor = Color.White,
				OutlineColor = Color.Accent,
				Padding = new Thickness(5, 10, 5, 0),
				Content = new StackLayout
				{

					Spacing = 0,
					Children = 
					{
						image
					}
				}
			};
		}

		void OnMenuTapped(object param)
		{
			//var ttt= "iohfiuh";
		}

		async void OnSquareTapped(object parameter)
		{
			if (isBusy)
				return;

			isBusy = true;
			MenuOption tappedMenuOption = (MenuOption)parameter;

			await tappedMenuOption.AnimateSelectAsync();

			isBusy = false;
		}

		void OnStackSizeChanged(object sender, EventArgs args)
		{
			double width = stackLayout.Width;
			double height = stackLayout.Height;

			if (width <= 0 || height <= 0)
				return;

			// Orient StackLayout based on portrait/landscape mode.
			stackLayout.Orientation = (width < height) ? StackOrientation.Vertical :
				StackOrientation.Horizontal;

			// Calculate square size and position based on stack size.
			menuItemSize = Math.Min(width, height) / NUM;
			absoluteLayout.WidthRequest = NUM * menuItemSize;
			absoluteLayout.HeightRequest = NUM * menuItemSize;
			Font font = Font.SystemFontOfSize(0.4 * menuItemSize, FontAttributes.Bold);

			foreach (View view in absoluteLayout.Children)
			{
				var menuItem = (MenuOption)view;

				AbsoluteLayout.SetLayoutBounds(menuItem,
					new Rectangle(menuItem.Col * menuItemSize,
						menuItem.Row * menuItemSize,
						menuItemSize,
						menuItemSize));
			}
		}

		void LoadToolbarItems()
		{
//			var tbiWuis= new ToolbarItem("Wuis", "icon_messages.png", () =>
		var tbiWuis= new ToolbarItem("Wuis", "ic_email_grey600_48dp", () =>
				{
					var viewModel = new WuiTabbedListViewModel (this.Navigation);

					var wuiTabbedList = new WuiTabbedList ();
					wuiTabbedList.BindingContext = viewModel;

//					wuiTabbedList.ContextActions.Add (moreAction);
//					ContextActions.Add (deleteAction);
//					wuiTabbedList.sentWuisList.IsPullToRefreshEnabled = true;

					var ee = Navigation.PushAsync(wuiTabbedList);

				}, 0, 0);
			ToolbarItems.Add (tbiWuis);

			var tbiSettings= new ToolbarItem("Settings", "ic_settings_grey600_48dp", () =>
				{
					var viewModel = new SettingsViewModel (this.Navigation);

					var settingsListView = new SettingsListView ();
					settingsListView.BindingContext = viewModel;

					var saveSettings = new ToolbarItem{
						Command = viewModel.SaveSettingsCommand,
						Icon = "ic_action_save",
						Text = "Save",
						Priority = 0,
						Order = ToolbarItemOrder.Default
					};

					var cancelSettings = new ToolbarItem{
						Command = viewModel.CancelSettingsCommand,
						Icon = "ic_action_undo",
						Text = "Undo",
						Priority = 0,
						Order = ToolbarItemOrder.Default
					};

					settingsListView.ToolbarItems.Add(saveSettings);
					settingsListView.ToolbarItems.Add(cancelSettings);

					var ee = Navigation.PushAsync(settingsListView);
				}, 0, 0);
			
			ToolbarItems.Add (tbiSettings);

//			var tbiProfile = new ToolbarItem("Profile", "icon_profile.png", () =>
//				{
//					var newPage = new NewPage();
//					Navigation.PushAsync(newPage);
//				}, 0, 0);
//
//			ToolbarItems.Add (tbiProfile);


			var tbiMap = new ToolbarItem("Map", "ic_action_place.png", () =>
			{
				var viewModel = new MapViewModel();
				//BindingContext = viewModel;

				Map = new MapPage(viewModel);
				var ee = Navigation.PushAsync(Map);
			}, 0, 0);
				
			ToolbarItems.Add (tbiMap);


            var tbiVehicles = new ToolbarItem("Vehicles", "ic_car.png", () =>
            {
                var viewModel = new VehiclesViewModel(this.Navigation);
                //BindingContext = viewModel;

                var vehiclesListView = new VehiclesListView();
                vehiclesListView.BindingContext = viewModel;

                var newVehicle = new ToolbarItem
                {
                    Command = viewModel.NewVehicleCommand,
                    Icon = "ic_action_new",
                    Text = "New",
                    Priority = 0,
                    Order = ToolbarItemOrder.Default
                };


                var saveSettings = new ToolbarItem
                {
                    Command = viewModel.SaveSettingsCommand,
                    Icon = "ic_action_save",
                    Text = "Save",
                    Priority = 0,
                    Order = ToolbarItemOrder.Default
                };

                var cancelSettings = new ToolbarItem
                {
                    Command = viewModel.CancelSettingsCommand,
                    Icon = "ic_action_undo",
                    Text = "Undo",
                    Priority = 0,
                    Order = ToolbarItemOrder.Default
                };

					vehiclesListView.ToolbarItems.Add(newVehicle);
                vehiclesListView.ToolbarItems.Add(cancelSettings);
                vehiclesListView.ToolbarItems.Add(saveSettings);

                var ee = Navigation.PushAsync(vehiclesListView);

            }, 0, 0);

			ToolbarItems.Add(tbiVehicles);
		}

		public MapPage Map { get; private set; }
	}
}