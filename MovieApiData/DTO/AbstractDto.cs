using System;

namespace MovieApi.Data.DTO
{
	/// <summary>
	/// Defines an abstract DTO type that can return itself, minus any EF navigation
	/// </summary>
	public abstract class AbstractDto
	{
		protected AbstractDto()
		{
		}

		/// <summary>
		/// Converts this object to the <typeparam name="T">type T</typeparam> specified
		/// </summary>
		/// <typeparam name="T">The DTO to be returned</typeparam>
		/// <returns></returns>
		public T AsDto<T>() where T : AbstractDto, new()
		{
			var t = typeof(T);
			if (!t.IsInstanceOfType(this))
				return new T();

			return (T)this.MemberwiseClone();
		}
	}
}