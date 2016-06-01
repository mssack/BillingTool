// Copyright (c) 2014, 2015 All Right Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-01-01</date>

using System;
using System.Linq;
using System.Reflection;
using CsWpfBase.Ev.Public.Extensions;






namespace CsWpfBase.Utilitys.codingTestUtils.data
{
	/// <summary>See at <see cref="CsTest" />.</summary>
	public sealed class CsTestDataGenerator
	{
		private static readonly Random Rand = new Random((int) DateTime.Now.Ticks);
		private static CsTestDataGenerator _instance;
		private static readonly object SingletonLock = new object();

		/// <summary>Returns the singleton instance</summary>
		internal static CsTestDataGenerator I
		{
			get
			{
				if (_instance != null)
					return _instance; //Advanced first check to improve performance (no lock needed).
				lock (SingletonLock)
				{
					return _instance ?? (_instance = new CsTestDataGenerator());
				}
			}
		}

		private CsTestDataGenerator()
		{
		}


		/// <summary>Generates random primitive data for all fields found in type.</summary>
		public void PrimitivesData_Into(object target)
		{
			var fieldInfos = target.GetType().GetFields_IncludingBaseClasses(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic).ToArray();

			foreach (var field in fieldInfos)
			{
				if (field.FieldType.IsPrimitive || Nullable.GetUnderlyingType(field.FieldType) != null || field.FieldType == typeof (string) || field.FieldType == typeof(decimal))
				{
					field.SetValue(target, Rand.Next_By_Type(field.FieldType));
				}
			}
		}
		/// <summary>Generates random primitive array data for all fields of type primitive[] found in type.</summary>
		public void PrimitiveArrayData_Into(object target)
		{
			var fieldInfos = target.GetType().GetFields_IncludingBaseClasses(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic).ToArray();

			foreach (var field in fieldInfos)
			{
				if (field.FieldType.IsArray && field.FieldType.GetElementType().IsPrimitive)
				{
					var elementType = field.FieldType.GetElementType();
					var array = (Array)Activator.CreateInstance(field.FieldType, Rand.Next(0, 30));

					for (int i = 0; i < array.Length; i++)
					{
						array.SetValue(Rand.Next_By_Type(elementType), i);
					}

					field.SetValue(target, array);
				}
			}
		}
	}
}