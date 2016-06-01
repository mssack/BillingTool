// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-05-06</date>

using System;
using System.Runtime.CompilerServices;
using CsWpfBase.Ev.Attributes;
using CsWpfBase.Ev.Objects;
using CsWpfBase.Ev.Public.Extensions;
using CsWpfBase.Global.debug;






namespace CsWpfBase.Global.message
{
	/// <summary>CsGlobal Message</summary>
	[Serializable]
	public sealed class CsMessage : Base
	{
		private CodePosition _code;
		private object _content;
		private string _iD;
		private MessageButtons _messageButton;
		private string _messageId;
		private MessageResults _result;
		private DateTime _time;
		private string _title;
		private Types _type;

		internal CsMessage(Types type, object content, string title, MessageButtons button, [CallerMemberName] string methodName = null, [CallerFilePath] string classFilePath = null, [CallerLineNumber] int classLineNumber = 0)
		{
			_time = DateTime.Now;
			_type = type;
			_content = content;
			_title = title;
			_messageButton = button;
			_code = new CodePosition(methodName, classFilePath, classLineNumber);
			_messageId = SmallHash.FromString(_content.ToString());
			_iD = SmallHash.FromString(new[] {Code.PositionId, MessageId}.Join(""));
		}


		#region Overrides/Interfaces
		/// <summary>Does not return the name of the type.</summary>
		public override string ToString()
		{
			return Id + " --> " + Type.ToString().Expand(12) + " (" + Time + " - " + Code.PositionId + ")" + " <CodePos: " + Code + ">: '" + Content + "'";
		}
		#endregion


		/// <summary>Depends on the location and the message.</summary>
		public string Id
		{
			get { return _iD; }
			private set { SetProperty(ref _iD, value); }
		}
		/// <summary>Depends on the message.</summary>
		public string MessageId
		{
			get { return _messageId; }
			private set { SetProperty(ref _messageId, value); }
		}
		/// <summary>Gets the type of the message.</summary>
		public Types Type
		{
			get { return _type; }
			private set { SetProperty(ref _type, value); }
		}
		/// <summary>Gets the occurrence time of the message.</summary>
		public DateTime Time
		{
			get { return _time; }
			private set { SetProperty(ref _time, value); }
		}
		/// <summary>Gets or sets the Title of the message.</summary>
		public string Title
		{
			get { return _title; }
			set { SetProperty(ref _title, value); }
		}
		/// <summary>Gets the content of the message.</summary>
		public object Content
		{
			get { return _content; }
			private set { SetProperty(ref _content, value); }
		}
		/// <summary>provides information of the code position where the message was fired.</summary>
		public CodePosition Code
		{
			get { return _code; }
			private set { SetProperty(ref _code, value); }
		}
		/// <summary>The message buttons to present, when presenting to the user.</summary>
		public MessageButtons MessageButton
		{
			get { return _messageButton; }
			private set { SetProperty(ref _messageButton, value); }
		}
		/// <summary>The result after the message was presented to the user.</summary>
		public MessageResults Result
		{
			get { return _result; }
			internal set { SetProperty(ref _result, value); }
		}



		/// <summary>Depending on the <see cref="MessageButtons" /> different buttons will be presented to the user.</summary>
		[Serializable]
		public enum MessageButtons //TODO include comments
		{
			/// <summary></summary>
			Undefined,
			/// <summary></summary>
			YesNo,
			/// <summary></summary>
			YesNoCancel,
			/// <summary></summary>
			Ok,
			/// <summary></summary>
			OkCancel,
			/// <summary></summary>
			NoButtons,
		}



		/// <summary>The result of a message which result can be chosen in the message box is defined by the
		///     <see cref="MessageButtons" /> enum.</summary>
		[Serializable]
		public enum MessageResults //TODO include comments
		{
			/// <summary></summary>
			Undefined,
			/// <summary></summary>
			Yes,
			/// <summary></summary>
			No,
			/// <summary></summary>
			Cancel,
			/// <summary></summary>
			Ok
		}



		/// <summary>Log types</summary>
		[Serializable]
		public enum Types : byte
		{
			/// <summary>Undefined.</summary>
			[EnumDescription("Undefined")] Undefined = 0,
			/// <summary>Use this to populate an information which is not necessarily needed.</summary>
			[EnumDescription("Information")] Information = 1,
			/// <summary>Use this type when something unexpected could happen.</summary>
			[EnumDescription("Warning")] Warning = 2,
			/// <summary>Use this when an error happened which is handle able.</summary>
			[EnumDescription("Error")] Error = 3,
			/// <summary>Use this when an error happened which is not handle able. The application must be closed.</summary>
			[EnumDescription("Fatal error")] FatalError = 4
		}
	}
}