// Copyright (c) 2014, 2015 All Right Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2015-07-24</date>

using System;
using CsWpfBase.Ev.Public.Extensions;






namespace CsWpfBase.Online.packets.v1.client
{
	/// <summary>Contains information about a exception happened at the client side.</summary>
	public class CsopClientException : CsoPacket
	{
		private DateTime _date;
		private string _exceptionType;
		private string _message;
		private string _originalException;
		private string _source;
		private string _stackTrace;
		private string _targetSite;


		#region Overrides/Interfaces
		/// <summary>Gets the type code which is used to parse an unknown packet into a specific .Net type.</summary>
		public override Types PacketType
		{
			get { return Types.ClientException; }
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
			Date = reader.DateTime();
			ExceptionType = reader.String();
			StackTrace = reader.String();
			Message = reader.String();
			Source = reader.String();
			TargetSite = reader.String();
			OriginalException = reader.String();
		}

		/// <summary>converts this object into binary and writes the content to the Writer.</summary>
		internal override void Write(Writer writer)
		{
			writer.DateTime(Date);
			writer.String(ExceptionType);
			writer.String(StackTrace);
			writer.String(Message);
			writer.String(Source);
			writer.String(TargetSite);
			writer.String(OriginalException);
		}
		#endregion


		/// <summary>Gets or sets the Date.</summary>
		public DateTime Date
		{
			get { return _date; }
			set { SetProperty(ref _date, value); }
		}
		/// <summary>Gets or sets the ExceptionType.</summary>
		public string ExceptionType
		{
			get { return _exceptionType; }
			set { SetProperty(ref _exceptionType, value); }
		}
		/// <summary>Gets or sets the StackTrace.</summary>
		public string StackTrace
		{
			get { return _stackTrace; }
			set { SetProperty(ref _stackTrace, value); }
		}
		/// <summary>Gets or sets the Message.</summary>
		public string Message
		{
			get { return _message; }
			set { SetProperty(ref _message, value); }
		}
		/// <summary>Gets or sets the Source.</summary>
		public string Source
		{
			get { return _source; }
			set { SetProperty(ref _source, value); }
		}
		/// <summary>Gets or sets the TargetSite.</summary>
		public string TargetSite
		{
			get { return _targetSite; }
			set { SetProperty(ref _targetSite, value); }
		}
		/// <summary>Gets or sets the OriginalException.</summary>
		public string OriginalException
		{
			get { return _originalException; }
			set { SetProperty(ref _originalException, value); }
		}

		/// <summary>Generates a packet form an exception. The exception can and should contain inner exceptions.</summary>
		public static CsopClientException From(Exception exception)
		{
			var rv = new CsopClientException();

			rv.OriginalException = exception.ToString();

			exception = exception.MostInner();

			rv.Date = DateTime.Now;
			rv.ExceptionType = exception.GetType().ToString();
			rv.StackTrace = exception.StackTrace;
			rv.Message = exception.Message;
			rv.Source = exception.Source;
			rv.TargetSite = (exception.TargetSite.DeclaringType == null ? "" : exception.TargetSite.DeclaringType.Name + " > ") + exception.TargetSite;
			return rv;
		}
	}
}