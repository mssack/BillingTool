// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-04-03</date>

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows;
using CsWpfBase.Ev.Objects;
using CsWpfBase.Ev.Public.Extensions;
using CsWpfBase.Global;






namespace CsWpfBase.Utilitys.templates
{
	/// <summary>A configuration file base class for reading and writing configuration files.</summary>
	[Serializable]
	public abstract class ConfigFileBase : Base
	{
		private bool _isLoaded;
		private Uri _packUri;
		private FileInfo _path;

		/// <summary>Creates a new instance by providing the source file path.</summary>
		protected ConfigFileBase(FileInfo path)
		{
			Path = path;
		}

		/// <summary>Creates a new instance by providing the source file path.</summary>
		protected ConfigFileBase(Uri packUri)
		{
			PackUri = packUri;
		}

		/// <summary>Gets or sets the Path.</summary>
		public FileInfo Path
		{
			get { return _path; }
			private set { SetProperty(ref _path, value); }
		}
		/// <summary>Gets or sets the PackUri.</summary>
		public Uri PackUri
		{
			get { return _packUri; }
			private set { SetProperty(ref _packUri, value); }
		}
		/// <summary>Gets or sets the IsLoaded.</summary>
		public bool IsLoaded
		{
			get { return _isLoaded; }
			private set { SetProperty(ref _isLoaded, value); }
		}
		/// <summary>Whenever invalid entrys are found in config file this event will fire.</summary>
		public event Action<string> LoadError;

		/// <summary>Loads the configuration file into the instance.</summary>
		public void Load()
		{
			Path?.Refresh();
			if (Path != null && !Path.Exists)
				return;

			var members = ReflectionHelper.GetKeyMembers(GetType());
			var keyValuePair = LoadKeyValuePair(Path == null ? CsGlobal.Storage.Resource.File.Read(PackUri) : Path.LoadAs_UTF8String());

			foreach (var keyMember in members)
			{
				string value;
				if (!keyValuePair.TryGetValue(keyMember.Name.ToLower(), out value))
					continue;

				if (string.IsNullOrEmpty(value))
				{
					keyMember.SetValue(this, Helper.GetDefault(keyMember.Type));
					continue;
				}

				try
				{
					if (keyMember.Type.IsEnum)
						keyMember.SetValue(this, Enum.Parse(keyMember.Type, value));
					else if (keyMember.Type == typeof(DateTime) && !string.IsNullOrEmpty(keyMember.Attribute.StringFormat))
						keyMember.SetValue(this, DateTime.ParseExact(value, keyMember.Attribute.StringFormat, CultureInfo.CurrentCulture)); 
					else
						keyMember.SetValue(this, Convert.ChangeType(value, keyMember.Type));

				}
				catch (Exception)
				{
					OnLoadError("Member: " + keyMember.Name + ", The value " + value + " could not be converted to " + keyMember.Type.Name);
				}
			}
			IsLoaded = true;
		}

		/// <summary>Saves the configuration file back to the file.</summary>
		public void Save()
		{
			if (Path == null)
				return;
			Path.Refresh();

			var members = ReflectionHelper.GetKeyMembers(GetType());

			var configString = members.Select(x =>
			{
				var value = x.GetValue(this);
				if (value == null)
					return x.Name + " = ";
				if (string.IsNullOrEmpty(x.Attribute.StringFormat))
					return x.Name + " = " + value;
				return x.Name + " = " + string.Format(x.Attribute.StringFormat, value);
			}).Join("\r\n");


			Path.CreateDirectory_IfNotExists();
			Path.DeleteFile_IfExists();
			using (var writer = new StreamWriter(Path.FullName))
			{
				writer.Write(configString);
			}
			Path.Refresh();
		}

		private Dictionary<string, string> LoadKeyValuePair(string filecontent)
		{
			var configLines = filecontent.Replace("\r\n", "\n").Split("\n");
			var keyValuePair = new Dictionary<string, string>();
			for (var line = 0; line < configLines.Length; line++)
			{
				var pos = 0;
				var configline = configLines[line];

				Helper.SkipWhiteSpaces(ref pos, configline);

				if (pos == configline.Length - 1)
					continue; //empty line
				if (Helper.IsEscapedLine(ref pos, configline))
					continue; //escaped line
				if (configline.Length - pos <= 2)
					continue; //line to short

				var key = Helper.ValueTillEqualChar(ref pos, configline);
				if (key == null)
				{
					OnLoadError("Line " + line + ", no '=' char where found.");
					continue;
				}
				key = key.Trim(' ', '\t').ToLower();
				if (keyValuePair.ContainsKey(key))
				{
					OnLoadError("Line " + line + ", The key [" + key + "] is duplicated.");
					continue;
				}
				var value = Helper.ValueAfterEqualChar(ref pos, configline);
				value = value.Trim(' ', '\t');
				if ((value.StartsWith("\"") && value.EndsWith("\"")) || (value.StartsWith("'") && value.EndsWith("'")))
					value = value.Substring(1, value.Length - 2);

				keyValuePair.Add(key, value);
			}
			return keyValuePair;
		}

