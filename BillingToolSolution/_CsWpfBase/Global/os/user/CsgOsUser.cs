// Copyright (c) 2014, 2015 All Right Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2015-07-24</date>

using System;
using System.Management;
using CsWpfBase.Ev.Objects;
using CsWpfBase.Ev.Public.Extensions;






namespace CsWpfBase.Global.os.user
{
	/// <summary>User in the current OS</summary>
	[Serializable]
	public sealed class CsgOsUser : Base
	{
		private UInt32 _accountType;
		private string _description;
		private string _domain;
		private string _fullName;
		private bool _isLocalAccount;
		private string _name;
		private string _sid;
		private SidTypes _sidType;
		private string _status;

		internal CsgOsUser()
		{
		}

		/// <summary>
		///     Current status of an object. Various operational and nonoperational statuses can be defined. Operational statuses include: "OK", "Degraded", and
		///     "Pred Fail", which is an element such as a SMART-enabled hard disk drive that may be functioning properly, but predicts a failure in the near
		///     future. Nonoperational statuses include: "Error", "Starting", "Stopping", and "Service", which can apply during mirror resilvering of a disk,
		///     reloading a user permissions list, or other administrative work.
		/// </summary>
		public string Status
		{
			get { return _status; }
			set { SetProperty(ref _status, value); }
		}
		/// <summary>If true, the account is defined on the local computer.</summary>
		public bool IsLocalAccount
		{
			get { return _isLocalAccount; }
			set { SetProperty(ref _isLocalAccount, value); }
		}
		/// <summary>Name of the Windows user account on the domain that the Domain property of this class specifies. see environment username</summary>
		/// <example>danwilson</example>
		public string Name
		{
			get { return _name; }
			private set { SetProperty(ref _name, value); }
		}
		/// <summary>Full name of a local user, for example: "Dan Wilson".</summary>
		public string FullName
		{
			get { return _fullName; }
			private set { SetProperty(ref _fullName, value); }
		}
		/// <summary>Name of the Windows domain to which a user account belongs, for example: "NA-SALES".</summary>
		public string Domain
		{
			get { return _domain; }
			private set { SetProperty(ref _domain, value); }
		}
		/// <summary>Description of the account.</summary>
		public string Description
		{
			get { return _description; }
			set { SetProperty(ref _description, value); }
		}
		/// <summary>Get the account type</summary>
		public UInt32 AccountType
		{
			get { return _accountType; }
			private set { SetProperty(ref _accountType, value); }
		}
		/// <summary>Enumerated value that specifies the type of SID.</summary>
		public SidTypes SidType
		{
			get { return _sidType; }
			private set { SetProperty(ref _sidType, value); }
		}
		/// <summary>
		///     Security identifier (SID) for this account. A SID is a string value of variable length that is used to identify a trustee. Each account has a
		///     unique SID that an authority, such as a Windows domain, issues. The SID is stored in the security database. When a user logs on, the system
		///     retrieves the user SID from the database, places the SID in the user access token, and then uses the SID in the user access token to identify the
		///     user in all subsequent interactions with Windows security. Each SID is a unique identifier for a user or group, and a different user or group
		///     cannot have the same SID.
		/// </summary>
		public string Sid
		{
			get { return _sid; }
			private set { SetProperty(ref _sid, value); }
		}

		internal static CsgOsUser FromManagementObject(ManagementObject o)
		{
			var usr = new CsgOsUser();
			usr.IsLocalAccount = o.TryGet<bool>("LocalAccount");
			usr.Status = o.TryGet<string>("Status");
			usr.Name = o.TryGet<string>("Name");
			usr.FullName = o.TryGet<string>("FullName");
			usr.Description = o.TryGet<string>("Description");
			usr.Domain = o.TryGet<string>("Domain");
			usr.AccountType = o.TryGet<UInt32>("AccountType");
			usr.SidType = (SidTypes) o.TryGet<byte>("SIDType");
			usr.Sid = o.TryGet<string>("SID");
			return usr;
		}

		internal static CsgOsUser FromEnvironment()
		{
			return new CsgOsUser {Name = Environment.UserName, Domain = Environment.UserDomainName, Sid = Environment.UserDomainName + "/" + Environment.UserName};
		}



		/// <summary>Sid types</summary>
		public enum SidTypes : byte
		{
			/// <summary>SidTypeUser</summary>
			User = 1,
			/// <summary>SidTypeGroup</summary>
			Group = 2,
			/// <summary>SidTypeDomain</summary>
			Domain = 3,
			/// <summary>SidTypeAlias</summary>
			Alias = 4,
			/// <summary>SidTypeWellKnownGroup</summary>
			WellKnownGroup = 5,
			/// <summary>SidTypeDeletedAccount</summary>
			DeletedAccount = 6,
			/// <summary>SidTypeInvalid</summary>
			Invalid = 7,
			/// <summary>SidTypeUnknown</summary>
			Unknown = 8,
			/// <summary>SidTypeComputer</summary>
			Computer = 9,
		}
	}
}