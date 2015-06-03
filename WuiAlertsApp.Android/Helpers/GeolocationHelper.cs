using System;
using Xamarin.Geolocation;
using System.Threading.Tasks;
using Android.App;
using Android.OS;
using System.Threading;

namespace WuiAlertsApp.Helpers
{
	public class GeolocationHelper: Activity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			if (this.geolocator != null)
				return;

			this.geolocator = new Geolocator (this) { DesiredAccuracy = 50 };
			this.geolocator.PositionError += OnListeningError;
			this.geolocator.PositionChanged += OnPositionChanged;
		}

		private Geolocator geolocator;
		private CancellationTokenSource cancelSource;

		private string	positionStatus, positionLatitude, positionLongitude, positionAccuracy,
		listenStatus, listenLatitude, listenLongitude, listenAccuracy;

		public void GetCurrentLocation()
		{
			this.geolocator.GetPositionAsync (timeout: 10000, cancelToken: this.cancelSource.Token)
				.ContinueWith (t => RunOnUiThread (() =>
					{
						if (t.IsFaulted)
							positionStatus= ((GeolocationException)t.Exception.InnerException).Error.ToString();
						else if (t.IsCanceled)
							positionStatus= "Canceled";
						else
						{
							positionStatus = t.Result.Timestamp.ToString("G");
							positionAccuracy = t.Result.Accuracy + "m";
							positionLatitude = "La: " + t.Result.Latitude.ToString("N4");
							positionLongitude = "Lo: " + t.Result.Longitude.ToString("N4");
						}
					}));
//			
//			var locator = new Geolocator(this) { DesiredAccuracy = 50 };
//			//            new Geolocator (this) { ... }; on Android
//			locator.GetPositionAsync (timeout: 10000).ContinueWith (t => {
//				var status = t.Result.Timestamp;
//				var latitude = t.Result.Latitude;
//				var longitude = t.Result.Longitude;
//				return t.Result;
//			}, TaskScheduler.FromCurrentSynchronizationContext());
//
//			return null;
		}


		private void OnListeningError (object sender, PositionErrorEventArgs e)
		{
			RunOnUiThread (() => {
				this.listenStatus= e.Error.ToString();
			});
		}

		private void OnPositionChanged (object sender, PositionEventArgs e)
		{
			RunOnUiThread (() => {
				listenStatus= e.Position.Timestamp.ToString("G");
				listenAccuracy= e.Position.Accuracy + "m";
				listenLatitude= "La: " + e.Position.Latitude.ToString("N4");
				listenLongitude= "Lo: " + e.Position.Longitude.ToString("N4");
			});
		}
	}
}

