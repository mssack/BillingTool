// Copyright (c) 2014, 2015 All Right Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2015-07-24</date>

using System;
using System.Management;
using CsWpfBase.Ev.Attributes;
using CsWpfBase.Ev.Objects;
using CsWpfBase.Ev.Public.Extensions;






namespace CsWpfBase.Global.computer.parts
{
	/// <summary>represents the capabilities and management capacity of the video controller on a computer system running Windows.</summary>
	[Serializable]
	public class CsgGraphicDevice : Base
	{
		private UInt32 _adapterRam;
		private string _caption;
		private UInt32 _currentBitsPerPixel;
		private UInt32 _currentHorizontalResolution;
		private UInt64 _currentNumberOfColors;
		private UInt32 _currentRefreshRate;
		private ScanModes _currentScanMode;
		private UInt32 _currentVerticalResolution;
		private string _description;
		private string _deviceId;
		private DateTime _driverDate;
		private string _driverVersion;
		private UInt32 _maxRefreshRate;
		private UInt32 _minRefreshRate;
		private string _name;
		private VideoArchitectures _videoArchitecture;
		private VideoMemoryTypes _videoMemoryType;
		private string _videoModeDescription;
		private string _videoProcessor;

		private CsgGraphicDevice()
		{
		}


		#region Overrides/Interfaces
		/// <summary>Returns the name of the type.</summary>
		public override string ToString()
		{
			return Name;
		}
		#endregion


		/// <summary>Gets or sets the DeviceId.</summary>
		public string DeviceId
		{
			get { return _deviceId; }
			private set { SetProperty(ref _deviceId, value); }
		}
		/// <summary>Memory size of the video adapter.</summary>
		public UInt32 AdapterRam
		{
			get { return _adapterRam; }
			private set { SetProperty(ref _adapterRam, value); }
		}
		/// <summary>Short description of the object.</summary>
		public string Caption
		{
			get { return _caption; }
			private set { SetProperty(ref _caption, value); }
		}
		/// <summary>Description of the object.</summary>
		public string Description
		{
			get { return _description; }
			private set { SetProperty(ref _description, value); }
		}
		/// <summary>Number of bits used to display each pixel.</summary>
		public UInt32 CurrentBitsPerPixel
		{
			get { return _currentBitsPerPixel; }
			private set { SetProperty(ref _currentBitsPerPixel, value); }
		}
		/// <summary>Current number of horizontal pixels.</summary>
		public UInt32 CurrentHorizontalResolution
		{
			get { return _currentHorizontalResolution; }
			private set { SetProperty(ref _currentHorizontalResolution, value); }
		}
		/// <summary>Current number of vertical pixels.</summary>
		public UInt32 CurrentVerticalResolution
		{
			get { return _currentVerticalResolution; }
			private set { SetProperty(ref _currentVerticalResolution, value); }
		}
		/// <summary>Number of colors supported at the current resolution.</summary>
		public UInt64 CurrentNumberOfColors
		{
			get { return _currentNumberOfColors; }
			private set { SetProperty(ref _currentNumberOfColors, value); }
		}
		/// <summary>
		///     Frequency at which the video controller refreshes the image for the monitor. A value of 0 (zero) indicates the default rate is being used, while
		///     0xFFFFFFFF indicates the optimal rate is being used.
		/// </summary>
		public UInt32 CurrentRefreshRate
		{
			get { return _currentRefreshRate; }
			private set { SetProperty(ref _currentRefreshRate, value); }
		}
		/// <summary>Current scan mode.</summary>
		public ScanModes CurrentScanMode
		{
			get { return _currentScanMode; }
			private set { SetProperty(ref _currentScanMode, value); }
		}
		/// <summary>Last modification date and time of the currently installed video driver.</summary>
		public DateTime DriverDate
		{
			get { return _driverDate; }
			private set { SetProperty(ref _driverDate, value); }
		}
		/// <summary>Version number of the video driver.</summary>
		public string DriverVersion
		{
			get { return _driverVersion; }
			private set { SetProperty(ref _driverVersion, value); }
		}
		/// <summary>Maximum refresh rate of the video controller in hertz.</summary>
		public UInt32 MaxRefreshRate
		{
			get { return _maxRefreshRate; }
			private set { SetProperty(ref _maxRefreshRate, value); }
		}
		/// <summary>Minimum refresh rate of the video controller in hertz.</summary>
		public UInt32 MinRefreshRate
		{
			get { return _minRefreshRate; }
			private set { SetProperty(ref _minRefreshRate, value); }
		}
		/// <summary>Label by which the object is known. When subclassed, the property can be overridden to be a key property.</summary>
		public string Name
		{
			get { return _name; }
			private set { SetProperty(ref _name, value); }
		}
		/// <summary>Type of video architecture.</summary>
		public VideoArchitectures VideoArchitecture
		{
			get { return _videoArchitecture; }
			private set { SetProperty(ref _videoArchitecture, value); }
		}
		/// <summary>Type of video memory.</summary>
		public VideoMemoryTypes VideoMemoryType
		{
			get { return _videoMemoryType; }
			private set { SetProperty(ref _videoMemoryType, value); }
		}
		/// <summary>Current resolution, color, and scan mode settings of the video controller.</summary>
		/// <example>"1024 x 768 x 256 colors"</example>
		public string VideoModeDescription
		{
			get { return _videoModeDescription; }
			private set { SetProperty(ref _videoModeDescription, value); }
		}
		/// <summary>Free-form string describing the video processor.</summary>
		public string VideoProcessor
		{
			get { return _videoProcessor; }
			private set { SetProperty(ref _videoProcessor, value); }
		}

