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
	[Register ("OrderView")]
	partial class OrderView
	{
		[Outlet]
		UIKit.UIButton addButton { get; set; }

		[Outlet]
		UIKit.UITextField firstName { get; set; }

		[Outlet]
		UIKit.UITextField lastName { get; set; }

		[Outlet]
		UIKit.UITextField latitude { get; set; }

		[Outlet]
		UIKit.UITextField longitude { get; set; }

		[Outlet]
		UIKit.UISegmentedControl selectColor { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (addButton != null) {
				addButton.Dispose ();
				addButton = null;
			}

			if (firstName != null) {
				firstName.Dispose ();
				firstName = null;
			}

			if (lastName != null) {
				lastName.Dispose ();
				lastName = null;
			}

			if (latitude != null) {
				latitude.Dispose ();
				latitude = null;
			}

			if (longitude != null) {
				longitude.Dispose ();
				longitude = null;
			}

			if (selectColor != null) {
				selectColor.Dispose ();
				selectColor = null;
			}
		}
	}
}
