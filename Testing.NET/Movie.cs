using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testing.NET
{
	public class Movie
	{
		public string Title { get; set; }
		public Decimal Price { get; set; }
		private IPricePlans _pricePlan;

		public Movie(string title, IPricePlans pricePlan)
		{
		}

		/// <summary>
		/// Calculating fee payable
		/// </summary>
		/// <param name="daysRented"></param>
		public decimal Charge(int daysRented)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Calculating added extra points
		/// </summary>
		/// <param name="daysRented"></param>
		public int FrequentRenderPoints(int daysRented)
		{
			throw new NotImplementedException();
		}
	}
}
