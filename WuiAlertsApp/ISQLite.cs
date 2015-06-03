using System;
using SQLite.Net;

namespace WuiAlertsApp
{
	public interface ISQLite
	{
		SQLiteConnection GetConnection();
	}
}

