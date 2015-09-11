using Cirrious.MvvmCross.ViewModels;
using System.Collections.Generic;
using MapCross.Core.Services;
using System.Windows.Input;
using System.Linq;

namespace MapCross.Core.ViewModels
{
	public class FirstViewModel  : MvxViewModel 
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
			_dataService.CreateTable <Order> ();
			Orders = _dataService.Filling <Order>();
		}
		

		public ICommand DeleteCommand
		{
			get
			{
				return  new MvxCommand(() => Remove ());
			}
		}

		public MvxCommand NewOrderCommand
		{
			get 
			{
				return new MvxCommand(() => {
					ShowViewModel<OrderViewModel>();
				});
			}
		}

		public MvxCommand GoMapCommand
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
			Orders = _dataService.Filling <Order> ();
		}

	}
}