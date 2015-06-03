using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using WuiAlertsApp.Resx;
using WuiAlertsApp.Models;
using WuiAlertsApp.Repository;
using WuiAlertsApp.Views;
using WuiAlertsApp.Helpers;
using WuiAlertsApp.ViewModels;

namespace WuiAlertsApp.Views
{
	public class WuiListView : ContentPage
	{
		private readonly IWuiRepository repository = new WuiRepository();
		ListView listView;
		IEnumerable<WuiAlert> wuis;


		public List<WuiAlert> SentWuis{ get; set; }
		public List<WuiAlert> ReceivedWuis{ get; set; }

		public WuiListView ()
		{
			var t = LoadWuisAsync ();
		}

		private async Task LoadWuisAsync()
		{
			wuis = await repository.GetWuisAsync();

			listView = new ListView { RowHeight = 40 };
			listView.ItemTemplate = new DataTemplate (typeof (WuiListItemView));

			listView.ItemSelected += (sender, e) => {
				var selectedWui = (WuiAlert)e.SelectedItem;

				var wuiDetailViewModel = new WuiDetailViewModel(selectedWui);
				// use C# localization
				var wuiDetailPage = new WuiDetailViewX();
				wuiDetailPage.BindingContext = wuiDetailViewModel;
				//wuiDetailPage.BindingContext = selectedWui;
				Navigation.PushAsync(wuiDetailPage);
			};

			listView.ItemsSource = wuis.Where(x=>x.Action=="sent").ToList();

			var layout = new StackLayout();
			if (Device.OS == TargetPlatform.WinPhone) { // WinPhone doesn't have the title showing
				layout.Children.Add(new Label{Text="Wui", Font=Font.SystemFontOfSize(NamedSize.Large, FontAttributes.Bold)});
			}
			layout.Children.Add(listView);
			layout.VerticalOptions = LayoutOptions.FillAndExpand;

			Content = layout;

//			var tt = GeolocationHelper.GetCurrentLocation ();

//			SentWuis= wuis.Where(x=>x.Action=="sent").ToList();
//			ReceivedWuis= wuis.Where(x=>x.Action=="received").ToList();
//
//			var ttt = new WuiTabbedList ();
//			ttt.BindingContext = this;
//
//			Navigation.PushAsync(ttt);
		}

		protected override void OnAppearing ()
		{
			base.OnAppearing ();

			if (listView != null) {
				//listView.ItemsSource = App.Database.GetWuis();
				listView.ItemsSource = wuis;
			}
		}
	}
}


