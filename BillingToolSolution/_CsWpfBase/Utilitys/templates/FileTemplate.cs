// Copyright (c) 2014, 2015 All Right Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2015-07-24</date>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using CsWpfBase.Ev.Objects;
using CsWpfBase.Ev.Public.Extensions;
using CsWpfBase.Global;
using CsWpfBase.Properties;






namespace CsWpfBase.Utilitys.templates
{
	/// <summary>A Template Wrapper for inserting text fragments into predefined files.</summary>
	[Serializable]
	public abstract class FileTemplate : Base
	{
		private static readonly Dictionary<Type, string> LoadedFiles = new Dictionary<Type, string>();


		#region Abstract
		/// <summary>At default the template string will be loaded from an .txt file with the exact same name of the source code file name with an .txt appended.</summary>
		protected virtual string TemplateString
		{
			get
			{
				var type = GetType();
				string rv;
				if (LoadedFiles.TryGetValue(type, out rv))
					return rv;


				string ns = type.Namespace, assemblyName, filePath;


				// ReSharper disable once PossibleNullReferenceException
				var iFirst = ns.IndexOf(".", StringComparison.Ordinal);
				if (iFirst != -1)
				{
					assemblyName = ns.Substring(0, iFirst);
					filePath = ns.Substring(iFirst + 1);
				}
				else
				{
					assemblyName = ns;
					filePath = "";
				}
				if (filePath != "")
					filePath = filePath.Replace('.', '/') + "/" + TemplateFileName;
				else
					filePath = TemplateFileName;



				rv = CsGlobal.Storage.Resource.File.Read(CsGlobal.Storage.Resource.Path.Get(assemblyName, filePath));
				LoadedFiles.Add(type, rv);

				return rv;
			}
		}
		/// <summary>The template file name. The name of the resource file at the same name space as the source file.</summary>
		protected virtual string TemplateFileName => GetType().Name + ".cs" + ".txt";

		/// <summary>Converts the <paramref name="propName" /> to the associated TemplateKey.</summary>
		protected virtual string PropNameToKey(string propName)
		{
			return "#" + propName + "#";
		}
		#endregion


		/// <summary>Gets the template String.</summary>
		public string GetString()
		{
			return GetString(0);
		}

		/// <summary>Creates the string, but with an indentation level.</summary>
		public string GetString(int indentationLevel)
		{
			var sb = new StringBuilder(TemplateString);
			foreach (var member in ReflectionHelper.GetKeyMembers(GetType()))
			{
				string keyname;
				if (!string.IsNullOrEmpty(member.Attribute.FullName))
					keyname = member.Attribute.FullName;
				else if (!string.IsNullOrEmpty(member.Attribute.Name))
					keyname = PropNameToKey(member.Attribute.Name);
				else
					keyname = PropNameToKey(member.Name);

				sb.Replace(keyname, member.GetValue(this));
			}
			if (indentationLevel != 0)
				sb.Replace("\r\n", "\r\n" + ("\t".Expand(indentationLevel, "\t")));
			return sb.ToString();
		}

		/// <summary>Takes all Key elements inside template and generates a MD5 hash.</summary>
		public string GetHash()
		{
			var sb = new StringBuilder(TemplateString);
			foreach (var member in ReflectionHelper.GetKeyMembers(GetType()).Where(x=>x.Attribute.IncludeInHash))
			{
				sb.Append(member.GetValue(this));
			}
			return sb.ToString().Md5Hash();
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



		/// <summary>Assign this Attribute to property's or field's which represent a value in a template file.</summary>
		[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
		[MeansImplicitUse]
		protected sealed class KeyAttribute : Attribute
		{
			/// <summary>Sets the complete key name including the starting sequence and ending sequence.</summary>
			public string FullName { get; set; }
			/// <summary>Sets the name of the key.</summary>
			public string Name { get; set; }
			/// <summary>Sets a value which is inserted before the value.</summary>
			public string ValuePrefix { get; set; }
			/// <summary>Sets a value which is appended to the value</summary>
			public string ValueSuffix { get; set; }
			/// <summary>The string format to apply.</summary>
			public string StringFormat { get; set; }
			/// <summary>Defines if the value should be included inside the hashing algorithm. Default is true</summary>
			public bool IncludeInHash { get; set; } = true;
		}



		private class KeyMember
		{
			private readonly FieldInfo _fi;
			private readonly Func<object, object> _getMemberValue;
			private readonly PropertyInfo _pi;
			//private Type _type;


			public KeyMember(PropertyInfo pi, KeyAttribute attr)
			{
				_pi = pi;
				//_type = _pi.PropertyType;
				_getMemberValue = o => _pi.GetValue(o, null);
				Attribute = attr;
				Name = pi.Name;
			}

			public KeyMember(FieldInfo fi, KeyAttribute attr)
			{
				_fi = fi;
				//_type = _fi.FieldType;
				_getMemberValue = o => _fi.GetValue(o);
				Attribute = attr;
				Name = fi.Name;
			}

			public KeyAttribute Attribute { get; set; }
			public string Name { get; set; }

			public string GetValue(object target)
			{
				var memberValue = _getMemberValue(target);

				if (memberValue == null)
					return "";

				var rv = string.IsNullOrEmpty(Attribute.StringFormat) ? memberValue.ToString() : string.Format(Attribute.StringFormat, memberValue);


				if (!string.IsNullOrEmpty(Attribute.ValuePrefix))
					rv = Attribute.ValuePrefix + rv;
				if (!string.IsNullOrEmpty(Attribute.ValueSuffix))
					rv = rv + Attribute.ValueSuffix;

				return rv;
			}
		}
	}
}