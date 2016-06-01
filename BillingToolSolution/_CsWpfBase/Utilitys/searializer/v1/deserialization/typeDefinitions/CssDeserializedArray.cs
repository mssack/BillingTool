// Copyright (c) 2014, 2015 All Right Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-01-01</date>

using System;






namespace CsWpfBase.Utilitys.searializer.v1.deserialization.typeDefinitions
{
	internal sealed class CssDeserializedArray : CssDeserializedClass
	{

		public CssDeserializedArray(ushort id) : base(id)
		{
		}


		#region Overrides/Interfaces
		public override void SetAqn(string aqn)
		{
			base.SetAqn(aqn);
			if (IsKnownType)
			{
				ElementType = Type;
				Type = ElementType.MakeArrayType();
			}
		}
		#endregion


		public Type ElementType { get; private set; }
	}
}