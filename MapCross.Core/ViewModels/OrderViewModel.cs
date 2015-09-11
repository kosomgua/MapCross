using System;
using Cirrious.MvvmCross.ViewModels;
using MapCross.Core.Services;
using System.Windows.Input;
using System.Collections.Generic;

namespace MapCross.Core.ViewModels
{
	public class OrderViewModel : MvxViewModel
	{
		IDataService _dataService;
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

		HamsterColor  finalColorSelected ()
		{
			 HamsterColor _color = new  HamsterColor (); 
				switch (_selectedSegment) {
				case 0:
				return HamsterColor.Red;
				case 1:
				return HamsterColor.Green;
				case 2:
				return HamsterColor.Yellow;
				}

			return _color;
		}
 
		List <int> _colorGroup = new List<int>
		{
			0,
			1,
			2
		};

		public List<int> ColorGroup 
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
						Color = finalColorSelected(),
						HamsterLatitude = _latitudeOrder,
						HamsterLongitude = _longitudeOrder

							
					});
					ShowViewModel<FirstViewModel>();
				});
			}
		}
	}
}

