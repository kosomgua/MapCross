using System;
using System.Collections.Generic;
using Cirrious.MvvmCross.Community.Plugins.Sqlite;
using System.Linq;

namespace MapCross.Core.Services
{
	public class DataService : IDataService
	{

		private  ISQLiteConnection _connection;

		public DataService (ISQLiteConnectionFactory factory)
		{
			_connection = factory.Create("one.sql");
			_connection.CreateTable<Order>();
		}

		#region IDataServices implementation

		public List<Order> Filling ()
		{
			return _connection.Table<Order> ().ToList ();
		}

		public void Add (Order order)
		{
			_connection.Insert (order);
		}

		public void Update (Order order)
		{
			_connection.Update (order);
		}

		public void Delete (Order order)
		{
			_connection.Delete (order);
		}

		public void Delete (int i)
		{
			_connection.Delete<Order> (i);
		}

		public int Count {
			get {
				return _connection.Table<Order> ().Count();
			}
		}

		#endregion
	}
}

