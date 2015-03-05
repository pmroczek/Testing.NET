using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Testing.NET;
using Testing.NET.Impl;
using Testing.NET.Interfaces;

namespace VideoRenta.Tests
{
	[TestClass()]
	public class ChildrenPriceTests
	{
		public TestContext TestContext { get; set; }

		[TestMethod()]
		public void InitializedObjectIsCorrectType()
		{
			Assert.IsInstanceOfType(new ChildrenPrice(), typeof(IPricePlans));
		}

		[TestMethod()]
		public void CalculateChargeNoExeption()
		{
			ChildrenPrice childrenPrice = new ChildrenPrice();
			try
			{
				childrenPrice.Charge(2);
			}
			catch (Exception ex)
			{
				Assert.Fail("Exepected no exeption" + ex != null ? ex.Message : string.Empty);
			}
		}

		[TestMethod()]
		[ExpectedException(typeof(System.ArgumentException))]
		public void CalculationChargeWithNegativeDaysRented()
		{
			ChildrenPrice childrenPrice = new ChildrenPrice();
			childrenPrice.Charge(-1);
		}

		[TestMethod()]
		[ExpectedException(typeof(System.ArgumentException))]
		public void CalculationWithZeroDaysRented()
		{
			ChildrenPrice childrenPrice = new ChildrenPrice();
			childrenPrice.Charge(0);
		}

		[TestMethod()]
		public void CalculationChargeWithPositiveDaysRented()
		{
			ChildrenPrice childrenPrice = new ChildrenPrice();
			var result = childrenPrice.Charge(5);
			Assert.AreEqual(4.5M, result);
		}

		[TestMethod()]
		[DataSource("System.Data.SqlClient",
		@"Data Source=(LocalDB)\v11.0;AttachDbFilename=D:\repos\Testing.NET\Testing.NET\Data\DataForTests.mdf;Integrated Security=True",
		"dbo.ChildrenPriceCharge", DataAccessMethod.Sequential)]
		public void CalculationChargeWithSetData()
		{
			ChildrenPrice childrenPrice = new ChildrenPrice();
			int days = Convert.ToInt32(TestContext.DataRow["DaysRented"]);
			decimal expectedResult = Convert.ToDecimal(TestContext.DataRow["Result"]);
			decimal actualResult = childrenPrice.Charge(days);
			Assert.AreEqual(expectedResult, actualResult);
		}

		[TestMethod()]
		public void CalculationFrequentRenterPointNoErrors()
		{
			ChildrenPrice childrenPrice = new ChildrenPrice();
			try
			{
				childrenPrice.FrequentRenterPoint(2);
			}
			catch (Exception ex)
			{
				Assert.Fail("Exepected no exeption" + ex != null ? ex.Message : string.Empty);
			}
		}

		[TestMethod()]
		public void CalculationFrequentPointGivesCorrectValue()
		{
			ChildrenPrice childrenPrice = new ChildrenPrice();
			var result = childrenPrice.FrequentRenterPoint(14);
			Assert.AreEqual(13, result);
		}

		[TestMethod()]
		[ExpectedException(typeof(System.ArgumentException))]
		public void CalculationFrequentPointWithNEgativeDaysRentedCausesExeption()
		{
			ChildrenPrice childrenPrice = new ChildrenPrice();
			childrenPrice.FrequentRenterPoint(-1);
		}

		[TestMethod()]
		[DataSource("System.Data.SqlClient",
		@"Data Source=(LocalDB)\v11.0;AttachDbFilename=D:\repos\Testing.NET\Testing.NET\Data\DataForTests.mdf;Integrated Security=True",
		"dbo.ChildrenPriceFrequentRenterPoints", DataAccessMethod.Sequential)]
		public void CalculationFrequentPointWithSetData()
		{
			ChildrenPrice childrenPrice = new ChildrenPrice();
			int days = Convert.ToInt32(TestContext.DataRow["DaysRented"]);
			int expectedResult = Convert.ToInt32(TestContext.DataRow["Result"]);

			var actualResult = childrenPrice.FrequentRenterPoint(days);
			Assert.AreEqual(expectedResult, actualResult);
		}
	}
}
