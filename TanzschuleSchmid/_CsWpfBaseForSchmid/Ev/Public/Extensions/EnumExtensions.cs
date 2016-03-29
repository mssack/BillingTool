// Copyright (c) 2014, 2015 All Right Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2015-07-24</date>

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using CsWpfBase.Ev.Attributes;






namespace CsWpfBase.Ev.Public.Extensions
{
	/// <summary>Wraps a bunch of extension methods for enum's.</summary>
	//[DebuggerStepThrough]
	public static class EnumExtensions
	{
		private static readonly Dictionary<Enum, DescriptionAttribute> Cache = new Dictionary<Enum, DescriptionAttribute>();

		/// <summary>Using one of the following Names: <see cref="EnumDescriptionAttribute.Name" /> or <see cref="Enum" /> value itself.</summary>
		public static string GetName(this Enum value)
		{
			if (value == null)
				return null;
			lock (Cache)
			{
				DescriptionAttribute descAttribute;
				if (Cache.TryGetValue(value, out descAttribute))
				{
					if (descAttribute == null)
						return value.ToString();
					if (descAttribute is EnumDescriptionAttribute)
						return (descAttribute as EnumDescriptionAttribute).Name;
					return value.ToString();
				}

				var attr = GetAttribute<DescriptionAttribute>(value);
				Cache.Add(value, attr);
				if (attr == null)
					return value.ToString();
				if (attr is EnumDescriptionAttribute)
					return ((EnumDescriptionAttribute) attr).Name;
				return value.ToString();
			}
		}

		/// <summary>
		///     Using one of the following Names: <see cref="EnumDescriptionAttribute.Name" /> or <see cref="DescriptionAttribute" /> or <see cref="Enum" />
		///     value itself.
		/// </summary>
		public static string GetDescription(this Enum value)
		{
			lock (Cache)
			{
				if (value == null)
					return null;
				DescriptionAttribute descAttribute;
				if (Cache.TryGetValue(value, out descAttribute))
				{
					if (descAttribute == null)
						return value.ToString();
					return descAttribute.Description;
				}

				var attr = GetAttribute<DescriptionAttribute>(value);
				Cache.Add(value, attr);
				if (attr == null)
					return value.ToString();
				return attr.Description;
			}
		}

		/// <summary>Finds all Attributes of specific type, even derived types.</summary>
		private static TAttributeType GetAttribute<TAttributeType>(Enum value) where TAttributeType : DescriptionAttribute
		{
			var fi = value.GetType().GetField(value.ToString());

			if (fi == null)
				return null;

			var enumba = fi.GetCustomAttributes(false).Where(x => x.GetType() == typeof (TAttributeType) || x.GetType().IsSubclassOf(typeof (TAttributeType)));
			var attributes = enumba.Cast<TAttributeType>().ToArray();

			return attributes.Length == 0 ? null : attributes[0];
		}
	}
}