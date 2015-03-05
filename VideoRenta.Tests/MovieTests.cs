using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Testing.NET;
using Testing.NET.Impl;
using Testing.NET.Interfaces;

namespace VideoRenta.Tests
{
	[TestClass()]
	public class MovieTests
	{
		[TestMethod()]
		public void MovieTest()
		{
			try
			{
				new Movie("Title", new RegularPrice());
			}
			catch (Exception ex)
			{
				Assert.Fail("Expected no exeption. " + ex != null ? ex.Message : string.Empty);
			}
		}

		[TestMethod()]
		public void CorrectSetTitle()
		{
			Movie movie = new Movie("Title", new RegularPrice());
			Assert.AreEqual("Title", movie.Title);
		}

		[TestMethod()]
		public void PassedPriceIsTheSameInMovie()
		{
			RegularPrice regular = new RegularPrice();
			Movie movie = new Movie("Title", regular);
			Assert.AreSame(movie.Price, regular);
		}

		[TestMethod()]
		public void CorretSetRegularPricePlan()
		{
			Movie movie = new Movie("Title", new RegularPrice());
			Assert.IsInstanceOfType(movie.Price, typeof(RegularPrice));
		}

		[TestMethod()]
		public void CorrectSetChildrenPricePlan()
		{
			Movie movie = new Movie("Title", new ChildrenPrice());
			Assert.IsInstanceOfType(movie.Price, typeof(ChildrenPrice));
		}

		[TestMethod()]
		public void CorrectSetChildrenNewReleasePricePlan()
		{
			Movie movie = new Movie("Title", new NewReleasePrice());
			Assert.IsInstanceOfType(movie.Price, typeof(NewReleasePrice));
		}

		[TestMethod()]
		[ExpectedException(typeof(System.ArgumentNullException))]
		public void FirstArgumentIsNullThrowExeption()
		{
			new Movie(null, new NewReleasePrice());
		}

		[TestMethod()]
		[ExpectedException(typeof(System.ArgumentNullException))]
		public void FirstArgumentIsEmptyStringThrowExeption()
		{
			new Movie(string.Empty, new NewReleasePrice());
		}

		[TestMethod()]
		[ExpectedException(typeof(System.ArgumentNullException))]
		public void SecondArgumentIsNullThrowExeption()
		{
			new Movie("Title", null);
		}

		// MOCKS
		[TestMethod()]
		public void ChargeCalculationForRegularPrice()
		{
			Mock<IPricePlans> mock = new Mock<IPricePlans>();
			mock.Setup(par => par.Charge(3)).Returns(3.5M);

			Movie movie = new Movie("Title", mock.Object);
			var result = movie.Charge(3);
			Assert.AreEqual(3.5M, result);
		}

		[TestMethod()]
		public void ChargeCalculationForChildrenPrice()
		{
			Movie movie = new Movie("Title", new ChildrenPrice());
			var result = movie.Charge(3);
			Assert.AreEqual(1.5M, result);
		}

		[TestMethod()]
		public void ChargeCalculationFoNewReleasePrice()
		{
			Movie movie = new Movie("Title", new NewReleasePrice());
			var result = movie.Charge(3);
			Assert.AreEqual(10.32M, result);
		}

		[TestMethod()]
		public void CalculationChargeNoExeption()
		{
			try
			{
				Movie movie = new Movie("Title", new NewReleasePrice());
				var result = movie.Charge(3);
			}
			catch (Exception ex)
			{
				Assert.Fail("Unexpected exeption while charge calculating.");
			}
		}

		[TestMethod()]
		public void FrequentRenderPointsCalculationForRegularPrice()
		{
			Movie movie = new Movie("Title", new RegularPrice());
			var result = movie.FrequentRenderPoints(3);
			Assert.AreEqual(1, result);
		}

		[TestMethod()]
		public void FrequentRenderPointsCalculationForChildrenPrice()
		{
			Movie movie = new Movie("Title", new ChildrenPrice());
			var result = movie.FrequentRenderPoints(3);
			Assert.AreEqual(2, result);
		}

		[TestMethod()]
		public void FrequentRenderPointsCalculationFoNewReleasePrice()
		{
			Movie movie = new Movie("Title", new NewReleasePrice());
			var result = movie.FrequentRenderPoints(3);
			Assert.AreEqual(6, result);
		}

		[TestMethod()]
		public void CalculationFrequentRenderPointsNoExeption()
		{
			try
			{
				Movie movie = new Movie("Title", new NewReleasePrice());
				var result = movie.FrequentRenderPoints(3);
			}
			catch (Exception ex)
			{
				Assert.Fail("Unexpected exeption while FrequentRenderPoints calculating.");
			}
		}
	}
}