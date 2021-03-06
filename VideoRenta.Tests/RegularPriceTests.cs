﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Testing.NET;
using Testing.NET.Impl;
using Testing.NET.Interfaces;

namespace VideoRenta.Tests
{
	[TestClass()]
	public class RegularPriceTests
	{
		public TestContext TestContext { get; set; }

		[TestMethod()]
		public void InitializedObjectIsCorrectType()
		{
			Assert.IsInstanceOfType(new RegularPrice(), typeof(IPricePlans));
		}

		[TestMethod()]
		public void CalculateChargeNoExeption()
		{
			RegularPrice regularPrice = new RegularPrice();
			try
			{
				regularPrice.Charge(2);
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
			RegularPrice regularPrice = new RegularPrice();
			regularPrice.Charge(-1);
		}

		[TestMethod()]
		[ExpectedException(typeof(System.ArgumentException))]
		public void CalculationWithZeroDaysRented()
		{
			RegularPrice regularPrice = new RegularPrice();
			regularPrice.Charge(0);
		}

		[TestMethod()]
		public void CalculationChargeWithPositiveDaysRented()
		{
			RegularPrice regularPrice = new RegularPrice();
			var result = regularPrice.Charge(14);
			Assert.AreEqual(20.0M, result);
		}

		[TestMethod()]
		[DataSource("System.Data.SqlClient",
		@"Data Source=(LocalDB)\v11.0;AttachDbFilename=D:\repos\Testing.NET\Testing.NET\Data\DataForTests.mdf;Integrated Security=True",
		"dbo.RegularPriceCharge", DataAccessMethod.Sequential)]
		public void CalculationChargeWithSetData()
		{
			RegularPrice regularPrice = new RegularPrice();
			int days = Convert.ToInt32(TestContext.DataRow["DaysRented"]);
			decimal expectedResult = Convert.ToDecimal(TestContext.DataRow["Result"]);
			decimal actualResult = regularPrice.Charge(days);
			Assert.AreEqual(expectedResult, actualResult);
		}

		[TestMethod()]
		public void CalculationFrequentRenterPointNoErrors()
		{
			RegularPrice regularPrice = new RegularPrice();
			try
			{
				regularPrice.FrequentRenterPoint(2);
			}
			catch (Exception ex)
			{
				Assert.Fail("Exepected no exeption" + ex != null ? ex.Message : string.Empty);
			}
		}

		[TestMethod()]
		public void CalculationFrequentPointGivesCorrectValue()
		{
			RegularPrice regularPrice = new RegularPrice();
			var result = regularPrice.FrequentRenterPoint(14);
			Assert.AreEqual(1, result);
		}

		[TestMethod()]
		[ExpectedException(typeof(System.ArgumentException))]
		public void CalculationFrequentPointWithNEgativeDaysRentedCausesExeption()
		{
			RegularPrice regularPrice = new RegularPrice();
			regularPrice.FrequentRenterPoint(-1);
		}

		[TestMethod()]
		[DataSource("System.Data.SqlClient",
		@"Data Source=(LocalDB)\v11.0;AttachDbFilename=D:\repos\Testing.NET\Testing.NET\Data\DataForTests.mdf;Integrated Security=True",
		"dbo.RegularFrequentRenterPoints", DataAccessMethod.Sequential)]
		public void CalculationFrequentPointWithSetData()
		{
			RegularPrice regularPrice = new RegularPrice();
			int days = Convert.ToInt32(TestContext.DataRow["DaysRented"]);
			int expectedResult = Convert.ToInt32(TestContext.DataRow["Result"]);

			var actualResult = regularPrice.FrequentRenterPoint(days);
			Assert.AreEqual(expectedResult, actualResult);
		}
	}
}
