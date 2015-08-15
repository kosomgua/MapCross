using System;
using Cirrious.MvvmCross.ViewModels;
using MapCross.Core.Services;
using System.Collections.Generic;

namespace MapCross.Core.ViewModels
{
	public class MapViewModel : MvxViewModel 
	{
		IDataService _dataService;


		List <Order> _markers;
		public List <Order> Markers
		{
			get { return _markers; }
			set { _markers = value; RaisePropertyChanged(() => Markers); }
		}

		public MapViewModel (IDataService dataService)
		{
			_dataService = dataService;
			Markers = _dataService.Filling ();
		}
	}
}

