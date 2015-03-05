using System.Collections.Generic;
using Testing.NET.Impl;

namespace Testing.NET.Interfaces
{
	public interface ICustomer
	{
		string Name { get; }
		List<Rental> Rentals { get; }
		bool AddRental(Rental rental);
		string GetStatement();
	}
}