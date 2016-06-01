// Copyright (c) 2014, 2015 All Right Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2015-07-24</date>

using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Management;
using System.Text.RegularExpressions;
using CsWpfBase.Ev.Objects;
using CsWpfBase.Ev.Public.Extensions;
using CsWpfBase.Global.computer.parts;
using CsWpfBase.Utilitys;






namespace CsWpfBase.Global.computer
{
	/// <summary>Internal see at <see cref="CsGlobal" />.</summary>
	[Serializable]
	public sealed class CsgComputerDiskDrive : Base
	{
		private static CsgComputerDiskDrive _instance;
		private static readonly object SingletonLock = new object();
		/// <summary>Returns the singleton instance</summary>
		internal static CsgComputerDiskDrive I
		{
			get
			{
				if (_instance != null)
					return _instance; //Advanced first check to improve performance (no lock needed).
				lock (SingletonLock)
				{
					return _instance ?? (_instance = new CsgComputerDiskDrive());
				}
			}
		}
		private readonly ObservableCollection<CsgDiskDriveDevice> _devices = new ObservableCollection<CsgDiskDriveDevice>();
		private bool _isDiscDrivesCollected;
		private bool _isDiscPartitionsCollected;
		private bool _islogicalDiscsCollected;
		private ReadOnlyObservableCollection<CsgDiskDriveDevice> _readOnlyDevices;

		private CsgComputerDiskDrive()
		{
		}

		/// <summary>Gets or sets the Devices.</summary>
		public ReadOnlyObservableCollection<CsgDiskDriveDevice> Devices
		{
			get
			{
				CollectDiscDrives(true);
				return _readOnlyDevices ?? (_readOnlyDevices = new ReadOnlyObservableCollection<CsgDiskDriveDevice>(_devices));
			}
		}

		/// <summary>Reloads the hardware informations.</summary>
		public void Reload()
		{
			CollectDiscDrives();
			CollectDiscPartitions();
			CollectLogicalDiscs();
		}

		private void CollectDiscDrives(bool useCache = false)
		{
			if (useCache && _isDiscDrivesCollected)
				return;

			try
			{
				var moc = new ManagementObjectSearcher("SELECT * FROM Win32_DiskDrive").Get();

				new ListAssimilator<CsgDiskDriveDevice, ManagementObject>(_devices, moc.OfType<ManagementObject>().ToList())
				{
					ConvertFunc = mo => CsgDiskDriveDevice.FromManagementObject(mo),
					EqualFunc = (device, mo) => device.DeviceId == mo.TryGet<string>("DeviceID"),
					OnPairFound = (device, mo) => device.Load(mo)
				}.Execute();
			}
			catch (Exception)
			{
				_devices.Clear();
			}


			_isDiscDrivesCollected = true;
		}

		internal void CollectDiscPartitions(bool useCache = false)
		{
			if (useCache && _isDiscPartitionsCollected)
				return;

			CollectDiscDrives(true);

			try
			{
				var moc = new ManagementObjectSearcher("SELECT * FROM Win32_DiskPartition").Get();

				var groupedPartitions = moc.OfType<ManagementObject>().GroupBy(x => x.TryGet<UInt32>("DiskIndex")).ToArray();
				foreach (var groupedPartition in groupedPartitions)
				{
					var device = Devices.FirstOrDefault(x => x.Index == groupedPartition.Key);
					if (device == null)
						continue;

					try
					{
						new ListAssimilator<CsgDiskPartition, ManagementObject>(device._partitions, groupedPartition.ToList())
						{
							ConvertFunc = mo => CsgDiskPartition.FromManagementObject(mo),
							EqualFunc = (partition, mo) => partition.DeviceId == mo.TryGet<string>("DeviceID"),
							OnPairFound = (partition, mo) => partition.Load(mo)
						}.Execute();
					}
					catch (Exception)
					{
						device._partitions.Clear();
					}
				}
			}
			catch (Exception)
			{
				foreach (var device in Devices)
				{
					device._partitions.Clear();
				}
			}

			_isDiscPartitionsCollected = true;
		}

		internal void CollectLogicalDiscs(bool useCache = false)
		{
			if (useCache && _islogicalDiscsCollected)
				return;
			CollectDiscPartitions(true);
			try
			{
				var partitions = Devices.SelectMany(x => x.Partitions).ToArray();
				Func<ManagementObject, string, string> getDeviceId = ((mo, selector) =>
				{
					var input = mo.TryGet<string>(selector).ToString();
					var match = Regex.Match(input, "DeviceID=\"(.*?)\"");
					return match.Groups[1].Value;
				});
				var mapping = new ManagementObjectSearcher("SELECT * FROM Win32_LogicalDiskToPartition").Get().OfType<ManagementObject>().ToDictionary(mo => getDeviceId(mo, "Dependent"), mo => partitions.FirstOrDefault(part => part.DeviceId == getDeviceId(mo, "Antecedent")));
				var groupedLogicalDisks = new ManagementObjectSearcher("SELECT * FROM Win32_LogicalDisk").Get().OfType<ManagementObject>().GroupBy(x => x.TryGet<string>("DeviceID"));


				foreach (var group in groupedLogicalDisks)
				{
					try
					{
						CsgDiskPartition targetPartition;
						if (!mapping.TryGetValue(group.Key, out targetPartition))
							continue;
						new ListAssimilator<CsgLogicalDisk, ManagementObject>(targetPartition._logicalDisks, group)
						{
							EqualFunc = (disk, o) => disk.DeviceId == o.TryGet<string>("DeviceID"),
							ConvertFunc = o => CsgLogicalDisk.FromManagementObject(o),
							OnPairFound = (disk, o) => disk.Load(o),
						}.Execute();
					}
					catch (Exception)
					{

					}
				}
			}
			catch (Exception)
			{
			}


			_islogicalDiscsCollected = true;
		}
	}
}