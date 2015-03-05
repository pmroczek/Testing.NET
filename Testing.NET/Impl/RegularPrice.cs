using System;
using Testing.NET.Interfaces;

namespace Testing.NET.Impl
{
	public class RegularPrice : IPricePlans
	{
		public int FrequentRenterPoint(int daysRented)
		{
			if (daysRented <= 0)
				throw new ArgumentException();

			return 1;
		}

		public decimal Charge(int daysRented)
		{
			if (daysRented <= 0)
				throw new ArgumentException();

			decimal result = 2;

			if (daysRented > 2)
				result += (daysRented - 2) * 1.5M;

			return result;
		}
	}
}
