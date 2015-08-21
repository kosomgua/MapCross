
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
using MapCross.Core.ViewModels;

namespace MapCross.Droid
{
	[Activity (Label = "OrderView")]			
	public class OrderView : MvxActivity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			SetContentView(Resource.Layout.OrderView);
			// Create your application here
		}
	
	public override bool OnCreateOptionsMenu (IMenu menu)
	{
		base.OnCreateOptionsMenu (menu);
		MenuInflater.Inflate (Resource.Menu.order, menu);
		menu.SetGroupVisible(0, true);
		return true;

	}
	public override bool OnOptionsItemSelected (IMenuItem item)
	{	
		base.OnOptionsItemSelected (item);
//			((OrderViewModel)ViewModel).AddCommand.Execute(null);
			((OrderViewModel)ViewModel).AddOrder.Execute(null);
		return true;
	}
	}
}

