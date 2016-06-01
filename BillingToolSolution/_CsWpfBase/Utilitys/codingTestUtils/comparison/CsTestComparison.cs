// Copyright (c) 2014, 2015 All Right Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-01-01</date>

using System;
using System.Linq;
using System.Reflection;
using CsWpfBase.Ev.Public.Extensions;






namespace CsWpfBase.Utilitys.codingTestUtils.comparison
{
	/// <summary>See at <see cref="CsTest" />.</summary>
	public sealed class CsTestComparison
	{
		private static CsTestComparison _instance;
		private static readonly object SingletonLock = new object();

		/// <summary>Returns the singleton instance</summary>
		internal static CsTestComparison I
		{
			get
			{
				if (_instance != null)
					return _instance; //Advanced first check to improve performance (no lock needed).
				lock (SingletonLock)
				{
					return _instance ?? (_instance = new CsTestComparison());
				}
			}
		}

		private CsTestComparison()
		{
		}


		/// <summary>Compares all fields of type primitive or primitive array of an object an asserts that both values are equal.</summary>
		/// <param name="firstObject">The first object</param>
		/// <param name="secondObject">The second object</param>
		public void Compare_PrimitiveFields(object firstObject, object secondObject)
		{
			if (firstObject == secondObject)
				return;
			if ((firstObject == null || secondObject == null))
				throw new Exception($"One object is null.");
			if (firstObject.GetType() != secondObject.GetType())
				throw new Exception($"Two object with different types. '{firstObject.GetType().Name}' and '{secondObject.GetType().Name}'");

			var fieldInfos = firstObject.GetType().GetFields_IncludingBaseClasses(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic).Where(x=> x.FieldType.IsSpecialPrimitive() || (x.FieldType.IsArray && x.FieldType.IsSpecialPrimitive())).ToArray();

			foreach (var field in fieldInfos)
			{
				var firstValue = field.GetValue(firstObject);
				var secondValue = field.GetValue(secondObject);
				if (field.FieldType.IsArray)
				{
					var firstArray = (Array) firstValue;
					var secondArray = (Array) secondValue;
					if (firstArray.Length != secondArray.Length)
						throw new Exception($"At field [{field.Name}:{field.FieldType.Name}] there is no equality ({firstValue} != {secondValue}) with objects of type [{firstObject.GetType()}]!");
					for (int i = 0; i < firstArray.Length; i++)
					{

						if (!Equals(firstArray.GetValue(i), secondArray.GetValue(i)))
							throw new Exception($"At array index {i} of field [{field.Name}:{field.FieldType.Name}] there is no equality ({firstValue} != {secondValue}) with objects of type [{firstObject.GetType()}]!");
					}
					continue;
				}


				if (!Equals(firstValue, secondValue))
					throw new Exception($"At field [{field.Name}:{field.FieldType.Name}] there is no equality ({firstValue} != {secondValue}) with objects of type [{firstObject.GetType()}]!");
            }
		}
		
	}
}