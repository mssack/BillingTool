// Copyright (c) 2014, 2015 All Right Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2015-07-24</date>

using System;
using CsWpfBase.Online.key;
using CsWpfBase.Online.parser;
using CsWpfBase.Online.response;
using CsWpfBase.Online.send;






namespace CsWpfBase.Online
{
	/// <summary>The online communication basis for the online server.</summary>
	public static class CsOnline
	{
		/// <summary>Asymmetric key management.</summary>
		public static CsoKey Key
		{
			get { return CsoKey.I; }
		}
		/// <summary>Packet parse helper.</summary>
		public static CsoParser Parser
		{
			get { return CsoParser.I; }
		}
		/// <summary>Helpers for packet send.</summary>
		public static CsoSend Send
		{
			get { return CsoSend.I; }
		}
		/// <summary>Helpers for async packet send.</summary>
		public static CsoSendAsync SendAsync
		{
			get { return CsoSendAsync.I; }
		}
		/// <summary>Response handling.</summary>
		public static CsoResponse Response
		{
			get { return CsoResponse.I; }
		}



	}
}