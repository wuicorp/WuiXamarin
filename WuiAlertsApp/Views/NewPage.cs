using System;

using Xamarin.Forms;

namespace WuiAlertsApp.Views
{
	public class NewPage : ContentPage
	{
		public NewPage ()
		{
			Title = L10n.Localize ("Settings", "");
			Content = new Label { Text = "Hello ContentView" };
		}
	}
}