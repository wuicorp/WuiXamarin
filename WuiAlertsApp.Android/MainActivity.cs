using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.IO;
using Android.Speech.Tts;
using Android.Content.PM;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms;
using Xamarin;

namespace WuiAlertsApp
{
	[Activity (Label = "WuiAlertsApp", Icon = "@drawable/icon", MainLauncher = true, ScreenOrientation=ScreenOrientation.Portrait)] 
//		,ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
	public class MainActivity : 
	global::Xamarin.Forms.Platform.Android.FormsApplicationActivity // superclass new in 1.3
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			//global::Xamarin.Forms.Forms.Init (this, bundle);
			Forms.Init(this, bundle);
			FormsMaps.Init(this, bundle);

			LoadApplication (new App ()); // method is new in 1.3
		}
	}


}


