using System;
using Xamarin.Forms;

namespace WuiAlertsApp.Helpers
{
	public class WuiTypeToImageConverter : IValueConverter
	{
		#region IValueConverter implementation

		public object Convert (object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			if (value==null)
				return GetImageResourceFromType.GetImageString (null);
				//return string.Empty;

			return GetImageResourceFromType.GetImageString (value.ToString ());
		}

		public object ConvertBack (object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			throw new NotImplementedException ();
		}

		#endregion
	}
}

