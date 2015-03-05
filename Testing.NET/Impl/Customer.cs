using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Testing.NET.Interfaces;

namespace Testing.NET.Impl
{
	public class Customer
	{
		public string Name { get; private set; }
		public List<IRental> Rentals { get; private set; }

		public Customer(string name)
		{
			if (string.IsNullOrEmpty(name)) throw new ArgumentNullException("Name cant be null or empty");

			Name = name;
			Rentals = new List<IRental>();
		}

		public bool AddRental(IRental rental)
		{
			bool success = false;

			if (rental != null)
			{
				Rentals.Add(rental);
				success = true;
			}

			return success;
		}

		public string GetStatement()
		{
			StringBuilder result = new StringBuilder();
			result.AppendLine(string.Format("Rental record for {0}", Name));

			Rentals.ForEach(rental =>
			{
				result.AppendLine(string.Format("{0} \t {1}", rental.Movie.Title, rental.Charge()));
			});

			result.AppendLine(string.Format("Amount owed is {0}", CalculateTotalCharge()));
			result.AppendLine(string.Format("You erned {0} frequent renter points", CalculateTotalFrequentRenterPoints()));

			return result.ToString();
		}

		private decimal CalculateTotalCharge()
		{
			return Rentals.Sum(r => r.Charge());
		}

		private int CalculateTotalFrequentRenterPoints()
		{
			return Rentals.Sum(r => r.FrequentRenterPoints());
		}
	}
}
