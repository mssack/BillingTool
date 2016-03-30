// Copyright (c) 2014, 2015 All Right Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2015-12-31</date>

using System;






namespace CsWpfBase.Utilitys.searializer.v1.deserialization
{
	internal class CssDeserializationHeader : CssDeserializationContextPart
	{

		public CssDeserializationHeader(CssDeserializationContext context) : base(context)
		{
		}


		public string FileType { get; private set; }
		public ushort Version { get; private set; }
		public DateTime CreationDate { get; private set; }
		public string ComputerName { get; private set; }
		public string UserName { get; private set; }
		public long DefinitionLength { get; private set; }

		public void Deserialize()
		{
			FileType = Rd.ReadString();
			Version = Rd.ReadUInt16();
			CreationDate = new DateTime(Rd.ReadInt64());
			ComputerName = Rd.ReadString();
			UserName = Rd.ReadString();
			DefinitionLength = Rd.ReadInt64();
		}
	}
}