// Copyright (c) 2014, 2015 All Right Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2015-07-24</date>

using System;
using System.Collections.Generic;
using System.Linq;
using CsWpfBase.Global;






namespace CsWpfBase.Online.packets.v1.client
{
	/// <summary>The default client request packet.</summary>
	[Serializable]
	public class CsopClient : CsoPacket
	{
		private Guid _applicationId;
		private string _applicationName;
		private string _applicationVersion;
		private Guid _computerId;
		private List<CsoPacket> _innerPackets;
		private Guid _instanceId;
		private DateTime _sendTime;
		private string _userSid;

		/// <summary>default constructor</summary>
		public CsopClient() : this(false)
		{
		}

		/// <summary>Creates an empty header if not true.</summary>
		public CsopClient(bool defaultData)
		{
			if (!defaultData)
				return;


			SendTime = DateTime.Now;
			ComputerId = CsGlobal.Computer.Id;
			UserSid = CsGlobal.Os.CurrentUser.Sid;
			ApplicationId = CsGlobal.App.Info.Id;
			ApplicationName = CsGlobal.App.Info.Name;
			ApplicationVersion = CsGlobal.App.Info.Version;

			InstanceId = CsGlobal.IsInstalled(GlobalFunctions.AppData) ? CsGlobal.App.Data.InstanceId : Guid.Empty;
		}


		#region Overrides/Interfaces
		/// <summary>Gets the type code which is used to parse an unknown packet into a specific .Net type.</summary>
		public override Types PacketType
		{
			get { return Types.ClientBase; }
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
			SendTime = reader.DateTime();
			ComputerId = reader.Guid();
			UserSid = reader.String();
			ApplicationId = reader.Guid();
			ApplicationName = reader.String();
			ApplicationVersion = reader.String();
			InstanceId = reader.Guid();

			InnerPackets = reader.ListOfPackets();
		}

		/// <summary>converts this object into binary and writes the content to the Writer.</summary>
		internal override void Write(Writer writer)
		{
			writer.DateTime(SendTime);
			writer.Guid(ComputerId);
			writer.String(UserSid);
			writer.Guid(ApplicationId);
			writer.String(ApplicationName);
			writer.String(ApplicationVersion);
			writer.Guid(InstanceId);

			writer.ListOfPackets(InnerPackets);
		}
		#endregion


		/// <summary>returns the send time of the request packet.</summary>
		public DateTime SendTime
		{
			get { return _sendTime; }
			set { SetProperty(ref _sendTime, value); }
		}
		/// <summary>returns the hosts computer id.</summary>
		public Guid ComputerId
		{
			get { return _computerId; }
			set { SetProperty(ref _computerId, value); }
		}
		/// <summary>returns the current user id.</summary>
		public string UserSid
		{
			get { return _userSid; }
			set { SetProperty(ref _userSid, value); }
		}
		/// <summary>returns the application id.</summary>
		public Guid ApplicationId
		{
			get { return _applicationId; }
			set { SetProperty(ref _applicationId, value); }
		}
		/// <summary>returns the hosts programm name.</summary>
		public string ApplicationName
		{
			get { return _applicationName; }
			set { SetProperty(ref _applicationName, value); }
		}
		/// <summary>returns the hosts application version.</summary>
		public string ApplicationVersion
		{
			get { return _applicationVersion; }
			set { SetProperty(ref _applicationVersion, value); }
		}
		/// <summary>returns the hosts application id.</summary>
		public Guid InstanceId
		{
			get { return _instanceId; }
			set { SetProperty(ref _instanceId, value); }
		}
		/// <summary>The inner packets of the packet.</summary>
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