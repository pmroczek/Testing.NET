using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Testing.NET
{
	public interface IPricePlans
	{
		int FrequentRenterPoint(int daysRented);
		decimal Charge(int daysRented);
	}
}
