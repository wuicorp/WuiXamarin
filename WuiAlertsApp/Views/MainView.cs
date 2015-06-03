using System;
using Xamarin.Forms;

namespace WuiAlertsApp.Views
{
	public class MainView : ContentPage
	{
		public MainView ()
		{
//			var layout = new StackLayout {
//				VerticalOptions = LayoutOptions.StartAndExpand,
//				Padding = new Thickness(20)};

			Grid grid = new Grid
			{
				BackgroundColor = Color.White,
				VerticalOptions = LayoutOptions.FillAndExpand,
				RowDefinitions = 
				{
					new RowDefinition { Height = new GridLength(1, GridUnitType.Star) },
					new RowDefinition { Height = new GridLength(1, GridUnitType.Star) },
					new RowDefinition { Height = new GridLength(1, GridUnitType.Star) },
				},
				ColumnDefinitions = 
				{
					new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
					new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
					new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
				}
			};

			grid.Children.Add(new Button{ Image="icon_accident", BackgroundColor=Color.Transparent}, 0, 0);
			grid.Children.Add(new Button{ Image="icon_claus", BackgroundColor=Color.Transparent}, 0, 1);
			grid.Children.Add(new Button{ Image="icon_grua", BackgroundColor=Color.Transparent}, 0, 2);
			grid.Children.Add(new Button{ Image="icon_lladre", BackgroundColor=Color.Transparent}, 1, 0);
			grid.Children.Add(new Button{ Image="icon_llums", BackgroundColor=Color.Transparent}, 1, 1);
			grid.Children.Add(new Button{ Image="icon_missatge", BackgroundColor=Color.Transparent}, 1, 2);
			grid.Children.Add(new Button{ Image="icon_poli", BackgroundColor=Color.Transparent}, 2, 0);
			grid.Children.Add(new Button{ Image="icon_rodes", BackgroundColor=Color.Transparent}, 2, 1);
			grid.Children.Add(new Button{ Image="icon_vidre", BackgroundColor=Color.Transparent}, 2, 2);

//			buttonStarRect= UIButton.FromType(UIButtonType.RoundedRect);
//			buttonStarRect.SetImage (UIImage.FromFile ("star-gold45.png"), UIControlState.Normal);
//			buttonStarRect.SetImage (UIImage.FromFile ("star-grey45.png"), UIControlState.Disabled);
			//layout.Children.Add (grid);

			Content = grid;
		}
	}
}

