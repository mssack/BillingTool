// Copyright (c) 2014, 2015 All Right Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2015-12-21</date>

using System;
using System.IO;
using System.Text;
using CsWpfBase.Ev.Objects;






namespace CsWpfBase.Utilitys.searializer.v1.deserialization
{
	internal class CssDeserializationContext : Base
	{
		public CssDeserializationContext(MemoryStream ms, Encoding encoding = null)
		{
			Encoding = encoding ?? Encoding.Unicode;


			Ms = ms;
			Rd = new BinaryReader(ms, Encoding);


			Header = new CssDeserializationHeader(this);
			Definition = new CssDeserializationDefinition(this);
			Body = new CssDeserializationBody(this);
		}


		#region Overrides/Interfaces
		/// <summary>
		///     All references to this object are freed by setting <see cref="Base.PropertyChanged" /> to null. In inherited classes override this method to free
		///     all other events.
		/// </summary>
		public override void Dispose()
		{
			Header.Dispose();
			Definition.Dispose();
			Body.Dispose();
			base.Dispose();
		}
		#endregion


		public Encoding Encoding { get; }
		public MemoryStream Ms { get; }
		public BinaryReader Rd { get; }

		public CssDeserializationHeader Header { get; }
		public CssDeserializationDefinition Definition { get; }
		public CssDeserializationBody Body { get; }


		public object Deserialize()
		{
			Header.Deserialize();
			Definition.Deserialize();
			return Body.Deserialize();
		}
	}
}