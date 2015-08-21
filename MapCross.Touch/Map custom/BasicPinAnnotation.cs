using CoreLocation;
using MapKit;

namespace MapCross.Touch
{
	
		public class BasicPinAnnotation : MKAnnotation
		{
			string title, subtitle,imageAdress;
			public override CLLocationCoordinate2D Coordinate { get { return this.Coords; }}
			public CLLocationCoordinate2D Coords;
			public override string Title { get{ return title; }}
			public override string Subtitle { get{ return subtitle; }}
			public string ImageAdress { get {return imageAdress;}}

			public BasicPinAnnotation (CLLocationCoordinate2D coordinate, string title, string subtitle, string imageAdress) 
			{
				this.Coords = coordinate;
				this.title = title;
				this.subtitle = subtitle;
				this.imageAdress = imageAdress;
			}
		}
}

