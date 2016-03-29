// Copyright (c) 2014, 2015 All Right Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2015-07-24</date>

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CsWpfBase.Ev.Objects;
using CsWpfBase.Ev.Public.Extensions;






namespace CsWpfBase.Global.storage.scopes
{
	/// <summary>A scope is used for handled serialization and deserialization. It stores all instances and take care of saving at the application exit.</summary>
	[Serializable]
	public class CsgStorageScope : Base
	{
		private readonly Dictionary<string, FileHandle> _handles = new Dictionary<string, FileHandle>();
		private DirectoryInfo _directory;
		private string _extension;

		/// <summary>Creates a new scope with the defined directory and extension.</summary>
		public CsgStorageScope(DirectoryInfo directory, string extension)
		{
			CsGlobal.App.OnExit += args => Save();
			Directory = directory;
			Extension = extension;
		}

		/// <summary>Gets or sets the Extension.</summary>
		public string Extension
		{
			get { return _extension ?? (_extension = ".txt"); }
			private set { SetProperty(ref _extension, value); }
		}
		/// <summary>Gets or sets the Directory.</summary>
		public DirectoryInfo Directory
		{
			get { return _directory; }
			private set { SetProperty(ref _directory, value); }
		}

		/// <summary>Handles a serialize able object by an id (= filename). It will be save on application exit.</summary>
		/// <typeparam name="TObjectType">The type of the serialize able object.</typeparam>
		/// <param name="id">The id to associate with the object, this id is also the filename.</param>
		/// <param name="createFunc">The function which will be used if the object does not exist.</param>
		public TObjectType Handle<TObjectType>(string id, Func<TObjectType> createFunc) where TObjectType : class
		{
			FileHandle handle;
			if (_handles.TryGetValue(id, out handle))
				return (TObjectType) handle.Reference;

			TObjectType reference = null;

			var file = GetFilePathByName(id);
			if (file.Exists)
			{
				try
				{
					reference = file.LoadAs_Object_From_SerializedBinary<TObjectType>();
					CsGlobal.Debug.Write("Object<" + typeof (TObjectType).Name + "> with ID[" + id + "] -> LOADED.");
				}
				catch (Exception ex)
				{
					CsGlobal.Message.Push(ex);
				}
			}

			if (reference == null)
			{
				reference = createFunc();
				CsGlobal.Debug.Write("Object<" + typeof (TObjectType).Name + "> with ID[" + id + "] -> CREATED.");
			}

			handle = new FileHandle(id, reference, createFunc);
			_handles.Add(id, handle);
			return reference;
		}

		/// <summary>Combines the directory with the name and the extension.</summary>
		public FileInfo GetFilePathByName(string filenameWithoutExtension)
		{
			return new FileInfo(Path.Combine(Directory.FullName, filenameWithoutExtension + Extension));
		}

		/// <summary>Copy all files and directory's to the new target folder. It automatically deletes the source folder.</summary>
		public void TransferDirectory(DirectoryInfo target)
		{
			if (target == null)
				throw new InvalidOperationException("The target directory cannot be changed to null");

			TransferDirectoryRecursive(Directory, target);
		}

		/// <summary>Rename the files with the current extension to the new extension.</summary>
		public void TransferExtension(string targetExtension)
		{
			TransferExtensionRecursive(Directory, Extension, targetExtension);
		}

		/// <summary>Saves the handles to disk</summary>
		public void Save()
		{
			var tasks = new List<Task>();
			foreach (var task in _handles.Values.Select(handle => new Task(() =>
			{
				var file = GetFilePathByName(handle.Id);
				file.DeleteFile_IfExists();
				file.CreateDirectory_IfNotExists();
				handle.Reference.SaveAs_SerializedBinary(file);
			}, TaskCreationOptions.LongRunning)))
			{
				task.Start(TaskScheduler.Default);
				tasks.Add(task);
			}

			Task.WaitAll(tasks.ToArray());
		}

		private void TransferDirectoryRecursive(DirectoryInfo source, DirectoryInfo target)
		{
			if (!target.Exists && !source.Exists)
				return;
			if (!target.Exists)
				target.Create();

			foreach (var subDirectory in source.GetDirectories())
			{
				TransferDirectoryRecursive(subDirectory, new DirectoryInfo(Path.Combine(target.FullName, subDirectory.Name)));
			}
			foreach (var file in source.GetFiles())
			{
				file.CopyTo(Path.Combine(target.FullName, file.Name));
				file.Delete();
			}
			source.Delete();
		}

		private void TransferExtensionRecursive(DirectoryInfo target, string sourceExtension, string targetExtension)
		{
			//TODO CHECK IF WORKING

			foreach (var subDirectory in target.GetDirectories())
			{
				TransferExtensionRecursive(subDirectory, sourceExtension, targetExtension);
			}
			foreach (var file in target.GetFiles("*" + sourceExtension))
			{
				// ReSharper disable once PossibleNullReferenceException
				File.Move(file.FullName, Path.Combine(file.Directory.FullName, file.Name.Replace(file.Extension, targetExtension)));
			}
		}



		private class FileHandle : Base
		{
			private Func<object> _createObject;
			private string _id;
			private object _reference;

			public FileHandle(string id, object reference, Func<object> createObject)
			{
				_id = id;
				_reference = reference;
				_createObject = createObject;
			}

			/// <summary>The unique id for this reference.</summary>
			public string Id
			{
				get { return _id; }
				private set { SetProperty(ref _id, value); }
			}
			/// <summary>The associated object.</summary>
			public object Reference
			{
				get { return _reference; }
				private set { SetProperty(ref _reference, value); }
			}
			/// <summary>Method used to create a new object.</summary>
			public Func<object> CreateObject
			{
				get { return _createObject; }
				private set { SetProperty(ref _createObject, value); }
			}
		}
	}
}