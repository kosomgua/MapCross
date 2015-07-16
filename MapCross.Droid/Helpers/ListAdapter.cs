using System;
using Cirrious.MvvmCross.Binding.Droid.Views;
using Android.Content;
using Cirrious.MvvmCross.Binding.Droid.BindingContext;
using Android.Views;
using Android.Graphics;
using Android.Widget;
using MapCross.Core;
using MapCross.Core.Services;
using Cirrious.MvvmCross.Community.Plugins.Sqlite;

namespace MapCross.Droid
{
	public class ListAdapter : MvxAdapter
		{
//		ISQLiteConnection connection;
		public ListAdapter(Context context, IMvxAndroidBindingContext bindingContext) : base(context, bindingContext)
			{
			}

//			protected override View GetBindableView (View convertView, object source, int templateId)
//			{
//			var view = convertView;
//			templateId = Resource.Layout.item_order;
//
//			var checkBox = view.FindViewById<CheckBox> (Resource.Id.checkBoxFocusArea);
//
//
//			checkBox.Click += (sender, e) => {
//				if (checkBox.Checked)
//					((Order)source).IsChecked = true;
//				else
//					((Order)source).IsChecked = false;
//			};
//
//
//				return base.GetBindableView(convertView, source, templateId);
//			}

//		protected override void BindSimpleView (View convertView, object dataContext)
//		{
//			dataContext
//		}

//		protected override View GetBindableView (View convertView, object dataContext)
//		{
//			var view = convertView;
//			var checkBox = view.FindViewById<CheckBox> (Resource.Id.checkBoxFocusArea);
//			var order = dataContext as Order;
//						checkBox.Click += (sender, e) => {
//						if (checkBox.Checked)
//					connection.Table<Order>().ElementAt()	order.IsChecked = true;
//							else
//								order.IsChecked = false;
//						};		
//			return view;
//		}
		
	
			public override View GetView (int position, View convertView, ViewGroup parent)
			{
			var v = base.GetView(position, convertView, parent);
			var checkBox = v.FindViewById<CheckBox> (Resource.Id.checkBoxFocusArea);
			var order = GetRawItem (position) as Order;

			checkBox.Click += (sender, e) => {
			if (checkBox.Checked)
					order.Check = true;
				else if(!checkBox.Checked)
					order.Check = false;
			};
				return v;
			}
		}

}

