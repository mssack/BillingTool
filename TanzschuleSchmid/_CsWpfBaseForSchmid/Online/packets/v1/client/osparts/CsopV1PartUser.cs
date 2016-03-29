// Copyright (c) 2014, 2015 All Right Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2015-07-24</date>

using System;
using CsWpfBase.Ev.Objects;
using CsWpfBase.Global.os.user;






namespace CsWpfBase.Online.packets.v1.client.osparts
{
	/// <summary>wrapper.</summary>
	[Serializable]
	public class CsopV1PartUser : Base, CsoPacket.IPart
	{
		private UInt32 _accountType;
		private string _description;
		private string _domain;
		private string _fullName;
		private bool _isLocalAccount;
		private string _name;
		private string _sid;
		private CsgOsUser.SidTypes _sidType;


		#region Overrides/Interfaces
		/// <summary>Interprets a binary into this object.</summary>
		public void Parse(CsoPacket.Reader reader, int length)
		{
			IsLocalAccount = reader.Byte() == 1;
			Status = reader.String();
			Name = reader.String();
			FullName = reader.String();
			Domain = reader.String();
			Description = reader.String();

			AccountType = reader.UInt32();
			SidType = (CsgOsUser.SidTypes) reader.Byte();
			Sid = reader.String();
		}

		/// <summary>converts this object into binary and writes the content to the Writer.</summary>
		public void Write(CsoPacket.Writer writer)
		{
			writer.Byte((byte) (IsLocalAccount ? 1 : 0));
			writer.String(Status);
			writer.String(Name);
			writer.String(FullName);
			writer.String(Domain);
			writer.String(Description);

			writer.UInt32(AccountType);
			writer.Byte((byte) SidType);
			writer.String(Sid);
		}
		#endregion


		private string _status;
		///<summary>see <see cref="CsgOsUser" />.</summary>
		public string Status
		{
			get { return _status; }
			set { SetProperty(ref _status, value); }
		}
		/// <summary>see <see cref="CsgOsUser" />.</summary>
		public bool IsLocalAccount
		{
			get { return _isLocalAccount; }
			set { SetProperty(ref _isLocalAccount, value); }
		}
		/// <summary>see <see cref="CsgOsUser" />.</summary>
		public string Name
		{
			get { return _name; }
			set { SetProperty(ref _name, value); }
		}
		/// <summary>see <see cref="CsgOsUser" />.</summary>
		public string FullName
		{
			get { return _fullName; }
			set { SetProperty(ref _fullName, value); }
		}
		/// <summary>see <see cref="CsgOsUser" />.</summary>
		public string Domain
		{
			get { return _domain; }
			set { SetProperty(ref _domain, value); }
		}
		/// <summary>see <see cref="CsgOsUser" />.</summary>
		public string Description
		{
			get { return _description; }
			set { SetProperty(ref _description, value); }
		}
		/// <summary>see <see cref="CsgOsUser" />.</summary>
		public UInt32 AccountType
		{
			get { return _accountType; }
			set { SetProperty(ref _accountType, value); }
		}
		/// <summary>see <see cref="CsgOsUser" />.</summary>
		public CsgOsUser.SidTypes SidType
		{
			get { return _sidType; }
			set { SetProperty(ref _sidType, value); }
		}
		/// <summary>see <see cref="CsgOsUser" />.</summary>
		public string Sid
		{
			get { return _sid; }
			set { SetProperty(ref _sid, value); }
		}

		/// <summary>converter, only the wrapper should be transmitted. If some local changes occur the wrapper have to stay the same!!!</summary>
		public static CsopV1PartUser FromCsg(CsgOsUser user)
		{
			var userWrapper = new CsopV1PartUser();
			userWrapper.Status = user.Status;
			userWrapper.IsLocalAccount = user.IsLocalAccount;
			userWrapper.Name = user.Name;
			userWrapper.FullName = user.FullName;
			userWrapper.Domain = user.Domain;
			userWrapper.Description = user.Description;
			userWrapper.AccountType = user.AccountType;
			userWrapper.SidType = user.SidType;
			userWrapper.Sid = user.Sid;
			return userWrapper;
		}
	}
}