// Copyright (c) 2014, 2015 All Right Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2015-07-24</date>

using System;
using System.Management;
using CsWpfBase.Ev.Objects;
using CsWpfBase.Ev.Public.Extensions;






namespace CsWpfBase.Global.computer
{
	/// <summary>Internal see at <see cref="CsGlobal" />.</summary>
	[Serializable]
	public sealed class CsgComputerSystem : Base
	{
		private static CsgComputerSystem _instance;
		private static readonly object SingletonLock = new object();
		/// <summary>Returns the singleton instance</summary>
		internal static CsgComputerSystem I
		{
			get
			{
				if (_instance != null)
					return _instance; //Advanced first check to improve performance (no lock needed).
				lock (SingletonLock)
				{
					return _instance ?? (_instance = new CsgComputerSystem());
				}
			}
		}

		private string _manufacturer;
		private string _model;
		private bool _partOfDomain;
		private string _systemFamily;
		private string _systemSkuNumber;
		private string _workgroup;
		private bool _isCollected;
		private CsgComputerSystem()
		{
		}

		/// <summary>Returns the name of the type.</summary>
		public override string ToString()
		{
			return Manufacturer + " " + Model;
		}

		/// <summary>Name of a computer manufacturer.</summary>
		public string Manufacturer
		{
			get
			{
				Reload(true);
				return _manufacturer;
			}
			private set { SetProperty(ref _manufacturer, value); }
		}
		/// <summary>Product name that a manufacturer gives to a computer. This property must have a value.</summary>
		public string Model
		{
			get
			{
				Reload(true);
				return _model;
			}
			private set { SetProperty(ref _model, value); }
		}
		/// <summary>The family to which a particular computer belongs. A family refers to a set of computers that are similar but not identical from a hardware or software point of view.</summary>
		public string SystemFamily
		{
			get
			{
				Reload(true);
				return _systemFamily;
			}
			private set { SetProperty(ref _systemFamily, value); }
		}
		/// <summary>Identifies a particular computer configuration for sale. It is sometimes also called a product ID or purchase order number.</summary>
		public string SystemSkuNumber
		{
			get
			{
				Reload(true);
				return _systemSkuNumber;
			}
			private set { SetProperty(ref _systemSkuNumber, value); }
		}
		/// <summary>If True, the computer is part of a domain. If the value is NULL, the computer is not in a domain or the status is unknown. If you remove the computer from a domain, the value becomes false.</summary>
		public bool PartOfDomain
		{
			get
			{
				Reload(true);
				return _partOfDomain;
			}
			private set { SetProperty(ref _partOfDomain, value); }
		}
		/// <summary>Name of the workgroup for this computer. If the value of the PartOfDomain property is False, then the name of the workgroup is returned.</summary>
		public string Workgroup
		{
			get
			{
				Reload(true);
				return _workgroup;
			}
			private set { SetProperty(ref _workgroup, value); }
		}


		/// <summary>Reloads the hardware informations.</summary>
		public void Reload(bool usecache = false)
		{
			if (usecache && _isCollected)
				return;

			try
			{
				var moc = new ManagementObjectSearcher("SELECT * FROM Win32_ComputerSystem").Get();
				foreach (var o in moc)
				{
					var mo = (ManagementObject)o;
					Manufacturer = mo.TryGet<string>("Manufacturer");
					Model = mo.TryGet<string>("Model");
					SystemFamily = mo.TryGet<string>("SystemFamily");
					SystemSkuNumber = mo.TryGet<string>("SystemSKUNumber");
					PartOfDomain = mo.TryGet<bool>("PartOfDomain");
					Workgroup = mo.TryGet<string>("Workgroup");
					CsGlobal.Computer.Memory.Total = mo.TryGet<UInt64>("TotalPhysicalMemory");
					break;
				}
			}
			catch (Exception)
			{
			}
			_isCollected = true;
		}
	
	}
}