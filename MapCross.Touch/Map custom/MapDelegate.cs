using MapKit;
using UIKit;
using System.Reflection;

namespace MapCross.Touch
{
	class MapDelegate : MKMapViewDelegate
	{
		protected string annotationIdentifier = "BasicAnnotation";
		
		//Override OverLayRenderer to draw Polyline returned from directions
		public override MKOverlayRenderer OverlayRenderer(MKMapView mapView, IMKOverlay overlay)
		{
			if (overlay is MKPolyline)
			{
				var route = (MKPolyline)overlay;
				var renderer = new MKPolylineRenderer(route) { StrokeColor = UIColor.Blue };
				return renderer;
			}
			return null;
		}

		public override MKAnnotationView GetViewForAnnotation (MKMapView mapView, IMKAnnotation annotation)
		{
			// try and dequeue the annotation view
			MKAnnotationView annotationView = mapView.DequeueReusableAnnotation(annotationIdentifier);   
			// if we couldn't dequeue one, create a new one
			if (annotationView == null)
				annotationView = new MKPinAnnotationView(annotation, annotationIdentifier);
			else // if we did dequeue one for reuse, assign the annotation to it
				annotationView.Annotation = annotation;

			// configure our annotation view properties
			annotationView.CanShowCallout = true;
			(annotationView as MKPinAnnotationView).AnimatesDrop = true;
			(annotationView as MKPinAnnotationView).PinColor = MKPinAnnotationColor.Red;
			annotationView.Selected = true;
		 
			Assembly ass = this.GetType ().Assembly;

			var annotationImage = 
				UIImage.FromBundle((annotation as BasicPinAnnotation).ImageAdress);

			annotationView.LeftCalloutAccessoryView = new UIImageView(
				ResizeImage.MaxResizeImage(annotationImage,(float)40,(float)20));
			return annotationView;
		}   
	}

}

