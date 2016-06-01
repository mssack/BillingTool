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






namespace CsWpfBase.Global.computer
{
	/// <summary>Internal see at <see cref="CsGlobal" />.</summary>
	[Serializable]
	public sealed class CsgComputerProcessor : Base
	{
		private static CsgComputerProcessor _instance;
		private static readonly object SingletonLock = new object();
		/// <summary>Returns the singleton instance</summary>
		internal static CsgComputerProcessor I
		{
			get
			{
				if (_instance != null)
					return _instance; //Advanced first check to improve performance (no lock needed).
				lock (SingletonLock)
				{
					return _instance ?? (_instance = new CsgComputerProcessor());
				}
			}
		}
		private Architectures _architecture;
		private Familys _family;
		private bool _isCollected;
		private string _manufacturer;
		private uint _maxClockSpeed;
		private string _name;
		private uint _numberOfCores;
		private uint _numberOfLogicalProcessors;

		private CsgComputerProcessor()
		{
		}


		#region Overrides/Interfaces
		/// <summary>Returns the name of the type.</summary>
		public override string ToString()
		{
			return Name;
		}
		#endregion


		/// <summary>Processor architecture used by the platform.</summary>
		public Architectures Architecture
		{
			get
			{
				Reload(true);
				return _architecture;
			}
			private set { SetProperty(ref _architecture, value); }
		}
		/// <summary>Processor family type.</summary>
		public Familys Family
		{
			get
			{
				Reload(true);
				return _family;
			}
			private set { SetProperty(ref _family, value); }
		}
		/// <summary>Name of the processor manufacturer.</summary>
		/// <example>A. Datum Corporation</example>
		public string Manufacturer
		{
			get
			{
				Reload(true);
				return _manufacturer;
			}
			private set { SetProperty(ref _manufacturer, value); }
		}
		/// <summary>Maximum speed of the processor, in MHz.</summary>
		public UInt32 MaxClockSpeed
		{
			get
			{
				Reload(true);
				return _maxClockSpeed;
			}
			private set { SetProperty(ref _maxClockSpeed, value); }
		}
		/// <summary>Label by which the object is known. When this property is a subclass, it can be overridden to be a key property.</summary>
		public string Name
		{
			get
			{
				Reload(true);
				return _name;
			}
			private set { SetProperty(ref _name, value); }
		}
		/// <summary>
		///     Number of cores for the current instance of the processor. A core is a physical processor on the integrated circuit. For example, in a dual-core
		///     processor this property has a value of 2.
		/// </summary>
		public uint NumberOfCores
		{
			get
			{
				Reload(true);
				return _numberOfCores;
			}
			private set { SetProperty(ref _numberOfCores, value); }
		}
		/// <summary>
		///     Number of logical processors for the current instance of the processor. For processors capable of hyperthreading, this value includes only the
		///     processors which have hyperthreading enabled.
		/// </summary>
		public uint NumberOfLogicalProcessors
		{
			get
			{
				Reload(true);
				return _numberOfLogicalProcessors;
			}
			private set { SetProperty(ref _numberOfLogicalProcessors, value); }
		}

		/// <summary>Reloads the hardware informations.</summary>
		public void Reload(bool usecache = false)
		{
			if (usecache && _isCollected)
				return;

			try
			{
				var moc = new ManagementObjectSearcher("SELECT * FROM Win32_Processor").Get();
				foreach (var o in moc)
				{
					var mo = (ManagementObject) o;
					Architecture = (Architectures) mo.TryGet<UInt16>("Architecture");
					Family = (Familys) mo.TryGet<UInt16>("Family");
					Manufacturer = mo.TryGet<string>("Manufacturer");
					MaxClockSpeed = mo.TryGet<UInt32>("MaxClockSpeed");
					Name = mo.TryGet<string>("Name");
					NumberOfCores = mo.TryGet<UInt32>("NumberOfCores");
					NumberOfLogicalProcessors = mo.TryGet<UInt32>("NumberOfLogicalProcessors");
					break;
				}
			}
			catch (Exception)
			{
			}
			_isCollected = true;
		}



		/// <summary>Processor architectures. Use <see cref="EnumExtensions.GetName"/> for proper string naming</summary>
		public enum Architectures : ushort
		{
			/// <summary>x86</summary>
			[EnumDescription("x86")] X86 = 0,
			/// <summary>MIPS</summary>
			[EnumDescription("MIPS")] Mips = 1,
			/// <summary>Alpha</summary>
			[EnumDescription("Alpha")] Alpha = 2,
			/// <summary>PowerPC</summary>
			[EnumDescription("PowerPC")] PowerPc = 3,
			/// <summary>ARM</summary>
			[EnumDescription("ARM")] Arm = 5,
			/// <summary>ia64</summary>
			[EnumDescription("ia64", "Itanium-based systems")] Ia64 = 6,
			/// <summary>x64</summary>
			[EnumDescription("x64", "")] X64 = 9,
		}



