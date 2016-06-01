// Copyright (c) 2014, 2015 All Right Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-01-01</date>

using System;
using System.Collections.Generic;
using System.IO;
using CsWpfBase.Utilitys.searializer.v1.deserialization.typeDefinitions;
using CsWpfBase.Utilitys.searializer.v1.reflection;






namespace CsWpfBase.Utilitys.searializer.v1.deserialization
{
	internal class CssDeserializationDefinition : CssDeserializationContextPart
	{
		public CssDeserializationDefinition(CssDeserializationContext context) : base(context)
		{
			TypePointers = new Dictionary<ushort, CssDeserializedClass>();
		}


		public Dictionary<ushort, CssDeserializedClass> TypePointers { get; }


		public void Deserialize()
		{
			var outerCount = Rd.ReadInt32();
			for (var i = 0; i < outerCount; i++)
			{
				Deserialize_Type();
			}
		}

		private CssTypeDefinition Deserialize_Type()
		{
			var opCode = (CssV1.DefinitionOpCodes) Rd.ReadByte();


			if (opCode == CssV1.DefinitionOpCodes.Primitive)
				return Deserialize_Primitive();

			if (opCode == CssV1.DefinitionOpCodes.Reference)
				return Deserialize_Reference();

			if (opCode == CssV1.DefinitionOpCodes.Dictionary)
				return Deserialize_Dictionary();

			if (opCode == CssV1.DefinitionOpCodes.List)
				return Deserialize_List();

			if (opCode == CssV1.DefinitionOpCodes.Array)
				return Deserialize_Array();

			if (opCode == CssV1.DefinitionOpCodes.Class)
				return Deserialize_Class();

			throw new InvalidDataException("Invalid opcode detected");
		}


		private CssTypeDefinitionPrimitive Deserialize_Primitive()
		{
			return CssV1.GetPrimitiveDefinition(Rd.ReadByte());
		}

		private CssTypeDefinition Deserialize_Reference()
		{
			return TypePointers[Rd.ReadUInt16()];
		}

		private CssTypeDefinition Deserialize_Class()
		{
			var classType = new CssDeserializedClass(Rd.ReadUInt16());
			TypePointers.Add(classType.Id, classType);

			Deserialize_ClassLayout(classType);

			return classType;
		}


		private CssDeserializedList Deserialize_List()
		{
			var listType = new CssDeserializedList(Rd.ReadUInt16());
			TypePointers.Add(listType.Id, listType);

			Deserialize_ClassLayout(listType);
			listType.SetElementType(Deserialize_Type());

			return listType;
		}

		private CssDeserializedDictionary Deserialize_Dictionary()
		{
			var dictType = new CssDeserializedDictionary(id: Rd.ReadUInt16());
			TypePointers.Add(dictType.Id, dictType);

			Deserialize_ClassLayout(dictType);
			dictType.SetTypes(Deserialize_Type(), Deserialize_Type());

			return dictType;
		}

		private CssDeserializedArray Deserialize_Array()
		{
			var arrType = new CssDeserializedArray(id: Rd.ReadUInt16());
			TypePointers.Add(arrType.Id, arrType);

			Deserialize_ClassLayout(arrType);

			return arrType;
		}

		private void Deserialize_ClassLayout(CssDeserializedClass classType)
		{
			var opCode = (CssV1.DefinitionOpCodes) Rd.ReadByte();
			if (opCode == CssV1.DefinitionOpCodes.Reference)
			{
				var typePointer = TypePointers[Rd.ReadUInt16()];
				classType.SetInherited(typePointer);
				return;
			}
			if (opCode == CssV1.DefinitionOpCodes.Type)
			{
				classType.SetAqn(Rd.ReadString());
				var fields = new CssDeserializedClass.Field[Rd.ReadInt32()];
				for (ushort number = 0; number < fields.Length; number++)
				{
					fields[number] = Deserialize_ClassField(classType, number);
				}
				classType.SetFields(fields);
				return;
			}

			throw new InvalidDataException("Invalid opcode detected");
		}

		private CssDeserializedClass.Field Deserialize_ClassField(CssDeserializedClass owner, ushort number)
		{
			var names = new string[Rd.ReadByte()];
			for (var j = 0; j < names.Length; j++)
			{
				names[j] = Rd.ReadString();
			}
			var type = Deserialize_Type();

			return new CssDeserializedClass.Field(number, names, type);
		}
	}
}