		private void OnLoadError(string error)
		{
			if (LoadError == null)
				return;
			LoadError(error);
		}



		private static class Helper
		{
			public static object GetDefault(Type type)
			{
				if (type.IsValueType)
				{
					return Activator.CreateInstance(type);
				}
				return null;
			}

			public static void SkipWhiteSpaces(ref int pos, string value)
			{
				while (pos < value.Length && (value[pos] == ' ' || value[pos] == '\t'))
				{
					pos++;
				}
			}

			public static bool IsEscapedLine(ref int pos, string value)
			{
				const string escapeSequence = "//";
				if (pos + escapeSequence.Length - 1 >= value.Length)
					return false;

				if (value[pos] == '/' && value[pos + 1] == '/')
					return true;
				return false;
			}

			public static string ValueTillEqualChar(ref int pos, string value)
			{
				var targetPos = value.IndexOf('=', pos);
				if (targetPos == -1)
					return null;
				var from = pos;
				pos = targetPos;
				return value.Substring(from, targetPos - from);
			}

			public static string ValueAfterEqualChar(ref int pos, string value)
			{
				if (value[pos] != '=')
					throw new InvalidOperationException("Use this method where =");


				var rv = value.Substring(pos + 1);
				pos = value.Length - 1;
				return rv;
			}
		}



		private static class ReflectionHelper
		{
			private static readonly Dictionary<Type, KeyMember[]> Cache = new Dictionary<Type, KeyMember[]>();

			public static KeyMember[] GetKeyMembers(Type t)
			{
				if (Cache.ContainsKey(t))
					return Cache[t];

				var members = ExtractMembers(t);
				Cache.Add(t, members);

				return members;
			}

			private static KeyMember[] ExtractMembers(Type t)
			{
				var properties = t.GetProperties(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
				var fields = t.GetFields(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);


				var members = new List<KeyMember>();

				foreach (var info in properties)
				{
					var attributes = info.GetCustomAttributes(typeof (KeyAttribute), false);
					if (attributes.Length == 0)
						continue;

					members.Add(new KeyMember(info, attributes[0] as KeyAttribute));
				}
				foreach (var info in fields)
				{
					var attributes = info.GetCustomAttributes(typeof (KeyAttribute), false);
					if (attributes.Length == 0)
						continue;

					members.Add(new KeyMember(info, attributes[0] as KeyAttribute));
				}
				return members.ToArray();
			}
		}



		/// <summary>Assign this Attribute to property's or fields which represent a key value pair in the config file.</summary>
		[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
		protected sealed class KeyAttribute : Attribute
		{
			/// <summary>Sets the name of the key.</summary>
			public string Name { get; set; }
			/// <summary>The String format which will be used on save.</summary>
			public string StringFormat { get; set; }
		}



		private class KeyMember
		{
			private readonly FieldInfo _fi;
			private readonly Func<object, object> _getMemberValue;
			private readonly PropertyInfo _pi;
			private readonly Action<object, object> _setMemberValue;

			public KeyMember(PropertyInfo pi, KeyAttribute attr)
			{
				_pi = pi;
				_getMemberValue = o => _pi.GetValue(o, null);
				_setMemberValue = (o1, o2) => _pi.SetValue(o1, o2, null);
				Type = _pi.PropertyType;
				Attribute = attr;
				if (attr != null && !string.IsNullOrEmpty(attr.Name))
					Name = attr.Name;
				else
					Name = pi.Name;
			}

			public KeyMember(FieldInfo fi, KeyAttribute attr)
			{
				_fi = fi;
				_getMemberValue = o => _fi.GetValue(o);
				_setMemberValue = (o1, o2) => _fi.SetValue(o1, o2);
				Type = _fi.FieldType;
				Attribute = attr;
				if (attr != null && !string.IsNullOrEmpty(attr.Name))
					Name = attr.Name;
				else
					Name = fi.Name;
			}


			#region Overrides/Interfaces
			/// <summary>Returns a <see cref="T:System.String" /> that represents the current <see cref="T:System.Object" />.</summary>
			/// <returns>A <see cref="T:System.String" /> that represents the current <see cref="T:System.Object" />.</returns>
			public override string ToString()
			{
				return Name;
			}
			#endregion


			public Type Type { get; set; }
			public KeyAttribute Attribute { get; set; }
			public string Name { get; set; }

			public string GetValue(object target)
			{
				var memberValue = _getMemberValue(target);
				return memberValue == null ? null : memberValue.ToString();
			}

			public void SetValue(object target, object value)
			{
				_setMemberValue(target, value);
			}
		}
	}
}