using System;
using SQLite.Net.Attributes;

namespace WuiAlertsApp.Models
{
	public class WuiAlert
	{
		public WuiAlert ()
		{
		}

		[PrimaryKey, AutoIncrement]
		public int Id { get; set; }
		public string Type { get; set; }
		public string Action { get; set; }
		public string Status { get; set; }
		//public Vehicle Vehicle { get; set; }
	}
}

