// Copyright (c) 2014, 2015 All Right Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2015-12-31</date>

using System;
using CsWpfBase.Utilitys.searializer.v1.reflection;






namespace CsWpfBase.Utilitys.searializer.v1.deserialization.typeDefinitions
{
	internal class CssDeserializedDictionary : CssDeserializedClass
	{

		public CssDeserializedDictionary(ushort id) : base(id)
		{
		}




		public CssTypeDefinition KeyType { get; private set; }
		public CssTypeDefinition ValueType { get; private set; }


		public void SetTypes(CssTypeDefinition keyType, CssTypeDefinition valueType)
		{
			KeyType = keyType;
			ValueType = valueType;
			if (IsKnownType && keyType.IsKnownType && valueType.IsKnownType)
			{
				Type = Type.MakeGenericType(keyType.Type, valueType.Type);
			}
			else
				Type = null;
		}
	}
}