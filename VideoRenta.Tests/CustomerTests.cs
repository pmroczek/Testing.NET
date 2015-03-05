using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Testing.NET;
using Testing.NET.Impl;
using Testing.NET.Interfaces;

namespace VideoRenta.Tests
{
	[TestClass()]
	public class CustomerTests
	{
		[TestMethod()]
		public void CustomerTestNoErrors()
		{
			try
			{
				new Customer("Przemek");
			}
			catch (Exception)
			{
				Assert.Fail("Expected no exeption while create new Customer");
			}
		}

		[TestMethod()]
		public void CorrectSetCustomerNameNoNull()
		{
			Assert.IsNotNull(new Customer("Przemek").Name);
		}

		[TestMethod()]
		public void CorrectSetCustomerName()
		{
			var customer = new Customer("Przemek");
			Assert.AreEqual("Przemek", customer.Name);
		}

		[TestMethod()]
		public void CustomerRentalsNoNull()
		{
			Assert.IsNotNull(new Customer("Przemek").Rentals);
		}

		[TestMethod()]
		public void CustomerRentalsIsValidType()
		{
			Assert.IsInstanceOfType(new Customer("Przemek").Rentals, typeof(List<Rental>));
		}

		[TestMethod()]
		[ExpectedException(typeof(ArgumentNullException))]
		public void InitializeWithEmptyName()
		{
			new Customer(string.Empty);
		}

		[TestMethod()]
		[ExpectedException(typeof(ArgumentNullException))]
		public void InitializeWithNullName()
		{
			new Customer(null);
		}


		// MOCKS
		[TestMethod()]
		public void CorrectAddRentalForUser()
		{
			Mock<IRental> rental = new Mock<IRental>();
			Customer customer = new Customer("Test");
			Assert.IsTrue(customer.AddRental(rental.Object));
		}

		[TestMethod()]
		public void IncorrectAddRentalForUser()
		{
			Customer customer = new Customer("Test");
			Assert.IsFalse(customer.AddRental(null));
		}

		[TestMethod()]
		public void GetStatementTest()
		{
			Mock<IRental> rental = new Mock<IRental>();
			Mock<IMovie> movie = new Mock<IMovie>();
			Mock<IPricePlans> price = new Mock<IPricePlans>();

			movie.Setup(c => c.Title).Returns("Title");
			movie.Setup(c => c.Price).Returns(price.Object);

			rental.Setup(c => c.Charge()).Returns(10);
			rental.Setup(c => c.Movie).Returns(movie.Object);

			Customer customer = new Customer("Test");
			customer.AddRental(rental.Object);

			StringBuilder result = new StringBuilder();
			result.AppendLine(string.Format("Rental record for {0}", "Test"));
			result.AppendLine(string.Format("{0} \t {1}", "Title", 10));
			result.AppendLine(string.Format("Amount owed is {0}", 10));
			result.AppendLine(string.Format("You erned {0} frequent renter points", 0));
			Assert.AreEqual(result.ToString(), customer.GetStatement());
		}
	}
}
