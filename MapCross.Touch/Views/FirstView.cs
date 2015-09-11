
using System;

using Foundation;
using UIKit;
using Cirrious.MvvmCross.Touch.Views;
using Cirrious.MvvmCross.Binding.BindingContext;
using Cirrious.MvvmCross.Binding.Touch.Views;
using MapCross.Core.ViewModels;

namespace MapCross.Touch
{
	public partial class FirstView : MvxViewController
	{
		public FirstView () : base ("FirstView", null)
		{
		} 

		new FirstViewModel ViewModel
		{
			get { return (FirstViewModel) base.ViewModel; }
			set { base.ViewModel = value; }
		}
 
		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			var leftNavBarItem = new UIBarButtonItem(UIBarButtonSystemItem.Add); 
			NavigationItem.SetLeftBarButtonItem(leftNavBarItem,true);
			var rightNavBarItem = new UIBarButtonItem();
			NavigationItem.SetRightBarButtonItem(rightNavBarItem,true);
			rightNavBarItem.Title = "Open Map";
			rightNavBarItem.TintColor = UIColor.Orange;
			leftNavBarItem.TintColor = UIColor.Orange;

			var source = new MvxDeleteStandardTableViewSource(ViewModel, Table, "TitleText .");

			Table.RowHeight = 50;
			Table.Source = source;
			var set = this.CreateBindingSet<FirstView, FirstViewModel>();
			set.Bind(source).To(vm => vm.Orders);
			set.Bind(leftNavBarItem).To(vm => vm.NewOrderCommand);
			set.Bind(rightNavBarItem).To(vm => vm.GoMapCommand);
			set.Apply();
		}

	}
}

