using System;

namespace WuiAlertsApp.ViewModels
{
	public class ProfileViewModel : BaseViewModel
	{
		public ProfileViewModel ()
		{
			Setting1 = "Handeeeerrrr";
			Setting2 = true;
			Setting3 = "Binded Text";
			Setting4 = "Binded Details";
		}

		private string _setting1;
		public string Setting1
		{
			get { return _setting1; }
			set
			{
				_setting1 = value;
				OnPropertyChanged("Setting1");
			}
		}

		private bool _setting2;
		public bool Setting2
		{
			get { return _setting2; }
			set
			{
				_setting2 = value;
				OnPropertyChanged("Setting2");
			}
		}

		private string _setting3;
		public string Setting3
		{
			get { return _setting3; }
			set
			{
				_setting3 = value;
				OnPropertyChanged("Setting3");
			}
		}

		private string _setting4;
		public string Setting4
		{
			get { return _setting4; }
			set
			{
				_setting4 = value;
				OnPropertyChanged("Setting4");
			}
		}
	}
}

