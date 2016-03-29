// Copyright (c) 2014, 2015 All Right Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2015-07-24</date>

using System;
using System.IO;
using System.Net;
using System.Net.Cache;
using CsWpfBase.Ev;
using CsWpfBase.Ev.Objects;
using CsWpfBase.Global;
using CsWpfBase.Online.packets;
using CsWpfBase.Online.packets.v1;
using CsWpfBase.Online.packets.v1.client;
using CsWpfBase.Online.packets.v1.client.hardwareinfo;
using CsWpfBase.Online.packets.v1.client.osparts;






namespace CsWpfBase.Online.send
{
	/// <summary>Internal see at <see cref="CsOnline" />.</summary>
	[Serializable]
	public class CsoSend : Base
	{
		private static CsoSend _instance;
		private static readonly object SingletonLock = new object();
		/// <summary>Returns the singleton instance</summary>
		internal static CsoSend I
		{
			get
			{
				if (_instance != null)
					return _instance; //Advanced first check to improve performance (no lock needed).
				lock (SingletonLock)
				{
					return _instance ?? (_instance = new CsoSend());
				}
			}
		}

		private CsoSend()
		{
		}

		/// <summary>Sends an empty request. checks for updates.</summary>
		public CsoPacket Ping()
		{
			var packet = new CsopClient(true);
			return Send(packet);
		}

		/// <summary>Sends an request with client informations.</summary>
		public CsoPacket AppInfo()
		{
			var packet = new CsopClient(true);
			packet.InnerPackets.Add(new CsopClientAppInfo(true));
			return Send(packet);
		}

		/// <summary>Sends an request with the complete computer informations.</summary>
		public CsoPacket CompleteInfo()
		{
			var packet = new CsopClient(true);

			if (CsGlobal.IsInstalled(GlobalFunctions.AppData))
				packet.InnerPackets.Add(new CsopClientAppInfo(true));

			packet.InnerPackets.Add(new CsopClientOsInfo(true));

			packet.InnerPackets.Add(new CsopClientHwComputerSystemInfo(true));
			packet.InnerPackets.Add(new CsopClientHwMainBoardInfo(true));
			packet.InnerPackets.Add(new CsopClientHwMemoryInfo(true));
			packet.InnerPackets.Add(new CsopClientHwProcessorInfo(true));
			packet.InnerPackets.Add(new CsopClientHwGraphicInfo(true));
			packet.InnerPackets.Add(new CsopClientHwScreenInfo(true));
			packet.InnerPackets.Add(new CsopClientHwDiskDriveInfo(true));
			packet.InnerPackets.Add(new CsopClientHwBiosInfo(true));
			packet.InnerPackets.Add(new CsopClientHwNetworkInfo(true));

			return Send(packet);
		}

		/// <summary>Sends an exception to server which logs this exception</summary>
		public CsoPacket Exception(Exception exception)
		{
			var packet = new CsopClient(true);
			packet.InnerPackets.Add(CsopClientException.From(exception));
			return Send(packet);
		}

		/// <summary>Sends an feedback.</summary>
		public CsoPacket Feedback(CsopClientFeedback feedback)
		{
			var packet = new CsopClient(true);
			packet.InnerPackets.Add(feedback);
			return Send(packet);
		}

		private CsoPacket Send(CsoPacket packet, bool encrypted = true)
		{
			CsopCrypto.Session session = null;
			if (encrypted)
			{
				session = new CsopCrypto.Session(CsOnline.Key.V1, true);
				packet = session.EncryptPacket(packet, 1);
				
			}


			var requestData = packet.GetData();
			var request = GetRequest();
			request.Timeout = 30000;

			using (var stream = request.GetRequestStream())
			{
				stream.Write(requestData, 0, requestData.Length);
			}



			byte[] responseData;
			var webResponse = request.GetResponse();

			responseData = new byte[webResponse.ContentLength];
			var pos = 0;
			using (var stream = webResponse.GetResponseStream())
			{
				while (pos < responseData.Length)
				{
					var bytesRead = stream.Read(responseData, pos, responseData.Length - pos);
					if (bytesRead == 0)
					{
						// End of data and we didn't finish reading. Oops.
						throw new IOException("Premature end of data");
					}
					pos += bytesRead;
				}
			}




			if (encrypted)
				packet = CsOnline.Parser.ParseCryptedPacket(new CsoPacket.Reader(responseData, 0), session);
			else
				packet = CsOnline.Parser.ParsePacket(responseData, 0);

			return packet;
		}

		private HttpWebRequest GetRequest()
		{
#if DEBUG
			var request = (HttpWebRequest) WebRequest.Create(CsWpfBaseConfig.I.CsOnlineDebugServer);
#else
			var request = (HttpWebRequest)WebRequest.Create(CsWpfBaseConfig.I.CsOnlineServer);
#endif
			
			request.Method = "POST";
			request.CachePolicy = new HttpRequestCachePolicy(HttpRequestCacheLevel.NoCacheNoStore);
			request.KeepAlive = false;
			request.UserAgent = null;
			request.Referer = null;
			return request;
		}
	}
}