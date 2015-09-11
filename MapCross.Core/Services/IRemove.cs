using System;
using System.Windows.Input;

namespace MapCross.Core
{
	public interface IRemove
	{
		ICommand RemoveCommand { get; }
	}
}

