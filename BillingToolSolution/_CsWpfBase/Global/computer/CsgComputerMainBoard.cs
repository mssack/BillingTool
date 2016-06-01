// Copyright (c) 2014, 2015 All Right Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2015-07-24</date>

using System;
using System.Linq;
using System.Management;
using CsWpfBase.Ev.Objects;
using CsWpfBase.Ev.Public.Extensions;






namespace CsWpfBase.Global.computer
{
	/// <summary>Internal see at <see cref="CsGlobal" />.</summary>
	[Serializable]
	public sealed class CsgComputerMainBoard : Base
	{
		private static CsgComputerMainBoard _instance;
		private static readonly object SingletonLock = new object();
		/// <summary>Returns the singleton instance</summary>
		internal static CsgComputerMainBoard I
		{
			get
			{
				if (_instance != null)
					return _instance; //Advanced first check to improve performance (no lock needed).
				lock (SingletonLock)
				{
					return _instance ?? (_instance = new CsgComputerMainBoard());
				}
			}
		}
		private bool _isBaseBoardCollected;
		private bool _isMotherboardCollected;
		private string _manufacturer;
		private string _primaryBusType;
		private string _product;
		private string _secondaryBusType;
		private string _serialNumber;

		private CsgComputerMainBoard()
		{
		}


		#region Overrides/Interfaces
		/// <summary>Returns the name of the type.</summary>
		public override string ToString()
		{
			return Manufacturer + Product;
		}
		#endregion


		/// <summary>Name of the organization responsible for producing the physical element.</summary>
		public string Manufacturer
		{
			get
			{
				CollectBaseBoard(true);
				return _manufacturer;
			}
			private set { SetProperty(ref _manufacturer, value); }
		}
		/// <summary>Baseboard part number defined by the manufacturer.</summary>
		public string Product
		{
			get
			{
				CollectBaseBoard(true);
				return _product;
			}
			private set { SetProperty(ref _product, value); }
		}
		/// <summary>Manufacturer-allocated number used to identify the physical element.</summary>
		public string SerialNumber
		{
			get
			{
				CollectBaseBoard(true);
				return _serialNumber;
			}
			private set { SetProperty(ref _serialNumber, value); }
		}
		/// <summary>Primary bus type of the motherboard.</summary>
		public string PrimaryBusType
		{
			get
			{
				CollectMotherboard(true);
				return _primaryBusType;
			}
			private set { SetProperty(ref _primaryBusType, value); }
		}
		/// <summary>Secondary bus type of the motherboard.</summary>
		public string SecondaryBusType
		{
			get
			{
				CollectMotherboard(true);
				return _secondaryBusType;
			}
			private set { SetProperty(ref _secondaryBusType, value); }
		}

		/// <summary>Reloads the hardware informations.</summary>
		public void Reload()
		{
			CollectBaseBoard(false);
			CollectMotherboard(false);
		}

		private void CollectBaseBoard(bool usecache)
		{
			if (usecache && _isBaseBoardCollected)
				return;

			try
			{
				var moc = new ManagementObjectSearcher("SELECT * FROM Win32_BaseBoard").Get();
				foreach (var o in moc)
				{
					var mo = (ManagementObject) o;
					Manufacturer = mo.TryGet<string>("Manufacturer");
					Product = mo.TryGet<string>("Product");
					SerialNumber = mo.TryGet<string>("SerialNumber");
					break;
				}
			}
			catch (Exception)
			{
			}


			_isBaseBoardCollected = true;
		}

		private void CollectMotherboard(bool usecache)
		{
			if (usecache && _isMotherboardCollected)
				return;

			try
			{
				var moc = new ManagementObjectSearcher("SELECT * FROM Win32_MotherboardDevice").Get();
				foreach (var o in moc.Cast<ManagementObject>())
				{
					PrimaryBusType = o.TryGet<string>("PrimaryBusType");
					SecondaryBusType = o.TryGet<string>("SecondaryBusType");
					break;
				}
			}
			catch (Exception)
			{
			}


			_isMotherboardCollected = true;
		}
	}
}