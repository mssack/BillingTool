// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-03-30</date>

using System;
using System.ComponentModel;
using CsWpfBase.Ev.Objects;






namespace BillingTool.btScope.configuration._interfaces
{
	/// <summary>Used as base for mail configuration settings.</summary>
	public interface IConfigMailSettings
	{
		#region Abstract
		/// <summary>see <see cref="Base.PropertyChanged" /> class for further information.</summary>
		event PropertyChangedEventHandler PropertyChanged;
		/// <summary>Gets or sets the mail address from which the mail should be send.</summary>
		string SenderMailAddress { get; set; }

		/// <summary>Gets or sets the SMTP server the mail should be send to.</summary>
		string SenderSmtpServer { get; set; }

		/// <summary>Gets or sets the SMTP server port the mail should be send to.</summary>
		ushort SenderSmtpPort { get; set; }

		/// <summary>Gets or sets the enable ssl mode.</summary>
		bool SenderSmtpEnableSsl { get; set; }

		/// <summary>Gets or sets the UserName for the SMTP server.</summary>
		string SenderSmtpUsername { get; set; }

		/// <summary>Gets or sets the Password for the SMTP server.</summary>
		string SenderSmtpPassword { get; set; }
		#endregion
	}
}