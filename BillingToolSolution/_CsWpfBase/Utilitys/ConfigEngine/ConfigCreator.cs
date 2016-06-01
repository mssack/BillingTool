// Copyright (c) 2014, 2015 All Right Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2015-06-11</date>

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CsWpfBase.Ev.Public.Extensions;
using CsWpfBase.Utilitys.ConfigEngine.Base;





namespace CsWpfBase.Utilitys.ConfigEngine
{
	/// <summary>Creates a configuration string for an given object.</summary>
	public class ConfigCreator
	{
		private Dictionary<Type, Func<object, string>> _converters;
		private Field[] _fields;
		private Property[] _propertys;
		private List<ReferenceEntry> _referenceConfigs;
		private Type _targetType;
		private List<ValueEntry> _valueConfigs;
		/// <summary>Creates an new instance of an <see cref="ConfigCreator" />.</summary>
		public ConfigCreator(string domain, object target)
		{
			Domain = domain;
			Target = target;
		}
		/// <summary>Creates an new instance of an <see cref="ConfigCreator" />.</summary>
		public ConfigCreator(string domain, Type targetType)
		{
			Domain = domain;
			_targetType = targetType;
		}
		/// <summary>Gets or sets the converters are used to convert a specific type to string.</summary>
		public Dictionary<Type, Func<object, string>> Converters
		{
			get
			{
				return _converters ?? (_converters = new Dictionary<Type, Func<object, string>>
													{
														{typeof (FileInfo), o => ((FileInfo) o).FullName},
														{typeof (DirectoryInfo), o => ((DirectoryInfo) o).FullName},
													});
			}
			set { _converters = value; }
		}
		private string Domain { get; set; }
		private Object Target { get; set; }
		private Type TargetType
		{
			get { return _targetType ?? (_targetType = Target.GetType()); }
		}
		private Property[] Propertys
		{
			get { return _propertys ?? (_propertys = ConfigExt.Reflection.GetProperties(TargetType)); }
		}
		private Field[] Fields
		{
			get { return _fields ?? (_fields = ConfigExt.Reflection.GetFields(TargetType)); }
		}
		private List<ValueEntry> ValueConfigs
		{
			get
			{
				if (_valueConfigs == null)
					Process();
				return _valueConfigs;
			}
		}
		private List<ReferenceEntry> ReferenceConfigs
		{
			get
			{
				if (_referenceConfigs == null)
					Process();
				return _referenceConfigs;
			}
		}


		#region Processing
		private void Process()
		{
			_valueConfigs = new List<ValueEntry>();
			_referenceConfigs = new List<ReferenceEntry>();

			ProcessFields();
			ProcessProperties();
		}

		private void ProcessProperties()
		{
			foreach (Property property in Propertys)
			{
				ProcessObjectMember(property);
			}
		}
		private void ProcessFields()
		{
			foreach (Field field in Fields)
			{
				ProcessObjectMember(field);
			}
		}
		private void ProcessObjectMember(ObjectMember member)
		{
			string name = (String.IsNullOrEmpty(member.Attribute.Name) ? member.Name : member.Attribute.Name);

			object value = Target == null ? null : member.GetValue(Target);

			if (IsValueType(member.Type))
				ValueConfigs.Add(new ValueEntry(Domain, name, GetValueString(value, member.Attribute.StringFormat)));
			else if (value != null)
			{
				string domain = Domain == null ? name : Domain + "." + name;
				var creator = new ConfigCreator(domain, value);
				ReferenceConfigs.Add(new ReferenceEntry(Domain, name, creator));
			}
		}


		private bool IsValueType(Type t)
		{
			return ConfigExt.IsValueType(t) || Converters.ContainsKey(t);
		}
		private string GetValueString(object value, string stringformat)
		{
			if (value == null)
				return "";

			var type = value.GetType();
			if (Converters.ContainsKey(type))
				return Converters[type](value);
			if (!String.IsNullOrEmpty(stringformat))
				return String.Format(stringformat, value);
			return value.ToString();
		}
		#endregion


		#region Exporting
		/// <summary>Returns the configuration text for the given object.</summary>
		public string GetConfigString()
		{
			string rv = "";

			if (ValueConfigs.Count != 0)
			{
				int valueKeyLength = ValueConfigs.Max(x => x.Key.Length);
				rv = ValueConfigs.OrderBy(x => x.Name).Select(x => x.Key.Expand(valueKeyLength) + " = " + x.Value).Join("\r\n");
			}
			if (ReferenceConfigs.Count != 0)
			{
				string joined = ReferenceConfigs.OrderBy(x => x.Name).Select(x => x.Creator.GetConfigString()).Where(x => x.Length != 0).Join("\r\n");
				if (joined.Length != 0)
					rv = rv + "\r\n" + joined;
			}


			return rv;
		}
		#endregion


		#region Classes
		private abstract class Entry
		{
			protected Entry(string domain, string name)
			{
				Domain = domain;
				Name = name;
				Key = domain == null ? name : domain + "." + name;
			}
			public string Name { get; private set; }
			public string Domain { get; private set; }
			public string Key { get; private set; }
		}





		private class ReferenceEntry : Entry
		{
			public ReferenceEntry(string domain, string name, ConfigCreator creator) : base(domain, name)
			{
				Creator = creator;
			}


			#region Overrides
			public override string ToString()
			{
				return Key + " : " + Creator;
			}
			#endregion


			public ConfigCreator Creator { get; private set; }
		}





		private class ValueEntry : Entry
		{
			public ValueEntry(string domain, string name, string value) : base(domain, name)
			{
				Value = value;
			}


			#region Overrides
			public override string ToString()
			{
				return Key + " : " + Value;
			}
			#endregion


			public string Value { get; private set; }
		}
		#endregion
	}
}