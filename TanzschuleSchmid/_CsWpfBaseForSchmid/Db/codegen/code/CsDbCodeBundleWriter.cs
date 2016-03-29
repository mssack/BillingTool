// Copyright (c) 2014 - 2016 All Rights Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-01-03</date>

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Linq;
using CsWpfBase.Db.codegen.code.files;
using CsWpfBase.Ev.Public.Extensions;
using CsWpfBase.Global;
using CsWpfBase.Utilitys.templates;






namespace CsWpfBase.Db.codegen.code
{
	/// <summary>Writes a code bundle to a project.</summary>
	internal class CsDbCodeBundleWriter
	{
		private static readonly Regex GetHashRegex = new Regex("Hash[ \t]*=[ \t]*\"(.*?)\"");
		private static readonly Regex GetCsDbRegionRegex = new Regex(@"\/\/START csdb\.dbserver3[\s\S]*?\/\/END csdb\.dbserver3");
		private bool _checkhash = true;
		private List<string> _generatedFiles;

		internal CsDbCodeBundleWriter(CsDbCodeBundle codeBundle)
		{
			CodeBundle = codeBundle;
		}


		/// <summary>The basis of this writer.</summary>
		public CsDbCodeBundle CodeBundle { get; }

		/// <summary>The files which have been generated (Created or modified) by the writer.</summary>
		private List<string> GeneratedFiles => _generatedFiles ?? (_generatedFiles = new List<string>());

		/// <summary>Starts the generator</summary>
		public void Start(bool checkHash = true)
		{
			if (string.IsNullOrEmpty(CodeBundle.Directory))
				throw new InvalidOperationException("The code bundle directory have to be defined.");
			if (string.IsNullOrEmpty(CodeBundle.NameSpace))
				throw new InvalidOperationException("The code bundle name space have to be defined.");
			if (string.IsNullOrEmpty(CodeBundle.ProjectFilePath))
				throw new InvalidOperationException("The project file path have to be defined.");

			_checkhash = checkHash;

			CsDb.CodeGen.Tracing.Trace($"Start writing code bundle '{CodeBundle.Architecture.Name}' with {CodeBundle.Databases.Length} Databases [{CodeBundle.Databases.Select(x=>x.Architecture.Name).Join()}]");
			
			WriteDataContext();
			WriteDatabases();
			WriteAssemblyFile();
			WriteProjectFile();
		}

		private void WriteDataContext()
		{
			CsDb.CodeGen.Tracing.Trace($"Writing datacontext files", 1);
			var fi = new FileInfo(Path.Combine(CodeBundle.Directory, CodeBundle.Context.Name + CodeBundle.FileExtension));

			var ns = new CsDbCodeNamespace { Namespace = CodeBundle.NameSpace };
			ns.Usings.AddRange(CodeBundle.Databases.Select(x=>x.DataSetNameSpace));

			WriteFile(fi, ns, CodeBundle.Context);
		}
		private void WriteDatabases()
		{
			foreach (var dbBundle in CodeBundle.Databases)
			{
				CsDb.CodeGen.Tracing.Trace($"BEGIN with database {dbBundle.Architecture.Name}",1);
				WriteDataSet(dbBundle);
				WriteTables(dbBundle);
				WriteViews(dbBundle);
				WriteInterfaces(dbBundle);
				WriteRows(dbBundle);
			}
		}
		private void WriteDataSet(CsDbCodeBundleForDb dbBundle)
		{
			CsDb.CodeGen.Tracing.Trace($"Writing dataset", 2);
			var fi = new FileInfo(Path.Combine(dbBundle.DataSetDirectory, dbBundle.DataSet.Name + CodeBundle.FileExtension));

			var ns = new CsDbCodeNamespace { Namespace = dbBundle.DataSetNameSpace };
			ns.Usings.Add(dbBundle.ViewsNameSpace);
			ns.Usings.Add(dbBundle.RowsNameSpace);
			ns.Usings.Add(dbBundle.TablesNameSpace);
			ns.Usings.Add(CodeBundle.NameSpace);

			WriteFile(fi, ns, dbBundle.DataSet);
		}

