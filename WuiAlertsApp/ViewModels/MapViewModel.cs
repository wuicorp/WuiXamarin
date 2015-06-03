using System;
using Xamarin.Forms.Maps;
using System.Collections.Generic;
using WuiAlertsApp.Models;

namespace WuiAlertsApp.ViewModels
{
	public class MapViewModel
	{
		public static readonly Position NullPosition = new Position(0, 0);

		public MapViewModel()
		{
//			Title = "Map";
//			Icon = "map.png";
		}

		public List<Pin> LoadPins()
		{
//			ExecuteLoadModelsCommand();
//
//			var pins = Models.Select(model =>
//				{
//					var item = (IContact)model;
//					var address = item.Address ?? item.ShippingAddress ?? item.BillingAddress;
//
//					var position = address != null ? new Position(address.Latitude, address.Longitude) : NullPosition;
//					var pin = new Pin
//					{
//						Type = PinType.Place,
//						Position = position,
//						Label = item.ToString(),
//						Address = address.ToString()
//					};
//					return pin;
//				}).ToList();

			var pins = new List<Pin> ();
			pins.Add(new Pin{
								Type = PinType.Place,
								Position = new Position(41.386654, 2.170134),
								Label = "WuiPolice Alert",
								Address = "0 alerts"
			});

			pins.Add(new Pin{
				Type = PinType.Place,
				Position = new Position(41.389350, 2.168449),
				Label = "WuiToe Alert",
				Address = "1 alert"
			});

			return pins;
		}
	}
}
