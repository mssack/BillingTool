// Copyright (c) 2014, 2015 All Right Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2015-07-21</date>

using System;
using CsWpfBase.Ev.Objects;
using CsWpfBase.Online.packets;
using CsWpfBase.Online.packets.v1;






namespace CsWpfBase.Online.parser
{
	/// <summary>Internal see at <see cref="CsOnline" />.</summary>
	[Serializable]
	public class CsoParser : Base
	{
		#region SINGLETON CLASS
		private static CsoParser _instance;
		private static readonly object SingletonLock = new object();
		private CsoParser()
		{
		}
		/// <summary>Returns the singleton instance</summary>
		internal static CsoParser I
		{
			get
			{
				if (_instance != null)
					return _instance; //Advanced first check to improve performance (no lock needed).
				lock (SingletonLock)
				{
					return _instance ?? (_instance = new CsoParser());
				}
			}
		}
		#endregion


		/// <summary>Parses an unencrypted packet.</summary>
		public CsoPacket ParsePacket(byte[] data, int start)
		{
			return ParsePacket(new CsoPacket.Reader(data, start));
		}
		/// <summary>Parses an unencrypted packet.</summary>
		public CsoPacket ParsePacket(CsoPacket.Reader reader)
		{
			return reader.Packet();
		}
		/// <summary>Parse the content of an encrypted packet. if root packet is no encrypted packet the packet will be returned.</summary>
		public CsoPacket ParseCryptedPacket(byte[] data, int start, CsopCrypto.Session cryptoSession)
		{
			return ParseCryptedPacket(new CsoPacket.Reader(data, start), cryptoSession);
		}
		/// <summary>Parse the content of an encrypted packet. if root packet is no encrypted packet the packet will be returned.</summary>
		public CsoPacket ParseCryptedPacket(CsoPacket.Reader reader, CsopCrypto.Session cryptoSession)
		{
			CsoPacket packet = ParsePacket(reader);
			if (packet.PacketType !=  CsoPacket.Types.Crypted)
				return packet;

			var cryptoPacket = packet as CsopCrypto;

			return ParseCryptedPacket(cryptoPacket, cryptoSession);
		}
		/// <summary>Parse the content of an encrypted packet.</summary>
		public CsoPacket ParseCryptedPacket(CsopCrypto cryptoPacket, CsopCrypto.Session cryptoSession)
		{
			if (!cryptoSession.IsKeyLoaded)
				cryptoSession.GetKeys(cryptoPacket);

			return cryptoSession.DecryptPacket(cryptoPacket);
		}
	}
}