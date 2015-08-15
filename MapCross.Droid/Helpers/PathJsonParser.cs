using System;
using Org.Json;
using System.Collections.Generic;
using Android.Gms.Maps.Model;


namespace MapCross.Droid
{
	public class PathJsonParser : Dictionary<string, string>
	{
		public List<List<LatLng>> parse (JSONObject jObject) 
		{
			JSONArray jRoutes = null;
			JSONArray jLegs = null;
			JSONArray jSteps = null;
			var ListListCoords = new List<List<LatLng>> ();
			var List = new List<LatLng> ();

			var validJson = jObject.GetJSONArray("routes").ToString();
			if(validJson != "[]"){
				jRoutes = jObject.GetJSONArray("routes");
				/** Traversing all routes */
				for (int i = 0; i < jRoutes.Length(); i++) {
					jLegs = ((JSONObject) jRoutes.Get(i)).GetJSONArray("legs");

					/** Traversing all legs */
					for (int j = 0; j < jLegs.Length(); j++) {
						jSteps = ((JSONObject) jLegs.Get(j)).GetJSONArray("steps");

						/** Traversing all steps */
						for (int k = 0; k < jSteps.Length(); k++) {
							var polyline = "";
							polyline = (string) ((JSONObject) ((JSONObject) jSteps
								.Get(k)).Get("polyline")).Get("points");
								List = DecodePoly(polyline);
							ListListCoords.Add(List);
							}

						}
					}
				}
			return ListListCoords;
		}


		public List<LatLng> DecodePoly (string encoded) 
		{
				List<LatLng> poly = new List<LatLng>();
				int index = 0, len = encoded.Length;
				int lat = 0, lng = 0;

				while (index < len) {
					int b, shift = 0, result = 0;
					do {
						b = encoded[index++] - 63;
						result |= (b & 0x1f) << shift;
						shift += 5;
					} while (b >= 0x20);
					int dlat = ((result & 1) != 0 ? ~(result >> 1) : (result >> 1));
					lat += dlat;

					shift = 0;
					result = 0;
					do {
					b = encoded[index++] - 63;
						result |= (b & 0x1f) << shift;
						shift += 5;
					} while (b >= 0x20);
					int dlng = ((result & 1) != 0 ? ~(result >> 1) : (result >> 1));
					lng += dlng;

					LatLng p = new LatLng((((double) lat / 1E5)),
						(((double) lng / 1E5)));
					poly.Add(p);
				}
				return poly;
		}
	}
}