// Copyright (c) 2014, 2015 All Right Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2015-07-24</date>

using System;






namespace CsWpfBase.Online.packets
{
	/// <summary>A packet where the type is unknown and so the binary couldn't be resolved.</summary>
	[Serializable]
	public class UnknownPacket : CsoPacket
	{
		private byte[] _data;
		private uint _typeIdentifier;

		/// <summary>Creates a packet where even the type is unknown.</summary>
		public UnknownPacket()
		{
			TypeIdentifier = (uint) Types.Unknown;
		}

		/// <summary>Creates a packet where the type identifier couldn't be resolved.</summary>
		public UnknownPacket(uint type)
		{
			TypeIdentifier = type;
		}


		#region Overrides/Interfaces
		/// <summary>Gets the type code which is used to parse an unknown packet into a specific .Net type.</summary>
		public override Types PacketType
		{
			get { return Types.Unknown; }
		}
		/// <summary>
		///     The initial Value of the <see cref="CsoPacket.PacketVersion" />, this value will be applied to the <see cref="CsoPacket.PacketVersion" />
		///     property whenever the packet is created or no version is defined.
		/// </summary>
		protected override uint InitialVersion
		{
			get { return 1; }
		}

		/// <summary>Interprets a binary into this object.</summary>
		internal override void Parse(Reader reader, int length)
		{
			Data = reader.Bytes(length);
		}

		/// <summary>converts this object into binary and writes the content to the Writer.</summary>
		internal override void Write(Writer writer)
		{
			throw new NotSupportedException("An unknown Packet can not be written.");
		}
		#endregion


		/// <summary>Gets or sets the TypeIdentifier.</summary>
		public uint TypeIdentifier
		{
			get { return _typeIdentifier; }
			set { SetProperty(ref _typeIdentifier, value); }
		}
		/// <summary>the data portion of the unresolved packet.</summary>
		public byte[] Data
		{
			get { return _data; }
			set { SetProperty(ref _data, value); }
		}
	}
}