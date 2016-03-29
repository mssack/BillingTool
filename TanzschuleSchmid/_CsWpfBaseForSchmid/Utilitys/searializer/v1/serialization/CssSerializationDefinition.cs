// Copyright (c) 2014, 2015 All Right Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-01-01</date>

using System;
using System.Collections;
using System.Collections.Generic;
using CsWpfBase.Ev.Public.Extensions;






namespace CsWpfBase.Utilitys.searializer.v1.serialization
{
	internal class CssSerializationDefinition : CssSerializationContextPart
	{

		private ushort _nextTypePointer;
		private Dictionary<Type, ushort> _typePointers;
		private int _typeCount;

		public CssSerializationDefinition(CssSerializationContext context) : base(context)
		{
			Wr.Write(TypeCount);
		}


		private int TypeCount => _typeCount;
		private Dictionary<Type, ushort> TypePointers => _typePointers ?? (_typePointers = new Dictionary<Type, ushort>());


		/// <summary>Assumed that there is no call with primitive types.</summary>
		public ushort GetOrCreate_TypeId(Type t)
		{
			ushort id;
			if (TypePointers.TryGetValue(t, out id))
				return id;
			_typeCount++;
			return Serialize_Type(t);
		}

		public void ActualizeTypeCount()
		{
			var endPos = Ms.Position;
			Ms.Position = 0;
			Wr.Write(TypeCount);
			Ms.Position = endPos;
		}

		private ushort Serialize_Type(Type t)
		{
			if (CssV1.IsPrimitive(t))
			{
				Wr.Write((byte) CssV1.DefinitionOpCodes.Primitive);
				return Serialize_Primitive(t);
			}

			ushort typeId;
			if (TypePointers.TryGetValue(t, out typeId))
			{
				Wr.Write((byte) CssV1.DefinitionOpCodes.Reference);
				return Serialize_Reference(typeId);
			}

			var opCode = CssV1.DefinitionOpCodes.Class;
			typeId = _nextTypePointer++;
			TypePointers.Add(t, typeId);



			if (t.IsSubclassOf(typeof (IDictionary)))
				opCode = CssV1.DefinitionOpCodes.Dictionary;
			else if (t.IsSubclassOf(typeof (IList)))
				opCode = CssV1.DefinitionOpCodes.List;
			else if (t.IsArray)
				opCode = CssV1.DefinitionOpCodes.Array;

			Wr.Write((byte) opCode);
			Serialize_Object(t, typeId, opCode);

			return typeId;
		}

		private byte Serialize_Primitive(Type t)
		{
			var primitiveId = CssV1.GetPrimitiveDefinition(t).Id;
			Wr.Write(primitiveId);
			return primitiveId;
		}

		private ushort Serialize_Reference(ushort typeId)
		{
			Wr.Write(typeId);
			return typeId;
		}

		private void Serialize_Object(Type t, ushort typeId, CssV1.DefinitionOpCodes code)
		{
			var isgeneric = code == CssV1.DefinitionOpCodes.Dictionary || code == CssV1.DefinitionOpCodes.List;

			Wr.Write(typeId);


			Type clLayoutType = t.IsArray ? t.GetElementType() : isgeneric ? t.GetGenericTypeDefinition() : t;

			Serialize_ClassLayout(clLayoutType, isgeneric || t.IsArray);


			if (code == CssV1.DefinitionOpCodes.Dictionary)
				Serialize_Dictionary(t);
			else if (code == CssV1.DefinitionOpCodes.List)
				Serialize_List(t);
			else if (code == CssV1.DefinitionOpCodes.Array)
				Serialize_Array(t);
			else
				Serialize_Class(t);

		}

		private void Serialize_Dictionary(Type t)
		{
			var dictArguments = t.GetGenericArguments();
			var keyType = dictArguments[0];
			var valueType = dictArguments[1];

			Serialize_Type(keyType);
			Serialize_Type(valueType);
		}

		private void Serialize_List(Type t)
		{
			var elementType = t.GetGenericArguments()[0];

			Serialize_Type(elementType);
		}

		private void Serialize_Array(Type t)
		{
		}

		private void Serialize_Class(Type t)
		{

		}


		private void Serialize_ClassLayout(Type t, bool tryMakeRef = false)
		{
			ushort pointer;
			if (tryMakeRef && TypePointers.TryGetValue(t, out pointer))
			{
				Wr.Write((byte) CssV1.DefinitionOpCodes.Reference);
				Wr.Write(pointer);
				return;
			}
			var typeDef = CssV1.GetReflectedDefinition(t);


			Wr.Write((byte) CssV1.DefinitionOpCodes.Type);
			Wr.Write(typeDef.FullQualifiedAssemblyName);
			Wr.Write(typeDef.Fields.Length);
			foreach (var field in typeDef.Fields)
			{
				Wr.Write((byte) (field.Aliases.Length + 1));
				Wr.Write(field.Name);
				field.Aliases.ForEach(alias => Wr.Write(alias.Name));
				Serialize_Type(field.Type);
			}
		}
	}



}