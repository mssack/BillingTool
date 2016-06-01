// Copyright (c) 2014, 2015 All Right Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2015-07-24</date>

using System;
using CsWpfBase.Ev.Objects;
using CsWpfBase.Online.packets;
using CsWpfBase.Online.packets.v1.server;






namespace CsWpfBase.Online.response
{
	/// <summary>Internal see at <see cref="CsOnline" />.</summary>
	[Serializable]
	public class CsoResponseProcess : Base
	{
		private static CsoResponseProcess _instance;
		private static readonly object SingletonLock = new object();
		/// <summary>Returns the singleton instance</summary>
		internal static CsoResponseProcess I
		{
			get
			{
				if (_instance != null)
					return _instance; //Advanced first check to improve performance (no lock needed).
				lock (SingletonLock)
				{
					return _instance ?? (_instance = new CsoResponseProcess());
				}
			}
		}

		private CsoResponseProcess()
		{
		}

		/// <summary>Handles all response packets and executes them.</summary>
		public void Complete(CsoPacket packet)
		{
			if (!(packet is CsopServer))
				return;
			var responsePacket = packet as CsopServer;
			foreach (var innerPacket in responsePacket.InnerPackets)
			{
				if (innerPacket.PacketType == CsoPacket.Types.ServerMessage)
					Handle(innerPacket as CsopServerMessage);
				else if (innerPacket.PacketType == CsoPacket.Types.ServerUpdateAvailable)
					Handle(innerPacket as CsopServerUpdateAvailable);

			}
		}

		private void Handle(CsopServerUpdateAvailable packet)
		{
			if (packet == null)
				return;
			packet.Execute();
		}

		private void Handle(CsopServerMessage packet)
		{
			if (packet == null)
				return;
			packet.Execute();
		}
	}
}