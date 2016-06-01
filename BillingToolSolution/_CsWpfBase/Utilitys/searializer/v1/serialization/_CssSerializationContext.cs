// Copyright (c) 2014, 2015 All Right Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-01-01</date>

using System;
using System.IO;
using System.Text;
using CsWpfBase.Ev.Objects;






namespace CsWpfBase.Utilitys.searializer.v1.serialization
{
	/// <summary>The version 1 serialization context.</summary>
	internal class CssSerializationContext : Base
	{


		public CssSerializationContext(object data, Encoding encoding = null)
		{
			Data = data;
			Encoding = encoding ?? Encoding.Unicode;
			Header = new CssSerializationHeader(this);
			Definition = new CssSerializationDefinition(this);
			Body = new CssSerializationBody(this);
		}

		public object Data { get; }

		public CssSerializationHeader Header { get; }
		public CssSerializationDefinition Definition { get; set; }
		public CssSerializationBody Body { get; set; }
		public Encoding Encoding { get; }


		public byte[] Serialize()
		{

			Body.Serialize();
			Definition.ActualizeTypeCount();
			Header.Serialize();

			var ms = new MemoryStream((int) (Header.Ms.Length + Definition.Ms.Length + Body.Ms.Length));
			Header.Ms.WriteTo(ms);
			Definition.Ms.WriteTo(ms);
			Body.Ms.WriteTo(ms);

			return ms.ToArray();
		}
	}


}