		private void WriteTables(CsDbCodeBundleForDb dbBundle)
		{
			CsDb.CodeGen.Tracing.Trace($"Writing {dbBundle.Tables.Length} Tables", 2);
			foreach (var table in dbBundle.Tables)
			{
				var fi = new FileInfo(Path.Combine(dbBundle.TablesDirectory, table.Name + CodeBundle.FileExtension));

				var ns = new CsDbCodeNamespace { Namespace = dbBundle.TablesNameSpace };

				ns.Usings.Add(dbBundle.DataSetNameSpace);
				ns.Usings.Add($"{table.Row.Name}={dbBundle.RowsNameSpace}.{table.Row.Name}");
				ns.Usings.Add($"{table.Row.Interface.Name}={dbBundle.RowInterfacesNameSpace}.{table.Row.Interface.Name}");
				ns.Usings.Add(CodeBundle.NameSpace);

				WriteFile(fi, ns, table);
			}
		}

		private void WriteViews(CsDbCodeBundleForDb dbBundle)
		{
			CsDb.CodeGen.Tracing.Trace($"Writing {dbBundle.Views.Length} Tables", 2);
			foreach (var view in dbBundle.Views)
			{
				var fi = new FileInfo(Path.Combine(dbBundle.ViewsDirectory, view.Name + CodeBundle.FileExtension));

				var ns = new CsDbCodeNamespace { Namespace = dbBundle.ViewsNameSpace };

				ns.Usings.Add(dbBundle.DataSetNameSpace);
				ns.Usings.Add($"{view.Row.Name}={dbBundle.RowsNameSpace}.{view.Row.Name}");

				WriteFile(fi, ns, view);
			}
		}

		private void WriteInterfaces(CsDbCodeBundleForDb dbBundle)
		{
			CsDb.CodeGen.Tracing.Trace($"Writing {dbBundle.Tables.Length} row interfaces", 2);
			foreach (var @interface in dbBundle.RowInterfaces)
			{
				var fi = new FileInfo(Path.Combine(dbBundle.RowInterfacesDirectory, @interface.Name + CodeBundle.FileExtension));

				var ns = new CsDbCodeNamespace { Namespace = dbBundle.RowInterfacesNameSpace };

				ns.Usings.Add($"{@interface.Name}={dbBundle.RowInterfacesNameSpace}.{@interface.Name}");

				WriteFile(fi, ns, @interface);
			}
		}

		private void WriteRows(CsDbCodeBundleForDb dbBundle)
		{
			CsDb.CodeGen.Tracing.Trace($"Writing {dbBundle.Tables.Length + dbBundle.Views.Length} rows", 2);
			foreach (var model in dbBundle.Tables.Select(x => x.Row))
			{
				var fi = new FileInfo(Path.Combine(dbBundle.RowsDirectory, model.Name + CodeBundle.FileExtension));

				var ns = new CsDbCodeNamespace { Namespace = dbBundle.RowsNameSpace };
				ns.Usings.Add(dbBundle.DataSetNameSpace);
				ns.Usings.Add(dbBundle.TablesNameSpace);
				ns.Usings.Add(dbBundle.RowInterfacesNameSpace);

				WriteFile(fi, ns, model);
			}
			foreach (var model in dbBundle.Views.Select(x => x.Row))
			{
				var fi = new FileInfo(Path.Combine(dbBundle.RowsDirectory, model.Name + CodeBundle.FileExtension));

				var ns = new CsDbCodeNamespace { Namespace = dbBundle.RowsNameSpace };
				ns.Usings.Add(dbBundle.DataSetNameSpace);
				ns.Usings.Add(dbBundle.ViewsNameSpace);

				WriteFile(fi, ns, model);
			}
		}

