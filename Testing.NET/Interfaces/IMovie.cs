namespace Testing.NET.Interfaces
{
	public interface IMovie
	{
		string Title { get; set; }
		IPricePlans Price { get; set; }
		decimal Charge(int daysRented);
		int FrequentRenderPoints(int daysRented);
	}
}