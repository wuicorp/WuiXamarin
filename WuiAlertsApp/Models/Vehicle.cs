using System;
using SQLite.Net.Attributes;

namespace WuiAlertsApp.Models
{
	public class Vehicle
	{
		public Vehicle ()
		{
		}

		[PrimaryKey, AutoIncrement]
		public int Id { get; set; }
		public string Identifier { get; set; }

		public override string ToString()
		{
			return Identifier;
		}
	}
}

