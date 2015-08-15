using Android.App;
using Android.OS;
using Cirrious.MvvmCross.Droid.Views;
using Android.Views;
using Android.Widget;
using System.Collections.Generic;
using Android.Graphics.Drawables;
using com.refractored.fab;
using System;
using MapCross.Core.ViewModels;
using Cirrious.MvvmCross.Binding.Droid.BindingContext;
using Cirrious.MvvmCross.Binding.BindingContext;
using Cirrious.MvvmCross.Binding.Droid.Views;
using Cirrious.MvvmCross.Binding.Droid;

namespace MapCross.Droid.Views
{
	[Activity (Theme = "@style/MyCustomTheme")]
	public class FirstView : MvxActivity
    {
//		CheckBox box;
		IMenu menuG;	
		ListView listView;
//		ListAdapter _adapter;
//		IMenuItem acceptButton, mapButton, editButton;
		FloatingActionButton fab;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
			SetContentView(Resource.Layout.FirstView);
			listView = FindViewById<ListView>(Resource.Id.listView);
			fab = FindViewById<FloatingActionButton>(Resource.Id.fab);
			fab.AttachToListView(listView);

        }
		protected FirstViewModel FirstViewModel
		{
			get { return base.ViewModel as FirstViewModel; }
		}

		protected override void OnStop ()
		{
			base.OnStop ();
			fab.Click -= FABClick;
		}

		protected override void OnStart ()
		{
			base.OnStart ();
			fab.Click += FABClick;
		}

		void FABClick (object sender, EventArgs e)
		{
			((FirstViewModel)ViewModel).NewOrderCommand.Execute(null);
		}
    

		public override bool OnCreateOptionsMenu (IMenu menu)
		{
			menuG = menu;
			base.OnCreateOptionsMenu (menuG);
			MenuInflater.Inflate (Resource.Menu.home, menuG);
//			acceptButton = menuG.FindItem (Resource.Id.AcceptButton);
//			mapButton = menuG.FindItem (Resource.Id.MapButton);
//			editButton = menuG.FindItem (Resource.Id.EditButton);

			return true;

		}
		public override bool OnOptionsItemSelected (IMenuItem item)
		{	
			base.OnOptionsItemSelected (item);

			switch (item.ItemId) {
			case Resource.Id.DeleteButton:
				((FirstViewModel)ViewModel).DeleteCommand.Execute(null);
				return true;

			case Resource.Id.MapButton:
				((FirstViewModel)ViewModel).GoMapCommand.Execute(null);
				return true;
			}
			return true;
		}


	}
}