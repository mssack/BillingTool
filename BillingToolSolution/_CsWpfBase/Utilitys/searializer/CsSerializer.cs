// Copyright (c) 2014, 2015 All Right Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2015-12-21</date>

using System;
using System.IO;
using System.Text;
using CsWpfBase.Utilitys.searializer.v1.deserialization;
using CssSerializationContext = CsWpfBase.Utilitys.searializer.v1.serialization.CssSerializationContext;






namespace CsWpfBase.Utilitys.searializer
{

	/// <summary>Used to serialize data, this serializer helps with version controlling.</summary>
	public static class CsSerializer
	{
		/// <summary>Serializes the object to binary.</summary>
		public static byte[] Serialize(object o)
		{
			var sc = new CssSerializationContext(o, Encoding.Unicode);
			return sc.Serialize();
		}
		/// <summary>Serializes the object to binary.</summary>
		public static object Deserialize(byte[] o)
		{
			var sc = new CssDeserializationContext(new MemoryStream(o), Encoding.Unicode);
			return sc.Deserialize();
		}
	}
}