using System;

namespace WuiAlertsApp
{
	public interface ILocale
	{
		string GetCurrent();

		void SetLocale();
	}
}

