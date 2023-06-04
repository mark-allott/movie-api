namespace MovieApi.Data.Interfaces
{
	public interface IPublicity
	{
		/// <summary>
		/// Allows the implementation to store details of the URL of the poster for the title
		/// </summary>
		string? PosterUrl { get; set; }
	}
}