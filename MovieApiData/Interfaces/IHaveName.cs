using System.ComponentModel.DataAnnotations;

namespace MovieApi.Data.Interfaces
{
	public interface IHaveName
	{
		/// <summary>
		/// Implementations shall have a name property that is required
		/// </summary>
		[Required]
		string Name { get; set; }
	}
}