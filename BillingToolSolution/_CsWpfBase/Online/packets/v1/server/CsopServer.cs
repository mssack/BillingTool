// Copyright (c) 2014, 2015 All Right Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2015-07-24</date>

using System;
using System.Collections.Generic;
using System.Linq;






namespace CsWpfBase.Online.packets.v1.server
{
	/// <summary>The default server response.</summary>
	[Serializable]
	public class CsopServer : CsoPacket
	{
		private List<CsoPacket> _innerPackets;
		private DateTime _serverTime;

		/// <summary>Creates a new server response packet.</summary>
		public CsopServer()
		{
			ServerTime = DateTime.Now;
		}


		#region Overrides/Interfaces
		/// <summary>Gets the type code which is used to parse an unknown packet into a specific .Net type.</summary>
		public override Types PacketType
		{
			get { return  Types.ServerBase; }
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
			reader.DateTime();
			InnerPackets = reader.ListOfPackets();
		}

		/// <summary>converts this object into binary and writes the content to the Writer.</summary>
		internal override void Write(Writer writer)
		{
			writer.DateTime(ServerTime);
			writer.ListOfPackets(InnerPackets);
		}
		#endregion


		/// <summary>Gets the current server time.</summary>
		public DateTime ServerTime
		{
			get { return _serverTime; }
			set { SetProperty(ref _serverTime, value); }
		}
		/// <summary>Gets the inner packets of the server response.</summary>
		public List<CsoPacket> InnerPackets
		{
			get { return _innerPackets ?? (_innerPackets = new List<CsoPacket>()); }
			set { SetProperty(ref _innerPackets, value); }
		}

		/// <summary>Get a specific packet Type if exist.</summary>
		public TPacketType GetInnerPacket<TPacketType>() where TPacketType : CsoPacket
		{
			return InnerPackets.FirstOrDefault(x => x is TPacketType) as TPacketType;
		}
	}
}