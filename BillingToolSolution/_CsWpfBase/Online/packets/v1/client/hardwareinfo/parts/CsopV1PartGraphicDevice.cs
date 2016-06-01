// Copyright (c) 2014, 2015 All Right Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2015-07-24</date>

using System;
using CsWpfBase.Ev.Objects;
using CsWpfBase.Global;
using CsWpfBase.Global.computer.parts;






namespace CsWpfBase.Online.packets.v1.client.hardwareinfo.parts
{
	/// <summary>wrapper</summary>
	[Serializable]
	public class CsopV1PartGraphicDevice : Base, CsoPacket.IPart
	{
		private UInt32 _adapterRam;
		private string _caption;
		private UInt32 _currentBitsPerPixel;
		private UInt32 _currentHorizontalResolution;
		private UInt64 _currentNumberOfColors;
		private UInt32 _currentRefreshRate;
		private CsgGraphicDevice.ScanModes _currentScanMode;
		private UInt32 _currentVerticalResolution;
		private string _description;
		private DateTime _driverDate;
		private string _driverVersion;
		private UInt32 _maxRefreshRate;
		private UInt32 _minRefreshRate;
		private string _name;
		private CsgGraphicDevice.VideoArchitectures _videoArchitecture;
		private CsgGraphicDevice.VideoMemoryTypes _videoMemoryType;
		private string _videoModeDescription;
		private string _videoProcessor;


		#region Overrides/Interfaces
		/// <summary>Interprets a binary into this object.</summary>
		public void Parse(CsoPacket.Reader reader, int length)
		{
			AdapterRam = reader.UInt32();
			Caption = reader.String();
			Description = reader.String();
			CurrentBitsPerPixel = reader.UInt32();
			CurrentHorizontalResolution = reader.UInt32();
			CurrentVerticalResolution = reader.UInt32();
			CurrentNumberOfColors = reader.UInt64();
			CurrentRefreshRate = reader.UInt32();
			CurrentScanMode = (CsgGraphicDevice.ScanModes) reader.UInt16();
			DriverDate = reader.DateTime();
			DriverVersion = reader.String();
			MaxRefreshRate = reader.UInt32();
			MinRefreshRate = reader.UInt32();
			Name = reader.String();
			VideoArchitecture = (CsgGraphicDevice.VideoArchitectures) reader.UInt16();
			VideoMemoryType = (CsgGraphicDevice.VideoMemoryTypes) reader.UInt16();
			VideoModeDescription = reader.String();
			VideoProcessor = reader.String();
		}

		/// <summary>converts this object into binary and writes the content to the Writer.</summary>
		public void Write(CsoPacket.Writer writer)
		{
			writer.UInt32(AdapterRam);
			writer.String(Caption);
			writer.String(Description);
			writer.UInt32(CurrentBitsPerPixel);
			writer.UInt32(CurrentHorizontalResolution);
			writer.UInt32(CurrentVerticalResolution);
			writer.UInt64(CurrentNumberOfColors);
			writer.UInt32(CurrentRefreshRate);
			writer.UInt16((ushort) CurrentScanMode);
			writer.DateTime(DriverDate);
			writer.String(DriverVersion);
			writer.UInt32(MaxRefreshRate);
			writer.UInt32(MinRefreshRate);
			writer.String(Name);
			writer.UInt16((ushort) VideoArchitecture);
			writer.UInt16((ushort) VideoMemoryType);
			writer.String(VideoModeDescription);
			writer.String(VideoProcessor);
		}
		#endregion


