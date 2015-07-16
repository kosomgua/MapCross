using Android.App;
using Android.OS;
using Cirrious.MvvmCross.Droid.Views;
using Android.Views;
using Android.Widget;
using System.Collections.Generic;
using Android.Graphics.Drawables;
using com.refractored.fab;
using Android.Content;
using System;
using MapCross.Core.ViewModels;
using Cirrious.MvvmCross.Binding.Droid.BindingContext;
using Cirrious.MvvmCross.Binding.BindingContext;
using Cirrious.MvvmCross.Binding.Droid.Views;

namespace MapCross.Droid.Views
{
	[Activity (Theme = "@style/MyCustomTheme")]
	public class FirstView : MvxActivity
    {
//		CheckBox box;
		IMenu menuG;	
		ListView listView;
		IMenuItem acceptButton, mapButton, editButton;
		FloatingActionButton fab;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
			SetContentView(Resource.Layout.FirstView);

			listView = FindViewById<ListView>(Resource.Id.listView);
//			listView.Adapter = new ListAdapter (this, (IMvxAndroidBindingContext) BindingContext);

			fab = FindViewById<FloatingActionButton>(Resource.Id.fab);
			fab.AttachToListView(listView);

			var _adapter = new ListAdapter(this,this.BindingContext as IMvxAndroidBindingContext);
			_adapter.ItemsSource = FirstViewModel.Orders;
			// Get template for item
			_adapter.ItemTemplateId = (listView.Adapter as IMvxAdapter).ItemTemplateId;

//			this.CreateBinding (_adapter).To<FirstViewModel> (vm => vm.Orders).Apply();
			listView.Adapter = _adapter;

//			this.CreateBinding(listView).For("ItemClick").To<FirstViewModel> (vm => vm.EditCommand).Apply();
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

		protected override void OnResume ()
		{
			base.OnResume ();
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
			acceptButton = menuG.FindItem (Resource.Id.AcceptButton);
			mapButton = menuG.FindItem (Resource.Id.MapButton);
			editButton = menuG.FindItem (Resource.Id.EditButton);

			acceptButton.SetVisible(false);


	//					if (list.Count == 0) {
	//						mapButton.SetVisible (false);
	//						editButton.SetVisible (false);
	//					}
			return true;

		}
		public override bool OnOptionsItemSelected (IMenuItem item)
		{	
			base.OnOptionsItemSelected (item);
//			box = FindViewById<CheckBox>(Resource.Id.checkBoxFocusArea);

			switch (item.ItemId) {
			case Resource.Id.AcceptButton:
				acceptButton.SetVisible (false);
//				box.Enabled = false;
				fab.Enabled = true;
//						DeleteSelectesRows ();
				ActionBar.SetBackgroundDrawable (new ColorDrawable (Resources.GetColor (Resource.Color.my_red)));
				listView.SetBackgroundColor (Resources.GetColor (Resource.Color.my_white));
				((FirstViewModel)ViewModel).DeleteCommand.Execute(null);
				mapButton.SetVisible (true);
				editButton.SetVisible (true);
				return true;

			case Resource.Id.EditButton:
//				box.Enabled = true;
				fab.Enabled = false;
				acceptButton.SetVisible(true);
				mapButton.SetVisible (false);
				editButton.SetVisible (false);
				   
				ActionBar.SetBackgroundDrawable (new ColorDrawable (Resources.GetColor (Resource.Color.colorActionBarEdit)));
				listView.SetBackgroundColor (Resources.GetColor (Resource.Color.colorBackgroundEdit));

				return true;

			case Resource.Id.MapButton:
				((FirstViewModel)ViewModel).GoMapCommand.Execute(null);
				return true;
			}
			return true;
		}


	}
}