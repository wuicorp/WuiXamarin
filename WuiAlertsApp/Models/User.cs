using System;
using System.Collections.Generic;
using SQLite.Net.Attributes;

namespace WuiAlertsApp.Models
{
	public class User
	{	
		public User ()
		{
		}

		[PrimaryKey, AutoIncrement]
		public int Id { get; set; }
		public string Name { get; set; }
		public string ProfileName { get; set; }
		public string Email { get; set; }
		public string Phone { get; set; }
		public bool Notifications { get; set; }
		public bool Sound { get; set; }
		public int Language { get; set; }

		//[OneToMany]
		//[ManyToOne] 
		//public IEnumerable<Vehicle> Vehicles{ get; set; }
	}
}

