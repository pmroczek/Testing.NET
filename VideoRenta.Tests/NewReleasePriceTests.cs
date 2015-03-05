using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Testing.NET;
using Testing.NET.Impl;
using Testing.NET.Interfaces;

namespace VideoRenta.Tests
{
	[TestClass()]
	public class NewReleasePriceTests
	{
		public TestContext TestContext { get; set; }

		[TestMethod()]
		public void InitializedObjectIsCorrectType()
		{
			Assert.IsInstanceOfType(new NewReleasePrice(), typeof(IPricePlans));
		}

		[TestMethod()]
		public void CalculateChargeNoExeption()
		{
			NewReleasePrice newReleasePrice = new NewReleasePrice();
			try
			{
				newReleasePrice.Charge(2);
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
			NewReleasePrice newReleasePrice = new NewReleasePrice();
			newReleasePrice.Charge(-1);
		}

		[TestMethod()]
		[ExpectedException(typeof(System.ArgumentException))]
		public void CalculationWithZeroDaysRented()
		{
			NewReleasePrice newReleasePrice = new NewReleasePrice();
			newReleasePrice.Charge(0);
		}

		[TestMethod()]
		public void CalculationChargeWithPositiveDaysRented()
		{
			NewReleasePrice newReleasePrice = new NewReleasePrice();
			var result = newReleasePrice.Charge(3);
			Assert.AreEqual(10.32M, result);
		}

		[TestMethod()]
		[DataSource("System.Data.SqlClient",
		@"Data Source=(LocalDB)\v11.0;AttachDbFilename=D:\repos\Testing.NET\Testing.NET\Data\DataForTests.mdf;Integrated Security=True",
		"dbo.NewReleasePriceCharge", DataAccessMethod.Sequential)]
		public void CalculationChargeWithSetData()
		{
			NewReleasePrice newReleasePrice = new NewReleasePrice();
			int days = Convert.ToInt32(TestContext.DataRow["DaysRented"]);
			decimal expectedResult = Convert.ToDecimal(TestContext.DataRow["Result"]);
			decimal actualResult = newReleasePrice.Charge(days);
			Assert.AreEqual(expectedResult, actualResult);
		}

		[TestMethod()]
		public void CalculationFrequentRenterPointNoErrors()
		{
			NewReleasePrice newReleasePrice = new NewReleasePrice();
			try
			{
				newReleasePrice.FrequentRenterPoint(2);
			}
			catch (Exception ex)
			{
				Assert.Fail("Exepected no exeption" + ex != null ? ex.Message : string.Empty);
			}
		}

		[TestMethod()]
		public void CalculationFrequentPointGivesCorrectValue()
		{
			NewReleasePrice newReleasePrice = new NewReleasePrice();
			var result = newReleasePrice.FrequentRenterPoint(3);
			Assert.AreEqual(6, result);
		}

		[TestMethod()]
		[ExpectedException(typeof(System.ArgumentException))]
		public void CalculationFrequentPointWithNEgativeDaysRentedCausesExeption()
		{
			NewReleasePrice newReleasePrice = new NewReleasePrice();
			newReleasePrice.FrequentRenterPoint(-1);
		}

		[TestMethod()]
		[DataSource("System.Data.SqlClient",
		@"Data Source=(LocalDB)\v11.0;AttachDbFilename=D:\repos\Testing.NET\Testing.NET\Data\DataForTests.mdf;Integrated Security=True",
		"dbo.NewReleasePriceFrequentRenterPoints", DataAccessMethod.Sequential)]
		public void CalculationFrequentPointWithSetData()
		{
			NewReleasePrice newReleasePrice = new NewReleasePrice();
			int days = Convert.ToInt32(TestContext.DataRow["DaysRented"]);
			int expectedResult = Convert.ToInt32(TestContext.DataRow["Result"]);

			var actualResult = newReleasePrice.FrequentRenterPoint(days);
			Assert.AreEqual(expectedResult, actualResult);
		}
	}
}
