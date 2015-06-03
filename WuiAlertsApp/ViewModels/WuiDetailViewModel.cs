using System;
using WuiAlertsApp.Models;
using WuiAlertsApp.Helpers;
//using Xamarin.Geolocation;

namespace WuiAlertsApp.ViewModels
{
	public class WuiDetailViewModel: BaseViewModel
	{
		public WuiDetailViewModel (WuiAlert wui)
		{
			CurrentWui = wui;
//			CurrentPosition = GeolocationHelper.GetCurrentLocation ();
		}

		private WuiAlert _currentWui;
		public WuiAlert CurrentWui
		{
			get { return _currentWui; }
			set
			{
				_currentWui = value;
				OnPropertyChanged("CurrentWui");
			}
		}

//		private Position _currentPosition;
//		public Position CurrentPosition
//		{
//			get { return _currentPosition; }
//			set
//			{
//				_currentPosition = value;
//				OnPropertyChanged("CurrentPosition");
//			}
//		}

	}
}

