using System;
using System.Collections.Generic;

using Xamarin.Forms;

using WuiAlertsApp.ViewModels;

namespace WuiAlertsApp.Views
{
	public partial class SettingsListView : ContentPage
	{
		public SettingsListView ()
		{
			InitializeComponent ();
		}

		protected override void OnBindingContextChanged()
		{
			SettingsViewModel vm = BindingContext as SettingsViewModel;
			if (vm != null) {
				this.languagesPicker.Items.Clear();
				foreach (var language in vm.Languages)
				{
					languagesPicker.Items.Add(language);
				}
			}
			//Call base after populating the collection so that the bound value SelectedIndex is attached properly 
			//and the selected value shown
			base.OnBindingContextChanged();
		}
	}
}