		private void WriteAssemblyFile()
		{
			var fi = new FileInfo(Path.Combine(new FileInfo(CodeBundle.ProjectFilePath).DirectoryName, "Properties", "AssemblyInfo.cs"));

			if (!fi.Exists)
				return;

			var newcontent = "//START csdb.dbserver3\r\n" + CodeBundle.Databases.Select(x =>
			{
				string prefix = $"{CodeBundle.Architecture.Name}.{x.DataSet.Name}";
				string name = $"{x.DataSet.Name}Db";

				string[] namespaces = {x.DataSetNameSpace, x.TablesNameSpace, x.RowsNameSpace, x.ViewsNameSpace, x.RowInterfacesNameSpace};

				return $"[assembly: XmlnsPrefix(\"{prefix}\", \"{name}\")]" + "\r\n" + namespaces.Select(ns => $"[assembly: XmlnsDefinition(\"{prefix}\", \"{ns}\")]").Join("\r\n");
			}).Join("\r\n\r\n") + "\r\n//END csdb.dbserver3";

			

			var content = fi.LoadAs_UnicodeString();
			var match = GetCsDbRegionRegex.Match(content);

			if (match.Success)
			{
				content  = GetCsDbRegionRegex.Replace(content, newcontent);
			}
			else
			{
				content = content + "\r\n\r\n" + newcontent;
			}

			fi.DeleteFile_IfExists();
			content.SaveAs_UnicodeString(fi);
		}
		private void WriteProjectFile()
		{
			CsDb.CodeGen.Tracing.Trace($"Writing project file: changed files: {GeneratedFiles.Count}", 1);
			var fi = new FileInfo(CodeBundle.ProjectFilePath);
			if (!fi.Exists)
				throw new InvalidOperationException("Project have to exist in order to save it");

			var doc = XDocument.Load(fi.FullName);
			var project = doc.Root;
			var ns = project.GetDefaultNamespace();

			var existingGroups = project.Descendants().Where(x => x.Name.LocalName == "ItemGroup");
			var insertTarget = existingGroups.Last();


			var oldItems = existingGroups.SelectMany(gr => gr.Descendants().Where(x => x.Attribute("Label") != null && x.Attribute("Label").Value == $"CsWpfBase.Db.codegen.[{CodeBundle.Architecture.Name}]")).ToArray();
			var newItems = GeneratedFiles.Select(x => x.Replace(fi.Directory.FullName + "\\", "")).ToArray();

			var filesToDelete = oldItems.Select(x => x.Attribute(XName.Get("Include")).Value).Except(newItems).ToArray();


			foreach (var item in oldItems)
			{
				item.Remove();
			}


			foreach (var s in _generatedFiles)
			{
				var relativeFile = s.Replace(fi.Directory.FullName + "\\", "");
				var node = new XElement(ns + "Compile");
				node.SetAttributeValue("Include", relativeFile);
				node.SetAttributeValue("Label", $"CsWpfBase.Db.codegen.[{CodeBundle.Architecture.Name}]");
				insertTarget.Add(node);
			}


			using (var xmlWriter = new XmlTextWriter(fi.FullName, Encoding.UTF8) { Formatting = Formatting.Indented, IndentChar = ' ', Indentation = 2 })
			{
				doc.Save(xmlWriter);
				xmlWriter.Close();
			}


			foreach (var relativePath in filesToDelete)
			{
				var fileInfo = new FileInfo(Path.Combine(fi.Directory.FullName, relativePath));
				fileInfo.DeleteFile_IfExists();
			}
		}



		private void WriteFile(FileInfo file, CsDbCodeNamespace codeNameSpace, FileTemplate content)
		{
			GeneratedFiles.Add(file.FullName);
			if (file.Exists && IsFileUnchanged(file, content.GetHash()))
			{
				return;
			}
			file.CreateDirectory_IfNotExists();
			codeNameSpace.Content = content.GetString(1);

			using (var wr = new StreamWriter(file.FullName, false, Encoding.Unicode))
			{
				wr.Write(codeNameSpace.GetString());
			}
		}

		private bool IsFileUnchanged(FileInfo fi, string fileHash)
		{
			if (!_checkhash)
				return false;

			string hash = null;
			using (Stream sr = fi.Open(FileMode.Open))
			{
				using (var reader = new StreamReader(sr, Encoding.Unicode))
				{
					var finished = false;
					while (!reader.EndOfStream && !finished)
					{
						var line = reader.ReadLine();
						if (string.IsNullOrEmpty(line))
							continue;
						var match = GetHashRegex.Match(line);
						if (match.Success)
						{
							hash = match.Groups[1].Value;
							finished = true;
						}
					}
				}
			}
			if (hash == fileHash)
				return true;
			return false;
		}
	}
}