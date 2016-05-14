// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-05-06</date>

using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Management;
using CsWpfBase.Ev.Objects;
using CsWpfBase.Ev.Public.Extensions;
using CsWpfBase.Global.computer.parts;
using CsWpfBase.Utilitys;






namespace CsWpfBase.Global.computer
{
	/// <summary>Internal see at <see cref="CsGlobal" />.</summary>
	[Serializable]
	public sealed class CsgComputerDevices : Base
	{
		private static CsgComputerDevices _instance;
		private static readonly object SingletonLock = new object();
		/// <summary>Returns the singleton instance</summary>
		internal static CsgComputerDevices I
		{
			get
			{
				if (_instance != null)
					return _instance; //Advanced first check to improve performance (no lock needed).
				lock (SingletonLock)
				{
					return _instance ?? (_instance = new CsgComputerDevices());
				}
			}
		}
		private bool _isCollected;
		private readonly ObservableCollection<PrinterDevice> _printerDevice = new ObservableCollection<PrinterDevice>();
		private ReadOnlyObservableCollection<PrinterDevice> _readOnlyPrinterDevice;

		private CsgComputerDevices()
		{
		}


		/// <summary>Name of a computer manufacturer.</summary>
		public ReadOnlyObservableCollection<PrinterDevice> Printers
		{
			get
			{
				Reload(true);
				return _readOnlyPrinterDevice ?? (_readOnlyPrinterDevice = new ReadOnlyObservableCollection<PrinterDevice>(_printerDevice));
			}
			private set { SetProperty(ref _readOnlyPrinterDevice, value); }
		}



		/// <summary>Reloads the hardware informations.</summary>
		public void Reload(bool usecache = false)
		{
			if (usecache && _isCollected)
				return;

			try
			{
				var moc = new ManagementObjectSearcher("SELECT * FROM Win32_Printer").Get();

				new ListAssimilator<PrinterDevice, ManagementObject>(_printerDevice, moc.OfType<ManagementObject>())
				{
					OnPairFound = (device, o) => device.Load(o),
					EqualFunc = (device, o) => device.Name == o.TryGet<string>("Name"),
					ConvertFunc = o => PrinterDevice.FromManagementObject(o)
				}.Execute();
			}
			catch (Exception)
			{
			}
			_isCollected = true;
		}
	}
}