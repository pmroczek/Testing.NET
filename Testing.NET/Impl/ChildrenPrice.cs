using System;
using Testing.NET.Interfaces;

namespace Testing.NET.Impl
{
	public class ChildrenPrice : IPricePlans
	{
		public int FrequentRenterPoint(int daysRented)
		{
			if (daysRented <= 0)
				throw new ArgumentException();

			return daysRented - 1;
		}

		public decimal Charge(int daysRented)
		{
			if (daysRented <= 0)
				throw new ArgumentException();

			decimal result = 1.5M;
			if (daysRented > 3)
				result += (daysRented - 3) * 1.5M;

			return result;
		}
	}
}
