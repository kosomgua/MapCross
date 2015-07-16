using System;
using Cirrious.MvvmCross.ViewModels;
using MapCross.Core.Services;
using System.Windows.Input;

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

		string _colorOrder;
//		public string ColorOrder
//		{
//			get { return _colorOrder; }
//			set { _colorOrder = value; RaisePropertyChanged(() => ColorOrder); }
//		}
		public ICommand SelectGreenCommand
		{
			get
			{
				return new MvxCommand(() => ColorSelect ("green"));
			}
		}
		public ICommand SelectRedCommand
		{
			get
			{
				return new MvxCommand(() => ColorSelect("red"));
			}
		}
		public ICommand SelectYellowCommand
		{
			get
			{
				return new MvxCommand(() => ColorSelect("yellow"));
			}
		}

		void ColorSelect(string color)
		{
			_colorOrder = color;
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
					HamsterColor = _colorOrder
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

