// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-04-03</date>

using System;






namespace BillingToolOutput.Interfaces
{
	/// <summary>The mail configurations needed for sending mails to receivers.</summary>
	public interface IContainMailConfiguration
	{
		/// <summary>Gets or sets the mail address from which the mail should be send.</summary>
		string SmtpMailAddress { get; }
		/// <summary>Gets or sets the SMTP server the mail should be send to.</summary>
		string SmtpServer { get; }
		/// <summary>Gets or sets the SMTP server port the mail should be send to.</summary>
		ushort SmtpPort { get; }
		/// <summary>Gets or sets the enable ssl mode.</summary>
		bool SmtpEnableSsl { get; }
		/// <summary>Gets or sets the UserName for the SMTP server.</summary>
		string SmtpUsername { get; }
		/// <summary>Gets or sets the Password for the SMTP server.</summary>
		string SmtpPassword { get; }
	}
}