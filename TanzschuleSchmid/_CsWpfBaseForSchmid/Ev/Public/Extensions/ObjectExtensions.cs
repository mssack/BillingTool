using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CsWpfBase.Ev.Public.Extensions
{
	/// <summary>Object Extensions.</summary>
	public static class ObjectExtensions
	{
		/// <summary>Checks if the <paramref name="obj" /> is the default value of the <paramref name="type" />.</summary>
		public static bool Is_DefaultValue_OfType(this object obj, Type type)
		{
			if (type.IsPrimitive && obj == Activator.CreateInstance(type))
				return true;
			if (type.IsValueType && obj == null)
				return true;
			return false;
		}
	}
}
