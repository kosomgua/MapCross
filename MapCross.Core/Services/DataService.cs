using System;
using System.Collections.Generic;
using Cirrious.MvvmCross.Community.Plugins.Sqlite;
using System.Linq;
using System.Linq.Expressions;
using System.Collections;

namespace MapCross.Core.Services
{
	public class DataService : IDataService
	{ 

		ISQLiteConnection _connection;
		const string dbPath = "one.sql";

		public DataService (ISQLiteConnectionFactory fac)
		{
			
			_connection = fac.Create(dbPath);
		}

		public void CreateTable <T> ()  where T :new()
		{
			_connection.CreateTable<T> ();
		}
		public List<T> Filling <T> () where T : new()
		{
			return _connection.Table <T>().ToList();
		}

		public void Add  <T> (T order)  where T :new()
		{
			_connection.Insert (order);
		}

		public void Update  <T> (T order)  where T :new()
		{
			_connection.Update (order);
		}

		public void Delete  <T> (T order) where T :new()
		{
			_connection.Delete (order);
		} 

	}
}

