using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

using Xamarin.Forms;
using Xamarin.Forms.Labs.Services.Geolocation;

namespace WuiAlertsApp.Helpers
{
	public class GeolocationHelper2
	{
		bool IsBusy;
		/// <summary>
		/// The scheduler
		/// </summary>
		private readonly TaskScheduler _scheduler = TaskScheduler.FromCurrentSynchronizationContext();
		/// <summary>
		/// The geolocator
		/// </summary>
		private IGeolocator _geolocator;
		/// <summary>
		/// The email service
		/// </summary>
		/// <summary>
		/// The cancel source
		/// </summary>
		private CancellationTokenSource _cancelSource;
		/// <summary>
		/// The position status
		/// </summary>
		private string _positionStatus = string.Empty;
		/// <summary>
		/// The position latitude
		/// </summary>
		private string _positionLatitude = string.Empty;
		/// <summary>
		/// The position longitude
		/// </summary>
		private string _positionLongitude = string.Empty;
		/// <summary>
		/// The get position command
		/// </summary>
		private Command _getPositionCommand;
		/// <summary>
		/// The send position email
		/// </summary>
		private ICommand _sendPositionEmail;
		/// <summary>
		/// The send position SMS
		/// </summary>
		private ICommand _sendPositionSms;
		/// <summary>
		/// The open position URI
		/// </summary>
		private ICommand _openPositionUri;
		/// <summary>
		/// The get driving directions
		/// </summary>
		private ICommand _getDrivingDirections;

		/// <summary>
		/// Gets or sets the position status.
		/// </summary>
		/// <value>The position status.</value>
		public string PositionStatus
		{
			get
			{
				return _positionStatus;
			}
			set
			{
				_positionStatus = value;
			}
		}

		/// <summary>
		/// Gets or sets the position latitude.
		/// </summary>
		/// <value>The position latitude.</value>
		public string PositionLatitude
		{
			get
			{
				return _positionLatitude;
			}
			set
			{
				_positionLatitude = value;
			}
		}

		/// <summary>
		/// Gets or sets the position longitude.
		/// </summary>
		/// <value>The position longitude.</value>
		public string PositionLongitude
		{
			get
			{
				return _positionLongitude;
			}
			set
			{
				_positionLongitude = value;
			}
		}

		/// <summary>
		/// Gets the get position command.
		/// </summary>
		/// <value>The get position command.</value>
		public Command GetPositionCommand 
		{
			get
			{ 
				return _getPositionCommand ??
					(_getPositionCommand = new Command(async () => await GetPosition(), () => Geolocator != null)); 
			}
		}

		/// <summary>
		/// Gets the geolocator.
		/// </summary>
		/// <value>The geolocator.</value>
		private IGeolocator Geolocator
		{
			get
			{
				if (_geolocator == null)
				{
					_geolocator = DependencyService.Get<IGeolocator>();
					_geolocator.PositionError += OnListeningError;
					_geolocator.PositionChanged += OnPositionChanged;
				}
				return _geolocator;
			}
		}

		//private void Setup()
		//{
		//    if (this.geolocator != null)
		//    {
		//        return;
		//    }

		//    this.geolocator = DependencyService.Get<IGeolocator>();
		//    this.geolocator.PositionError += OnListeningError;
		//    this.geolocator.PositionChanged += OnPositionChanged;
		//}

		/// <summary>
		/// Gets the position.
		/// </summary>
		/// <returns>Task.</returns>
		public async Task GetPosition()
		{
			_cancelSource = new CancellationTokenSource();

			PositionStatus = string.Empty;
			PositionLatitude = string.Empty;
			PositionLongitude = string.Empty;
			IsBusy = true;
			await
			Geolocator.GetPositionAsync(10000, _cancelSource.Token, true)
				.ContinueWith(t =>
					{
						IsBusy = false;
						if (t.IsFaulted)
						{
							PositionStatus = ((GeolocationException) t.Exception.InnerException).Error.ToString();
						}
						else if (t.IsCanceled)
						{
							PositionStatus = "Canceled";
						}
						else
						{
							PositionStatus = t.Result.Timestamp.ToString("G");
							PositionLatitude = "La: " + t.Result.Latitude.ToString("N4");
							PositionLongitude = "Lo: " + t.Result.Longitude.ToString("N4");
						}
					}, _scheduler);
		}

		/// <summary>
		/// Handles the <see cref="E:ListeningError" /> event.
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The <see cref="PositionErrorEventArgs"/> instance containing the event data.</param>
		private void OnListeningError(object sender, PositionErrorEventArgs e)
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
		private void OnPositionChanged(object sender, PositionEventArgs e)
		{
			////			BeginInvokeOnMainThread (() => {
			////				ListenStatus.Text = e.Position.Timestamp.ToString("G");
			////				ListenLatitude.Text = "La: " + e.Position.Latitude.ToString("N4");
			////				ListenLongitude.Text = "Lo: " + e.Position.Longitude.ToString("N4");
			////			});
		}
	}
}

