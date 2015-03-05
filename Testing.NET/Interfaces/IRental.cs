namespace Testing.NET.Interfaces
{
	public interface IRental
	{
		IMovie Movie { get; }
		int DaysRented { get; }
		decimal Charge();
		int FrequentRenterPoints();
	}
}