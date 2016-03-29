// Copyright (c) 2014, 2015 All Right Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2015-07-24</date>

using System;
using System.Collections.Generic;
using System.Management;
using CsWpfBase.Ev.Attributes;
using CsWpfBase.Ev.Objects;
using CsWpfBase.Ev.Public.Extensions;
using CsWpfBase.Global.os.user;






namespace CsWpfBase.Global.os
{
	/// <summary>Internal see at <see cref="CsGlobal" />.</summary>
	[Serializable]
	public class CsgOs : Base
	{
		private static CsgOs _instance;
		private static readonly object SingletonLock = new object();
		/// <summary>Returns the singleton instance</summary>
		internal static CsgOs I
		{
			get
			{
				if (_instance != null)
					return _instance; //Advanced first check to improve performance (no lock needed).
				lock (SingletonLock)
				{
					return _instance ?? (_instance = new CsgOs());
				}
			}
		}
		private string _architecture;
		private string _buildType;
		private string _codeSet;
		private string _computerName;
		private string _countryCode;
		private CsgOsUser _currentUser;
		private string _description;
		private DateTime _installDate;
		private bool _isCollected;
		private bool _isUserCollected;
		private string _name;
		private OperatingSystemSkus _operatingSystemSku;
		private ProductTypes _productType;
		private string _registeredUser;
		private string _serialNumber;
		private CsgOsUser[] _users;
		private string _version;

		private CsgOs()
		{
		}

		/// <summary>Determines whether the current operating system is a 64-bit operating system.</summary>
		public bool Is64Bit
		{
			get { return Environment.Is64BitOperatingSystem; }
		}
		/// <summary> Gets the computer name.</summary>
		public string ComputerName
		{
			get
			{
				if (_computerName == null)
					_computerName = Environment.MachineName;
				return _computerName;
			}
			private set { SetProperty(ref _computerName, value); }
		}
		/// <summary>
		///     Short description of the object—a one-line string. The string includes the operating system version. For example, "Microsoft Microsoft Windows 7
		///     Enterprise ". This property can be localized.
		/// </summary>
		/// <example>
		///     Windows Vista and Windows 7:   This property may contain trailing characters. For example, the string "Microsoft Windows 7 Enterprise " (trailing
		///     space included) may be necessary to retrieve information using this property.
		/// </example>
		public string Name
		{
			get
			{
				Collect();
				return _name;
			}
			private set { SetProperty(ref _name, value); }
		}
		/// <summary>
		///     Description of the Windows operating system. Some user interfaces for example, those that allow editing of this description, limit its length to
		///     48 characters.
		/// </summary>
		public string Description
		{
			get
			{
				Collect();
				return _description;
			}
			private set { SetProperty(ref _description, value); }
		}
		/// <summary>
		///     Code page value an operating system uses. A code page contains a character table that an operating system uses to translate strings for different
		///     languages. The American National Standards Institute (ANSI) lists values that represent defined code pages. If an operating system does not use
		///     an ANSI code page, this member is set to 0 (zero). The CodeSet string can use a maximum of six characters to define the code page value.
		/// </summary>
		/// <example>"1255"</example>
		public string CodeSet
		{
			get
			{
				Collect();
				return _codeSet;
			}
			private set { SetProperty(ref _codeSet, value); }
		}
		/// <summary>
		///     Code for the country/region that an operating system uses. Values are based on international phone dialing prefixes—also referred to as IBM
		///     country/region codes. This property can use a maximum of six characters to define the country/region code value.
		/// </summary>
		/// <example>"1" (United States)</example>
		public string CountryCode
		{
			get
			{
				Collect();
				return _countryCode;
			}
			private set { SetProperty(ref _countryCode, value); }
		}
		/// <summary>Stock Keeping Unit (SKU) number for the operating system.</summary>
		public OperatingSystemSkus OperatingSystemSku
		{
			get
			{
				Collect();
				return _operatingSystemSku;
			}
			private set { SetProperty(ref _operatingSystemSku, value); }
		}
		/// <summary>Date object was installed. This property does not require a value to indicate that the object is installed.</summary>
		public DateTime InstallDate
		{
			get
			{
				Collect();
				return _installDate;
			}
			private set { SetProperty(ref _installDate, value); }
		}
		/// <summary>Type of build used for an operating system.</summary>
		/// <example> Examples: ""retail build"", ""checked build""</example>
		public string BuildType
		{
			get
			{
				Collect();
				return _buildType;
			}
			private set { SetProperty(ref _buildType, value); }
		}
		/// <summary>Version number of the operating system.</summary>
		/// <example>"4.0"</example>
		public string Version
		{
			get
			{
				Collect();
				return _version;
			}
			private set { SetProperty(ref _version, value); }
		}
		/// <summary>Architecture of the operating system, as opposed to the processor. This property can be localized.</summary>
		/// <example>32-bit</example>
		public string Architecture
		{
			get
			{
				Collect();
				return _architecture;
			}
			private set { SetProperty(ref _architecture, value); }
		}
		/// <summary>Additional system information.</summary>
		/// <example>1=Work Station;2=Domain Controller;3=Server</example>
		public ProductTypes ProductType
		{
			get
			{
				Collect();
				return _productType;
			}
			private set { SetProperty(ref _productType, value); }
		}
		/// <summary>Name of the registered user of the operating system.</summary>
		/// <example>Ben Smith</example>
		public string RegisteredUser
		{
			get
			{
				Collect();
				return _registeredUser;
			}
			private set { SetProperty(ref _registeredUser, value); }
		}
		/// <summary>Operating system product serial identification number.</summary>
		/// <example>10497-OEM-0031416-71674</example>
		public string SerialNumber
		{
			get
			{
				Collect();
				return _serialNumber;
			}
			private set { SetProperty(ref _serialNumber, value); }
		}
		/// <summary>Gets the current active user.</summary>
		public CsgOsUser CurrentUser
		{
			get
			{
				CollectUser();
				return _currentUser;
			}
			private set { SetProperty(ref _currentUser, value); }
		}
		/// <summary>Gets all available users.</summary>
		public CsgOsUser[] Users
		{
			get
			{
				CollectUser();
				return _users;
			}
			private set { SetProperty(ref _users, value); }
		}

