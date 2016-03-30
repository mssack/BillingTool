// Copyright (c) 2014, 2015 All Right Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-01-01</date>

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using CsWpfBase.Ev.Objects;
using CsWpfBase.Utilitys.searializer.v1.deserialization.typeDefinitions;






namespace CsWpfBase.Utilitys.searializer.v1.deserialization
{
	internal class CssDeserializationBody : Base
	{
		public CssDeserializationBody(CssDeserializationContext context)
		{
			Context = context;
			InstanceCache = new Dictionary<uint, object>();
		}




		public CssDeserializationContext Context { get; }
		public BinaryReader Rd => Context.Rd;
		private Dictionary<uint, object> InstanceCache { get; }


		public object Deserialize()
		{
			return Deserialize_Data();
		}

		private object Deserialize_Data()
		{
			var opCode = (CssV1.DataOpCodes) Rd.ReadByte();



			if (opCode == CssV1.DataOpCodes.Null)
				return Deserialize_Null();
			if (opCode == CssV1.DataOpCodes.Primitive)
				return Deserialize_Primitive();
			if (opCode == CssV1.DataOpCodes.Reference)
				return Deserialize_Reference();
			if (opCode == CssV1.DataOpCodes.Object)
				return Deserialize_Object();

			throw new InvalidDataException("Invalid op code detected");
		}


		private object Deserialize_Null()
		{
			return null;
		}

		private object Deserialize_Primitive()
		{
			var primitiveType = CssV1.GetPrimitiveDefinition(Rd.ReadByte());
			var t = primitiveType.IsNullable ? Nullable.GetUnderlyingType(primitiveType.Type) : primitiveType.Type;


			if (t == typeof (bool))
				return Rd.ReadBoolean();
			if (t == typeof (char))
				return Rd.ReadChar();
			if (t == typeof (string))
				return Rd.ReadString();
			if (t == typeof (sbyte))
				return Rd.ReadSByte();
			if (t == typeof (short))
				return Rd.ReadInt16();
			if (t == typeof (int))
				return Rd.ReadInt32();
			if (t == typeof (long))
				return Rd.ReadInt64();
			if (t == typeof (byte))
				return Rd.ReadByte();
			if (t == typeof (ushort))
				return Rd.ReadUInt16();
			if (t == typeof (uint))
				return Rd.ReadUInt32();
			if (t == typeof (ulong))
				return Rd.ReadUInt64();
			if (t == typeof (float))
				return Rd.ReadSingle();
			if (t == typeof (double))
				return Rd.ReadDouble();
			if (t == typeof (decimal))
				return Rd.ReadDecimal();

			throw new InvalidDataException();
		}


		private object Deserialize_Reference()
		{
			return InstanceCache[Rd.ReadUInt32()];
		}

		private object Deserialize_Object()
		{
			var instanceId = Rd.ReadUInt32();
			var typedef = Context.Definition.TypePointers[Rd.ReadUInt16()];
			var length = Rd.ReadInt64();

			if (!typedef.IsKnownType)
			{
				throw new Exception("Is not defined");
				Rd.BaseStream.Seek(length, SeekOrigin.Current);
			}




			object des;


			if (typedef is CssDeserializedDictionary)
				des = Deserialize_Dictionary((CssDeserializedDictionary) typedef, instanceId);
			else if (typedef is CssDeserializedList)
				des = Deserialize_List((CssDeserializedList) typedef, instanceId);
			else if (typedef is CssDeserializedArray)
			{
				var cssDeserializedArray = (CssDeserializedArray) typedef;
				if (cssDeserializedArray.ElementType.IsPrimitive)
					des = Deserialize_PrimitiveArray(cssDeserializedArray, instanceId);
				else
					des = Deserialize_Array(cssDeserializedArray, instanceId);
				return des; // avoid not necessary deserialization of fields.
			}
			else
				des = Deserialize_Class(typedef, instanceId);

			Deserialize_Fields(typedef, des);

			return des;
		}

		private object Deserialize_Class(CssDeserializedClass typedef, uint instanceId)
		{
			var o = FormatterServices.GetUninitializedObject(typedef.Type);

			InstanceCache.Add(instanceId, o);
			return o;
		}

		private object Deserialize_List(CssDeserializedList typedef, uint instanceId)
		{
			var o = (IList) FormatterServices.GetUninitializedObject(typedef.Type);
			InstanceCache.Add(instanceId, o);


			var count = Rd.ReadInt32();
			for (var i = 0; i < count; i++)
			{
				o.Add(Deserialize_Data());
			}

			return o;
		}

		private object Deserialize_Array(CssDeserializedArray typedef, uint instanceId)
		{
			var count = Rd.ReadInt32();

			var o = (Array) Activator.CreateInstance(typedef.Type, count);

			InstanceCache.Add(instanceId, o);

			for (var i = 0; i < count; i++)
			{
				var targetIndex = Rd.ReadInt32();
				var value = Deserialize_Data();

				o.SetValue(value, targetIndex);
			}

			return o;
		}

		private object Deserialize_PrimitiveArray(CssDeserializedArray typedef, uint instanceId)
		{
			var count = Rd.ReadInt32();

			if (typedef.Type == typeof (byte[]))
			{
				var byteArr = Rd.ReadBytes(count);
				InstanceCache.Add(instanceId, byteArr);
				return byteArr;
			}

			var o = (Array) Activator.CreateInstance(typedef.Type, count);
			InstanceCache.Add(instanceId, o);
			Func<object> op = null;

			if (typedef.ElementType == typeof (bool))
				op = () => Rd.ReadBoolean();
			if (typedef.ElementType == typeof (char))
				op = () => Rd.ReadChar();
			if (typedef.ElementType == typeof (sbyte))
				op = () => Rd.ReadSByte();
			if (typedef.ElementType == typeof (short))
				op = () => Rd.ReadInt16();
			if (typedef.ElementType == typeof (int))
				op = () => Rd.ReadInt32();
			if (typedef.ElementType == typeof (long))
				op = () => Rd.ReadInt64();
			if (typedef.ElementType == typeof (ushort))
				op = () => Rd.ReadUInt16();
			if (typedef.ElementType == typeof (uint))
				op = () => Rd.ReadUInt32();
			if (typedef.ElementType == typeof (ulong))
				op = () => Rd.ReadUInt64();
			if (typedef.ElementType == typeof (float))
				op = () => Rd.ReadSingle();
			if (typedef.ElementType == typeof (double))
				op = () => Rd.ReadDouble();
			//if (typedef.ElementType == typeof(decimal)) // NO PRIMITIVE
			//	op = () => Rd.ReadDecimal();

			for (var i = 0; i < count; i++)
			{
				o.SetValue(op(), i);
			}

			return o;
		}

		private object Deserialize_Dictionary(CssDeserializedDictionary typedef, uint instanceId)
		{
			var o = (IDictionary) FormatterServices.GetUninitializedObject(typedef.Type);
			InstanceCache.Add(instanceId, o);


			var pairCount = Rd.ReadInt32();
			for (var i = 0; i < pairCount; i++)
			{
				var keyData = Deserialize_Data();
				var valueData = Deserialize_Data();

				o.Add(keyData, valueData);
			}

			return o;
		}

		private void Deserialize_Fields(CssDeserializedClass obj, object o)
		{
			var fieldCount = Rd.ReadInt32();
			for (var i = 0; i < fieldCount; i++)
			{
				var field = obj.Fields[Rd.ReadUInt16()];
				field.SetValue(o, Deserialize_Data());
			}
		}
	}
}