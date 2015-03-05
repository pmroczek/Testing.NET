using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Testing.NET;
using Testing.NET.Impl;
using Testing.NET.Interfaces;

namespace VideoRenta.Tests
{
	[TestClass()]
	public class RentalTests
	{
		[TestMethod()]
		public void RentalTest()
		{
			try
			{
				Mock<IMovie> mock = new Mock<IMovie>();
				Rental rental = new Rental(mock.Object, 1);
			}
			catch (Exception)
			{
				Assert.Fail("Expected no exception");
			}
		}

		[TestMethod()]
		[ExpectedException(typeof(ArgumentNullException))]
		public void InitWithNullArgument()
		{
			new Rental(null, 1);
		}

		[TestMethod()]
		[ExpectedException(typeof(ArgumentException))]
		public void InitWitIncorrectDaysRented()
		{
			Mock<IMovie> mock = new Mock<IMovie>();
			new Rental(mock.Object, -1);
		}

		[TestMethod()]
		[ExpectedException(typeof(ArgumentException))]
		public void InitWithZeroDaysRented()
		{
			Mock<IMovie> mock = new Mock<IMovie>();
			new Rental(mock.Object, 0);
		}

		[TestMethod()]
		public void CorrectSetMovie()
		{
			Mock<IMovie> mock = new Mock<IMovie>();
			var rental = new Rental(mock.Object, 1);

			Assert.AreSame(mock.Object, rental.Movie);
		}

		[TestMethod()]
		public void CalculateCharge()
		{
			Mock<IMovie> mock = new Mock<IMovie>();
			mock.Setup(c => c.Charge(1)).Returns(3.5M);
			Rental rental = new Rental(mock.Object, 1);
			Assert.AreEqual(3.5M, rental.Charge());
		}

		[TestMethod()]
		public void CalculateFrequentRenterPoints()
		{
			Mock<IMovie> mock = new Mock<IMovie>();
			mock.Setup(c => c.FrequentRenderPoints(1)).Returns(10);
			Rental rental = new Rental(mock.Object, 1);
			Assert.AreEqual(10, rental.FrequentRenterPoints());
		}
	}
}