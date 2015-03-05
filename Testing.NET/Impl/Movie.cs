using System;
using Testing.NET.Interfaces;

namespace Testing.NET.Impl
{
	public class Movie : IMovie
	{
		public string Title { get; set; }
		public IPricePlans Price { get; set; }

		public Movie(string title, IPricePlans pricePlan)
		{
			if (string.IsNullOrEmpty(title) || pricePlan == null)
				throw new ArgumentNullException();

			Title = title;
			Price = pricePlan;
		}

		public decimal Charge(int daysRented)
		{
			return Price.Charge(daysRented);
		}

		public int FrequentRenderPoints(int daysRented)
		{
			return Price.FrequentRenterPoint(daysRented);
		}
	}
}
