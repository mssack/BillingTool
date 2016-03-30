// Copyright (c) 2014, 2015 All Right Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-01-01</date>

using System;
using System.Collections.Generic;
using System.Linq;






namespace CsWpfBase.Utilitys.searializer.v1.reflection
{
	/// <summary>The definition wrapper for primitive types.</summary>
	public sealed class CssTypeDefinitionPrimitive : CssTypeDefinition
	{
		/// <summary>All primitives encapsulated into an instance of <see cref="CssTypeDefinitionPrimitive" />.</summary>
		public static readonly
			Dictionary<Type, CssTypeDefinitionPrimitive> Instances = new Dictionary<Type, CssTypeDefinitionPrimitive>()
			{
				{typeof (bool), new CssTypeDefinitionPrimitive(typeof (bool), false, 11)},
				{typeof (char), new CssTypeDefinitionPrimitive(typeof (char), false, 12)},
				{typeof (string), new CssTypeDefinitionPrimitive(typeof (string), false, 13)},
				{typeof (sbyte), new CssTypeDefinitionPrimitive(typeof (sbyte), false, 14)},
				{typeof (short), new CssTypeDefinitionPrimitive(typeof (short), false, 15)},
				{typeof (int), new CssTypeDefinitionPrimitive(typeof (int), false, 16)},
				{typeof (long), new CssTypeDefinitionPrimitive(typeof (long), false, 17)},
				{typeof (byte), new CssTypeDefinitionPrimitive(typeof (byte), false, 18)},
				{typeof (ushort), new CssTypeDefinitionPrimitive(typeof (ushort), false, 19)},
				{typeof (uint), new CssTypeDefinitionPrimitive(typeof (uint), false, 20)},
				{typeof (ulong), new CssTypeDefinitionPrimitive(typeof (ulong), false, 21)},
				{typeof (float), new CssTypeDefinitionPrimitive(typeof (float), false, 22)},
				{typeof (double), new CssTypeDefinitionPrimitive(typeof (double), false, 23)},
				{typeof (decimal), new CssTypeDefinitionPrimitive(typeof (decimal), false, 24)},
				{typeof (bool?), new CssTypeDefinitionPrimitive(typeof (bool?), true, 31)},
				{typeof (char?), new CssTypeDefinitionPrimitive(typeof (char?), true, 32)},
				{typeof (sbyte?), new CssTypeDefinitionPrimitive(typeof (sbyte?), true, 34)},
				{typeof (short?), new CssTypeDefinitionPrimitive(typeof (short?), true, 35)},
				{typeof (int?), new CssTypeDefinitionPrimitive(typeof (int?), true, 36)},
				{typeof (long?), new CssTypeDefinitionPrimitive(typeof (long?), true, 37)},
				{typeof (byte?), new CssTypeDefinitionPrimitive(typeof (byte?), true, 38)},
				{typeof (ushort?), new CssTypeDefinitionPrimitive(typeof (ushort?), true, 39)},
				{typeof (uint?), new CssTypeDefinitionPrimitive(typeof (uint?), true, 40)},
				{typeof (ulong?), new CssTypeDefinitionPrimitive(typeof (ulong?), true, 41)},
				{typeof (float?), new CssTypeDefinitionPrimitive(typeof (float?), true, 42)},
				{typeof (double?), new CssTypeDefinitionPrimitive(typeof (double?), true, 43)},
				{typeof (decimal?), new CssTypeDefinitionPrimitive(typeof (decimal?), true, 44)},
			};

		/// <summary>All primitives encapsulated into an instance of <see cref="CssTypeDefinitionPrimitive" />.</summary>
		public static readonly Dictionary<byte, CssTypeDefinitionPrimitive> InstancesById = Instances.ToDictionary(t => t.Value.Id, t => t.Value);

		private CssTypeDefinitionPrimitive(Type t, bool isnullable, byte id)
		{
			Type = t;
			IsNullable = isnullable;
			Id = id;


		}


		#region Overrides/Interfaces
		/// <summary>Gets the full qualified assembly name</summary>
		public override string FullQualifiedAssemblyName
		{
			get { return Type.AssemblyQualifiedName; }
			protected set { throw new NotImplementedException(); }
		}
		/// <summary>The type of the definition.</summary>
		public override Type Type { get; protected set; }
		/// <summary>Gets a boolean indicating if the type is known in this assembly and can be created.</summary>
		public override bool IsKnownType => Type != null;
		#endregion


		/// <summary>Indicates if the primitive type is a null able type.</summary>
		public bool IsNullable { get; }
		/// <summary>The unique global identifier for this primitive type.</summary>
		public byte Id { get; }
	}
}