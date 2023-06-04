using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieApi.Data.Interfaces
{
	public interface IIdentifiable
	{
		/// <summary>
		/// Provides the implementor with the equivalent of a BIGINT IDENTITY(1,1) NOT NULL column in
		/// SQL Server which should be declared as the first column of the table
		/// </summary>
		[Required, Key, Column(Order = 0)]
		long Id { get; set; }
	}
}