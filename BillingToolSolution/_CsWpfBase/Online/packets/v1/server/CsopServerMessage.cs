// Copyright (c) 2014, 2015 All Right Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2015-07-24</date>

using System;
using CsWpfBase.Global;
using CsWpfBase.Global.message;






namespace CsWpfBase.Online.packets.v1.server
{
	/// <summary>Contains a message from the server which should be presented to the user.</summary>
	[Serializable]
	public class CsopServerMessage : CsoPacket
	{
		private CsMessage.Types _messageType;
		private string _text;
		private string _title;


		#region Overrides/Interfaces
		/// <summary>Gets the type code which is used to parse an unknown packet into a specific .Net type.</summary>
		public override Types PacketType
		{
			get { return Types.ServerMessage; }
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
			MessageType = (CsMessage.Types) reader.Byte();
			Title = reader.String();
			Text = reader.String();
		}

		/// <summary>converts this object into binary and writes the content to the Writer.</summary>
		internal override void Write(Writer writer)
		{
			writer.Byte((byte) MessageType);
			writer.String(Title);
			writer.String(Text);
		}
		#endregion


		/// <summary>Gets or sets the MessageType.</summary>
		public CsMessage.Types MessageType
		{
			get { return _messageType; }
			set { SetProperty(ref _messageType, value); }
		}
		/// <summary>Gets or sets the Title.</summary>
		public string Title
		{
			get { return _title; }
			set { SetProperty(ref _title, value); }
		}
		/// <summary>Gets or sets the Text.</summary>
		public string Text
		{
			get { return _text; }
			set { SetProperty(ref _text, value); }
		}

		/// <summary>Executes the packet</summary>
		public void Execute()
		{
			CsGlobal.Message.Push(this, MessageType, Title);
		}
	}
}