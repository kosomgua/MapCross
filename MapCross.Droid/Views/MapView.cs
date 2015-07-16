
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Cirrious.MvvmCross.Droid.Views;

namespace MapCross.Droid
{
	[Activity (Label = "MapView")]			
	public class MapView : MvxActivity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			SetContentView(Resource.Layout.MapView);

			// Create your application here
		}
	}
}

