
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Cirrious.MvvmCross.Droid.Views;
using MapCross.Core.ViewModels;
using System.Text.RegularExpressions;
using Android.Graphics;

namespace MapCross.Droid
{
    [Activity (Theme = "@style/MyCustomTheme")]
	public class OrderView : MvxActivity
	{
        IMenu _menu;
		OrderViewModel _viewModel;
        EditText firstName, lastName, latitude, longitude;
        const string incorrectString = "Invalid characters";

		public new OrderViewModel ViewModel
		{
			get { return _viewModel ?? (_viewModel = base.ViewModel as OrderViewModel); }
		}

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			SetContentView(Resource.Layout.OrderView);
			// Create your application here

            firstName = FindViewById<EditText> (Resource.Id.first_name);
            lastName = FindViewById<EditText> (Resource.Id.last_name);
            latitude = FindViewById<EditText> (Resource.Id.latitudeT);
            longitude = FindViewById<EditText> (Resource.Id.longitudeT);


		}
        protected override void OnStart()
        {
            base.OnStart();
            firstName.TextChanged += Validation;
            lastName.TextChanged += Validation;
            latitude.TextChanged += Validation;
            longitude.TextChanged += Validation; 
        }

        protected override void OnStop()
        {
            base.OnStop();
            firstName.TextChanged -= Validation;
            lastName.TextChanged -= Validation;
            latitude.TextChanged -= Validation;
            longitude.TextChanged -= Validation; 
        }


		public override bool OnCreateOptionsMenu (IMenu menu)
		{
            _menu = menu; 
            base.OnCreateOptionsMenu (_menu);
            MenuInflater.Inflate (Resource.Menu.order,  this._menu);
            _menu.SetGroupVisible(0, false);
			return true; 
		}

		public override bool OnOptionsItemSelected (IMenuItem item)
		{	
			base.OnOptionsItemSelected (item);
			ViewModel.AddOrder.Execute(null);
			return true;
		}



        void Validation (object obj, EventArgs e)
        {
            var field = (EditText)obj;
            Regex regularExpression;
            bool validation;
            if (field.Text.Length >= 0)
            {
                if (field.Equals(firstName))
                {  
                    regularExpression = new Regex("[0-9]");
                    validation = regularExpression.IsMatch(field.Text); 

                    if (validation)
                    {
                        field.Text = "";
                        field.Hint = incorrectString;
                        field.SetHintTextColor(Color.IndianRed);
                    }
                    else
                        checkField(field);

                }
                else if (field.Equals(lastName))
                {
                    regularExpression = new Regex("[0-9]");
                    validation = regularExpression.IsMatch(field.Text); 
                    if (validation)
                    {
                        field.Text = "";
                        field.Hint = incorrectString;
                        field.SetHintTextColor(Color.IndianRed);
                    }
                    else
                        checkField(field); 
                }
                else if (field.Equals(latitude))
                {  
                    regularExpression = new Regex("[a-z]");
                    validation = regularExpression.IsMatch(field.Text); 
                    if (validation)
                    {
                        field.Text = "";
                        field.Hint = incorrectString;
                        field.SetHintTextColor(Color.IndianRed);

                    }
                    else
                        checkField(field);
                }
                else if (field.Equals(longitude))
                {
                    regularExpression = new Regex("[a-z]");
                    validation = regularExpression.IsMatch(field.Text); 
                    if (validation)
                    {
                        field.Text = "";
                        field.Hint = incorrectString;
                        field.SetHintTextColor(Color.IndianRed);

                    }
                    else
                        checkField(field);
                }

            }

        }

        void checkField (EditText field)
        {
            if (firstName.Text.Length > 0 && lastName.Text.Length > 0
                && longitude.Text.Length > 0 && latitude.Text.Length > 0)
            {
                _menu.SetGroupVisible(0, true);

            } 
            else
                _menu.SetGroupVisible(0, false);
            
            field.Hint = "";

        }
	}
}

