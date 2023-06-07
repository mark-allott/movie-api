using System.Diagnostics.CodeAnalysis;
using MovieApi.Data.Interfaces;

namespace MovieApi.Data.DTO
{
	public class ActorDto :
		AbstractDto,
		IHaveName
	{
		#region IHaveName implementation

		/// <inheritdoc />
		[AllowNull]
		public string Name { get; set; }

		#endregion IHaveName implementation
	}
}