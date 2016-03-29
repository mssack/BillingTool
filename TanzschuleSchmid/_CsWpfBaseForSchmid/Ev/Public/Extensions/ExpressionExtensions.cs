// Copyright (c) 2014, 2015 All Right Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2015-07-24</date>

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;






namespace CsWpfBase.Ev.Public.Extensions
{




	/// <summary>Gets Expression extensions.</summary>
	public static class ExpressionExtensions
	{
		/// <summary>Returns a list of all properties referenced from <typeparamref name="T" />.</summary>
		public static PropertyInfo[] GetReferencedProperties<T, TU>(this Expression<Func<T, TU>> expression)
		{
			var v = new ReferencedPropertyFinder(typeof (T));
			v.Visit(expression);
			return v.Properties;
		}



		private sealed class ReferencedPropertyFinder : ExpressionVisitor
		{
			private readonly Type _ownerType;
			private readonly List<PropertyInfo> _properties = new List<PropertyInfo>();

			public ReferencedPropertyFinder(Type ownerType)
			{
				_ownerType = ownerType;
			}


			#region Overrides/Interfaces
			protected override Expression VisitMember(MemberExpression node)
			{
				var propertyInfo = node.Member as PropertyInfo;
				if (propertyInfo != null && _ownerType.IsAssignableFrom(propertyInfo.DeclaringType))
				{
					// probably more filtering required
					_properties.Add(propertyInfo);
				}
				return base.VisitMember(node);
			}
			#endregion


			public PropertyInfo[] Properties
			{
				get { return _properties.ToArray(); }
			}
		}
	}
}