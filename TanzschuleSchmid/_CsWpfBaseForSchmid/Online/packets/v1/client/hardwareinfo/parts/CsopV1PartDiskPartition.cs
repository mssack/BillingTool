// Copyright (c) 2014, 2015 All Right Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2015-07-24</date>

using System;
using System.Collections.Generic;
using System.Linq;
using CsWpfBase.Ev.Objects;
using CsWpfBase.Global;
using CsWpfBase.Global.computer.parts;






namespace CsWpfBase.Online.packets.v1.client.hardwareinfo.parts
{
	/// <summary>wrapper</summary>
	[Serializable]
	public class CsopV1PartDiskPartition : Base, CsoPacket.IPart
	{
		private bool _bootable;
		private bool _bootPartition;
		private string _description;
		private UInt32 _diskIndex;
		private List<CsopV1PartLogicalDisk> _logicalDisks;
		private bool _primaryPartition;
		private UInt64 _size;
		private string _type;


		#region Overrides/Interfaces
		/// <summary>Interprets a binary into this object.</summary>
		public void Parse(CsoPacket.Reader reader, int length)
		{
			DiskIndex = reader.UInt32();
			Bootable = reader.Byte() == 1;
			BootPartition = reader.Byte() == 1;
			PrimaryPartition = reader.Byte() == 1;
			Description = reader.String();
			Type = reader.String();
			Size = reader.UInt64();
			LogicalDisks = reader.ListOfParts<CsopV1PartLogicalDisk>();
		}

		/// <summary>converts this object into binary and writes the content to the Writer.</summary>
		public void Write(CsoPacket.Writer writer)
		{
			writer.UInt32(DiskIndex);
			writer.Byte((byte) (Bootable ? 1 : 0));
			writer.Byte((byte) (BootPartition ? 1 : 0));
			writer.Byte((byte) (PrimaryPartition ? 1 : 0));
			writer.String(Description);
			writer.String(Type);
			writer.UInt64(Size);
			writer.ListOfParts(LogicalDisks);
		}
		#endregion


		/// <summary>Index number of the disk containing this partition.</summary>
		public UInt32 DiskIndex
		{
			get { return _diskIndex; }
			set { SetProperty(ref _diskIndex, value); }
		}
		/// <summary>Indicates whether the computer can be booted from this partition.</summary>
		public bool Bootable
		{
			get { return _bootable; }
			set { SetProperty(ref _bootable, value); }
		}
		/// <summary>Partition is the active partition. The operating system uses the active partition when booting from a hard disk.</summary>
		public bool BootPartition
		{
			get { return _bootPartition; }
			set { SetProperty(ref _bootPartition, value); }
		}
		/// <summary>If True, this is the primary partition.</summary>
		public bool PrimaryPartition
		{
			get { return _primaryPartition; }
			set { SetProperty(ref _primaryPartition, value); }
		}
		/// <summary>Description of the object.</summary>
		public string Description
		{
			get { return _description; }
			set { SetProperty(ref _description, value); }
		}
		/// <summary>Type of the partition. </summary>
		public string Type
		{
			get { return _type; }
			set { SetProperty(ref _type, value); }
		}
		/// <summary>Total size of the partition.</summary>
		public UInt64 Size
		{
			get { return _size; }
			set { SetProperty(ref _size, value); }
		}
		/// <summary>Gets or sets the LogicalDisks.</summary>
		public List<CsopV1PartLogicalDisk> LogicalDisks
		{
			get { return _logicalDisks ?? (_logicalDisks = new List<CsopV1PartLogicalDisk>()); }
			set { SetProperty(ref _logicalDisks, value); }
		}

		/// <summary>Creates a part from the <see cref="CsGlobal" /> hardware section.</summary>
		public static CsopV1PartDiskPartition From(CsgDiskPartition partition)
		{
			var rv = new CsopV1PartDiskPartition();

			rv.DiskIndex = partition.DiskIndex;
			rv.Bootable = partition.Bootable;
			rv.BootPartition = partition.BootPartition;
			rv.PrimaryPartition = partition.PrimaryPartition;
			rv.Description = partition.Description;
			rv.Type = partition.Type;
			rv.Size = partition.Size;

			rv.LogicalDisks.AddRange(partition.LogicalDisks.Select(CsopV1PartLogicalDisk.From));

			return rv;
		}
	}
}