		private void Collect()
		{
			if (_isCollected)
				return;

			try
			{
				var moc = new ManagementObjectSearcher("SELECT * FROM Win32_OperatingSystem").Get();
				foreach (var o in moc)
				{
					var mo = (ManagementObject) o;
					Name = mo.TryGet<string>("Caption");
					Description = mo.TryGet<string>("Description");
					CodeSet = mo.TryGet<string>("CodeSet");
					CountryCode = mo.TryGet<string>("CountryCode");
					OperatingSystemSku = (OperatingSystemSkus) mo.TryGet<UInt32>("OperatingSystemSKU");
					InstallDate = mo.TryGet<DateTime>("InstallDate");
					BuildType = mo.TryGet<string>("BuildType");
					ProductType = (ProductTypes) mo.TryGet<UInt32>("ProductType");
					Architecture = mo.TryGet<string>("OSArchitecture");
					Version = mo.TryGet<string>("Version");
					RegisteredUser = mo.TryGet<string>("RegisteredUser");
					SerialNumber = mo.TryGet<string>("SerialNumber");
					break;
				}
			}
			catch (Exception)
			{
			}
			_isCollected = true;
		}

		private void CollectUser()
		{
			if (_isUserCollected)
				return;
			CsgOsUser currentUser = null;
			try
			{
				var moc = new ManagementObjectSearcher("SELECT * FROM Win32_UserAccount").Get();
				var users = new List<CsgOsUser>();
				foreach (var o in moc)
				{
					var item = CsgOsUser.FromManagementObject((ManagementObject) o);
					users.Add(item);
					if (item.Name.Equals(Environment.UserName, StringComparison.InvariantCultureIgnoreCase))
						currentUser = item;
				}
				Users = users.ToArray();
			}
			catch (Exception)
			{
				Users = null;
			}
			finally
			{
				CurrentUser = currentUser ?? CsgOsUser.FromEnvironment();
			}
			_isUserCollected = true;
		}



