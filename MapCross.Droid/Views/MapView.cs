
using System;
using System.Collections.Generic;
using System.Linq;
using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using Cirrious.MvvmCross.Droid.Views;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using System.Threading.Tasks;
using Android.Graphics;
using MapCross.Core.ViewModels;
using Org.Json;
using System.Net.Http;
using MapCross.Core;

namespace MapCross.Droid
{
	[Activity (Label = "MapView")]			
	public class MapView : MvxActivity, IOnMapReadyCallback, GoogleMap.IInfoWindowAdapter
	{ 
		#region privateField
		MapViewModel _viewModel;
		string  selectTypeOfOrders;
		RadioButton radioAll, radioRedRoute, radioGreenRoute, radioYellowRoute;
		Button dialogButton;
		IMenu menuG;	
		LatLng latlng;  
		GoogleMap globMap;
		Dialog dialog;
		TextView textview, waitTextView;
		RadioGroup radioGroup;
		const float redFloat = 1.000001F;
		const float yellowFloat = 1.000002F;
		const float greenFloat = 1.000003F;
		const string error = "failed";
		const string allSelect = "all";
        Dictionary<string, HamsterColor> markerColor;
        #endregion 

		public new MapViewModel ViewModel
		{
			get { return _viewModel ?? (_viewModel = base.ViewModel as MapViewModel); }
		}

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			SetContentView (Resource.Layout.MapView);
			SetupMap (); 
		} 
		
		void SetupMap ()
		{
			if(globMap==null)
				FragmentManager.FindFragmentById<MapFragment>(Resource.Id.mapLay).GetMapAsync(this);
		}

		public void OnMapReady(GoogleMap googleMap)
		{
			globMap = googleMap;
            markerColor = new Dictionary<string, HamsterColor>();
			foreach ( var a in ViewModel.Markers) {
				var markerOpt = new MarkerOptions();
				double la = double.Parse(a.HamsterLatitude, System.Globalization.CultureInfo.InvariantCulture);
				double lon = double.Parse (a.HamsterLongitude, System.Globalization.CultureInfo.InvariantCulture);
				latlng = new LatLng (la, lon);
				markerOpt.SetPosition (latlng);
				markerOpt.SetTitle (String.Format("Name: {0} {1} ", a.FirstName, a.LastName));
				markerOpt.SetSnippet (String.Format("Latitude: {0}  Longitude: {1} ", a.HamsterLatitude, a.HamsterLongitude));
               
				globMap.AddMarker(markerOpt); 
                markerColor.Add(String.Format("{0}{1}",markerOpt.Title, markerOpt.Snippet), a.Color); 
			}
			globMap.MyLocationEnabled = true;
			globMap.UiSettings.CompassEnabled = true;
			globMap.UiSettings.MapToolbarEnabled = false;
			globMap.UiSettings.ZoomControlsEnabled = true;
			globMap.UiSettings.MyLocationButtonEnabled = false;

			globMap.SetInfoWindowAdapter (this);
		}

		public View GetInfoContents (Marker marker)
		{
			return null;
		}

		public View GetInfoWindow (Marker marker)
		{
			View view = LayoutInflater.Inflate (Resource.Layout.markerView, null);

			view.FindViewById<TextView> (Resource.Id.MarkerName).Text = marker.Title;
			view.FindViewById<TextView> (Resource.Id.MarkerName).SetTextColor (Color.White);

			var coord = view.FindViewById<TextView> (Resource.Id.MarkerCoord);
			var image = view.FindViewById<ImageView> (Resource.Id.MarkerImage);
			coord.Text = marker.Snippet;
            HamsterColor color;
            markerColor.TryGetValue(String.Format("{0}{1}", marker.Title, marker.Snippet), out color);

            if (color == HamsterColor.Red) {
				image.SetImageDrawable ((ImageAssetManager.Get (this, "images/hamster_red_icon.png")));
				coord.SetTextColor (Color.Red);
            } else if (color == HamsterColor.Yellow) {
				image.SetImageDrawable ((ImageAssetManager.Get (this, "images/hamster_yellow_icon.png")));
				coord.SetTextColor (Color.Yellow);
            } else if (color == HamsterColor.Green) {
				image.SetImageDrawable ((ImageAssetManager.Get (this, "images/hamster_green_icon.png")));
				coord.SetTextColor (Color.Green);
			} 
			return view;
		}


		public override bool OnCreateOptionsMenu (IMenu menu)
		{
			menuG = menu;
			base.OnCreateOptionsMenu (menuG);
			MenuInflater.Inflate (Resource.Menu.map, menuG);
			return true;

		}
		public override bool OnOptionsItemSelected (IMenuItem item)
		{	
			base.OnOptionsItemSelected (item);
			dialog = new Dialog(this, Resource.Style.CustomDialogThemeTrasparent); 
			dialog.SetContentView (Resource.Layout.dialog);
			dialogButton = (Button) dialog.FindViewById (Resource.Id.BuildRoute);
			textview = (TextView) dialog.FindViewById (Resource.Id.textviewdialog);
			radioGroup = (RadioGroup) dialog.FindViewById (Resource.Id.radioGroupdialog);
			waitTextView = (TextView) dialog.FindViewById (Resource.Id.textwaitdialog);
			dialog.Show();

			radioAll = dialog.FindViewById<RadioButton> (Resource.Id.allPointRoute);
			radioRedRoute = dialog.FindViewById<RadioButton> (Resource.Id.redRoute);
			radioGreenRoute = dialog.FindViewById<RadioButton> (Resource.Id.greenRoute);
			radioYellowRoute = dialog.FindViewById<RadioButton> (Resource.Id.yellowRoute);



			radioAll.Click += RadioButtonClick;
			radioRedRoute.Click += RadioButtonClick;
			radioGreenRoute.Click += RadioButtonClick;
			radioYellowRoute.Click += RadioButtonClick;
			dialogButton.Click += DialogButtonClick;
			return true;
		}

