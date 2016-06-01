// Copyright (c) 2014, 2015 All Right Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2015-12-20</date>

using System;
using System.Linq;
using System.Reflection;
using CsWpfBase.Ev.Public.Extensions;
using CsWpfBase.Utilitys.searializer.attributes;






namespace CsWpfBase.Utilitys.searializer.v1.reflection
{
	/// <summary>The reflected properties of a class.</summary>
	public sealed class CssTypeDefinitionClass : CssTypeDefinition
	{
		/// <summary>Create a new class definition.</summary>
		public static CssTypeDefinitionClass Create(Type t)
		{
			return new CssTypeDefinitionClass(t);
		}


		/// <summary>Creates a new instance from a known type.</summary>
		private CssTypeDefinitionClass(Type t)
		{
			Type = t;
			var tmp = Type.GetFields_IncludingBaseClasses(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic).Where(x => x.GetCustomAttributes(typeof(NonSerializedAttribute), false).Length == 0);
			Fields = tmp.Select(x => new Field(x)).ToArray();
		}


		#region Overrides/Interfaces
		/// <summary>Gets a boolean indicating if the type is known in this assembly and can be created.</summary>
		public override bool IsKnownType => true;
		/// <summary>Gets the full qualified assembly name</summary>
		public override string FullQualifiedAssemblyName
		{
			get { return Type.AssemblyQualifiedName; }
			protected set { throw new NotImplementedException(); }
		}
		/// <summary>The .NET type of the class.</summary>
		public override Type Type { get; protected set; }
		#endregion


		/// <summary>Serialize able or deserialize able fields</summary>
		public Field[] Fields { get; }



		/// <summary>A reflected field definition inside a <see cref="CssTypeDefinitionClass" />.</summary>
		public class Field
		{
			private CssLegacyAliasAttribute[] _aliases;

			public Field(FieldInfo fieldInfo)
			{
				FieldInfo = fieldInfo;
			}

			public string Name => FieldInfo.Name;
			public Type Type => FieldInfo.FieldType;
			/// <summary>The legacy aliases for the fields.</summary>
			public CssLegacyAliasAttribute[] Aliases => _aliases ?? (_aliases = Type.GetCustomAttributes(typeof(CssLegacyAliasAttribute), false).OfType<CssLegacyAliasAttribute>().ToArray());
			private FieldInfo FieldInfo { get; }

			/// <summary>Gets the value of the field by providing the owning class object. Internally calling <see cref="System.Reflection.FieldInfo.GetValue" />.</summary>
			/// <param name="container">The class which contains the field.</param>
			public object GetValue(object container)
			{
				return FieldInfo.GetValue(container);
			}

			/// <summary>
			///     Sets the value of the field by providing the owning class object where the value needs to be set. Internally calling
			///     <see cref="System.Reflection.FieldInfo.SetValue(object,object)" />.
			/// </summary>
			/// <param name="container">The containing class instance where the value needs to be set.</param>
			/// <param name="value">The value to be set on the field.</param>
			public void SetValue(object container, object value)
			{
				FieldInfo.SetValue(container, value);
			}
		}
	}




}