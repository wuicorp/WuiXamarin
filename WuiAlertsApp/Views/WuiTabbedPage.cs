using System;
using Xamarin.Forms;
using WuiAlertsApp.Resx;

namespace WuiAlertsApp.Views
{
	public class WuiTabbedPage:TabbedPage
	{
		public WuiTabbedPage ()
		{
			Title = AppResources.ApplicationTitle;

			this.Children.Add (new WuiMenuView {Title = L10n.Localize ("Menu", ""), Icon="icon_home"});
			this.Children.Add (new WuiListView {Title = L10n.Localize ("Wuis", ""), Icon="icon_messages.png"});
			this.Children.Add (new ContentPage {Title = L10n.Localize ("Settings", ""), Icon="icon_settings.png"});

			//this.Children.Add (new ContentPage {Title = L10n.Localize ("Profile", ""), Icon="icon_profile.png"});
		}
	}
}

