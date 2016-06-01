// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-05-06</date>

using System;
using System.Management;
using CsWpfBase.Ev.Objects;
using CsWpfBase.Ev.Public.Extensions;






namespace CsWpfBase.Global.computer.parts
{
	/// <summary>represents an installed printer on the current machine.</summary>
	[Serializable]
	public class PrinterDevice : Base
	{
		internal static PrinterDevice FromManagementObject(ManagementObject mo)
		{
			var device = new PrinterDevice();
			device.Load(mo);
			return device;
		}

		private string _comment;
		private bool _default;
		private string _driverName;
		private string _name;

		/// <summary>Gets or sets the Name.</summary>
		public string Name
		{
			get { return _name; }
			set { SetProperty(ref _name, value); }
		}
		/// <summary>Gets or sets the DriverName.</summary>
		public string DriverName
		{
			get { return _driverName; }
			set { SetProperty(ref _driverName, value); }
		}
		/// <summary>Gets or sets the Comment.</summary>
		public string Comment
		{
			get { return _comment; }
			set { SetProperty(ref _comment, value); }
		}
		/// <summary>Gets or sets the Default.</summary>
		public bool Default
		{
			get { return _default; }
			set { SetProperty(ref _default, value); }
		}

		internal void Load(ManagementObject mo)
		{
			Name = mo.TryGet<string>("Name");
			DriverName = mo.TryGet<string>("DriverName");
			Comment = mo.TryGet<string>("Comment");
			Default = mo.TryGet<bool>("Default");
		}
	}
}