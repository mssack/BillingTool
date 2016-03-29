// Copyright (c) 2014, 2015 All Right Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2015-12-21</date>

using System;
using System.IO;






namespace CsWpfBase.Ev.Public.Extensions
{
	/// <summary>Wraps <see cref="BinaryWriter" /> extensions</summary>
	public static class BinaryWriterExtensions
	{

		/// <summary>Writes the length of the following block encapsuled by the using block to the stream.</summary>
		public static IDisposable Write_LengthOfFollowingBlock(this BinaryWriter wr)
		{
			return new LengthHelper(wr);
		}




		private class LengthHelper : IDisposable
		{
			public LengthHelper(BinaryWriter wr)
			{
				Wr = wr;
				LengthPos = wr.BaseStream.Position;

				Wr.Write(0L);
				DataStartingPos = Wr.BaseStream.Position;
			}


			#region Overrides/Interfaces
			public void Dispose()
			{
				var dataEndPos = Wr.BaseStream.Position;
				Wr.BaseStream.Position = LengthPos;
				Wr.Write(dataEndPos - DataStartingPos);
				Wr.BaseStream.Position = dataEndPos;
			}
			#endregion


			private BinaryWriter Wr { get; }
			private long LengthPos { get; }
			private long DataStartingPos { get; }
		}
	}
}