using System;
using Testing.NET.Interfaces;

namespace Testing.NET.Impl
{
	public class NewReleasePrice : IPricePlans
	{
		public int FrequentRenterPoint(int daysRented)
		{
			if (daysRented <= 0)
				throw new ArgumentException();

			return daysRented * 2;
		}

		public decimal Charge(int daysRented)
		{
			if (daysRented <= 0)
				throw new ArgumentException();

			return daysRented * 3.44M;
		}
	}
}
