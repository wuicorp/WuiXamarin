using System;
using SQLite.Net;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using WuiAlertsApp.Models;

namespace WuiAlertsApp
{
	public class WuiDatabase 
	{
		static object locker = new object ();

		SQLiteConnection database;

		/// <summary>
		/// Initializes a new instance of the <see cref="Tasky.DL.TaskDatabase"/> TaskDatabase. 
		/// if the database doesn't exist, it will create the database and all the tables.
		/// </summary>
		/// <param name='path'>
		/// Path.
		/// </param>
		public WuiDatabase()
		{
			database = DependencyService.Get<ISQLite> ().GetConnection ();
			// create the tables
			database.CreateTable<TodoItem>();
			database.CreateTable<Vehicle>();
			database.CreateTable<User>();
			database.CreateTable<WuiAlert>();
		}

		#region Items

		public IEnumerable<TodoItem> GetItems ()
		{
			lock (locker) {
				return (from i in database.Table<TodoItem>() select i).ToList();
			}
		}

		public IEnumerable<TodoItem> GetItemsNotDone ()
		{
			lock (locker) {
				return database.Query<TodoItem>("SELECT * FROM [TodoItem] WHERE [Done] = 0");
			}
		}

		public TodoItem GetItem (int id) 
		{
			lock (locker) {
				return database.Table<TodoItem>().FirstOrDefault(x => x.ID == id);
			}
		}

		public int SaveItem (TodoItem item) 
		{
			lock (locker) {
				if (item.ID != 0) {
					database.Update(item);
					return item.ID;
				} else {
					return database.Insert(item);
				}
			}
		}

		public int DeleteItem(int id)
		{
			lock (locker) {
				return database.Delete<TodoItem>(id);
			}
		}

		#endregion

		#region Wui

		public IEnumerable<WuiAlert> GetWuis ()
		{
			lock (locker) {
				return (from i in database.Table<WuiAlert>() select i).ToList();
			}
		}

		public IEnumerable<WuiAlert> GetSentWuis ()
		{
			lock (locker) {
				return database.Query<WuiAlert>("SELECT * FROM [WuiAlert] WHERE [action] = 'sent'");
			}
		}


		public IEnumerable<WuiAlert> GetReceivedWuis ()
		{
			lock (locker) {
				return database.Query<WuiAlert>("SELECT * FROM [WuiAlert] WHERE [action] = 'received'");
			}
		}


		public WuiAlert GetWui (int id) 
		{
			lock (locker) {
				return database.Table<WuiAlert>().FirstOrDefault(x => x.Id == id);
			}
		}

		public int SaveWui (WuiAlert wui) 
		{
			lock (locker) {
				if (wui.Id != 0) {
					database.Update(wui);
					return wui.Id;
				} else {
					return database.Insert(wui);
				}
			}
		}

		public int DeleteWui(int id)
		{
			lock (locker) {
				return database.Delete<WuiAlert>(id);
			}
		}


		public int ClearWuis ()
		{
			lock (locker) {
				return database.DeleteAll<WuiAlert>();
			}
		}

		#endregion


		#region Vehicles

		public IEnumerable<Vehicle> GetVehicles ()
		{
			lock (locker) {
				return (from i in database.Table<Vehicle>() select i).ToList();
			}
		}

		public Vehicle GetVehicle(int id) 
		{
			lock (locker) {
				return database.Table<Vehicle>().FirstOrDefault(x => x.Id == id);
			}
		}

		public Vehicle GetVehicleByMatricula(string identifier) 
		{
			lock (locker) {
				return database.Table<Vehicle>().FirstOrDefault(x => x.Identifier == identifier);
			}
		}

		public int SaveVehicle (Vehicle vehicle) 
		{
			lock (locker) {
				if (vehicle.Id != 0) {
					database.Update(vehicle);
					return vehicle.Id;
				} else {
					return database.Insert(vehicle);
				}
			}
		}

		public int DeleteVehicle(int id)
		{
			lock (locker) {
				return database.Delete<Vehicle>(id);
			}
		}

		#endregion


		#region User

		public User GetUserSettings() 
		{
			lock (locker) {
				return database.Table<User>().FirstOrDefault();
			}
		}

		public int UpdateUserSettings(User user) 
		{
			lock (locker) 
			{
				var userSettings = database.Table<User> ().FirstOrDefault ();

				if (userSettings != null) 
				{
					userSettings.Name = user.Name;
					userSettings.ProfileName= user.ProfileName;
					userSettings.Notifications= user.Notifications;
					userSettings.Sound= user.Sound;
					userSettings.Language = user.Language;
					userSettings.Phone = user.Phone;
					userSettings.Email = user.Email;

					database.Update(userSettings);

					return userSettings.Id;
				} else {
					return database.Insert(user);
				}
			}
		}

		public int DeleteUser(int id)
		{
			lock (locker) {
				return database.Delete<User>(id);
			}
		}

		#endregion

	}
}

