// Copyright (c) 2014, 2015 All Right Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2015-12-31</date>

using System;
using CsWpfBase.Utilitys.searializer.v1.reflection;






namespace CsWpfBase.Utilitys.searializer.v1.deserialization.typeDefinitions
{
	internal sealed class CssDeserializedList : CssDeserializedClass
	{

		public CssDeserializedList(ushort id) : base(id)
		{
		}


		
		public CssTypeDefinition ElementType { get; private set; }



		public void SetElementType(CssTypeDefinition elementType)
		{
			ElementType = elementType;
			if (IsKnownType && elementType.IsKnownType)
			{
				Type = Type.MakeGenericType(elementType.Type);
			}
			else
			{
				Type = null;
			}
		}
	}
}