		/// <summary>Stock Keeping Unit (SKU) number for the operating system.</summary>
		public enum OperatingSystemSkus : uint
		{
			/// <summary>An unknown product</summary>
			[EnumDescription("An unknown product")] Anunknownproduct = 0,
			/// <summary>Ultimate</summary>
			[EnumDescription("Ultimate")] Ultimate = 1,
			/// <summary>Home Basic</summary>
			[EnumDescription("Home Basic")] HomeBasic = 2,
			/// <summary>Home Premium</summary>
			[EnumDescription("Home Premium")] HomePremium = 3,
			/// <summary>Enterprise</summary>
			[EnumDescription("Enterprise")] Enterprise = 4,
			/// <summary>Home Basic N</summary>
			[EnumDescription("Home Basic N")] HomeBasicN = 5,
			/// <summary>Business</summary>
			[EnumDescription("Business")] Business = 6,
			/// <summary>Server Standard</summary>
			[EnumDescription("Server Standard")] ServerStandard = 7,
			/// <summary>Server Datacenter (full installation)</summary>
			[EnumDescription("Server Datacenter (full installation)")] ServerDatacenterFullinstallation = 8,
			/// <summary>Windows Small Business Server</summary>
			[EnumDescription("Windows Small Business Server")] WindowsSmallBusinessServer = 9,
			/// <summary>Server Enterprise (full installation)</summary>
			[EnumDescription("Server Enterprise (full installation)")] ServerEnterpriseFullinstallation = 10,
			/// <summary>Starter</summary>
			[EnumDescription("Starter")] Starter = 11,
			/// <summary>Server Datacenter (core installation)</summary>
			[EnumDescription("Server Datacenter (core installation)")] ServerDatacenterCoreinstallation = 12,
			/// <summary>Server Standard (core installation)</summary>
			[EnumDescription("Server Standard (core installation)")] ServerStandardCoreinstallation = 13,
			/// <summary>Server Enterprise (core installation)</summary>
			[EnumDescription("Server Enterprise (core installation)")] ServerEnterpriseCoreinstallation = 14,
			/// <summary>Server Enterprise for Itanium-based Systems</summary>
			[EnumDescription("Server Enterprise for Itanium-based Systems")] ServerEnterpriseforItaniumBasedSystems = 15,
			/// <summary>Business N</summary>
			[EnumDescription("Business N")] BusinessN = 16,
			/// <summary>Web Server (full installation)</summary>
			[EnumDescription("Web Server (full installation)")] WebServerFullinstallation = 17,
			/// <summary>HPC Edition</summary>
			[EnumDescription("HPC Edition")] HpcEdition = 18,
			/// <summary>Windows Storage Server 2008 R2 Essentials</summary>
			[EnumDescription("Windows Storage Server 2008 R2 Essentials")] WindowsStorageServer2008R2Essentials = 19,
			/// <summary>Storage Server Express</summary>
			[EnumDescription("Storage Server Express")] StorageServerExpress = 20,
			/// <summary>Storage Server Standard</summary>
			[EnumDescription("Storage Server Standard")] StorageServerStandard = 21,
			/// <summary>Storage Server Workgroup</summary>
			[EnumDescription("Storage Server Workgroup")] StorageServerWorkgroup = 22,
			/// <summary>Storage Server Enterprise</summary>
			[EnumDescription("Storage Server Enterprise")] StorageServerEnterprise = 23,
			/// <summary>Windows Server 2008 for Windows Essential Server Solutions</summary>
			[EnumDescription("Windows Server 2008 for Windows Essential Server Solutions")] WindowsServer2008ForWindowsEssentialServerSolutions = 24,
			/// <summary>Small Business Server Premium</summary>
			[EnumDescription("Small Business Server Premium")] SmallBusinessServerPremium = 25,
			/// <summary>Home Premium N</summary>
			[EnumDescription("Home Premium N")] HomePremiumN = 26,
			/// <summary>Enterprise N</summary>
			[EnumDescription("Enterprise N")] EnterpriseN = 27,
			/// <summary>Ultimate N</summary>
			[EnumDescription("Ultimate N")] UltimateN = 28,
			/// <summary>Web Server (core installation)</summary>
			[EnumDescription("Web Server (core installation)")] WebServerCoreinstallation = 29,
			/// <summary>Windows Essential Business Server Management Server</summary>
			[EnumDescription("Windows Essential Business Server Management Server")] WindowsEssentialBusinessServerManagementServer = 30,
			/// <summary>Windows Essential Business Server Security Server</summary>
			[EnumDescription("Windows Essential Business Server Security Server")] WindowsEssentialBusinessServerSecurityServer = 31,
			/// <summary>Windows Essential Business Server Messaging Server</summary>
			[EnumDescription("Windows Essential Business Server Messaging Server")] WindowsEssentialBusinessServerMessagingServer = 32,
			/// <summary>Server Foundation</summary>
			[EnumDescription("Server Foundation")] ServerFoundation = 33,
			/// <summary>Windows Home Server 2011</summary>
			[EnumDescription("Windows Home Server 2011")] WindowsHomeServer2011 = 34,
			/// <summary>Windows Server 2008 without Hyper-V for Windows Essential Server Solutions</summary>
			[EnumDescription("Windows Server 2008 without Hyper-V for Windows Essential Server Solutions")] WindowsServer2008WithoutHyperVforWindowsEssentialServerSolutions = 35,
			/// <summary>Server Standard without Hyper-V</summary>
			[EnumDescription("Server Standard without Hyper-V")] ServerStandardwithoutHyperV = 36,
			/// <summary>Server Datacenter without Hyper-V (full installation)</summary>
			[EnumDescription("Server Datacenter without Hyper-V (full installation)")] ServerDatacenterwithoutHyperVFullinstallation = 37,
			/// <summary>Server Enterprise without Hyper-V (full installation)</summary>
			[EnumDescription("Server Enterprise without Hyper-V (full installation)")] ServerEnterprisewithoutHyperVFullinstallation = 38,
			/// <summary>Server Datacenter without Hyper-V (core installation)</summary>
			[EnumDescription("Server Datacenter without Hyper-V (core installation)")] ServerDatacenterwithoutHyperVCoreinstallation = 39,
			/// <summary>Server Standard without Hyper-V (core installation)</summary>
			[EnumDescription("Server Standard without Hyper-V (core installation)")] ServerStandardwithoutHyperVCoreinstallation = 40,
			/// <summary>Server Enterprise without Hyper-V (core installation)</summary>
			[EnumDescription("Server Enterprise without Hyper-V (core installation)")] ServerEnterprisewithoutHyperVCoreinstallation = 41,
			/// <summary>Microsoft Hyper-V Server</summary>
			[EnumDescription("Microsoft Hyper-V Server")] MicrosoftHyperVServer = 42,
			/// <summary>Storage Server Express (core installation)</summary>
			[EnumDescription("Storage Server Express (core installation)")] StorageServerExpressCoreinstallation = 43,
			/// <summary>Storage Server Standard (core installation)</summary>
			[EnumDescription("Storage Server Standard (core installation)")] StorageServerStandardCoreinstallation = 44,
			/// <summary>Storage Server Workgroup (core installation)</summary>
			[EnumDescription("Storage Server Workgroup (core installation)")] StorageServerWorkgroupCoreinstallation = 45,
			/// <summary>Storage Server Enterprise (core installation)</summary>
			[EnumDescription("Storage Server Enterprise (core installation)")] StorageServerEnterpriseCoreinstallation = 46,
			/// <summary>Starter N</summary>
			[EnumDescription("Starter N")] StarterN = 47,
			/// <summary>Professional</summary>
			[EnumDescription("Professional")] Professional = 48,
			/// <summary>Professional N</summary>
			[EnumDescription("Professional N")] ProfessionalN = 49,
			/// <summary>Windows Small Business Server 2011 Essentials</summary>
			[EnumDescription("Windows Small Business Server 2011 Essentials")] WindowsSmallBusinessServer2011Essentials = 50,
			/// <summary>Server For SB Solutions</summary>
			[EnumDescription("Server For SB Solutions")] ServerForSbSolutions = 51,
			/// <summary>Server Solutions Premium</summary>
			[EnumDescription("Server Solutions Premium")] ServerSolutionsPremium = 52,
			/// <summary>Server Solutions Premium (core installation)</summary>
			[EnumDescription("Server Solutions Premium (core installation)")] ServerSolutionsPremiumCoreinstallation = 53,
			/// <summary>Server For SB Solutions EM</summary>
			[EnumDescription("Server For SB Solutions EM")] ServerForSbSolutionsEm54 = 54,
			/// <summary>Server For SB Solutions EM</summary>
			[EnumDescription("Server For SB Solutions EM")] ServerForSbSolutionsEm55 = 55,
			/// <summary>Windows MultiPoint Server</summary>
			[EnumDescription("Windows MultiPoint Server")] WindowsMultiPointServer = 56,
			/// <summary>Windows Essential Server Solution Management</summary>
			[EnumDescription("Windows Essential Server Solution Management")] WindowsEssentialServerSolutionManagement = 59,
			/// <summary>Windows Essential Server Solution Additional</summary>
			[EnumDescription("Windows Essential Server Solution Additional")] WindowsEssentialServerSolutionAdditional = 60,
			/// <summary>Windows Essential Server Solution Management SVC</summary>
			[EnumDescription("Windows Essential Server Solution Management SVC")] WindowsEssentialServerSolutionManagementSvc = 61,
			/// <summary>Windows Essential Server Solution Additional SVC</summary>
			[EnumDescription("Windows Essential Server Solution Additional SVC")] WindowsEssentialServerSolutionAdditionalSvc = 62,
			/// <summary>Small Business Server Premium (core installation)</summary>
			[EnumDescription("Small Business Server Premium (core installation)")] SmallBusinessServerPremiumCoreinstallation = 63,
			/// <summary>Server Hyper Core V</summary>
			[EnumDescription("Server Hyper Core V")] ServerHyperCoreV = 64,
			/// <summary>Starter E</summary>
			[EnumDescription("Starter E")] StarterE = 66,
			/// <summary>Home Basic E</summary>
			[EnumDescription("Home Basic E")] HomeBasicE = 67,
			/// <summary>Home Premium E</summary>
			[EnumDescription("Home Premium E")] HomePremiumE = 68,
			/// <summary>Professional E</summary>
			[EnumDescription("Professional E")] ProfessionalE = 69,
			/// <summary>Enterprise E</summary>
			[EnumDescription("Enterprise E")] EnterpriseE = 70,
			/// <summary>Ultimate E</summary>
			[EnumDescription("Ultimate E")] UltimateE = 71,
			/// <summary>Server Enterprise (evaluation installation)</summary>
			[EnumDescription("Server Enterprise (evaluation installation)")] ServerEnterpriseEvaluationinstallation = 72,
			/// <summary>Windows MultiPoint Server Standard (full installation)</summary>
			[EnumDescription("Windows MultiPoint Server Standard (full installation)")] WindowsMultiPointServerStandardFullinstallation = 76,
			/// <summary>Windows MultiPoint Server Premium (full installation)</summary>
			[EnumDescription("Windows MultiPoint Server Premium (full installation)")] WindowsMultiPointServerPremiumFullinstallation = 77,
			/// <summary>Server Standard (evaluation installation)</summary>
			[EnumDescription("Server Standard (evaluation installation)")] ServerStandardEvaluationinstallation = 79,
			/// <summary>Server Datacenter (evaluation installation)</summary>
			[EnumDescription("Server Datacenter (evaluation installation)")] ServerDatacenterEvaluationinstallation = 80,
			/// <summary>Enterprise N (evaluation installation)</summary>
			[EnumDescription("Enterprise N (evaluation installation)")] EnterpriseNEvaluationinstallation = 84,
			/// <summary>Storage Server Workgroup (evaluation installation)</summary>
			[EnumDescription("Storage Server Workgroup (evaluation installation)")] StorageServerWorkgroupEvaluationinstallation = 95,
			/// <summary>Storage Server Standard (evaluation installation)</summary>
			[EnumDescription("Storage Server Standard (evaluation installation)")] StorageServerStandardEvaluationinstallation = 96,
			/// <summary>Windows 8 N</summary>
			[EnumDescription("Windows 8 N")] Windows8N = 98,
			/// <summary>Windows 8 China</summary>
			[EnumDescription("Windows 8 China")] Windows8China = 99,
			/// <summary>Windows 8 Single Language</summary>
			[EnumDescription("Windows 8 Single Language")] Windows8SingleLanguage = 100,
			/// <summary>Windows 8</summary>
			[EnumDescription("Windows 8")] Windows8 = 101,
			/// <summary>Professional with Media Center</summary>
			[EnumDescription("Professional with Media Center")] ProfessionalwithMediaCenter = 103,

		}



		/// <summary>Product types</summary>
		public enum ProductTypes : uint
		{
			/// <summary>Work Station</summary>
			[EnumDescription("Work Station")] WorkStation = 1,
			/// <summary>Domain Controller</summary>
			[EnumDescription("Domain Controller")] DomainController = 2,
			/// <summary>Server</summary>
			[EnumDescription("Server")] Server = 3
		}
	}
}