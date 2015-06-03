using System;
using Xamarin.Forms;
using WuiAlertsApp.Resx;
using System.Globalization;

using System.Threading.Tasks;
using WuiAlertsApp.Repository;
using WuiAlertsApp.Views;

using System.Threading;

namespace WuiAlertsApp
{
	public  class App : Application
	{
		static WuiDatabase database;

		public App ()
		{
			L10n.SetLocale ();

			var netLanguage = DependencyService.Get<ILocale>().GetCurrent();
			AppResources.Culture = new CultureInfo (netLanguage);

			var splash = new SplashView ();

			MainPage = splash;

			ShowMainView ();
		}

		public async void ShowMainView()
		{
			await Task.Delay(TimeSpan.FromSeconds(3));

			var mainNav = new NavigationPage (new WuiMenuView ());
//			var mainNav = new NavigationPage (new WuiTabbedPage ());
			MainPage = mainNav;
		}

		public static WuiDatabase Database {
			get { 
				if (database == null) {
					database = new WuiDatabase ();
				}
				return database; 
			}
		}
	}
}

