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
	internal class CssSerializationBody : CssSerializationContextPart
	{
		private readonly Dictionary<object, uint> _instanceCache = new Dictionary<object, uint>();
		private uint _nextInstanceId;

		public CssSerializationBody(CssSerializationContext context) : base(context)
		{
		}



		public void Serialize()
		{
			Serialize_Data(Context.Data);
		}

		private void Serialize_Data(object o)
		{
			if (o == null)
			{
				Wr.Write((byte) CssV1.DataOpCodes.Null);
				return;
			}

			var type = o.GetType();
			if (CssV1.IsPrimitive(type))
			{
				Wr.Write((byte) CssV1.DataOpCodes.Primitive);
				Serialize_Primitive(o, type);
				return;
			}

			uint instanceId;
			if (_instanceCache.TryGetValue(o, out instanceId))
			{
				Wr.Write((byte) CssV1.DataOpCodes.Reference);
				Serialize_Reference(instanceId);
				return;
			}

			Wr.Write((byte) CssV1.DataOpCodes.Object);
			Serialize_Object(o, type);
		}

		private void Serialize_Primitive(object obj, Type type)
		{
			Wr.Write(CssV1.GetPrimitiveDefinition(type).Id);

			type = Nullable.GetUnderlyingType(type) ?? type;

			if (type == typeof (bool))
				Wr.Write((bool) obj);
			else if (type == typeof (char))
				Wr.Write((char) obj);
			else if (type == typeof (string))
				Wr.Write((string) obj);
			else if (type == typeof (sbyte))
				Wr.Write((sbyte) obj);
			else if (type == typeof (short))
				Wr.Write((short) obj);
			else if (type == typeof (int))
				Wr.Write((int) obj);
			else if (type == typeof (long))
				Wr.Write((long) obj);
			else if (type == typeof (byte))
				Wr.Write((byte) obj);
			else if (type == typeof (ushort))
				Wr.Write((ushort) obj);
			else if (type == typeof (uint))
				Wr.Write((uint) obj);
			else if (type == typeof (ulong))
				Wr.Write((ulong) obj);
			else if (type == typeof (float))
				Wr.Write((float) obj);
			else if (type == typeof (double))
				Wr.Write((double) obj);
			else if (type == typeof (decimal))
				Wr.Write((decimal) obj);

		}


		private void Serialize_Reference(uint instanceId)
		{
			Wr.Write(instanceId);
		}

		private void Serialize_Object(object obj, Type type)
		{
			var instanceId = _nextInstanceId++;
			_instanceCache.Add(obj, instanceId);

			Wr.Write(instanceId);
			Wr.Write(Context.Definition.GetOrCreate_TypeId(type));

			using (Wr.Write_LengthOfFollowingBlock())
			{
				if (type.IsSubclassOf(typeof (IDictionary)))
					Serialize_Dictionary((IDictionary) obj);
				else if (type.IsSubclassOf(typeof (IList)))
					Serialize_List((IList) obj);
				else if (type.IsArray)
				{
					var elementType = type.GetElementType();

					if (elementType.IsPrimitive)
						Serialize_PrimitiveArray((Array) obj, elementType);
					else
						Serialize_Array((Array) obj, elementType);
					return; // avoid not necessary fields in arrays.
				}
				else
					Serialize_Class(obj);


				Serialize_Fields(obj, type);
			}
		}

		private void Serialize_Dictionary(IDictionary dict)
		{
			Wr.Write(dict.Count);
			foreach (DictionaryEntry item in dict)
			{
				Serialize_Data(item.Key);
				Serialize_Data(item.Value);
			}
		}

		private void Serialize_List(IList col)
		{
			Wr.Write(col.Count);
			foreach (var obj in col)
			{
				Serialize_Data(obj);
			}
		}

		private void Serialize_Array(Array arr, Type elementType)
		{
			Wr.Write(arr.Length);
			for (var i = 0; i < arr.Length; i++)
			{
				var obj = arr.GetValue(i);

				if (obj.Is_DefaultValue_OfType(elementType))
					continue;

				Wr.Write(i);
				Serialize_Data(obj);
			}
		}

		private void Serialize_PrimitiveArray(Array obj, Type type)
		{
			Wr.Write(obj.Length); //int
			if (type == typeof(byte))
				Wr.Write((byte[])obj);
			else if (type == typeof(bool))
				((bool[])obj).ForEach(Wr.Write);
			else if (type == typeof(char))
				((char[])obj).ForEach(Wr.Write);
			else if (type == typeof(sbyte))
				((sbyte[])obj).ForEach(Wr.Write);
			else if (type == typeof(short))
				((short[])obj).ForEach(Wr.Write);
			else if (type == typeof(int))
				((int[])obj).ForEach(Wr.Write);
			else if (type == typeof(long))
				((long[])obj).ForEach(Wr.Write);
			else if (type == typeof(ushort))
				((ushort[])obj).ForEach(Wr.Write);
			else if (type == typeof(uint))
				((uint[])obj).ForEach(Wr.Write);
			else if (type == typeof(ulong))
				((ulong[])obj).ForEach(Wr.Write);
			else if (type == typeof(float))
				((float[])obj).ForEach(Wr.Write);
			else if (type == typeof(double))
				((double[])obj).ForEach(Wr.Write);
			//else if (type == typeof(decimal))    // NOT AN PRIMITIVE
			//	((decimal[])obj).ForEach(Wr.Write);
		}

		private void Serialize_Class(object cla)
		{

		}

		private void Serialize_Fields(object obj, Type type)
		{
			var typedefinition = CssV1.GetReflectedDefinition(type);
			var countPos = Wr.BaseStream.Position;
			var writtenFields = typedefinition.Fields.Length;


			Wr.Write(writtenFields);
			for (var fieldIndex = 0; fieldIndex < typedefinition.Fields.Length; fieldIndex++)
			{
				var field = typedefinition.Fields[fieldIndex];
				var value = field.GetValue(obj);

				if (value.Is_DefaultValue_OfType(field.Type))
				{
					writtenFields--;
					continue;
				}

				Wr.Write((ushort) fieldIndex);
				Serialize_Data(value);
			}

			if (writtenFields == typedefinition.Fields.Length) return;

			var current = Wr.BaseStream.Position;
			Wr.BaseStream.Position = countPos;
			Wr.Write(writtenFields);
			Wr.BaseStream.Position = current;
		}
	}



}