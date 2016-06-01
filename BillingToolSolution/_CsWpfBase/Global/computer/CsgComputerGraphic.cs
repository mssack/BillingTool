// Copyright (c) 2014, 2015 All Right Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2015-07-24</date>

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
	public sealed class CsgComputerGraphic : Base
	{
		private static CsgComputerGraphic _instance;
		private static readonly object SingletonLock = new object();
		/// <summary>Returns the singleton instance</summary>
		internal static CsgComputerGraphic I
		{
			get
			{
				if (_instance != null)
					return _instance; //Advanced first check to improve performance (no lock needed).
				lock (SingletonLock)
				{
					return _instance ?? (_instance = new CsgComputerGraphic());
				}
			}
		}
		private readonly ObservableCollection<CsgGraphicDevice> _devices = new ObservableCollection<CsgGraphicDevice>();
		private bool _isCollected;
		private ReadOnlyObservableCollection<CsgGraphicDevice> _readOnlyDevices;

		private CsgComputerGraphic()
		{
		}

		/// <summary>Gets or sets the Devices.</summary>
		public ReadOnlyObservableCollection<CsgGraphicDevice> Devices
		{
			get
			{
				Reload(true);
				return _readOnlyDevices ?? (_readOnlyDevices = new ReadOnlyObservableCollection<CsgGraphicDevice>(_devices));
			}
		}

		/// <summary>Reloads all the items in <see cref="Devices" /> list.</summary>
		/// <param name="usecache">if true the WMI will only be queried if it haven't been done before.</param>
		public void Reload(bool usecache = false)
		{
			if (usecache && _isCollected)
				return;

			try
			{
				var moc = new ManagementObjectSearcher("SELECT * FROM Win32_VideoController").Get();
				new ListAssimilator<CsgGraphicDevice, ManagementObject>(_devices, moc.OfType<ManagementObject>())
				{
					ConvertFunc = o => CsgGraphicDevice.FromManagementObject(o),
					EqualFunc = (device, o) => device.DeviceId == o.TryGet<string>("DeviceID"),
					OnPairFound = (device, o) => device.Load(o)
				}.Execute();
			}
			catch (Exception)
			{
				_devices.Clear();
			}


			_isCollected = true;
		}
	}
}