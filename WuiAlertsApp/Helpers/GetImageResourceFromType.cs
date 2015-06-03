using System;
using Xamarin.Forms;

namespace WuiAlertsApp.Helpers
{
	public static class GetImageResourceFromType
	{
		public static ImageSource GetImageResource(string wuiType)
		{
			switch (wuiType) {
			case "crash":
				return ImageSource.FromFile ("icon_accident.png");
			case "park":
				return ImageSource.FromFile ("icon_grua.png");
			case "lladre":
				return ImageSource.FromFile ("icon_lladre.png");
			case "llums":
				return ImageSource.FromFile ("icon_llums.png");
			case "missatge":
				return ImageSource.FromFile ("icon_missatge.png");
			case "poli":
				return ImageSource.FromFile ("icon_poli.png");
			case "rodes":
				return ImageSource.FromFile ("icon_rodes.png");
			case "vidre":
				return ImageSource.FromFile ("icon_vidre.png");
			case "claus":
				return ImageSource.FromFile ("icon_claus.png");
			default:
				return ImageSource.FromFile ("icon_unknown.png");
			}
		}


		public static string GetImageString(string wuiType)
		{
			switch (wuiType) 
			{
			case "crash":
				return "icon_accident.png";
			case "park":
				return "icon_grua.png";
			case "lladre":
				return "icon_lladre.png";
			case "llums":
				return "icon_llums.png";
			case "missatge":
				return "icon_missatge.png";
			case "poli":
				return "icon_poli.png";
			case "rodes":
				return "icon_rodes.png";
			case "vidre":
				return "icon_vidre.png";
			case "claus":
				return "icon_claus.png";
			default:
				return "icon_unknown.png";
			}
		}
	}
}

