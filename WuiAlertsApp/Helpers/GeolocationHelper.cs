using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Labs.Services.Geolocation;

namespace WuiAlertsApp.Helpers
{
	public static class GeolocationHelper
	{
		private static readonly TaskScheduler _scheduler = TaskScheduler.FromCurrentSynchronizationContext();
		private static CancellationTokenSource _cancelSource;
		public static string PositionStatus;
		public static string PositionLatitude;
		public static string PositionLongitude;
		public static Pin CurrentPosition;

		public static IGeolocator Geolocator;

//		public static async Task<Pin> GetCurrentPosition ()
		public static Pin GetCurrentPosition ()
		{
			if (Geolocator == null)
			{
				Geolocator = DependencyService.Get<IGeolocator>();
//				Geolocator.PositionError += OnListeningError;
//				Geolocator.PositionChanged += OnPositionChanged;
			}

			_cancelSource = new CancellationTokenSource();

			PositionStatus = String.Empty;
			PositionLatitude = String.Empty;
			PositionLongitude = String.Empty;

//			TaskScheduler scheduler = TaskScheduler.FromCurrentSynchronizationContext();
//			CancellationTokenSource cancelSource = new CancellationTokenSource();
//			await Geolocator.GetPositionAsync(1000, cancelSource.Token, false)
//				.ContinueWith(t =>
//					{
//						//IsBusy = false;
//						if (t.IsFaulted)
//						{
//							PositionStatus = ((GeolocationException)t.Exception.InnerException).Error.ToString();
//						}
//						else if (t.IsCanceled)
//						{
//							PositionStatus = "Canceled";
//						}
//						else
//						{
//							PositionStatus = t.Result.Timestamp.ToString("G");
//							CurrentPosition = new Xamarin.Forms.Maps.Position(t.Result.Latitude, t.Result.Longitude);
//						}
//					}, scheduler);
			//IsBusy = true;
			//await
			Geolocator.GetPositionAsync (timeout: 10000, cancelToken: _cancelSource.Token, includeHeading: true)
				.ContinueWith (t =>
					{
						//IsBusy = false;
						if (t.IsFaulted)
							PositionStatus = ((GeolocationException)t.Exception.InnerException).Error.ToString();
						else if (t.IsCanceled)
							PositionStatus = "Canceled";
						else
						{
							PositionStatus = t.Result.Timestamp.ToString("G");
							PositionLatitude = "La: " + t.Result.Latitude.ToString("N4");
							PositionLongitude = "Lo: " + t.Result.Longitude.ToString("N4");
							CurrentPosition = new Pin{
								Type = PinType.Place,
								Position = new Xamarin.Forms.Maps.Position(t.Result.Latitude, t.Result.Longitude),
								Label = "CurrentPosition",
								Address = ""
							};
						}

					}, _scheduler);

			return CurrentPosition;
		}

		/// <summary>
		/// Handles the <see cref="E:ListeningError" /> event.
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The <see cref="PositionErrorEventArgs"/> instance containing the event data.</param>
		private static void OnListeningError(object sender, PositionErrorEventArgs e)
		{
			////			BeginInvokeOnMainThread (() => {
			////				ListenStatus.Text = e.Error.ToString();
			////			});
		}

		/// <summary>
		/// Handles the <see cref="E:PositionChanged" /> event.
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The <see cref="PositionEventArgs"/> instance containing the event data.</param>
		private static void OnPositionChanged(object sender, PositionEventArgs e)
		{
			////			BeginInvokeOnMainThread (() => {
			////				ListenStatus.Text = e.Position.Timestamp.ToString("G");
			////				ListenLatitude.Text = "La: " + e.Position.Latitude.ToString("N4");
			////				ListenLongitude.Text = "Lo: " + e.Position.Longitude.ToString("N4");
			////			});
		}

	}
}

