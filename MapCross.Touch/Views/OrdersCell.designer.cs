// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace MapCross.Touch
{
	[Register ("OrdersCell")]
	partial class OrdersCell
	{
		[Outlet]
		UIKit.UIImageView ImageHamster { get; set; }

		[Outlet]
		UIKit.UILabel LatitudeLabel { get; set; }

		[Outlet]
		UIKit.UILabel LongitudeLabel { get; set; }

		[Outlet]
		UIKit.UILabel NameLabel { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (NameLabel != null) {
				NameLabel.Dispose ();
				NameLabel = null;
			}

			if (LatitudeLabel != null) {
				LatitudeLabel.Dispose ();
				LatitudeLabel = null;
			}

			if (LongitudeLabel != null) {
				LongitudeLabel.Dispose ();
				LongitudeLabel = null;
			}

			if (ImageHamster != null) {
				ImageHamster.Dispose ();
				ImageHamster = null;
			}
		}
	}
}
