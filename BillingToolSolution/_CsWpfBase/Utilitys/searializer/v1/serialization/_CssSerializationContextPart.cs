// Copyright (c) 2014, 2015 All Right Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2015-12-31</date>

using System;
using System.IO;
using CsWpfBase.Ev.Objects;






namespace CsWpfBase.Utilitys.searializer.v1.serialization
{
	/// <summary>A part of the <see cref="CssSerializationContext" />.</summary>
	internal abstract class CssSerializationContextPart : Base
	{
		protected CssSerializationContextPart(CssSerializationContext context)
		{
			Context = context;
			Ms = new MemoryStream();
			Wr = new BinaryWriter(Ms, Context.Encoding);
		}

		public MemoryStream Ms { get; }
		protected CssSerializationContext Context { get; }
		protected BinaryWriter Wr { get; }


		/// <summary>
		///     All references to this object are freed by setting <see cref="Base.PropertyChanged" /> to null. In inherited classes override this method to free
		///     all other events.
		/// </summary>
		public override void Dispose()
		{
			base.Dispose();
			Wr.Close();
			Ms.Close();
			Ms.Dispose();
		}
	}
}