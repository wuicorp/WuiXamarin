using System;
using System.Linq;
using System.Collections.Generic;
using Xamarin.Forms;
using WuiAlertsApp.Resx;
using WuiAlertsApp.Models;
using WuiAlertsApp.Repository;
using WuiAlertsApp.Views;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace WuiAlertsApp.ViewModels
{
	public class WuiTabbedListViewModel: BaseViewModel
	{
		private readonly IWuiRepository repository = new WuiRepository();
		private INavigation _navigation;

		public WuiTabbedListViewModel(INavigation navigation)
		{
			_navigation = navigation; 
			var temp = OpenWuiList ();
		}

		private async Task OpenWuiList()
		{
			SentWuis = App.Database.GetSentWuis ().ToList();
			ReceivedWuis = App.Database.GetReceivedWuis().ToList();

			wuis = await repository.GetWuisAsync();
			var serverSentWuis= wuis.Where(x=>x.Action=="sent").ToList();
			var serverReceivedWuis= wuis.Where(x=>x.Action=="received").ToList();

			foreach (var wui in serverSentWuis) 
			{
				if (!SentWuis.Contains (wui))
					SentWuis.Add(wui);
			}

			foreach (var wui in serverReceivedWuis) 
			{
				if (!ReceivedWuis.Contains (wui))
					ReceivedWuis.Add(wui);
			}
		}

		IEnumerable<WuiAlert> wuis;

		private List<WuiAlert> _sentWuis;
		public List<WuiAlert> SentWuis
		{
			get { return _sentWuis; }
			set
			{
				_sentWuis = value;
				OnPropertyChanged("SentWuis");
			}
		}


		private List<WuiAlert> _receivedWuis;
		public List<WuiAlert> ReceivedWuis
		{
			get { return _receivedWuis; }
			set
			{
				_receivedWuis = value;
				OnPropertyChanged("ReceivedWuis");
			}
		}

		private WuiAlert _selectedWui;
		public WuiAlert SelectedWui
		{
			get { return _selectedWui; }
			set
			{
				_selectedWui = value;
				OnPropertyChanged("SelectedWui");

				//TODO change to onTapped because selection change, doesn't change if u press the same list item
				var wuiDetailView = new WuiDetailViewX();
				wuiDetailView.BindingContext = _selectedWui;

				_navigation.PushAsync(wuiDetailView);
			}
		}

	}
}

