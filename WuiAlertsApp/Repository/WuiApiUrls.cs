using System;

namespace WuiAlertsApp.Repository
{
	public class WuiApiUrls
	{
		public WuiApiUrls ()
		{
		} 

		public static readonly string Base = "http://private-anon-2e6f18548-wuiguardians.apiary-mock.com/";

		//GET (List) POST (Create) + /id = PUT (Update) DELETE (Delete) 
		public static readonly string Users = Base + "users/me";
		public static readonly string Vehicles = Base + "vehicles";
		public static readonly string Wuis = Base + "wuis";
	}
}