		/// <summary>Processor family's Use <see cref="EnumExtensions.GetName"/> for proper string naming</summary>
		public enum Familys : ushort
		{
			/// <summary>Other</summary>
			[EnumDescription("Other", "")] Other = 1,
			/// <summary>Unknown</summary>
			[EnumDescription("Unknown", "")] Unknown = 2,
			/// <summary>8086</summary>
			[EnumDescription("8086", "")] T8086 = 3,
			/// <summary>80286</summary>
			[EnumDescription("80286", "")] T80286 = 4,
			/// <summary>Intel386™ Processor</summary>
			[EnumDescription("Intel386™ Processor", "")] Intel386Processor = 5,
			/// <summary>Intel486™ Processor</summary>
			[EnumDescription("Intel486™ Processor", "")] Intel486Processor = 6,
			/// <summary>8087</summary>
			[EnumDescription("8087", "")] T8087 = 7,
			/// <summary>80287</summary>
			[EnumDescription("80287", "")] T80287 = 8,
			/// <summary>80387</summary>
			[EnumDescription("80387", "")] T80387 = 9,
			/// <summary>80487</summary>
			[EnumDescription("80487", "")] T80487 = 10,
			/// <summary>Pentium Brand</summary>
			[EnumDescription("Pentium Brand", "")] PentiumBrand = 11,
			/// <summary>Pentium Pro</summary>
			[EnumDescription("Pentium Pro", "")] PentiumPro = 12,
			/// <summary>Pentium II</summary>
			[EnumDescription("Pentium II", "")] Pentium2 = 13,
			/// <summary>Pentium Processor with MMX™ Technology</summary>
			[EnumDescription("Pentium Processor with MMX™ Technology", "")] PentiumProcessorwithMmxTechnology = 14,
			/// <summary>Celeron™</summary>
			[EnumDescription("Celeron™", "")] Celeron = 15,
			/// <summary>Pentium II Xeon™</summary>
			[EnumDescription("Pentium II Xeon™", "")] Pentium2Xeon = 16,
			/// <summary>Pentium III</summary>
			[EnumDescription("Pentium III", "")] Pentium3 = 17,
			/// <summary>M1 Family</summary>
			[EnumDescription("M1 Family", "")] M1Family = 18,
			/// <summary>M2 Family</summary>
			[EnumDescription("M2 Family", "")] M2Family = 19,
			/// <summary>AMD Duron™ Processor Family</summary>
			[EnumDescription("AMD Duron™ Processor Family", "")] AmdDuronProcessorFamily = 24,
			/// <summary>K5 Family</summary>
			[EnumDescription("K5 Family", "")] K5Family = 25,
			/// <summary>K6 Family</summary>
			[EnumDescription("K6 Family", "")] K6Family = 26,
			/// <summary>K6-2</summary>
			[EnumDescription("K6-2", "")] K62 = 27,
			/// <summary>K6-3</summary>
			[EnumDescription("K6-3", "")] K63 = 28,
			/// <summary>AMD Athlon™ Processor Family</summary>
			[EnumDescription("AMD Athlon™ Processor Family", "")] AmdAthlonProcessorFamily = 29,
			/// <summary>AMD2900 Family</summary>
			[EnumDescription("AMD2900 Family", "")] Amd2900Family = 30,
			/// <summary>K6-2+</summary>
			[EnumDescription("K6-2+", "")] K62Plus = 31,
			/// <summary>Power PC Family</summary>
			[EnumDescription("Power PC Family", "")] PowerPcFamily = 32,
			/// <summary>Power PC 601</summary>
			[EnumDescription("Power PC 601", "")] PowerPc601 = 33,
			/// <summary>Power PC 603</summary>
			[EnumDescription("Power PC 603", "")] PowerPc603 = 34,
			/// <summary>Power PC 603+</summary>
			[EnumDescription("Power PC 603+", "")] PowerPc603Plus = 35,
			/// <summary>Power PC 604</summary>
			[EnumDescription("Power PC 604", "")] PowerPc604 = 36,
			/// <summary>Power PC 620</summary>
			[EnumDescription("Power PC 620", "")] PowerPc620 = 37,
			/// <summary>Power PC X704</summary>
			[EnumDescription("Power PC X704", "")] PowerPcx704 = 38,
			/// <summary>Power PC 750</summary>
			[EnumDescription("Power PC 750", "")] PowerPc750 = 39,
			/// <summary>Alpha Family</summary>
			[EnumDescription("Alpha Family", "")] AlphaFamily = 48,
			/// <summary>Alpha 21064</summary>
			[EnumDescription("Alpha 21064", "")] Alpha21064 = 49,
			/// <summary>Alpha 21066</summary>
			[EnumDescription("Alpha 21066", "")] Alpha21066 = 50,
			/// <summary>Alpha 21164</summary>
			[EnumDescription("Alpha 21164", "")] Alpha21164 = 51,
			/// <summary>Alpha 21164PC</summary>
			[EnumDescription("Alpha 21164PC", "")] Alpha21164Pc = 52,
			/// <summary>Alpha 21164a</summary>
			[EnumDescription("Alpha 21164a", "")] Alpha21164A = 53,
			/// <summary>Alpha 21264</summary>
			[EnumDescription("Alpha 21264", "")] Alpha21264 = 54,
			/// <summary>Alpha 21364</summary>
			[EnumDescription("Alpha 21364", "")] Alpha21364 = 55,
			/// <summary>MIPS Family</summary>
			[EnumDescription("MIPS Family", "")] MipsFamily = 64,
			/// <summary>MIPS R4000</summary>
			[EnumDescription("MIPS R4000", "")] Mipsr4000 = 65,
			/// <summary>MIPS R4200</summary>
			[EnumDescription("MIPS R4200", "")] Mipsr4200 = 66,
			/// <summary>MIPS R4400</summary>
			[EnumDescription("MIPS R4400", "")] Mipsr4400 = 67,
			/// <summary>MIPS R4600</summary>
			[EnumDescription("MIPS R4600", "")] MipsR4600 = 68,
			/// <summary>MIPS R10000</summary>
			[EnumDescription("MIPS R10000", "")] MipsR10000 = 69,
			/// <summary>SPARC Family</summary>
			[EnumDescription("SPARC Family", "")] SparcFamily = 80,
			/// <summary>SuperSPARC</summary>
			[EnumDescription("SuperSPARC", "")] SuperSparc = 81,
			/// <summary>microSPARC II</summary>
			[EnumDescription("microSPARC II", "")] MicroSparc2 = 82,
			/// <summary>microSPARC IIep</summary>
			[EnumDescription("microSPARC IIep", "")] MicroSparc2Ep = 83,
			/// <summary>UltraSPARC</summary>
			[EnumDescription("UltraSPARC", "")] UltraSparc = 84,
			/// <summary>UltraSPARC II</summary>
			[EnumDescription("UltraSPARC II", "")] UltraSparc2 = 85,
			/// <summary>UltraSPARC IIi</summary>
			[EnumDescription("UltraSPARC IIi", "")] UltraSparc2I = 86,
			/// <summary>UltraSPARC III</summary>
			[EnumDescription("UltraSPARC III", "")] UltraSparc3 = 87,
			/// <summary>UltraSPARC IIIi</summary>
			[EnumDescription("UltraSPARC IIIi", "")] UltraSparc3I = 88,
			/// <summary>68040</summary>
			[EnumDescription("68040", "")] T68040 = 96,
			/// <summary>68xxx Family</summary>
			[EnumDescription("68xxx Family", "")] T68XxxFamily = 97,
			/// <summary>68000</summary>
			[EnumDescription("68000", "")] T68000 = 98,
			/// <summary>68010</summary>
			[EnumDescription("68010", "")] T68010 = 99,
			/// <summary>68020</summary>
			[EnumDescription("68020", "")] T68020 = 100,
			/// <summary>68030</summary>
			[EnumDescription("68030", "")] T68030 = 101,
			/// <summary>Hobbit Family</summary>
			[EnumDescription("Hobbit Family", "")] HobbitFamily = 112,
			/// <summary>Crusoe™ TM5000 Family</summary>
			[EnumDescription("Crusoe™ TM5000 Family", "")] CrusoeTm5000Family = 120,
			/// <summary>Crusoe™ TM3000 Family</summary>
			[EnumDescription("Crusoe™ TM3000 Family", "")] CrusoeTm3000Family = 121,
			/// <summary>Efficeon™ TM8000 Family</summary>
			[EnumDescription("Efficeon™ TM8000 Family", "")] EfficeonTm8000Family = 122,
			/// <summary>Weitek</summary>
			[EnumDescription("Weitek", "")] Weitek = 128,
			/// <summary>Itanium™ Processor</summary>
			[EnumDescription("Itanium™ Processor", "")] ItaniumProcessor = 130,
			/// <summary>AMD Athlon™ 64 Processor Family</summary>
			[EnumDescription("AMD Athlon™ 64 Processor Family", "")] AmdAthlon64ProcessorFamily = 131,
			/// <summary>AMD Opteron™ Processor Family</summary>
			[EnumDescription("AMD Opteron™ Processor Family", "")] AmdOpteronProcessorFamily = 132,
			/// <summary>PA-RISC Family</summary>
			[EnumDescription("PA-RISC Family", "")] PariscFamily = 144,
			/// <summary>PA-RISC 8500</summary>
			[EnumDescription("PA-RISC 8500", "")] Parisc8500 = 145,
			/// <summary>PA-RISC 8000</summary>
			[EnumDescription("PA-RISC 8000", "")] Parisc8000 = 146,
			/// <summary>PA-RISC 7300LC</summary>
			[EnumDescription("PA-RISC 7300LC", "")] Parisc7300Lc = 147,
			/// <summary>PA-RISC 7200</summary>
			[EnumDescription("PA-RISC 7200", "")] Parisc7200 = 148,
			/// <summary>PA-RISC 7100LC</summary>
			[EnumDescription("PA-RISC 7100LC", "")] Parisc7100Lc = 149,
			/// <summary>PA-RISC 7100</summary>
			[EnumDescription("PA-RISC 7100", "")] Parisc7100 = 150,
			/// <summary>V30 Family</summary>
			[EnumDescription("V30 Family", "")] V30Family = 160,
			/// <summary>Pentium III Xeon™ Processor</summary>
			[EnumDescription("Pentium III Xeon™ Processor", "")] Pentium3XeonProcessor = 176,
			/// <summary>Pentium III Processor with Intel SpeedStep™ Technology</summary>
			[EnumDescription("Pentium III Processor with Intel SpeedStep™ Technology", "")] Pentium3ProcessorwithIntelSpeedStepTechnology = 177,
			/// <summary>Pentium 4</summary>
			[EnumDescription("Pentium 4", "")] Pentium4 = 178,
			/// <summary>Intel Xeon™</summary>
			[EnumDescription("Intel Xeon™", "")] IntelXeon = 179,
			/// <summary>AS400 Family</summary>
			[EnumDescription("AS400 Family", "")] As400Family = 180,
			/// <summary>Intel Xeon™ Processor MP</summary>
			[EnumDescription("Intel Xeon™ Processor MP", "")] IntelXeonProcessorMp = 181,
			/// <summary>AMD Athlon™ XP Family</summary>
			[EnumDescription("AMD Athlon™ XP Family", "")] AmdAthlonXpFamily = 182,
			/// <summary>AMD Athlon™ MP Family</summary>
			[EnumDescription("AMD Athlon™ MP Family", "")] AmdAthlonMpFamily = 183,
			/// <summary>Intel Itanium 2</summary>
			[EnumDescription("Intel Itanium 2", "")] IntelItanium2 = 184,
			/// <summary>Intel Pentium M Processor</summary>
			[EnumDescription("Intel Pentium M Processor", "")] IntelPentiumMProcessor = 185,
			/// <summary>K7</summary>
			[EnumDescription("K7", "")] K7 = 190,
			/// <summary>Intel Core™ i7-2760QM</summary>
			[EnumDescription("Intel Core™ i7-2760QM", "")] IntelCorei72760Qm = 198,
			/// <summary>IBM390 Family</summary>
			[EnumDescription("IBM390 Family", "")] Ibm390Family = 200,
			/// <summary>G4</summary>
			[EnumDescription("G4", "")] G4 = 201,
			/// <summary>G5</summary>
			[EnumDescription("G5", "")] G5 = 202,
			/// <summary>G6</summary>
			[EnumDescription("G6", "")] G6 = 203,
			/// <summary>z/Architecture Base</summary>
			[EnumDescription("z/Architecture Base", "")] ArchitectureBase = 204,
			/// <summary>i860</summary>
			[EnumDescription("i860", "")] I860 = 250,
			/// <summary>i960</summary>
			[EnumDescription("i960", "")] I960 = 251,
			/// <summary>SH-3</summary>
			[EnumDescription("SH-3", "")] Sh3 = 260,
			/// <summary>SH-4</summary>
			[EnumDescription("SH-4", "")] Sh4 = 261,
			/// <summary>ARM</summary>
			[EnumDescription("ARM", "")] Arm = 280,
			/// <summary>StrongARM</summary>
			[EnumDescription("StrongARM", "")] StrongArm = 281,
			/// <summary>6x86</summary>
			[EnumDescription("6x86", "")] T6X86 = 300,
			/// <summary>MediaGX</summary>
			[EnumDescription("MediaGX", "")] MediaGx = 301,
			/// <summary>MII</summary>
			[EnumDescription("MII", "")] Mii = 302,
			/// <summary>WinChip</summary>
			[EnumDescription("WinChip", "")] WinChip = 320,
			/// <summary>DSP</summary>
			[EnumDescription("DSP", "")] Dsp = 350,
			/// <summary>Video Processor</summary>
			[EnumDescription("Video Processor", "")] VideoProcessor = 500,
		}
	}
}