using Cirrious.MvvmCross.ViewModels;
using System.Collections.Generic;
using MapCross.Core.Services;
using Cirrious.MvvmCross.Plugins.File;
using System;
using System.Windows.Input;

namespace MapCross.Core.ViewModels
{
	public class FirstViewModel : MvxViewModel
    {
		public IDataService _dataService;

		List <Order> _orders;
		public List <Order> Orders
		{
			get { return _orders; }
			set { _orders = value; RaisePropertyChanged(() => Orders); }
		}
//		bool _isChecked;
//		public bool IsChecked
//		{
//			get { return _isChecked; }
//			set { _isChecked = value; RaisePropertyChanged(() => IsChecked); }
//		}
//		public List<Order> GetItems(){
//			return _dataService.Filling();
//		}
		public FirstViewModel (IDataService dataService)
		{
			_dataService = dataService;
//			DebugData ();
			Orders = _dataService.Filling ();
		}

		MvxCommand _deleteCommand;
		public System.Windows.Input.ICommand DeleteCommand
		{
			get
			{
				_deleteCommand = _deleteCommand ?? new MvxCommand(Remove);
				return _deleteCommand;
			}
		}

		public IMvxCommand NewOrderCommand
		{
			get 
			{
				return new MvxCommand(() => {
					ShowViewModel<OrderViewModel>();
				});
			}
		}

		public IMvxCommand GoMapCommand
		{
			get 
			{
				return new MvxCommand(() => {
					ShowViewModel<MapViewModel>();
				});
			}
		}

		public void Remove (){
			_dataService.RemoveSelected(Orders);
		}
	}
}
