namespace MovieApi.Data.Interfaces;

public interface IHaveActors
{
	/// <summary>
	/// Provides a property to store a comma separated list of actors for the production
	/// </summary>
	string Actors { get; set; }
}