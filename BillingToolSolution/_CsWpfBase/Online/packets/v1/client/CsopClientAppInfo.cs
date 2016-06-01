// Copyright (c) 2014, 2015 All Right Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2015-07-24</date>

using System;
using CsWpfBase.Global;






namespace CsWpfBase.Online.packets.v1.client
{
	/// <summary>Contains informations about the client.</summary>
	public class CsopClientAppInfo : CsoPacket
	{
		private UInt32 _startupCount;
		private TimeSpan _usageTime;

		/// <summary>ctor</summary>
		public CsopClientAppInfo()
		{
		}

		/// <summary>ctor</summary>
		public CsopClientAppInfo(bool defaultData)
		{
			if (!defaultData)
				return;

			StartupCount = CsGlobal.App.Data.StartupCount;
			UsageTime = CsGlobal.App.Data.UseageTime;
		}


		#region Overrides/Interfaces
		/// <summary>Gets the type code which is used to parse an unknown packet into a specific .Net type.</summary>
		public override Types PacketType
		{
			get { return  Types.ClientAppInfo; }
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
			StartupCount = reader.UInt32();
			UsageTime = reader.TimeSpan();
		}

		/// <summary>converts this object into binary and writes the content to the Writer.</summary>
		internal override void Write(Writer writer)
		{
			writer.UInt32(StartupCount);
			writer.TimeSpan(UsageTime);
		}
		#endregion


		/// <summary>returns the startup count.</summary>
		public UInt32 StartupCount
		{
			get { return _startupCount; }
			set { SetProperty(ref _startupCount, value); }
		}
		/// <summary>returns the usage time.</summary>
		public TimeSpan UsageTime
		{
			get { return _usageTime; }
			set { SetProperty(ref _usageTime, value); }
		}
	}
}