        async void DialogButtonClick (object sender, EventArgs e)
		{
			ProgressBar myProgressBar;
			myProgressBar = (ProgressBar)dialog.FindViewById (Resource.Id.PBar);

			dialogButton.Visibility = ViewStates.Invisible;
			textview.Visibility = ViewStates.Invisible;
			radioGroup.Visibility = ViewStates.Invisible;
			myProgressBar.Visibility = ViewStates.Visible;
			waitTextView.Visibility = ViewStates.Visible;
			myProgressBar.Indeterminate = true;

			await GetUrlAndGrubData ();

		}

		void DialogDismiss ()
		{
			radioAll.Click -= RadioButtonClick;
			radioRedRoute.Click -= RadioButtonClick;
			radioGreenRoute.Click -= RadioButtonClick;
			radioYellowRoute.Click -= RadioButtonClick;
			dialogButton.Click -= DialogButtonClick;
			dialog.Dismiss ();

		}

		void RadioButtonClick (object sender, EventArgs e)
		{
			var rb = (RadioButton)sender;
			if (rb == radioAll)
				selectTypeOfOrders = "all";
			else if (rb == radioRedRoute)
				selectTypeOfOrders = "Red";
			else if (rb == radioGreenRoute)
				selectTypeOfOrders = "Green";
			else if (rb == radioYellowRoute)
				selectTypeOfOrders = "Yellow";
		}


		public async Task GetUrlAndGrubData ()
		{
			var getData = GetMapsApiDirectionsUrl (selectTypeOfOrders);

			if (getData.Equals(error))
			{
				Toast.MakeText (this, "Not enough coordinates for route planning", ToastLength.Long).Show ();
				DialogDismiss ();
		}
			else { 
				var client = new HttpClient ();
				await client.GetStringAsync (getData);
				var data = client.GetStringAsync (getData).Result;
				client.Dispose ();
				await AsyncFilling (data);
				DialogDismiss ();
			}
		}


		public async Task AsyncFilling (string someData)
		{
			var List = await Filling(someData);
			if (List.Count != 0) {
				foreach (var a in List) {
					globMap.AddPolyline (a);
				}
				DialogDismiss ();
			} else if (List.Count == 0) {
				Toast.MakeText (this, "We can't build route", ToastLength.Long).Show ();
				DialogDismiss ();
			}
		}


		public string GetMapsApiDirectionsUrl (string select)
		{
			const string sensor = "sensor=false";
			const string output = "json";
			List<string> latitudeString = new List <string> ();
			List<string> longitudeString = new List <string> ();
			string urlLocal;
			foreach (var a in ViewModel.Markers) {

				if (select.Equals(allSelect)) {
					latitudeString.Add (a.HamsterLatitude);
					longitudeString.Add (a.HamsterLongitude);
				}
				else if (Enum.GetName(typeof(HamsterColor), a.Color) == select) {
					latitudeString.Add (a.HamsterLatitude);
					longitudeString.Add (a.HamsterLongitude);
				}
			}
			if (latitudeString.Count > 1 && longitudeString.Count > 1) {
				
				string origin = String.Format ("origin={0},{1}", latitudeString.First (), longitudeString.First ());
				string destination = String.Format ("destination={0},{1}", latitudeString.Last (), longitudeString.Last ());
				string waypoints = String.Format ("waypoints=optimize:true|{0},{1}", latitudeString [1], longitudeString [1]);
				if (latitudeString.Count > 2) {
					for (int coord = 2; coord < latitudeString.Count; coord++) {
						waypoints += "|" + latitudeString [coord] + "," + longitudeString [coord];
					}
				}
				string param = String.Format ("{0}&{1}&{2}&{3}", origin, destination, waypoints, sensor);

				urlLocal = String.Format("https://maps.googleapis.com/maps/api/directions/" +
					"{0}?{1}&key=AIzaSyCMtv1AjBlUMtALqpxG980FdFiLQYA3okU", output, param);
			} 
			else
				urlLocal = error;
			return urlLocal;
		}

 		public Task <List<PolylineOptions>> Filling (string data)
		{
			return Task.Run(() => {
				
			var polyLineOptions = new PolylineOptions ();
			var ListPolylineOptions = new List<PolylineOptions>();
			var jsonData = "";
				jsonData = data;
				var jObject = new JSONObject (jsonData);
				var parser = new PathJsonParser ();
				var ListCoords = parser.parse (jObject);

				if(ListCoords.Count > 0){
				for (int j = 0; j < ListCoords.Count; j++) {
					var listCoord = ListCoords[j];
					foreach(var list in listCoord)
					{
						polyLineOptions.Add (list);
					}
					polyLineOptions.InvokeWidth (5);
					polyLineOptions.InvokeColor (Color.Red);

				}
				ListPolylineOptions.Add(polyLineOptions);
				}

				return ListPolylineOptions;
			});
		}
	}
}

