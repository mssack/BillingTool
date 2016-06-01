// Copyright (c) 2014, 2015 All Right Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2015-07-24</date>

using System;
using CsWpfBase.Global;






namespace CsWpfBase.Online.packets.v1.client.hardwareinfo
{
	/// <summary>wrapper</summary>
	[Serializable]
	public sealed class CsopClientHwScreenInfo : CsoPacket
	{
		private double _totalHeight;
		private double _totalWidth;

		/// <summary>ctor</summary>
		public CsopClientHwScreenInfo()
		{
		}

		/// <summary>ctor</summary>
		public CsopClientHwScreenInfo(bool defaultData)
		{
			if (!defaultData)
				return;

			TotalWidth = CsGlobal.Computer.Screen.TotalWidth;
			TotalHeight = CsGlobal.Computer.Screen.TotalHeight;
		}


		#region Overrides/Interfaces
		/// <summary>Gets the type code which is used to parse an unknown packet into a specific .Net type.</summary>
		public override Types PacketType
		{
			get { return Types.ClientHwScreenInfo; }
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
			TotalWidth = reader.Double();
			TotalHeight = reader.Double();
		}

		/// <summary>converts this object into binary and writes the content to the Writer.</summary>
		internal override void Write(Writer writer)
		{
			writer.Double(TotalWidth);
			writer.Double(TotalHeight);
		}
		#endregion


		/// <summary>Gets the total screen width.</summary>
		public double TotalWidth
		{
			get { return _totalWidth; }
			set { SetProperty(ref _totalWidth, value); }
		}
		/// <summary>Gets the total screen height.</summary>
		public double TotalHeight
		{
			get { return _totalHeight; }
			set { SetProperty(ref _totalHeight, value); }
		}
	}
}