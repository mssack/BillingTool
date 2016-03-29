// Copyright (c) 2014, 2015 All Right Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2015-07-24</date>

using System;






namespace CsWpfBase.Online.packets.v1.client
{
	/// <summary>A packet for remotly send a feedback to the server</summary>
	public class CsopClientFeedback : CsoPacket
	{
		private string _text;
		private byte _rating;
		private string _senderMail;
		private string _senderName;
		private string _title;


		#region Overrides/Interfaces
		/// <summary>Gets the type code which is used to parse an unknown packet into a specific .Net type.</summary>
		public override Types PacketType
		{
			get { return  Types.ClientFeedback; }
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
			Title = reader.String();
			SenderMail = reader.String();
			SenderName = reader.String();
			Text = reader.String();
			Rating = reader.Byte();
		}

		/// <summary>converts this object into binary and writes the content to the Writer.</summary>
		internal override void Write(Writer writer)
		{
			writer.String(Title);
			writer.String(SenderMail);
			writer.String(SenderName);
			writer.String(Text);
			writer.Byte(Rating);
		}
		#endregion


		/// <summary> [INotify] Gets or sets Title.</summary>
		public string Title
		{
			get { return _title; }
			set { SetProperty(ref _title, value); }
		}
		/// <summary> [INotify] Gets or sets SenderMail.</summary>
		public string SenderMail
		{
			get { return _senderMail; }
			set { SetProperty(ref _senderMail, value); }
		}
		/// <summary> [INotify] Gets or sets SenderName.</summary>
		public string SenderName
		{
			get { return _senderName; }
			set { SetProperty(ref _senderName, value); }
		}
		/// <summary> [INotify] Gets or sets Content.</summary>
		public string Text
		{
			get { return _text; }
			set { SetProperty(ref _text, value); }
		}
		/// <summary> The program Rating.</summary>
		public byte Rating
		{
			get { return _rating; }
			set { SetProperty(ref _rating, value); }
		}
	}
}