using System;

namespace WuiAlertsApp.Models
{
	public interface ILocatable
	{
		double Latitude { get; }
		double Longitude { get; }
	}
}