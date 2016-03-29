// Copyright (c) 2014, 2015 All Right Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2015-06-11</date>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;





namespace CsWpfBase.Utilitys.ConfigEngine.Base
{
	internal static class ConfigExt
	{
		public static bool IsValueType(Type t)
		{
			return t.IsValueType || t == typeof (string);
		}





		public static class Reflection
		{
			private static readonly Dictionary<Type, Property[]> PropertyCache = new Dictionary<Type, Property[]>();
			private static readonly Dictionary<Type, Field[]> FieldCache = new Dictionary<Type, Field[]>();

			public static Property[] GetProperties(Type t)
			{
				if (PropertyCache.ContainsKey(t))
					return PropertyCache[t];

				var propertyInfos = t.GetProperties(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
				var properties = propertyInfos
					.Select(x => new Property {Info = x, Attribute = x.GetCustomAttributes(typeof (ConfigMemberAttribute), false).FirstOrDefault() as ConfigMemberAttribute})
					.Where(x => x.Attribute != null).ToArray();
				PropertyCache.Add(t, properties);
				return properties;
			}
			public static Field[] GetFields(Type t)
			{
				if (FieldCache.ContainsKey(t))
					return FieldCache[t];

				var fieldInfos = t.GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
				var fields = fieldInfos
					.Select(x => new Field {Info = x, Attribute = x.GetCustomAttributes(typeof (ConfigMemberAttribute), false).FirstOrDefault() as ConfigMemberAttribute})
					.Where(x => x.Attribute != null).ToArray();
				FieldCache.Add(t, fields);
				return fields;
			}
		}
	}





	internal abstract class ObjectMember
	{
		#region Abstracts
		public abstract string Name { get; }
		public abstract Type Type { get; }
		public abstract object GetValue(object target);
		public abstract void SetValue(object target, object value);
		#endregion


		public ConfigMemberAttribute Attribute { get; set; }
	}





	internal class Property : ObjectMember
	{
		#region Overrides
		public override string Name
		{
			get { return Info.Name; }
		}
		public override Type Type
		{
			get { return Info.PropertyType; }
		}
		public override object GetValue(object target)
		{
			return target == null ? null : Info.GetValue(target, null);
		}
		public override void SetValue(object target, object value)
		{
			if (target == null)
				return;
			Info.SetValue(target, value, null);
		}
		#endregion


		public PropertyInfo Info { get; set; }
	}





	internal class Field : ObjectMember
	{
		#region Overrides
		public override string Name
		{
			get { return Info.Name; }
		}
		public override Type Type
		{
			get { return Info.FieldType; }
		}
		public override object GetValue(object target)
		{
			return target == null ? null : Info.GetValue(target);
		}
		public override void SetValue(object target, object value)
		{
			if (target == null)
				return;
			Info.SetValue(target, value);
		}
		#endregion


		public FieldInfo Info { get; set; }
	}
}