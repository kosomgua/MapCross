
using System;

using Foundation;
using UIKit;
using MapCross.Core.ViewModels;
using Cirrious.MvvmCross.Touch.Views;
using Cirrious.MvvmCross.Binding.BindingContext;
using System.Collections.Generic;
using MapCross.Core;
using MapKit;
using CoreGraphics;
using CoreLocation;

namespace MapCross.Touch
{
	public partial class MapView : MvxViewController
	{
		const string green = "Green";
		const string red = "Red";
		const string yellow = "Yellow";
		const string All = "All";
		LoadingOverlay _loadPop;
		UIBarButtonItem navBarItem;



		public MapView () : base ("MapView", null)
		{
		}

		public new MapViewModel ViewModel
		{
			get { return (MapViewModel) base.ViewModel; }
			set { base.ViewModel = value; }
		}

		public override void ViewDidLoad()
		{

			base.ViewDidLoad();
			var set = this.CreateBindingSet<MapView, MapViewModel>();
			navBarItem = new UIBarButtonItem ();
			NavigationItem.SetRightBarButtonItem(navBarItem, true);
			navBarItem.Title = "Build Route";
			set.Apply();



			var mapDelegate = new MapDelegate ();
			map.Delegate = mapDelegate;
			Annotation ();
			navBarItem.Clicked += RouteDialogShow;
		}
		void RouteAlert ()
		{
			var okCancelAlertController = UIAlertController.Create("We can't build a route",
				"We regret but we can't build a route across one or more selected points",
				UIAlertControllerStyle.Alert);
			//Add Actions
			okCancelAlertController.AddAction(UIAlertAction.Create("Ok", UIAlertActionStyle.Cancel, null));
			//Present Alert
			PresentViewController(okCancelAlertController, true, null);
		}

		void RouteDialogShow (object s, EventArgs e)
		{
			//Create Alert
			var okCancelAlertController = UIAlertController.Create(
				"Build route", "Select a type of orders for building the route"
				, UIAlertControllerStyle.Alert);

			//Add Actions
			okCancelAlertController.AddAction(UIAlertAction.Create(All,
				UIAlertActionStyle.Default, alert => CreateRoute (All)));

			okCancelAlertController.AddAction(UIAlertAction.Create(green, 
				UIAlertActionStyle.Default, alert => CreateRoute (green)));

			okCancelAlertController.AddAction(UIAlertAction.Create(yellow, 
				UIAlertActionStyle.Default, alert => CreateRoute (yellow)));

			okCancelAlertController.AddAction(UIAlertAction.Create(red, 
				UIAlertActionStyle.Default, alert => CreateRoute (red)));

			//Present Alert
			PresentViewController(okCancelAlertController, true, null);

		}


		public void Annotation  ()
		{   
			foreach ( var a in ViewModel.Markers)  {
				string description = string.Format("Latitude: {0} Longitude: {1} ", a.HamsterLatitude, a.HamsterLongitude);
				var annotation = new BasicPinAnnotation (new CLLocationCoordinate2D(
					double.Parse (a.HamsterLatitude, System.Globalization.CultureInfo.InvariantCulture),
					double.Parse (a.HamsterLongitude, System.Globalization.CultureInfo.InvariantCulture)),
					string.Format("Name: {0} {1} ", a.FirstName, a.LastName),
					description, ImagePath(a.HamsterColor));
				map.AddAnnotation (annotation);
			}

		}

		string ImagePath (string color)
		{
			switch (color){
			case "Red":
//				return "Images/hamster_red_icon.jpg";
			return "hamster_red_icon.png";
			case "Green":
				return "hamster_green_icon.png";
			case "Yellow":
				return "hamster_yellow_icon.png";
				default:
				return "error";
			}
		}


		void OverlayOptions ()
		{
			// Determine the correct size to start the overlay (depending on device orientation)
			var bounds = UIScreen.MainScreen.Bounds; // portrait bounds
			if (UIApplication.SharedApplication.StatusBarOrientation == UIInterfaceOrientation.LandscapeLeft ||
				UIApplication.SharedApplication.StatusBarOrientation == UIInterfaceOrientation.LandscapeRight) 
			{
				bounds.Size = new CGSize(bounds.Size.Height, bounds.Size.Width);
			}
			// show the loading overlay on the UI thread using the correct orientation sizing
			this._loadPop = new LoadingOverlay (bounds);
			this.View.Add ( this._loadPop );
		}

		void CreateRoute(string select)
		{
			//start 
			OverlayOptions ();

			List <Order> selectedColor = new List<Order> ();;
			foreach (var l in ViewModel.Markers) {
				if (l.HamsterColor == select) {
					selectedColor.Add (l);
				}
			}
			if(select.Equals(All))
				selectedColor = ViewModel.Markers;

			for (int i = 0; i < selectedColor.Count -1;i++) {

				var orignPlaceMark = new MKPlacemark (new CLLocationCoordinate2D (
					double.Parse (selectedColor [i].HamsterLatitude, System.Globalization.CultureInfo.InvariantCulture),
					double.Parse (selectedColor [i].HamsterLongitude, System.Globalization.CultureInfo.InvariantCulture)), 
					new MKPlacemarkAddress ());
				var sourceItem = new MKMapItem (orignPlaceMark);
				int itemNext = i + 1;
				var destPlaceMark = new MKPlacemark (new CLLocationCoordinate2D (
					double.Parse (selectedColor [itemNext].HamsterLatitude, System.Globalization.CultureInfo.InvariantCulture),
					double.Parse (selectedColor [itemNext].HamsterLongitude, System.Globalization.CultureInfo.InvariantCulture)),
					new MKPlacemarkAddress ());

				var destItem = new MKMapItem (destPlaceMark);

				//Create Directions request using the source and dest items
				var request = new MKDirectionsRequest {
					Source = sourceItem,
					Destination = destItem,
					RequestsAlternateRoutes = false
				};

				var directions = new MKDirections (request);

				//Hit Apple Directions server
				directions.CalculateDirections ((response, error) => {
					if (error != null) {
						RouteAlert ();
					} else {
						//Add each polyline from route to map as overlay
						foreach (var route in response.Routes) {
							map.AddOverlay (route.Polyline);
						}
					}
				});
			}
			_loadPop.Hide ();
			_loadPop.Dispose ();
		}

		public override void ViewDidDisappear (bool animated)
		{
			base.ViewDidDisappear (animated);
			map.RemoveAnnotations(map.Annotations);
			navBarItem.Clicked -= RouteDialogShow;

			// Perform any additional setup after loading the view, typically from a nib.
		}
	}
}

