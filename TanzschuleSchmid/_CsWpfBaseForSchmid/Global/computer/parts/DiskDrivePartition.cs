// Copyright (c) 2014, 2015 All Right Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2015-07-24</date>

using System;
using System.Collections.ObjectModel;
using System.Management;
using CsWpfBase.Ev.Objects;
using CsWpfBase.Ev.Public.Extensions;






namespace CsWpfBase.Global.computer.parts
{
	/// <summary>represents the capabilities and management capacity of a partitioned area of a physical disk on a computer system running Windows.</summary>
	[Serializable]
	public class CsgDiskPartition : Base
	{
		internal readonly ObservableCollection<CsgLogicalDisk> _logicalDisks = new ObservableCollection<CsgLogicalDisk>();
		private bool _bootable;
		private bool _bootPartition;
		private string _description;
		private string _deviceId;
		private UInt32 _diskIndex;
		private bool _primaryPartition;
		private ReadOnlyObservableCollection<CsgLogicalDisk> _readOnlyLogicalDisks;
		private UInt64 _size;
		private string _type;

		private CsgDiskPartition()
		{
		}


		#region Overrides/Interfaces
		/// <summary>Returns the name of the type.</summary>
		public override string ToString()
		{
			return DeviceId + " - " + Type;
		}
		#endregion


		/// <summary>Unique identifier of the disk drive and partition, from the rest of the system.</summary>
		public string DeviceId
		{
			get { return _deviceId; }
			private set { SetProperty(ref _deviceId, value); }
		}
		/// <summary>Index number of the disk containing this partition.</summary>
		public UInt32 DiskIndex
		{
			get { return _diskIndex; }
			private set { SetProperty(ref _diskIndex, value); }
		}
		/// <summary>Indicates whether the computer can be booted from this partition.</summary>
		public bool Bootable
		{
			get { return _bootable; }
			private set { SetProperty(ref _bootable, value); }
		}
		/// <summary>Partition is the active partition. The operating system uses the active partition when booting from a hard disk.</summary>
		public bool BootPartition
		{
			get { return _bootPartition; }
			private set { SetProperty(ref _bootPartition, value); }
		}
		/// <summary>If True, this is the primary partition.</summary>
		public bool PrimaryPartition
		{
			get { return _primaryPartition; }
			private set { SetProperty(ref _primaryPartition, value); }
		}
		/// <summary>Description of the object.</summary>
		public string Description
		{
			get { return _description; }
			private set { SetProperty(ref _description, value); }
		}
		/// <summary>Type of the partition. </summary>
		public string Type
		{
			get { return _type; }
			private set { SetProperty(ref _type, value); }
		}
		/// <summary>Total size of the partition.</summary>
		public UInt64 Size
		{
			get { return _size; }
			private set { SetProperty(ref _size, value); }
		}
		/// <summary>Gets or sets the LogicalDisks.</summary>
		public ReadOnlyObservableCollection<CsgLogicalDisk> LogicalDisks
		{
			get
			{
				CsGlobal.Computer.DiskDrive.CollectLogicalDiscs(true);
				return _readOnlyLogicalDisks ?? (_readOnlyLogicalDisks = new ReadOnlyObservableCollection<CsgLogicalDisk>(_logicalDisks));
			}
		}

		internal static CsgDiskPartition FromManagementObject(ManagementObject mo)
		{
			var partition = new CsgDiskPartition();
			partition.Load(mo);
			return partition;
		}

		internal void Load(ManagementObject mo)
		{
			DeviceId = mo.TryGet<string>("DeviceID");
			DiskIndex = mo.TryGet<UInt32>("DiskIndex");
			Bootable = mo.TryGet<bool>("Bootable");
			BootPartition = mo.TryGet<bool>("BootPartition");
			PrimaryPartition = mo.TryGet<bool>("PrimaryPartition");
			Description = mo.TryGet<string>("Description");
			Type = mo.TryGet<string>("Type");
			Size = mo.TryGet<UInt64>("Size");
		}
	}
}