using System.ComponentModel;
using System.Runtime.CompilerServices;
//using MVVM.Annotations;

namespace WuiAlertsApp.ViewModels
{
	public class BaseViewModel: INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		//[NotifyPropertyChangedInvocator]
		protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			var handler = PropertyChanged;
			if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}

