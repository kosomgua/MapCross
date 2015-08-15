using System;
using System.Collections.Generic;

namespace MapCross.Core.Services
{
	public interface IDataService
	{
		List<Order> Filling();
		void Add(Order order);
		void Update(Order order);
		void Delete(Order order);
		int Count { get; }
	}
}

