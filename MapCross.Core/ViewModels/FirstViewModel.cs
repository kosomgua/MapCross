using Cirrious.MvvmCross.ViewModels;
using System.Collections.Generic;
using MapCross.Core.Services;
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

		public FirstViewModel (IDataService dataService)
		{
			_dataService = dataService;
			Orders = _dataService.Filling ();
		}

		MvxCommand _deleteCommand;
		public ICommand DeleteCommand
		{
			get
			{
				_deleteCommand = _deleteCommand ?? new MvxCommand(() => Remove ());
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
				return new MvxCommand (() =>  {
					ShowViewModel<MapViewModel> ();
				});
			}
		}


		public void Remove ()
		{
			foreach (var a in Orders) {
				if (a.IsSelected) {
					_dataService.Delete(a);
				}

			}
			Orders = _dataService.Filling ();
		}

	}
}
