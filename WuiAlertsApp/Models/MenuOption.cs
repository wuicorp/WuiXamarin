using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using WuiAlertsApp.Views;
using WuiAlertsApp.Helpers;

namespace WuiAlertsApp.Models
{
	public class MenuOption: ContentView
	{
		Image img;
		Frame frame;

		public int Index { private set; get; }
		public int Row { set; get; }
		public int Col { set; get; }
		public string WuiType { set; get; }

		bool isBusy;

		public MenuOption(int index)
		{
			Index = index;
			WuiType = GetWuiTypeFromIndex (index);

			img = new Image
			{
				Aspect = Aspect.AspectFit,

			};

			img.Source = GetImageResourceFromType.GetImageResource(WuiType);


			var tapGestureRecognizer = new TapGestureRecognizer
			{
				Command = new Command(OnImageTapped),
				CommandParameter = WuiType
			};

			img.GestureRecognizers.Add(tapGestureRecognizer);

			frame = new Frame
			{
				BackgroundColor = Color.White,
				OutlineColor = Color.Transparent,
				Padding = new Thickness(5, 10, 5, 0),
				Content = new StackLayout
				{
					Spacing = 0,
					Children = 
					{
						img,
					}
				}
			};
			this.Content = frame;
			this.BackgroundColor = Color.Transparent;
		}

		async void OnImageTapped(object parameter)
		{
			if (isBusy)
				return;

			isBusy = true;
			await AnimateSelectAsync();

			var newWui = new WuiAlert{Action="sent"};
			//var newWui = new WuiAlert();

			newWui.Type = WuiType;

			var wuiDetailView = new WuiDetailViewX();
			wuiDetailView.BindingContext = newWui;
			wuiDetailView.ToolbarItems.Add(new ToolbarItem("Save", "ic_action_save", ()=>
				{
					SaveItem(newWui);
				}, 0,0));
			wuiDetailView.ToolbarItems.Add(new ToolbarItem("Cancel", "ic_action_undo", ()=>
				{
					CancelItem(newWui);
				}, 0,0));
			
			await Navigation.PushAsync(wuiDetailView);

			isBusy = false;
		}



		public void SaveItem(WuiAlert wui)
		{
			
			App.Database.SaveWui(wui);
			this.Navigation.PopAsync();
			//Navigation.PopToRootAsync();
		}

		public void CancelItem(WuiAlert wui)
		{
			Navigation.PopToRootAsync();
		}


		public string GetWuiTypeFromIndex(int index)
		{
			switch (index) {
			case 0:
				return "crash";
			case 1:
				return "park";
			case 2:
				return "claus";
			case 3:
				return "lladre";
			case 4:
				return "llums";
			case 5:
				return "missatge";
			case 6:
				return "poli";
			case 7:
				return "rodes";
			case 8:
				return "vidre";
			default:
				return "claus";
			}
		}


		public ImageSource GetImageFromIndex(int index)
		{
			switch (index) {
			case 0:
				return ImageSource.FromFile("icon_accident.png");
			case 1:
				return ImageSource.FromFile("icon_claus.png");
			case 2:
				return ImageSource.FromFile("icon_grua.png");
			case 3:
				return ImageSource.FromFile("icon_lladre.png");
			case 4:
				return ImageSource.FromFile("icon_llums.png");
			case 5:
				return ImageSource.FromFile("icon_missatge.png");
			case 6:
				return ImageSource.FromFile("icon_poli.png");
			case 7:
				return ImageSource.FromFile("icon_rodes.png");
			case 8:
				return ImageSource.FromFile("icon_vidre.png");

			default:
				return ImageSource.FromFile ("icon_claus.png");
			}
		}

		public async Task AnimateSelectAsync()
		{
			uint length = 150;

			await Task.WhenAll(this.ScaleTo(0.7, length));
			await Task.WhenAll(this.ScaleTo(1, length));

			this.Rotation = 0;
		}

		public async Task AnimateWinAsync()
		{
			uint length = 150;
			await Task.WhenAll(this.ScaleTo(1, length), this.RotateTo(180, length),this.TranslateTo(0,-300,length));
			await Task.WhenAll(this.ScaleTo(2, length), this.RotateTo(360, length));
			this.Rotation = 0;
		}


//		public async Task CardDown()
//		{
//			if (img.IsVisible)
//			{
//				img.IsVisible = false;
//				frame.BackgroundColor = Color.Maroon;
//			}
//			else
//			{
//				img.IsVisible = true;
//				frame.BackgroundColor = Color.White;
//			}
//		}


	}
}