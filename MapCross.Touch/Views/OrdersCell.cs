
using System;

using Foundation;
using UIKit;
using Cirrious.MvvmCross.Binding.Touch.Views;
using Cirrious.MvvmCross.Binding.BindingContext;
using MapCross.Core;

namespace MapCross.Touch
{
	public partial class OrdersCell : MvxTableViewCell
	{
		public static readonly UINib Nib = UINib.FromName ("OrdersCell", NSBundle.MainBundle);
		public static readonly NSString Key = new NSString ("OrdersCell");
		readonly MvxImageViewLoader _imageViewLoader;

		public OrdersCell (IntPtr handle) : base (handle)
		{
			_imageViewLoader = new MvxImageViewLoader(() => ImageHamster);
			this.DelayBind(() => {
				var set = this.CreateBindingSet<OrdersCell, Order>();
				set.Bind(NameLabel).To(order => order.Name);
				set.Bind(LatitudeLabel).To (order => order.HamsterLatitude);
				set.Bind(LongitudeLabel).To (order => order.HamsterLongitude);
				set.Bind(_imageViewLoader).To (order => order.ImageName);
				set.Apply ();
			});
		}

		public static OrdersCell Create ()
		{
			return (OrdersCell)Nib.Instantiate (null, null) [0];
		}
	}
}

