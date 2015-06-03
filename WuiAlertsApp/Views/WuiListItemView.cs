using System;

using Xamarin.Forms;

namespace WuiAlertsApp.Views
{
	public class WuiListItemView : ViewCell
	{
		public WuiListItemView ()
		{
			var typeLabel = new Label {YAlign = TextAlignment.Center};
			typeLabel.SetBinding (Label.TextProperty, "Type");

			var actionLabel = new Label {YAlign = TextAlignment.Center};
			actionLabel.SetBinding (Label.TextProperty, "Action");


//			var tick = new Image {
//				Source = ImageSource.FromFile ("check.png"),
//			};
//			tick.SetBinding (Image.IsVisibleProperty, "Done");

			var layout = new StackLayout {
				Padding = new Thickness(20, 0, 0, 0),
				Orientation = StackOrientation.Horizontal,
				HorizontalOptions = LayoutOptions.StartAndExpand,
				Children = {typeLabel, actionLabel}
			};
			View = layout;
		}
	}
}


