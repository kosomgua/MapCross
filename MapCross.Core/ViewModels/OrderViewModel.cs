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

		public ICommand AddCommand
		{
			get
			{
				return new MvxCommand (() => _dataService.Add (new Order {
					FirstName = _firstNameOrder,
					LastName = _lastNameOrder,
					HamsterLatitude = _latitudeOrder,
					HamsterLongitude = _longitudeOrder,
					HamsterColor = _selectedColor
				}));

				}
		}

		public ICommand AddOrder
		{
			get
			{
				return new MvxCommand(() => {
					ShowViewModel<FirstViewModel>();
				});
			}
		}
	}
}

