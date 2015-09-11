using System;
using System.Collections.Generic;

namespace MapCross.Core.Services
{
	public interface IDataService
	{
		void CreateTable <T> () where T :new();  
		List<T> Filling <T> () where T :new();
		void Add <T>(T order) where T :new(); 
		void Update <T>(T order) where T :new();
		void Delete <T>(T order) where T :new(); 
	}  
}

