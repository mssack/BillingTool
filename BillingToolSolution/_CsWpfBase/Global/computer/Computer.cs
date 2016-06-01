// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-05-06</date>

using System;
using System.Management;
using System.Security.Cryptography;
using System.Text;
using CsWpfBase.Ev.Objects;






namespace CsWpfBase.Global.computer
{
	/// <summary>Internal see at <see cref="CsGlobal" />.</summary>
	[Serializable]
	public sealed class CsgComputer : Base
	{
		private static CsgComputer _instance;
		/// <summary>Returns the singleton instance</summary>
		internal static CsgComputer I
		{
			get
			{
				if (_instance != null)
					return _instance;

				if (CsGlobal.IsInstalled(GlobalFunctions.WpfStorage))
					_instance = CsGlobal.Storage.Private.Handle("Configuration-Computer", () => new CsgComputer());
				else
					_instance = new CsgComputer();
				return _instance;
			}
		}
		private Guid? _id;

		private CsgComputer()
		{
		}

		/// <summary>Gets the computer id, this id will be saved to disk to gain speed.</summary>
		public Guid Id
		{
			get { return (_id ?? (_id = FingerPrint.Value())).Value; }
			private set { SetProperty(ref _id, value); }
		}
		/// <summary>Get computer system informations</summary>
		public CsgComputerSystem System => CsgComputerSystem.I;
		/// <summary>Get processor informations</summary>
		public CsgComputerProcessor Processor => CsgComputerProcessor.I;
		/// <summary>Get memory informations</summary>
		public CsgComputerMemory Memory => CsgComputerMemory.I;
		/// <summary>Get screen informations</summary>
		public CsgComputerScreen Screen => CsgComputerScreen.I;
		/// <summary>Get main board informations</summary>
		public CsgComputerMainBoard Mainboard => CsgComputerMainBoard.I;
		/// <summary>Get graphic informations</summary>
		public CsgComputerGraphic Graphic => CsgComputerGraphic.I;
		/// <summary>Get drives informations</summary>
		public CsgComputerDiskDrive DiskDrive => CsgComputerDiskDrive.I;
		/// <summary>Get bios informations</summary>
		public CsgComputerBios Bios => CsgComputerBios.I;
		/// <summary>Get network informations</summary>
		public CsgComputerNetwork Network => CsgComputerNetwork.I;
		/// <summary>Get device informations</summary>
		public CsgComputerDevices Devices => CsgComputerDevices.I;



		private class FingerPrint
		{
			private static Guid? _fingerPrint;

			public static Guid Value()
			{
				if (_fingerPrint == null)
				{
					_fingerPrint = GetHash("CPU >> " + CpuId() + "\nBIOS >> " + BiosId() + "\nBASE >> " + BaseId() + VideoId());
				}
				return _fingerPrint.Value;
			}

			private static Guid GetHash(string s)
			{
				var sec = new MD5CryptoServiceProvider();
				var enc = new ASCIIEncoding();
				var bt = enc.GetBytes(s);
				return new Guid(sec.ComputeHash(bt));
			}

			private static string GetHexString(byte[] bt)
			{
				var s = string.Empty;
				for (var i = 0; i < bt.Length; i++)
				{
					var b = bt[i];
					int n, n1, n2;
					n = b;
					n1 = n & 15;
					n2 = (n >> 4) & 15;
					if (n2 > 9)
						s += ((char) (n2 - 10 + 'A')).ToString();
					else
						s += n2.ToString();
					if (n1 > 9)
						s += ((char) (n1 - 10 + 'A')).ToString();
					else
						s += n1.ToString();
					if (i + 1 != bt.Length && (i + 1)%2 == 0)
						s += "-";
				}
				return s;
			}

			//Return a hardware identifier
			private static string Identifier(string wmiClass, string wmiProperty, string wmiMustBeTrue)
			{
				try
				{
					var result = "";
					var mc = new ManagementClass(wmiClass);
					var moc = mc.GetInstances();
					foreach (ManagementObject mo in moc)
					{
						if (mo[wmiMustBeTrue].ToString() == "True")
						{
							//Only get the first one
							if (result == "")
							{
								try
								{
									result = mo[wmiProperty].ToString();
									break;
								}
								catch
								{
								}
							}
						}
					}
					return result;
				}
				catch (Exception)
				{
					return "";
				}
			}

			//Return a hardware identifier
			private static string Identifier(string wmiClass, string wmiProperty)
			{
				try
				{
					var result = "";
					var mc = new ManagementClass(wmiClass);
					var moc = mc.GetInstances();
					foreach (ManagementObject mo in moc)
					{
						//Only get the first one
						if (result == "")
						{
							try
							{
								result = mo[wmiProperty].ToString();
								break;
							}
							catch
							{
							}
						}
					}
					return result;
				}
				catch (Exception)
				{
					return "";
				}
			}

			private static string CpuId()
			{
				//Uses first CPU identifier available in order of preference
				//Don't get all identifiers, as it is very time consuming
				var retVal = Identifier("Win32_Processor", "UniqueId");
				if (retVal == "") //If no UniqueID, use ProcessorID
				{
					retVal = Identifier("Win32_Processor", "ProcessorId");
					if (retVal == "") //If no ProcessorId, use Name
					{
						retVal = Identifier("Win32_Processor", "Name");
						if (retVal == "") //If no Name, use Manufacturer
						{
							retVal = Identifier("Win32_Processor", "Manufacturer");
						}
						//Add clock speed for extra security
						retVal += Identifier("Win32_Processor", "MaxClockSpeed");
					}
				}
				return retVal;
			}

			//BIOS Identifier
			private static string BiosId()
			{
				return Identifier("Win32_BIOS", "Manufacturer")
						+ Identifier("Win32_BIOS", "SMBIOSBIOSVersion")
						+ Identifier("Win32_BIOS", "IdentificationCode")
						+ Identifier("Win32_BIOS", "SerialNumber")
						+ Identifier("Win32_BIOS", "ReleaseDate")
						+ Identifier("Win32_BIOS", "Version");
			}

			//Main physical hard drive ID
			private static string DiskId()
			{
				return Identifier("Win32_DiskDrive", "Model")
						+ Identifier("Win32_DiskDrive", "Manufacturer")
						+ Identifier("Win32_DiskDrive", "Signature")
						+ Identifier("Win32_DiskDrive", "TotalHeads");
			}

			//Motherboard ID
			private static string BaseId()
			{
				return Identifier("Win32_BaseBoard", "Model")
						+ Identifier("Win32_BaseBoard", "Manufacturer")
						+ Identifier("Win32_BaseBoard", "Name")
						+ Identifier("Win32_BaseBoard", "SerialNumber");
			}

			//Primary video controller ID
			private static string VideoId()
			{
				return Identifier("Win32_VideoController", "DriverVersion")
						+ Identifier("Win32_VideoController", "Name");
			}

			//First enabled network card ID
			private static string MacId()
			{
				return Identifier("Win32_NetworkAdapterConfiguration",
					"MACAddress", "IPEnabled");
			}
		}
	}
}