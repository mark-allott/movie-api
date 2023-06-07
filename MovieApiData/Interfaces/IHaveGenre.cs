namespace MovieApi.Data.Interfaces;

public interface IHaveGenre
{
	/// <summary>
	/// Provides a property to store a comma separated list of genres for the production
	/// </summary>
	string Genre { get; set; }
}