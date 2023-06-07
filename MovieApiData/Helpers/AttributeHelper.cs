using System.ComponentModel.DataAnnotations.Schema;
using System.Linq.Expressions;
using System.Reflection;

namespace MovieApi.Data.Helpers
{
	public static class AttributeHelper
	{
		/// <summary>
		/// Attempts to extract property data from the <paramref name="propertyExpression"/>, using the
		/// <paramref name="valueSelector"/> for the given type, attribute etc. using reflection
		/// </summary>
		/// <typeparam name="T">The class being interrogated</typeparam>
		/// <typeparam name="TOut">The type of the underlying property</typeparam>
		/// <typeparam name="TAttribute">The type of attribute being used</typeparam>
		/// <typeparam name="TValue">The output value for the result</typeparam>
		/// <param name="propertyExpression">The property in <typeparamref name="T"/> to extract the <typeparamref name="TValue"/> from</param>
		/// <param name="valueSelector">The property of <typeparamref name="TAttribute"/> to extract the value</param>
		/// <returns>The value, or default</returns>
		/// <remarks>Based on responses in https://stackoverflow.com/questions/6637679/reflection-get-attribute-name-and-value-on-property</remarks>
		public static TValue? GetPropertyAttributeValue<T, TOut, TAttribute, TValue>(
			Expression<Func<T, TOut>> propertyExpression, Func<TAttribute, TValue> valueSelector)
			where TAttribute : Attribute
		{
			var expr = propertyExpression.Body as MemberExpression;
			var pi = expr?.Member as PropertyInfo;
			var attr = pi?.GetCustomAttributes(typeof(TAttribute), true)
				.FirstOrDefault() as TAttribute;

			return attr is not null
				? valueSelector(attr)
				: default(TValue);
		}

		/// <summary>
		///	Attribute-specific helper to pull the <see cref="ColumnAttribute.Name"/> property from the <see cref="ColumnAttribute"/> decoration
		/// </summary>
		/// <typeparam name="T">The class being interrogated</typeparam>
		/// <typeparam name="TOut">The type of the underlying property</typeparam>
		/// <param name="propertyExpression">The expression being used to find the value</param>
		/// <returns>A value from the <see cref="ColumnAttribute"/> decoration, or the property name itself</returns>
		public static string GetColumnName<T, TOut>(Expression<Func<T, TOut>> propertyExpression)
		{
			//	Try to get a column name from the ColumnAttribute value
			var name = GetPropertyAttributeValue<T, TOut, ColumnAttribute, string?>(propertyExpression, attr => attr.Name);

			if (!string.IsNullOrWhiteSpace(name))
				return name;

			//	None present, so let's resort to the name of the property itself (or empty!)
			var expr = propertyExpression.Body as MemberExpression;

			return expr?.Member is PropertyInfo pi
				? pi.Name
				: string.Empty;
		}
	}
}