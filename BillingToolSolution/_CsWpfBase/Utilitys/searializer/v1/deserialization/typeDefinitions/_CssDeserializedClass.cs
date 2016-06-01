// Copyright (c) 2014, 2015 All Right Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2015-12-31</date>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using CsWpfBase.Ev.Objects;
using CsWpfBase.Ev.Public.Extensions;
using CsWpfBase.Utilitys.searializer.v1.reflection;






namespace CsWpfBase.Utilitys.searializer.v1.deserialization.typeDefinitions
{
	/// <summary>The base class for all deserialized type definitions.</summary>
	internal class CssDeserializedClass : CssTypeDefinition
	{


		public CssDeserializedClass(ushort id)
		{
			Id = id;
		}


		#region Abstract
		public virtual void SetAqn(string aqn)
		{
			FullQualifiedAssemblyName = aqn;
			Type = Type.GetType(FullQualifiedAssemblyName);
		}

		public virtual void SetFields(Field[] fields)
		{
			if (!IsKnownType) return;
			var fieldInfos = Type.GetFields_IncludingBaseClasses(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public).ToList();
			foreach (var field in fields)
			{
				var associatedField = fieldInfos.FirstOrDefault(x => field.Aliases.Contains(x.Name));
				if (associatedField != null)
					field.SetFieldInfo(associatedField);
			}
			foreach (var field in fields)
			{
				Fields.Add(field.Number, field);
			}
		}

		public virtual void SetInherited(CssDeserializedClass typedef)
		{
			SetAqn(typedef.FullQualifiedAssemblyName);
			Fields = typedef.Fields;
		}

		#endregion


		#region Overrides/Interfaces
		/// <summary>Gets the full qualified assembly name</summary>
		public override string FullQualifiedAssemblyName { get; protected set; }
		/// <summary>The type of the definition.</summary>
		public override Type Type { get; protected set; }
		/// <summary>Gets a boolean indicating if the type is known in this assembly and can be created.</summary>
		public override bool IsKnownType => Type != null;
		#endregion


		public ushort Id { get; }
		public Dictionary<ushort, Field> Fields { get; protected set; } = new Dictionary<ushort,Field>();



		public class Field : Base
		{


			private FieldInfo _fi;

			public Field(ushort number, IEnumerable<string> names, CssTypeDefinition type)
			{
				Number = number;
				Aliases = new HashSet<string>(names);
				Type = type;
			}

			public ushort Number { get; }
			public HashSet<string> Aliases { get; }
			public CssTypeDefinition Type { get; }

			public bool IsResolveAble => _fi != null;


			public void SetFieldInfo(FieldInfo associatedField)
			{
				_fi = associatedField;
			}

			public void SetValue(object target, object value)
			{
				_fi?.SetValue(target, value);
			}
		}
	}
}