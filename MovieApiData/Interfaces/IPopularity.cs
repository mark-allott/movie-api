namespace MovieApi.Data.Interfaces
{
	public interface IPopularity
	{
		/// <summary>
		/// Holds the total score for the title
		/// </summary>
		double Popularity { get; set; }

		/// <summary>
		/// Holds the total number of votes cast
		/// </summary>
		long VoteCount { get; set; }

		/// <summary>
		/// Gives the average rating for the title
		/// </summary>
		double VoteAverage { get; set; }
	}
}