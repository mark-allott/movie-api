using System.Linq.Expressions;
using CsvHelper.Configuration;
using MovieApi.Data.DTO;
using MovieApi.Data.Helpers;

namespace MovieApi.Data.Providers
{
	public class KaggleDataImportClassMap :
		ClassMap<KaggleData>
	{
		#region Ctor

		public KaggleDataImportClassMap(List<string> headerList)
		{
			if (!headerList.Any())
				DoDefaultIndexMapping();
			else
				DoDefaultNamedMapping();
		}

		#endregion Ctor

		/// <summary>
		/// Assumes the data is as exported in the same order listed on the website
		/// </summary>
		private void DoDefaultIndexMapping()
		{
			Map(m => m.ReleaseDate).Index(0);
			Map(m => m.Title).Index(1);
			Map(m => m.Overview).Index(2);
			Map(m => m.Popularity).Index(3);
			Map(m => m.VoteCount).Index(4);
			Map(m => m.VoteAverage).Index(5);
			Map(m => m.OriginalLanguage).Index(6);
			Map(m => m.Genres).Index(7);
			Map(m => m.PosterUrl).Index(8);
		}

		/// <summary>
		/// Assembles an array of alternate names from the specified name
		/// </summary>
		/// <param name="name">The nominal column name</param>
		/// <returns>An array of possible alternate column names</returns>
		private string[] GetNames(string name)
		{
			var names = new List<string>
			{
				name,
				name.ToLowerInvariant(),
				name.ToUpperInvariant(),
				name.Replace(" ", ""),
				name.Replace(" ", "").ToLowerInvariant(),
				name.Replace(" ", "").ToUpperInvariant(),
				name.Replace("_", "").ToLowerInvariant(),
				name.Replace("_", "").ToUpperInvariant(),
			};

			return names.Distinct().ToArray();
		}

		/// <summary>
		/// Gets the name of the column from the property itself (if decorated with the <seealso cref="System.ComponentModel.DataAnnotations.Schema.ColumnAttribute"/>)
		/// </summary>
		/// <typeparam name="T">The type of the property</typeparam>
		/// <param name="expression">An expression to extract the property</param>
		/// <returns>The name of the column to look for</returns>
		private string GetColumnName<T>(Expression<Func<KaggleData, T>> expression)
		{
			return AttributeHelper.GetColumnName<KaggleData, T>(expression);
		}

		/// <summary>
		/// Assumes the default naming conventions of the data extract
		/// </summary>
		private void DoDefaultNamedMapping()
		{
			Map(m => m.ReleaseDate)
				.Name(GetNames(GetColumnName(m => m.ReleaseDate)));
			Map(m => m.Title)
				.Name(GetNames(GetColumnName(m => m.Title)));
			Map(m => m.Overview)
				.Name(GetNames(GetColumnName(m => m.Overview)));
			Map(m => m.Popularity)
				.Name(GetNames(GetColumnName(m => m.Popularity)));
			Map(m => m.VoteCount)
				.Name(GetNames(GetColumnName(m => m.VoteCount)));
			Map(m => m.VoteAverage)
				.Name(GetNames(GetColumnName(m => m.VoteAverage)));
			Map(m => m.OriginalLanguage)
				.Name(GetNames(GetColumnName(m => m.OriginalLanguage)));
			Map(m => m.Genres)
				.Name(GetNames(GetColumnName(m => m.Genres)));
			Map(m => m.PosterUrl)
				.Name(GetNames(GetColumnName(m => m.PosterUrl)));
		}
	}
}