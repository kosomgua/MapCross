using Cirrious.MvvmCross.Binding.BindingContext;
using Cirrious.MvvmCross.Touch.Views;
using CoreGraphics;
using Foundation;
using ObjCRuntime;
using UIKit;
using Cirrious.CrossCore;
using Cirrious.MvvmCross.ViewModels;

namespace MapCross.Touch.Views
{
	[Register("FirstView")]
	public partial class FirstView : MvxViewController
	{ 
				public FirstView () : base ("FirstView", null)
				{
				}
//		public FirstView ()
//		{
//		}
//		
//
//		public FirstView (System.IntPtr handle) : base (handle)
//		{
//		}
//		
//
//		public FirstView (string nibName, NSBundle bundle) : base (nibName, bundle)
//		{
//		}
		
		public override void ViewDidLoad()
		{

			//            View = new UIView { BackgroundColor = UIColor.White };
			base.ViewDidLoad();

			//
			// ios7 layout
			//            if (RespondsToSelector(new Selector("edgesForExtendedLayout")))
			//            {
			//               EdgesForExtendedLayout = UIRectEdge.None;
			//            }
			//			Table
			//			AddOrderButton
			//			OpenMapButton

			//            var label = new UILabel(new CGRect(10, 10, 300, 40));
			//            Add(label);
			//            var textField = new UITextField(new CGRect(10, 50, 300, 40));
			//            Add(textField);
			//
			var set = this.CreateBindingSet<FirstView, MapCross.Core.ViewModels.FirstViewModel>();
			set.Bind(Table).To(vm => vm.Orders);
			set.Bind(AddOrderButton).To(vm => vm.NewOrderCommand);
			set.Bind(OpenMapButton).To(vm => vm.GoMapCommand);
			set.Apply();
		}
	}
}