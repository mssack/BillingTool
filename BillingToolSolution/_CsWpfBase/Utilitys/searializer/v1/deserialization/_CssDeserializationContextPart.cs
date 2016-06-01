using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using CsWpfBase.Ev.Objects;






namespace CsWpfBase.Utilitys.searializer.v1.deserialization
{
	internal class CssDeserializationContextPart : Base
	{
		public CssDeserializationContextPart(CssDeserializationContext context)
		{
			Context = context;
		}

		public CssDeserializationContext Context { get; }
		public BinaryReader Rd => Context.Rd;
	}
}
