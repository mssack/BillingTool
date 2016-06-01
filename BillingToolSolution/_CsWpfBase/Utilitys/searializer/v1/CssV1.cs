// Copyright (c) 2014, 2015 All Right Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-01-01</date>

using System;
using System.Collections.Generic;
using CsWpfBase.Ev.Public.Extensions;
using CsWpfBase.Utilitys.searializer.v1.reflection;






namespace CsWpfBase.Utilitys.searializer.v1
{
	internal static class CssV1
	{

		private static Dictionary<Type, CssTypeDefinitionClass> _cache;
		private static Dictionary<Type, CssTypeDefinitionClass> Cache => _cache ?? (_cache = new Dictionary<Type, CssTypeDefinitionClass>());

		/// <summary>Gets a cached well known type definition</summary>
		public static CssTypeDefinitionClass GetReflectedDefinition(Type t)
		{
			CssTypeDefinitionClass def;
			if (Cache.TryGetValue(t, out def))
				return def;
			def = CssTypeDefinitionClass.Create(t);
			Cache.Add(def.Type, def);
			return def;
		}


		public static bool IsPrimitive(Type t)
		{
			return t.IsSpecialPrimitive();
		}

		public static CssTypeDefinitionPrimitive GetPrimitiveDefinition(Type t)
		{
			return CssTypeDefinitionPrimitive.Instances[t];
		}

		public static CssTypeDefinitionPrimitive GetPrimitiveDefinition(byte id)
		{
			return CssTypeDefinitionPrimitive.InstancesById[id];
		}



		public enum DataOpCodes : byte
		{
			Primitive = 0,
			Reference = 1,
			Object = 2,
			Null = 5,
		}



		public enum DefinitionOpCodes : byte
		{
			Primitive = 1,
			Reference = 2,
			Type = 3,
			Dictionary = 4,
			List = 5,
			Array = 6,
			Class = 7,
		}
	}
}