using System;
using Cirrious.MvvmCross.ViewModels;
using MapCross.Core.Services;
using System.Windows.Input;
using System.Collections.Generic;

namespace MapCross.Core.ViewModels
{
	public class OrderViewModel : MvxViewModel
	{
		readonly IDataService _dataService;
		public OrderViewModel (IDataService dataService)
		{
			_dataService = dataService;
		}

		string _firstNameOrder;
		public string FirstNameOrder
		{
			get { return _firstNameOrder; }
			set { _firstNameOrder = value; RaisePropertyChanged(() => FirstNameOrder); }
		}

		string _lastNameOrder;
		public string LastNameOrder
		{
			get { return _lastNameOrder; }
			set { _lastNameOrder = value; RaisePropertyChanged(() => LastNameOrder); }
		}

		string _latitudeOrder;
		public string LatitudeOrder
		{
			get { return _latitudeOrder; }
			set { _latitudeOrder = value; RaisePropertyChanged(() => LatitudeOrder); }
		}

		string _longitudeOrder;
		public string LongitudeOrder
		{
			get { return _longitudeOrder; }
			set { _longitudeOrder = value; RaisePropertyChanged(() => LongitudeOrder); }
		}

		string _selectedColor;
		public string SelectedColor {
			get {
				return _selectedColor;
			}
			set {
				_selectedColor = value; RaisePropertyChanged (() => SelectedColor);
			}
		}

		int _selectedSegment;

		public int SelectedSegment
		{
			get {return _selectedSegment;}
			set
			{
				_selectedSegment = value;
				RaisePropertyChanged(() => SelectedSegment);
			}
		}

		string finalColorSelected ()
		{
			if (_selectedColor != null)
				return _selectedColor;
			else
				switch (_selectedSegment) {
					case 0:
				return "Red"; 
					case 1:
				return "Green"; 
					case 2:
				return "Yellow"; 
					default:
				return "error"; 
					}

		}

//		public int ColorIos {
//			get {
//				return 0;
//			}
//			set {
//				switch (value) {
//				case 0:
//					_selectedColor = "Red";
//					break;
//				case 1:
//					_selectedColor = "Green";
//					break;
//				case 2:
//					_selectedColor = "Yellow";
//					break;
//				default:
//					_selectedColor = "error";
//					break;
//				}
//				RaisePropertyChanged (() => ColorIos);
//			}
//		}


		List <string> _colorGroup = new List<string>
		{
			"Red",
			"Green",
			"Yellow"
		};

		public List<string> ColorGroup 
		{
			get { return _colorGroup; }
		}

		public ICommand AddOrder
		{
			get
			{
				return new MvxCommand(() => {
					_dataService.Add (new Order {
						FirstName = _firstNameOrder,
						LastName = _lastNameOrder,
						HamsterLatitude = _latitudeOrder,
						HamsterLongitude = _longitudeOrder,
						HamsterColor = finalColorSelected(),
					});
					ShowViewModel<FirstViewModel>();
				});
			}
		}
	}
}

