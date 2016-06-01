// Copyright (c) 2014, 2015 All Right Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2015-12-31</date>

using System;






namespace CsWpfBase.Utilitys.searializer.v1.serialization
{
	internal class CssSerializationHeader : CssSerializationContextPart
	{
		private static readonly ushort Version = 1;
		private static readonly string FileType = "CS.Serialize";
		private bool _hasBeenSerialized;

		public CssSerializationHeader(CssSerializationContext context) : base(context)
		{
		}

		public void Serialize()
		{
			if (_hasBeenSerialized)
				return;
			_hasBeenSerialized = true;

			Wr.Write(FileType);
			Wr.Write(Version);
			Wr.Write(DateTime.Now.Ticks);
			Wr.Write(Environment.MachineName);
			Wr.Write(Environment.UserName);
			Wr.Write(Context.Definition.Ms.Length);
		}
	}
}