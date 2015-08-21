
using System;

using Foundation;
using UIKit;
using MapCross.Core.ViewModels;
using Cirrious.MvvmCross.Touch.Views;
using Cirrious.MvvmCross.Binding.BindingContext;

namespace MapCross.Touch
{
	public partial class OrderView : MvxViewController
	{
		public OrderView () : base ("OrderView", null)
		{
		}

		public new OrderViewModel ViewModel
		{
			get { return (OrderViewModel) base.ViewModel; }
			set { base.ViewModel = value; }
		}

		public override void ViewDidLoad()
		{

			base.ViewDidLoad();
			NavigationItem.HidesBackButton = true;
			var set = this.CreateBindingSet<OrderView, OrderViewModel>();
			set.Bind(firstName).To(vm => vm.FirstNameOrder);
			set.Bind(lastName).To(vm => vm.LastNameOrder);
			set.Bind(latitude).To(vm => vm.LatitudeOrder);
			set.Bind(longitude).To(vm => vm.LongitudeOrder); 
			set.Bind(selectColor).For(sm=>sm.SelectedSegment).To(vm=>vm.SelectedSegment); 
			set.Bind(addButton).To(vm => vm.AddOrder);
			set.Apply();
		}
	}
}

