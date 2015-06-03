using System;
using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Forms;
using WuiAlertsApp.Views;
using WuiAlertsApp.Models;
using System.Collections.ObjectModel;
using Refractored.Xam.Settings;
//using Xam.Plugins.Settings;

namespace WuiAlertsApp.ViewModels
{
	public class SettingsViewModel : BaseViewModel
	{
		// ICommand implementations
		public ICommand SaveSettingsCommand{ protected set; get; }
		public ICommand CancelSettingsCommand{ protected set; get; }

		private INavigation _navigation;
		private User _userSettings;

		public SettingsViewModel(INavigation navigation)
		{
			_navigation = navigation; 

			Languages = new ObservableCollection<string>()
			{
				"English", "Español", "Català"
			};

			_userSettings = App.Database.GetUserSettings ();
			if (_userSettings == null) {

				_userSettings = new User ();

				ProfileName = "";
				Notifications = true;
				Sound = true;
				SelectedLanguage = 0;
				Email = "";
				Phone = "";
			} 
			else 
			{
				ProfileName = _userSettings.ProfileName;
				Notifications = _userSettings.Notifications;
				Sound = _userSettings.Sound;
				SelectedLanguage = _userSettings.Language;
				Email = _userSettings.Email;
				Phone = _userSettings.Phone;
			}

			this.SaveSettingsCommand = new Command<string>((parameter) => SaveSettings());
			this.CancelSettingsCommand = new Command<string>((parameter) => CancelSettings());
		}

		public void SaveSettings()
		{
			_userSettings.ProfileName = ProfileName;
			_userSettings.Notifications = Notifications;
			_userSettings.Sound = Sound;
			_userSettings.Language = SelectedLanguage;
			_userSettings.Email= Email;
			_userSettings.Phone= Phone;

			App.Database.UpdateUserSettings (_userSettings);

			_navigation.PopAsync ();
		}

		public void CancelSettings()
		{
			_navigation.PopAsync ();
		}

		private int _selectedLanguage;
		public int SelectedLanguage
		{
			get { return _selectedLanguage; }
			set
			{
				_selectedLanguage = value;
				OnPropertyChanged("SelectedLanguage");
			}
		}

		private ObservableCollection<string> _languages;
		public ObservableCollection<string> Languages
		{
			get { return _languages; }
			set
			{
				_languages = value;
				OnPropertyChanged("Languages");
			}
		}

		private bool _notifications;
		public bool Notifications
		{
			get { return _notifications; }
			set
			{
				_notifications = value;
				OnPropertyChanged("Notifications");
			}
		}

		private bool _sound;
		public bool Sound
		{
			get { return _sound; }
			set
			{
				_sound = value;
				OnPropertyChanged("Sound");
			}
		}

		private string _profileName;
		public string ProfileName
		{
			get { return _profileName; }
			set
			{
				_profileName = value;
				OnPropertyChanged("ProfileName");
			}
		}

		private string _email;
		public string Email
		{
			get { return _email; }
			set
			{
				_email = value;
				OnPropertyChanged("Email");
			}
		}

		private string _phone;
		public string Phone
		{
			get { return _phone; }
			set
			{
				_phone = value;
				OnPropertyChanged("Phone");
			}
		}
	}
}