		internal static CsgGraphicDevice FromManagementObject(ManagementObject o)
		{
			var dg = new CsgGraphicDevice();

			dg.Load(o);

			return dg;
		}

		internal void Load(ManagementObject o)
		{
			DeviceId = o.TryGet<string>("DeviceID");
			AdapterRam = o.TryGet<UInt32>("AdapterRAM");
			Caption = o.TryGet<string>("Caption");
			Description = o.TryGet<string>("Description");
			CurrentBitsPerPixel = o.TryGet<UInt32>("CurrentBitsPerPixel");
			CurrentHorizontalResolution = o.TryGet<UInt32>("CurrentHorizontalResolution");
			CurrentVerticalResolution = o.TryGet<UInt32>("CurrentVerticalResolution");
			CurrentNumberOfColors = o.TryGet<UInt64>("CurrentNumberOfColors");
			CurrentRefreshRate = o.TryGet<UInt32>("CurrentRefreshRate");
			CurrentScanMode = (ScanModes) o.TryGet<UInt16>("CurrentScanMode");
			DriverDate = o.TryGet<DateTime>("DriverDate");
			DriverVersion = o.TryGet<string>("DriverVersion");
			MaxRefreshRate = o.TryGet<UInt32>("MaxRefreshRate");
			MinRefreshRate = o.TryGet<UInt32>("MinRefreshRate");
			Name = o.TryGet<string>("Name");
			VideoArchitecture = (VideoArchitectures) o.TryGet<UInt16>("VideoArchitecture");
			VideoMemoryType = (VideoMemoryTypes) o.TryGet<UInt16>("VideoMemoryType");
			VideoModeDescription = o.TryGet<string>("VideoModeDescription");
			VideoProcessor = o.TryGet<string>("VideoProcessor");
		}



		/// <summary>Scan modes</summary>
		public enum ScanModes : short
		{
			/// <summary>Other</summary>
			Other = 1,
			/// <summary>Unknown</summary>
			Unknown = 2,
			/// <summary>Interlaced</summary>
			Interlaced = 3,
			/// <summary>NonInterlaced</summary>
			NonInterlaced = 4,
		}



		/// <summary>Video architecture</summary>
		public enum VideoArchitectures : short
		{
			/// <summary>Other</summary>
			[EnumDescription("Other")] Other = 1,
			/// <summary>Unknown</summary>
			[EnumDescription("Unknown")] Unknown = 2,
			/// <summary>CGA</summary>
			[EnumDescription("CGA")] Cga = 3,
			/// <summary>EGA</summary>
			[EnumDescription("EGA")] Ega = 4,
			/// <summary>VGA</summary>
			[EnumDescription("VGA")] Vga = 5,
			/// <summary>SVGA</summary>
			[EnumDescription("SVGA")] Svga = 6,
			/// <summary>MDA</summary>
			[EnumDescription("MDA")] Mda = 7,
			/// <summary>HGC</summary>
			[EnumDescription("HGC")] Hgc = 8,
			/// <summary>MCGA</summary>
			[EnumDescription("MCGA")] Mcga = 9,
			/// <summary>8514A</summary>
			[EnumDescription("8514A")] T8514A = 10,
			/// <summary>XGA</summary>
			[EnumDescription("XGA")] Xga = 11,
			/// <summary>Linear Frame Buffer</summary>
			[EnumDescription("Linear Frame Buffer")] LinearFrameBuffer = 12,
			/// <summary>PC-9</summary>
			[EnumDescription("PC-9")] Pc9 = 160,
		}



		/// <summary>Memory types</summary>
		public enum VideoMemoryTypes : short
		{
			/// <summary>Other</summary>
			[EnumDescription("Other")] Other = 1,
			/// <summary>Unknown</summary>
			[EnumDescription("Unknown")] Unknown = 2,
			/// <summary>VRAM</summary>
			[EnumDescription("VRAM")] Vram = 3,
			/// <summary>DRAM</summary>
			[EnumDescription("DRAM")] Dram = 4,
			/// <summary>SRAM</summary>
			[EnumDescription("SRAM")] Sram = 5,
			/// <summary>WRAM</summary>
			[EnumDescription("WRAM")] Wram = 6,
			/// <summary>EDO RAM</summary>
			[EnumDescription("EDO RAM")] Edoram = 7,
			/// <summary>Burst Synchronous DRAM</summary>
			[EnumDescription("Burst Synchronous DRAM")] BurstSynchronousDram = 8,
			/// <summary>Pipelined Burst SRAM</summary>
			[EnumDescription("Pipelined Burst SRAM")] PipelinedBurstSram = 9,
			/// <summary>CDRAM</summary>
			[EnumDescription("CDRAM")] Cdram = 10,
			/// <summary>3DRAM</summary>
			[EnumDescription("3DRAM")] T3Dram = 11,
			/// <summary>SDRAM</summary>
			[EnumDescription("SDRAM")] Sdram = 12,
			/// <summary>SGRAM</summary>
			[EnumDescription("SGRAM")] Sgram = 13,
		}
	}
}