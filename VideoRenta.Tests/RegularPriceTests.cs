using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Testing.NET;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace Testing.NET.Tests
{
	[TestClass()]
	public class RegularPriceTests
	{
		private TestContext testContextInstance;
		//public TestContext TestContext
		//{
		//	get { return testContextInstance; }
		//	set { testContextInstance = value; }
		//}

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
			Assert.AreEqual(14, result);
		}

		// zbiór danych z bazy danych/csv etc
		[TestMethod()]
		[DataSource("System.Data.SqlClient",
		@"Data Source=(LocalDB)\v11.0;AttachDbFilename=D:\repos\Testing.NET\Testing.NET\Data\DataForTests.mdf;Integrated Security=True",
		"dbo.RegularPriceCharge", DataAccessMethod.Sequential)]
		public void CalculationChargeWithSetData()
		{
			RegularPrice regularPrice = new RegularPrice();
			int days = (int)testContextInstance.DataRow["DaysRented"];
			int expectedResult = (int)testContextInstance.DataRow["Result"];

			var actualResult = regularPrice.Charge(days);
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
			var result = regularPrice.FrequentRenterPoint(-1);
		}

		[TestMethod()]
		[DataSource("System.Data.SqlClient",
		@"Data Source=(LocalDB)\v11.0;AttachDbFilename=D:\repos\Testing.NET\Testing.NET\Data\DataForTests.mdf;Integrated Security=True",
		"dbo.RegularFrequentRenterPoints", DataAccessMethod.Sequential)]
		[ExpectedException(typeof(System.ArgumentException))]
		public void CalculationFrequentPointWithSetData()
		{
			RegularPrice regularPrice = new RegularPrice();
			int days = (int)testContextInstance.DataRow["DaysRented"];
			int expectedResult = (int)testContextInstance.DataRow["Result"];

			var actualResult = regularPrice.FrequentRenterPoint(days);
			Assert.AreEqual(expectedResult, actualResult);
		}
	}
}
