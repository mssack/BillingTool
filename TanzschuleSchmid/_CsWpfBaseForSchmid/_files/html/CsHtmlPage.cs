// Copyright (c) 2014, 2015 All Right Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2015-07-24</date>

using System;
using System.Globalization;
using System.IO;
using System.Windows.Media.Imaging;
using CsWpfBase.Ev.Public;
using CsWpfBase.Ev.Public.Extensions;
using CsWpfBase.Global;
using CsWpfBase.Utilitys.templates;






namespace CsWpfBase._files.html
{
	/// <summary>The default base file template for html pages. If you are generating html files this should be your base.</summary>
	[Serializable]
	public sealed class CsHtmlPage : FileTemplate
	{
		private static readonly string HtmlStyle = CsGlobal.Storage.Resource.File.Read(CsGlobal.Storage.Resource.Path.Get("CsWpfBase", "_files/html/css/default.css"));
		private static readonly string HtmlScripts = CsGlobal.Storage.Resource.File.Read(CsGlobal.Storage.Resource.Path.Get("CsWpfBase", "_files/html/scripts/TableSortScript.js"));

		private string _content;
		private string _copyRights = CsGlobal.App.Info.Copyright;
		private string _creationDate = DateTime.Now.ToString(CultureInfo.InvariantCulture);
		private string _head;
		private string _headerText;
		private Hdi _hiddenDocInfo;
		private string _hiddenDocumentText = "Created by " + CsGlobal.App.Info.ProductTitle + "\r\nProduced by '" + Environment.MachineName + "' with user '" + Environment.UserName + "'";
		private string _icon;
		private string _scripts = HtmlScripts;
		private string _styles = HtmlStyle;
		private string _title;


		#region Overrides/Interfaces
		/// <summary>The template file name. The name of the resource file at the same name space as the source file.</summary>
		protected override string TemplateFileName
		{
			get { return GetType().Name + ".cs.html"; }
		}
		#endregion


		/// <summary>Gets or sets the associated key.</summary>
		[Key(FullName = "#DocInfo#")]
		public Hdi HiddenDocInfo
		{
			get { return _hiddenDocInfo ?? (_hiddenDocInfo = new Hdi()); }
			set { SetProperty(ref _hiddenDocInfo, value); }
		}
		/// <summary>Gets or sets the associated key.</summary>
		[Key]
		public string HiddenDocumentText
		{
			get { return _hiddenDocumentText; }
			set { SetProperty(ref _hiddenDocumentText, value); }
		}
		/// <summary>Gets or sets the associated key.</summary>
		[Key]
		public string Title
		{
			get { return _title; }
			set { SetProperty(ref _title, value); }
		}
		/// <summary>Gets or sets the associated key.</summary>
		[Key(ValuePrefix = "<style type=\"text/css\">\r\n", ValueSuffix = "\r\n</style>")]
		public string Styles
		{
			get { return _styles; }
			set { SetProperty(ref _styles, value); }
		}
		/// <summary>Gets or sets the associated key.</summary>
		[Key(ValuePrefix = "<script type=\"text/javascript\">\r\n", ValueSuffix = "\r\n</script>")]
		public string Scripts
		{
			get { return _scripts; }
			set { SetProperty(ref _scripts, value); }
		}
		/// <summary>Gets or sets the associated key.</summary>
		[Key]
		public string Head
		{
			get { return _head; }
			set { SetProperty(ref _head, value); }
		}
		/// <summary>Gets or sets the associated key.</summary>
		[Key(ValuePrefix = "<img src=\"data:image/png;base64,\r\n", ValueSuffix = "\r\n\" />")]
		public string Icon
		{
			get { return _icon; }
			set { SetProperty(ref _icon, value); }
		}
		/// <summary>Gets or sets the associated key.</summary>
		[Key]
		public string HeaderText
		{
			get { return _headerText; }
			set { SetProperty(ref _headerText, value); }
		}
		/// <summary>Gets or sets the associated key.</summary>
		[Key]
		public string Content
		{
			get { return _content; }
			set { SetProperty(ref _content, value); }
		}
		/// <summary>Gets or sets the associated key.</summary>
		[Key]
		public string CreationDate
		{
			get { return _creationDate; }
			set { SetProperty(ref _creationDate, value); }
		}
		/// <summary>Gets or sets the associated key.</summary>
		[Key]
		public string CopyRights
		{
			get { return _copyRights; }
			set { SetProperty(ref _copyRights, value); }
		}


