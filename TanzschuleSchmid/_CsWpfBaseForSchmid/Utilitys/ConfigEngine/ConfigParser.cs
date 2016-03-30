// Copyright (c) 2014, 2015 All Right Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2015-06-11</date>

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;
using CsWpfBase.Global;
using CsWpfBase.Utilitys.ConfigEngine.Base;





namespace CsWpfBase.Utilitys.ConfigEngine
{
	/// <summary>Parses the output of an <see cref="ConfigCreator" /> output.</summary>
	public class ConfigParser
	{
		private static readonly Regex ParseRegex = new Regex("(.*?) ?= ?([^\r\n]*)");
		private Dictionary<Type, Func<string, object>> _converters;
		private Dictionary<string, string> _entrys;
		private Field[] _fields;
		private Property[] _propertys;
		private Type _targetType;


		/// <summary>
		///     Parses a given string into a <paramref name="target" />. The object should hold the properties flagged with
		///     the appropriate <see cref="ConfigMemberAttribute" />.
		/// </summary>
		public ConfigParser(string input, object target)
		{
			Input = input;
			Target = target;
		}
		/// <summary>
		///     Parses a given value collection into a  <paramref name="target" />. The object should hold the properties
		///     flagged with the appropriate <see cref="ConfigMemberAttribute" />.
		/// </summary>
		public ConfigParser(Dictionary<string, string> entrys, object target)
		{
			_entrys = entrys;
			Target = target;
		}
		/// <summary>Gets or sets the converters are used to convert a specific type to string.</summary>
		public Dictionary<Type, Func<string, object>> Converters
		{
			get
			{
				return _converters ?? (_converters = new Dictionary<Type, Func<string, object>>
													{
														{typeof (FileInfo), o => String.IsNullOrEmpty(o) ? null : new FileInfo(o)},
														{typeof (DirectoryInfo), o => String.IsNullOrEmpty(o) ? null : new DirectoryInfo(o)},
													});
			}
			set { _converters = value; }
		}
		private string Input { get; set; }
		private object Target { get; set; }
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

		private Dictionary<string, string> Entrys
		{
			get { return _entrys ?? (_entrys = ParseEntrys()); }
		}
		/// <summary>Parses a given string into the T. The T should hold the properties flagged with the appropriate
		///     <see cref="ConfigMemberAttribute" />. If no <see cref="Activator" /> is used the <see cref="FormatterServices" />
		///     will be used to create an uninitialized instance of the T.</summary>
		public static T Parse<T>(string input, bool useActivator = true)
		{
			var target = (T) (useActivator ? Activator.CreateInstance(typeof (T)) : FormatterServices.GetUninitializedObject(typeof (T)));
			new ConfigParser(input, target).ParseToObject();
			return target;
		}

		private Dictionary<string, string> ParseEntrys()
		{
			return ParseRegex.Matches(Input)
							.OfType<Match>()
							.Where(x => x.Groups.Count == 3 && x.Groups[1].Value.StartsWith("//") == false)
							.Select(x => new Tuple<string, string>(x.Groups[1].Value.Trim(), x.Groups[2].Value))
							.GroupBy(x => x.Item1)
							.ToDictionary(x => x.Key, x => x.Last().Item2);
		}

		/// <summary>Starts parsing the input.</summary>
		public void ParseToObject()
		{
			Fields.Concat<ObjectMember>(Propertys).ToList().ForEach(om =>
			{
				string name = String.IsNullOrEmpty(om.Attribute.Name) ? om.Name : om.Attribute.Name;

				if (IsValueType(om.Type))
				{
					if (!Entrys.ContainsKey(name))
						return;
					SetValue(om, Entrys[name]);
				}
				else
				{
					Dictionary<string, string> domainSpecificDict = Entrys
						.Where(x => x.Key.StartsWith(name))
						.ToDictionary(x => x.Key.Replace(name + ".", ""), x => x.Value);
					if (domainSpecificDict.Count == 0)
						return;

					object innerTarget = om.GetValue(Target);
					if (innerTarget == null)
					{
						try
						{
							innerTarget = Activator.CreateInstance(om.Type);
						}
						catch (Exception)
						{
							CsGlobal.Debug.Write("An innertarget couldn't be created");
						}

						if (innerTarget == null)
							return;
						om.SetValue(Target, innerTarget);
					}

					var parser = new ConfigParser(domainSpecificDict, innerTarget);
					parser.ParseToObject();
				}
			});
		}

		private bool IsValueType(Type t)
		{
			return ConfigExt.IsValueType(t) || Converters.ContainsKey(t);
		}
		private void SetValue(ObjectMember member, string value)
		{
			try
			{
				if (Converters.ContainsKey(member.Type))
					member.SetValue(Target, Converters[member.Type](value));
				else if (member.Type.IsEnum)
					member.SetValue(Target, Enum.Parse(member.Type, value));
				else if (typeof (IConvertible).IsAssignableFrom(member.Type))
					member.SetValue(Target, Convert.ChangeType(value, member.Type));
				else if (Nullable.GetUnderlyingType(member.Type) != null)
					member.SetValue(Target, String.IsNullOrEmpty(value) ? null : Convert.ChangeType(value, Nullable.GetUnderlyingType(member.Type)));
			}
			catch (Exception)
			{
				if (member.Attribute.UseFallbackValue)
					member.SetValue(Target, null);
				else
					throw;
			}
		}
	}
}