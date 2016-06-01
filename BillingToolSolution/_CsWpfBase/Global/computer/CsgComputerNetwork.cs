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
	public sealed class CsgComputerNetwork : Base
	{
		private static CsgComputerNetwork _instance;
		private static readonly object SingletonLock = new object();
		/// <summary>Returns the singleton instance</summary>
		internal static CsgComputerNetwork I
		{
			get
			{
				if (_instance != null)
					return _instance; //Advanced first check to improve performance (no lock needed).
				lock (SingletonLock)
				{
					return _instance ?? (_instance = new CsgComputerNetwork());
				}
			}
		}
		private readonly ObservableCollection<CsgNetworkDevice> _devices = new ObservableCollection<CsgNetworkDevice>();
		private ReadOnlyObservableCollection<CsgNetworkDevice> _readOnlyDevices;
		private bool _isCollected;

		private CsgComputerNetwork()
		{
		}

		/// <summary>Gets a list of all network devices.</summary>
		public ReadOnlyObservableCollection<CsgNetworkDevice> Devices
		{
			get
			{
				Reload(true);
				return _readOnlyDevices ?? (_readOnlyDevices = new ReadOnlyObservableCollection<CsgNetworkDevice>(_devices));
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
				var moc = new ManagementObjectSearcher("ROOT\\StandardCimv2", "SELECT * FROM MSFT_NetAdapter WHERE HardwareInterface='True'").Get();

				new ListAssimilator<CsgNetworkDevice, ManagementObject>(_devices, moc.OfType<ManagementObject>())
				{
					OnPairFound = (device, o) => device.Load(o),
					EqualFunc = (device, o) => device.DeviceId == o.TryGet<string>("DeviceID"),
					ConvertFunc = o => CsgNetworkDevice.FromManagementObject(o)
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