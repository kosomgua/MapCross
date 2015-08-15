// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace MapCross.Touch.Views
{
	partial class FirstView
	{
		[Outlet]
		UIKit.UIBarButtonItem AddOrderButton { get; set; }

		[Outlet]
		UIKit.UIBarButtonItem OpenMapButton { get; set; }

		[Outlet]
		UIKit.UITableView Table { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (OpenMapButton != null) {
				OpenMapButton.Dispose ();
				OpenMapButton = null;
			}

			if (AddOrderButton != null) {
				AddOrderButton.Dispose ();
				AddOrderButton = null;
			}

			if (Table != null) {
				Table.Dispose ();
				Table = null;
			}
		}
	}
}
