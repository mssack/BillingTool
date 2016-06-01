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
	public sealed class CsgComputerMemory : Base
	{
		private static CsgComputerMemory _instance;
		private static readonly object SingletonLock = new object();
		/// <summary>Returns the singleton instance</summary>
		internal static CsgComputerMemory I
		{
			get
			{
				if (_instance != null)
					return _instance; //Advanced first check to improve performance (no lock needed).
				lock (SingletonLock)
				{
					return _instance ?? (_instance = new CsgComputerMemory());
				}
			}
		}
		private readonly ObservableCollection<CsgMemoryDevice> _devices = new ObservableCollection<CsgMemoryDevice>();
		private bool _isCollected;
		private ReadOnlyObservableCollection<CsgMemoryDevice> _readOnlyDevices;
		private UInt64 _total;

		private CsgComputerMemory()
		{
		}


		#region Overrides/Interfaces
		/// <summary>Returns the name of the type.</summary>
		public override string ToString()
		{
			return Total/1024.0/1024.0 + " MB";
		}
		#endregion


		/// <summary>
		///     Total size of physical memory. Be aware that, under some circumstances, this property may not return an accurate value for the physical memory.
		///     For example, it is not accurate if the BIOS is using some of the physical memory. For an accurate value, use the Capacity property in
		///     Win32_PhysicalMemory instead.
		/// </summary>
		public UInt64 Total
		{
			get
			{
				CsGlobal.Computer.System.Reload(true);
				return _total;
			}
			internal set { SetProperty(ref _total, value); }
		}
		/// <summary>Gets or sets the Devices.</summary>
		public ReadOnlyObservableCollection<CsgMemoryDevice> Devices
		{
			get
			{
				Reload(true);
				return _readOnlyDevices ?? (_readOnlyDevices = new ReadOnlyObservableCollection<CsgMemoryDevice>(_devices));
			}
			set { SetProperty(ref _readOnlyDevices, value); }
		}

		/// <summary>Reloads all the items in <see cref="Devices" /> list.</summary>
		/// <param name="usecache">if true the WMI will only be queried if it haven't been done before.</param>
		public void Reload(bool usecache = false)
		{
			if (usecache && _isCollected)
				return;

			try
			{
				var moc = new ManagementObjectSearcher("SELECT * FROM Win32_PhysicalMemory").Get();

				new ListAssimilator<CsgMemoryDevice, ManagementObject>(_devices, moc.OfType<ManagementObject>())
				{
					OnPairFound = (device, o) => device.Load(o),
					EqualFunc = (device, o) => device.Tag == o.TryGet<string>("Tag"),
					ConvertFunc = o => CsgMemoryDevice.FromManagementObject(o)
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