		/// <summary>Sets the <see cref="Icon" /> field appropriate.</summary>
		public void SetIcon(BitmapSource image)
		{
			Icon = image == null ? "" : image.ConvertTo_PngByteArray().ConvertTo_Base64();
		}





		/// <summary>Represents hidden document informations.</summary>
		[Serializable]
		public class Hdi : FileTemplate
		{
			private string _copyRights = CsGlobal.App.Info.Copyright;
			private DateTime? _creationDate = DateTime.Now;
			private Guid? _id = Guid.NewGuid();
			private string _title;
			private string _type = CsGlobal.App.Info.Name + "-html";


			#region Overrides/Interfaces
			/// <summary>At default the template string will be loaded from an .txt file with the exact same name of the source code file name with an .txt appended.</summary>
			protected override string TemplateString
			{
				get
				{
					return
						"<!--DocInfo\r\n" +
						"Type=#Type#\r\n" +
						"Id=#Id#\r\n" +
						"Title=#Title#\r\n" +
						"CreationDate=#CreationDate#\r\n" +
						"CopyRights=#CopyRights#\r\n" +
						"DocInfo-->\r\n\r\n\r\n";
				}
			}
			#endregion


			/// <summary>Gets or sets the associated key.</summary>
			[Key]
			public string Type
			{
				get { return _type; }
				set { SetProperty(ref _type, value); }
			}

			/// <summary>Gets or sets the associated key.</summary>
			[Key]
			public Guid? Id
			{
				get { return _id; }
				set { SetProperty(ref _id, value); }
			}

			/// <summary>Gets or sets the associated key.</summary>
			[Key]
			public string Title
			{
				get { return _title; }
				set { SetProperty(ref _title, value); }
			}

			/// <summary>Gets or sets the associated key.</summary>
			[Key(StringFormat = "{0:dd.MM.yyyy HH:mm:ss}")]
			public DateTime? CreationDate
			{
				get { return _creationDate; }
				set { SetProperty(ref _creationDate, value); }
			}

			/// <summary>Gets or sets the associated key.</summary>
			[Key]
			public string CopyRights
			{
				get { return _copyRights; }
				set { SetProperty(ref _copyRights, value); }
			}

			/// <summary>Parses the beginning of a file to match the DocInfo.</summary>
			public static Hdi Parse(FileInfo fi)
			{
				var rv = new Hdi();
				using (var fs = fi.Open(FileMode.Open))
				{
					using (var rs = new StreamReader(fs))
					{
						if (rs.ReadLine() != "<!--DocInfo")
							return null;

						string line;
						while ((line = rs.ReadLine()) != null)
						{
							line = line.Trim(new[] {' ', '\t'});
							string key;

							if (line.StartsWith((key = "Type=")))
								rv.Type = line.Substring(key.Length);
							else if (line.StartsWith((key = "Id=")))
								rv.Id = Guid.Parse(line.Substring(key.Length));
							else if (line.StartsWith((key = "Name=")))
								rv.Title = line.Substring(key.Length);
							else if (line.StartsWith((key = "CreationDate=")))
								rv.CreationDate = DateTime.ParseExact(line.Substring(key.Length), "dd.MM.yyyy HH:mm:ss", null);
							else if (line.StartsWith((key = "CopyRights=")))
								rv.CopyRights = line.Substring(key.Length);
							else
								break;
						}
					}
				}
				return rv;
			}
		}
	}
}