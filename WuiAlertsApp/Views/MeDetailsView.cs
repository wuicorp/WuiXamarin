using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using WuiAlertsApp.Resx;
using WuiAlertsApp.Models;
using WuiAlertsApp.Repository;

namespace WuiAlertsApp.Views
{
	public class MeDetailsView : ContentPage
	{
		User user;

		public MeDetailsView ()
		{
			Title = AppResources.ApplicationTitle; // "Todo";

			//var s = L10n.Localize ("SpeakTaskCount", "Number of tasks to do");

			var ttt = LoadUserDetailsAsync();
		}

		private readonly IWuiRepository repository = new WuiRepository();

		private async Task LoadUserDetailsAsync()
		{
			var meNot = await repository.GetMyDetailsAsync();

			user = await repository.GetMyDetailsAsync();

			var vehicles = await repository.GetVehiclesAsync();

			var wuis = await repository.GetWuisAsync();

			var layout = new StackLayout {
				VerticalOptions = LayoutOptions.StartAndExpand,
				Padding = new Thickness(20)};

			if (user != null) {
				var IdLabel = new Label {Text = "Id: " + user.Id }; 
				var EmailLabel = new Label {Text = "Email: " + user.Email }; 
				var NameLabel = new Label {Text = "Name: " + user.Name }; 

				layout.Children.Add (IdLabel);
				layout.Children.Add (EmailLabel);
				layout.Children.Add (NameLabel);


//				if (user.Vehicles != null) 
//				{
//					var VehicleListLabel = new Label {Text = "VehicleList: "}; 
//					layout.Children.Add (VehicleListLabel);
//
//					foreach (var vechicle in user.Vehicles) {
//						var VehicleLabel = new Label {Text = vechicle.ToString(), TranslationX = 80}; 
//						layout.Children.Add (VehicleLabel);
//					}
//				}
			}

			Content = layout;
		}
	}
}

