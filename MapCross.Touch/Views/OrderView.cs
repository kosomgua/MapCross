
using System;
using System.Text.RegularExpressions;
using Foundation;
using UIKit;
using MapCross.Core.ViewModels;
using Cirrious.MvvmCross.Touch.Views;
using Cirrious.MvvmCross.Binding.BindingContext;

namespace MapCross.Touch
{
	public partial class OrderView : MvxViewController
	{
		public OrderView () : base ("OrderView", null)
		{
		}

		public new OrderViewModel ViewModel
		{
			get { return (OrderViewModel) base.ViewModel; }
			set { base.ViewModel = value; }
		}

		public override void ViewDidLoad()
		{

			base.ViewDidLoad();

			var set = this.CreateBindingSet<OrderView, OrderViewModel>();
			set.Bind(firstName).To(vm => vm.FirstNameOrder);
			set.Bind(lastName).To(vm => vm.LastNameOrder);
			set.Bind(latitude).To(vm => vm.LatitudeOrder);
			set.Bind(longitude).To(vm => vm.LongitudeOrder); 
			set.Bind(selectColor).For(sm=>sm.SelectedSegment).To(vm=>vm.SelectedSegment); 
			set.Bind(addButton).To(vm => vm.AddOrder);
			set.Apply();

            addButton.Enabled = false;
            
            firstName.EditingChanged += Validation;
            lastName.EditingChanged += Validation;
            latitude.EditingChanged += Validation;
            longitude.EditingChanged += Validation;
		}

        void Validation (object obj, EventArgs e)
        {
            var field = (UITextField)obj;
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
                        field.Layer.BorderColor = UIColor.Red.CGColor;
                        field.Layer.BorderWidth = 2; 
                        field.Layer.CornerRadius = 4;
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
                        field.Layer.BorderColor = UIColor.Red.CGColor;
                        field.Layer.BorderWidth = 2; 
                        field.Layer.CornerRadius = 4;

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
                        field.Layer.BorderColor = UIColor.Red.CGColor;
                        field.Layer.BorderWidth = 2; 
                        field.Layer.CornerRadius = 4;

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
                        field.Layer.BorderColor = UIColor.Red.CGColor;
                        field.Layer.BorderWidth = 2; 
                        field.Layer.CornerRadius = 4;
 
                    }
                    else
                        checkField(field);
                }

            }

        }

        void checkField (UITextField field)
        {
            if (firstName.Text.Length > 0 && lastName.Text.Length > 0
                && longitude.Text.Length > 0 && latitude.Text.Length > 0)
            {
                addButton.Enabled = true; 
                addButton.SetTitleColor(UIColor.Orange, UIControlState.Normal);

            }
                field.Layer.BorderColor = UIColor.Clear.CGColor;  
        }

        public override void ViewDidDisappear(bool animated)
        {
            base.ViewDidDisappear(animated);
            firstName.EditingDidEnd -= Validation;
            lastName.EditingDidEnd -= Validation;
            latitude.EditingDidEnd -= Validation;
            longitude.EditingDidEnd -= Validation;
        }

	}
}

