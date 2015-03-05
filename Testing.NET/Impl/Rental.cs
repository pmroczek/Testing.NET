using System;
using Testing.NET.Interfaces;

namespace Testing.NET.Impl
{
	public class Rental
	{
		public IMovie Movie { get; private set; }
		public int DaysRented { get; private set; }

		public Rental(IMovie movie, int daysRented)
		{
			if (movie == null)
				throw new ArgumentNullException();

			if (daysRented <= 0)
				throw new ArgumentException("Incorrect number days");

			Movie = movie;
			DaysRented = daysRented;
		}

		public decimal Charge()
		{
			return Movie.Charge(DaysRented);
		}

		public int FrequentRenterPoints()
		{
			return Movie.FrequentRenderPoints(DaysRented);
		}
	}
}
