// Copyright (c) 2014, 2015 All Right Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2015-07-24</date>

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows;
using CsWpfBase.Ev.Exceptions;
using CsWpfBase.Ev.Objects;






namespace CsWpfBase.Global.app
{
	/// <summary>Internal see at <see cref="CsGlobal" />.</summary>
	[Serializable]
	public sealed class CsgAppInfo : Base
	{
		private static CsgAppInfo _instance;
		/// <summary>Returns the singleton instance</summary>
		internal static CsgAppInfo I
		{
			get
			{
				if (_instance == null)
				{
					if (Application.Current == null)
						throw new CsGlobalException("This scope is not provided outside of WPF Applications.");
					_instance = new CsgAppInfo();
					_instance.CollectInfos();
				}
				return _instance;
			}
		}
		private List<string> _attachedFiles;
		private string _company;
		private string _copyright;
		private string _description;
		private Guid _id;
		private string _name;
		private FileInfo _processFile;
		private string _product;
		private string _productTitle;
		private string _version;

		private CsgAppInfo()
		{
			CollectInfos();
		}

		/// <summary>Gets the assembly id, defined in project properties.</summary>
		public Guid Id
		{
			get
			{
				if (_id == Guid.Empty)
					throw new CsGlobalException("No assembly guid defined.");
				return _id;
			}
			private set { SetProperty(ref _id, value); }
		}
		/// <summary>Gets the assembly name, defined in project properties.</summary>
		public string Name
		{
			get { return _name; }
			private set { SetProperty(ref _name, value); }
		}
		/// <summary>Gets the file path to the current process.</summary>
		public FileInfo ProcessFile
		{
			get { return _processFile; }
			private set { SetProperty(ref _processFile, value); }
		}
		/// <summary>Gets the product value, defined in project properties under the assembly informations.</summary>
		public string Product
		{
			get { return _product; }
			private set { SetProperty(ref _product, value); }
		}
		/// <summary>Gets the product title value, defined in project properties under the assembly informations.</summary>
		public string ProductTitle
		{
			get { return _productTitle; }
			private set { SetProperty(ref _productTitle, value); }
		}
		/// <summary>Gets the assembly version, defined in project properties under the assembly informations.</summary>
		public string Version
		{
			get { return _version; }
			private set { SetProperty(ref _version, value); }
		}
		/// <summary>Gets the description, defined in project properties under the assembly informations.</summary>
		public string Description
		{
			get { return _description; }
			private set { SetProperty(ref _description, value); }
		}
		/// <summary>Gets the copyright, defined in project properties under the assembly informations.</summary>
		public string Copyright
		{
			get { return _copyright; }
			private set { SetProperty(ref _copyright, value); }
		}
		/// <summary>Gets the company, defined in project properties under the assembly informations.</summary>
		public string Company
		{
			get { return _company; }
			private set { SetProperty(ref _company, value); }
		}
		/// <summary>Gets the attached files to the '.exe' file. All referenced dll's should be included and also all files.</summary>
		public List<string> AttachedFiles
		{
			get { return _attachedFiles ?? (_attachedFiles = new List<string>()); }
		}

		private void CollectInfos()
		{
			var entryAssembly = Assembly.GetEntryAssembly();

			if (entryAssembly == null)
			{
				return;
			}

			var attributes = entryAssembly.GetCustomAttributes(typeof (Attribute), false).Cast<Attribute>().ToArray();
			var assemblyName = entryAssembly.GetName();

			var guidAttribute = ExtractAttrValue<GuidAttribute>(attributes, atr => atr.Value);

			Name = assemblyName.Name;
			Id = guidAttribute == null ? Guid.Empty : Guid.Parse(guidAttribute);
			ProcessFile = new FileInfo(Process.GetCurrentProcess().MainModule.FileName);
			ProductTitle = ExtractAttrValue<AssemblyTitleAttribute>(attributes, atr => atr.Title, Path.GetFileNameWithoutExtension(entryAssembly.CodeBase));
			Version = assemblyName.Version == null ? "1.0.0.0" : assemblyName.Version.ToString();
			Description = ExtractAttrValue<AssemblyDescriptionAttribute>(attributes, atr => atr.Description);
			Product = ExtractAttrValue<AssemblyProductAttribute>(attributes, atr => atr.Product);
			Copyright = ExtractAttrValue<AssemblyCopyrightAttribute>(attributes, atr => atr.Copyright);
			Company = ExtractAttrValue<AssemblyCompanyAttribute>(attributes, atr => atr.Company);



			//TODO Only direct referencedAssembly copied ERROR
			Assembly.ReflectionOnlyLoadFrom(entryAssembly.Location).GetReferencedAssemblies().Select(x => x.Name + ".dll").Where(x => new FileInfo(x).Exists).ToList().ForEach(x => AttachedFiles.Add(x));
		}

		private static string ExtractAttrValue<TAttr>(IEnumerable<Attribute> attributes, Func<TAttr, string> resolveFunc, string defaultResult = null) where TAttr : Attribute
		{
			var attribute = attributes.FirstOrDefault(x => x is TAttr);
			return attribute != null ? resolveFunc((TAttr) attribute) : defaultResult;
		}
	}
}