		/// <summary>Memory size of the video adapter.</summary>
		public UInt32 AdapterRam
		{
			get { return _adapterRam; }
			set { SetProperty(ref _adapterRam, value); }
		}
		/// <summary>Short description of the object.</summary>
		public string Caption
		{
			get { return _caption; }
			set { SetProperty(ref _caption, value); }
		}
		/// <summary>Description of the object.</summary>
		public string Description
		{
			get { return _description; }
			set { SetProperty(ref _description, value); }
		}
		/// <summary>Number of bits used to display each pixel.</summary>
		public UInt32 CurrentBitsPerPixel
		{
			get { return _currentBitsPerPixel; }
			set { SetProperty(ref _currentBitsPerPixel, value); }
		}
		/// <summary>Current number of horizontal pixels.</summary>
		public UInt32 CurrentHorizontalResolution
		{
			get { return _currentHorizontalResolution; }
			set { SetProperty(ref _currentHorizontalResolution, value); }
		}
		/// <summary>Current number of vertical pixels.</summary>
		public UInt32 CurrentVerticalResolution
		{
			get { return _currentVerticalResolution; }
			set { SetProperty(ref _currentVerticalResolution, value); }
		}
		/// <summary>Number of colors supported at the current resolution.</summary>
		public UInt64 CurrentNumberOfColors
		{
			get { return _currentNumberOfColors; }
			set { SetProperty(ref _currentNumberOfColors, value); }
		}
		/// <summary>
		///     Frequency at which the video controller refreshes the image for the monitor. A value of 0 (zero) indicates the default rate is being used, while
		///     0xFFFFFFFF indicates the optimal rate is being used.
		/// </summary>
		public UInt32 CurrentRefreshRate
		{
			get { return _currentRefreshRate; }
			set { SetProperty(ref _currentRefreshRate, value); }
		}
		/// <summary>Current scan mode.</summary>
		public CsgGraphicDevice.ScanModes CurrentScanMode
		{
			get { return _currentScanMode; }
			set { SetProperty(ref _currentScanMode, value); }
		}
		/// <summary>Last modification date and time of the currently installed video driver.</summary>
		public DateTime DriverDate
		{
			get { return _driverDate; }
			set { SetProperty(ref _driverDate, value); }
		}
		/// <summary>Version number of the video driver.</summary>
		public string DriverVersion
		{
			get { return _driverVersion; }
			set { SetProperty(ref _driverVersion, value); }
		}
		/// <summary>Maximum refresh rate of the video controller in hertz.</summary>
		public UInt32 MaxRefreshRate
		{
			get { return _maxRefreshRate; }
			set { SetProperty(ref _maxRefreshRate, value); }
		}
		/// <summary>Minimum refresh rate of the video controller in hertz.</summary>
		public UInt32 MinRefreshRate
		{
			get { return _minRefreshRate; }
			set { SetProperty(ref _minRefreshRate, value); }
		}
		/// <summary>Label by which the object is known. When subclassed, the property can be overridden to be a key property.</summary>
		public string Name
		{
			get { return _name; }
			set { SetProperty(ref _name, value); }
		}
		/// <summary>Type of video architecture.</summary>
		public CsgGraphicDevice.VideoArchitectures VideoArchitecture
		{
			get { return _videoArchitecture; }
			set { SetProperty(ref _videoArchitecture, value); }
		}
		/// <summary>Type of video memory.</summary>
		public CsgGraphicDevice.VideoMemoryTypes VideoMemoryType
		{
			get { return _videoMemoryType; }
			set { SetProperty(ref _videoMemoryType, value); }
		}
		/// <summary>Current resolution, color, and scan mode settings of the video controller.</summary>
		/// <example>"1024 x 768 x 256 colors"</example>
		public string VideoModeDescription
		{
			get { return _videoModeDescription; }
			set { SetProperty(ref _videoModeDescription, value); }
		}
		/// <summary>Free-form string describing the video processor.</summary>
		public string VideoProcessor
		{
			get { return _videoProcessor; }
			set { SetProperty(ref _videoProcessor, value); }
		}

		/// <summary>Creates a part from the <see cref="CsGlobal" /> hardware section.</summary>
		public static CsopV1PartGraphicDevice From(CsgGraphicDevice device)
		{
			var rv = new CsopV1PartGraphicDevice();

			rv.AdapterRam = device.AdapterRam;
			rv.Caption = device.Caption;
			rv.Description = device.Description;
			rv.CurrentBitsPerPixel = device.CurrentBitsPerPixel;
			rv.CurrentHorizontalResolution = device.CurrentHorizontalResolution;
			rv.CurrentVerticalResolution = device.CurrentVerticalResolution;
			rv.CurrentNumberOfColors = device.CurrentNumberOfColors;
			rv.CurrentRefreshRate = device.CurrentRefreshRate;
			rv.CurrentScanMode = device.CurrentScanMode;
			rv.DriverDate = device.DriverDate;
			rv.DriverVersion = device.DriverVersion;
			rv.MaxRefreshRate = device.MaxRefreshRate;
			rv.MinRefreshRate = device.MinRefreshRate;
			rv.Name = device.Name;
			rv.VideoArchitecture = device.VideoArchitecture;
			rv.VideoMemoryType = device.VideoMemoryType;
			rv.VideoModeDescription = device.VideoModeDescription;
			rv.VideoProcessor = device.VideoProcessor;

			return rv;
		}
	}
}