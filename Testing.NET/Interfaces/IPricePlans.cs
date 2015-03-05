namespace Testing.NET.Interfaces
{
	public interface IPricePlans
	{
		int FrequentRenterPoint(int daysRented);
		decimal Charge(int daysRented);
	}
}
