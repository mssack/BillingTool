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






namespace CsWpfBase.Online.packets
{
	/// <summary>The packet base for all types which are transmitted.</summary>
	[Serializable]
	public abstract class CsoPacket : Base
	{
		private uint? _packetVersion;


		#region Abstract
		/// <summary>Gets the type code which is used to parse an unknown packet into a specific .Net type.</summary>
		public abstract Types PacketType { get; }
		/// <summary>
		///     The initial Value of the <see cref="PacketVersion" />, this value will be applied to the <see cref="PacketVersion" /> property whenever the
		///     packet is created or no version is defined.
		/// </summary>
		protected abstract uint InitialVersion { get; }

		/// <summary>Interprets a binary into this object.</summary>
		internal abstract void Parse(Reader reader, int length);

		/// <summary>converts this object into binary and writes the content to the Writer.</summary>
		internal abstract void Write(Writer writer);



		/// <summary>can be used in the <see cref="Writer" /> or <see cref="Reader" />.</summary>
		public interface IPart
		{
			#region Abstract
			/// <summary>Interprets a binary into this object.</summary>
			void Parse(Reader reader, int length);

			/// <summary>converts this object into binary and writes the content to the Writer.</summary>
			void Write(Writer writer);
			#endregion
		}



		/// <summary>
		///     Gets or Sets the packet version. This field can be used to extend an existing packet with properties only available in a specific version. This
		///     field is intended for extensibility issues.
		/// </summary>
		public virtual UInt32 PacketVersion
		{
			get
			{
				if (_packetVersion == null)
					_packetVersion = InitialVersion;
				return _packetVersion.Value;
			}
			set { _packetVersion = value; }
		}
		#endregion


		/// <summary>Get the packat data.</summary>
		/// <returns></returns>
		public byte[] GetData()
		{
			var writer = new Writer();
			writer.Packet(this);
			return writer.Data.ToArray();
		}



		/// <summary>Contains a list of all known packet types.</summary>
		public enum Types : uint
		{
			/// <summary>A crypted packet container.</summary>
			Crypted = 1,
			/// <summary>client request, packet.</summary>
			ClientBase = 2,
			/// <summary>server response, packet.</summary>
			ServerBase = 3,



			/// <summary>client request, client info.</summary>
			ClientAppInfo = 50,
			/// <summary>client request, client exception.</summary>
			ClientException = 51,


			//Range from 110 - 129 OS Packets

			/// <summary>client request, client os info.</summary>
			ClientOsInfo = 110,


			//Range from 130 - 149 Hardware Packets

			/// <summary>client request, client mainboard hardware info.</summary>
			ClientHwMainboardInfo = 130,
			/// <summary>client request, client graphic hardware info.</summary>
			ClientHwGraphicInfo = 131,
			/// <summary>client request, client processor hardware info.</summary>
			ClientHwProcessorInfo = 132,
			/// <summary>client request, client computer hardware info.</summary>
			ClientHwComputerSystemInfo = 133,
			/// <summary>client request, client screen hardware info.</summary>
			ClientHwScreenInfo = 134,
			/// <summary>client request, client memory hardware info.</summary>
			ClientHwMemoryInfo = 135,
			/// <summary>client request, client disc drive hardware info.</summary>
			ClientHwDiskDriveInfo = 136,
			/// <summary>client request, client bios hardware info.</summary>
			ClientHwBiosInfo = 137,
			/// <summary>client request, client network hardware info.</summary>
			ClientHwNetworkInfo = 138,



			/// <summary>client request, client feedback.</summary>
			ClientFeedback = 151,


			/// <summary>server response, update available</summary>
			ServerUpdateAvailable = 201,
			/// <summary>server response, message for user</summary>
			ServerMessage = 202,

			/// <summary>unknown packet type.</summary>
			Unknown = uint.MaxValue,
		}



		/// <summary>packet reader</summary>
		public class Reader
		{
			private static Dictionary<uint, Type> _packetTypes;
			private static Dictionary<uint, Type> PacketTypes
			{
				get
				{
					if (_packetTypes != null)
						return _packetTypes;


					_packetTypes = new Dictionary<uint, Type>();
					foreach (var type in Assembly.GetAssembly(typeof (CsoPacket)).GetTypes().Where(myType => myType.IsClass && !myType.IsAbstract && myType.IsSubclassOf(typeof (CsoPacket))))
					{
						_packetTypes.Add((uint) ((CsoPacket) Activator.CreateInstance(type)).PacketType, type);
					}

					return _packetTypes;
				}
			}

			/// <summary>data to read.</summary>
			public Reader(byte[] data, int position)
			{
				Data = data;
				Position = position;
			}

			/// <summary>data to read.</summary>
			public byte[] Data { get; private set; }
			/// <summary>current read position.</summary>
			public int Position { get; private set; }

			/// <summary>read a byte and increment position by 1.</summary>
			public byte Byte()
			{
				if (Invalid(1))
					return 0;
				var rv = Data[Position];
				Position++;
				return rv;
			}

			/// <summary>read two bytes and increment position by 2.</summary>
			public Int16 Int16()
			{
				if (Invalid(2))
					return 0;
				var rv = BitConverter.ToInt16(Data, Position);
				Position = Position + 2;
				return rv;
			}

			/// <summary>read four bytes and increment position by 4.</summary>
			public Int32 Int32()
			{
				if (Invalid(4))
					return 0;
				var rv = BitConverter.ToInt32(Data, Position);
				Position = Position + 4;
				return rv;
			}

			/// <summary>read eight bytes and increment position by 8.</summary>
			public Int64 Int64()
			{
				if (Invalid(8))
					return 0;
				var rv = BitConverter.ToInt64(Data, Position);
				Position = Position + 8;
				return rv;
			}

			/// <summary>read two bytes and increment position by 2.</summary>
			public UInt16 UInt16()
			{
				if (Invalid(2))
					return 0;
				var rv = BitConverter.ToUInt16(Data, Position);
				Position = Position + 2;
				return rv;
			}

			/// <summary>read four bytes and increment position by 4.</summary>
			public UInt32 UInt32()
			{
				if (Invalid(4))
					return 0;
				var rv = BitConverter.ToUInt32(Data, Position);
				Position = Position + 4;
				return rv;
			}

			/// <summary>read eight bytes and increment position by 8.</summary>
			public UInt64 UInt64()
			{
				if (Invalid(8))
					return 0;
				var rv = BitConverter.ToUInt64(Data, Position);
				Position = Position + 8;
				return rv;
			}

			/// <summary>read eight bytes and increment position by 8.</summary>
			public double Double()
			{
				if (Invalid(8))
					return 0;
				var rv = BitConverter.ToDouble(Data, Position);
				Position = Position + 8;
				return rv;
			}

			/// <summary>read the first four bytes defining the length of the string and then reads the string to its end.</summary>
			public string String()
			{
				var length = Int32();
				if (length == 0)
					return null;

				var output = Encoding.Unicode.GetString(Data, Position, length);

				Position = Position + length;

				return output;
			}

			/// <summary>read the first four bytes defining the length of the byte array and then reads the byte array to its end.</summary>
			public byte[] ByteArray()
			{
				var length = Int32();
				if (length == 0)
					return null;

				return Bytes(length);
			}

			/// <summary>read sixteen bytes and increment position by 16.</summary>
			public Guid Guid()
			{
				if (Invalid(16))
					return System.Guid.Empty;

				var guidData = new byte[16];
				Buffer.BlockCopy(Data, Position, guidData, 0, 16);

				Position = Position + 16;
				return new Guid(guidData);
			}

			/// <summary>read eight bytes and increment position by 8.</summary>
			public TimeSpan TimeSpan()
			{
				return new TimeSpan(Int64());
			}

			/// <summary>read eight bytes and increment position by 8. Gets the local time from serialized utc time.</summary>
			public DateTime DateTime()
			{
				return new DateTime(Int64(), DateTimeKind.Utc).ToLocalTime();
			}

			/// <summary>reads a specific amount of bytes.</summary>
			public byte[] Bytes(int length)
			{
				if (Invalid(length))
					return new byte[length];

				var data = new byte[length];
				Buffer.BlockCopy(Data, Position, data, 0, length);

				Position = Position + length;
				return data;
			}

			/// <summary>reads a part of a packet collapsed inside another class.</summary>
			public TTarget Part<TTarget>() where TTarget : IPart
			{
				var instance = (TTarget) Activator.CreateInstance(typeof (TTarget));
				instance.Parse(this, 0);
				return instance;
			}

			/// <summary>reads a packet</summary>
			public CsoPacket Packet()
			{
				var packetType = UInt32();
				var packetVersion = UInt32();
				var packetLength = Int32();

				Type targetPacketType;
				var targetPosition = Position + packetLength;
				CsoPacket packet;
				if (PacketTypes.TryGetValue(packetType, out targetPacketType))
				{
					packet = (CsoPacket) Activator.CreateInstance(targetPacketType);
					packet.Parse(this, packetLength);
				}
				else
				{
					packet = new UnknownPacket(packetType);
					packet.Parse(this, packetLength);
				}
				packet.PacketVersion = packetVersion;
				Position = targetPosition; //ensures that new packet begins at the right position no matter of the data parsed

				return packet;
			}

			/// <summary>reads a list of fix defined Types</summary>
			public List<UInt16> ListOfUInt16()
			{
				return List(UInt16);
			}

			/// <summary>reads a list of fix defined Types</summary>
			public List<String> ListOfString()
			{
				return List(String);
			}

			/// <summary>reads a list of fix defined Types</summary>
			public List<TTarget> ListOfParts<TTarget>() where TTarget : IPart
			{
				return List(Part<TTarget>);
			}

			/// <summary>reads a list of packets.</summary>
			public List<CsoPacket> ListOfPackets()
			{
				return List(Packet);
			}

			/// <summary>reads a list of types. The list must be preceded by an Int32.</summary>
			/// <param name="readItem">The function for reading an item in the list.</param>
			public List<TValueType> List<TValueType>(Func<TValueType> readItem)
			{
				var length = Int32();
				if (length == 0)
					return new List<TValueType>();

				var rv = new List<TValueType>();
				for (var i = 0; i < length; i++)
				{
					rv.Add(readItem());
				}

				return rv;
			}

			private bool Invalid(int length)
			{
				if (Position + length > Data.Length)
					return true;
				return false;
			}
		}



		/// <summary>packet writer.</summary>
		public class Writer
		{
			private List<byte> _data;
			/// <summary>the written data.</summary>
			public List<byte> Data
			{
				get { return _data ?? (_data = new List<byte>()); }
				set { _data = value; }
			}

			/// <summary>Add to data</summary>
			public void String(string input)
			{
				if (System.String.IsNullOrEmpty(input))
				{
					Int32(0); //Int32
					return;
				}
				var inputBytes = Encoding.Unicode.GetBytes(input);
				Int32(inputBytes.Length);
				Bytes(inputBytes);
			}

			/// <summary>Add to data</summary>
			public void Byte(byte input)
			{
				Data.Add(input);
			}

			/// <summary>Add to data</summary>
			public void UInt16(UInt16 input)
			{
				Data.AddRange(BitConverter.GetBytes(input));
			}

			/// <summary>Add to data</summary>
			public void UInt32(UInt32 input)
			{
				Data.AddRange(BitConverter.GetBytes(input));
			}

			/// <summary>Add to data</summary>
			public void UInt64(UInt64 input)
			{
				Data.AddRange(BitConverter.GetBytes(input));
			}

			/// <summary>Add to data</summary>
			public void Int16(Int16 input)
			{
				Data.AddRange(BitConverter.GetBytes(input));
			}

			/// <summary>Add to data</summary>
			public void Int32(Int32 input)
			{
				Data.AddRange(BitConverter.GetBytes(input));
			}

			/// <summary>Add to data</summary>
			public void Int64(Int64 input)
			{
				Data.AddRange(BitConverter.GetBytes(input));
			}

			/// <summary>Add to data</summary>
			public void Double(Double input)
			{
				Data.AddRange(BitConverter.GetBytes(input));
			}

			/// <summary>Add to data</summary>
			public void Bytes(IEnumerable<byte> input)
			{
				Data.AddRange(input);
			}

			/// <summary>Add to data</summary>
			public void ByteArray(byte[] input)
			{
				if (input == null || input.Length == 0)
				{
					Int32(0);
					return;
				}

				Int32(input.Length);
				Data.AddRange(input);
			}

			/// <summary>Add to data</summary>
			public void TimeSpan(TimeSpan input)
			{
				Int64(input.Ticks);
			}

			/// <summary>Add to data</summary>
			public void DateTime(DateTime input)
			{
				Int64(input.ToUniversalTime().Ticks);
			}

			/// <summary>Add to data</summary>
			public void Guid(Guid id)
			{
				Data.AddRange(id.ToByteArray());
			}

			/// <summary>write a part.</summary>
			public void Part(IPart part)
			{
				part.Write(this);
			}

			/// <summary>Add to data</summary>
			public void Packet(CsoPacket input)
			{
				UInt32((uint) input.PacketType);
				UInt32(input.PacketVersion);

				var packetLengthStart = Data.Count;
				Int32(0);

				var dataStart = Data.Count;
				input.Write(this);

				var dataLength = Data.Count - dataStart;
				var dataLengthBytes = BitConverter.GetBytes(dataLength);

				Data[packetLengthStart] = dataLengthBytes[0];
				Data[packetLengthStart + 1] = dataLengthBytes[1];
				Data[packetLengthStart + 2] = dataLengthBytes[2];
				Data[packetLengthStart + 3] = dataLengthBytes[3];
			}

			/// <summary>writes the UInt16 to the writer</summary>
			public void ListOfUInt16(IEnumerable<UInt16> items)
			{
				List(items, UInt16);
			}

			/// <summary>writes the string to the writer</summary>
			public void ListOfString(IEnumerable<String> items)
			{
				List(items, String);
			}

			/// <summary>writes the parts to the writer</summary>
			public void ListOfParts(IEnumerable<IPart> parts)
			{
				List(parts, Part);
			}

			/// <summary>writes the parts to the writer</summary>
			public void ListOfPackets(IEnumerable<CsoPacket> packets)
			{
				List(packets, Packet);
			}

			/// <summary>Writes a list of elements to the stream. The list will be preceded by an Int32 count.</summary>
			/// <param name="list">the list to be written to the stream</param>
			/// <param name="writeAction">The action to write an item of the list to the stream.</param>
			public void List<TType>(IEnumerable<TType> list, Action<TType> writeAction)
			{
				if (list == null)
				{
					Int32(0);
					return;
				}
				var items = list as TType[] ?? list.ToArray();
				if (items.Length == 0)
				{
					Int32(0);
					return;
				}
				Int32(items.Length);
				foreach (var item in items)
				{
					writeAction(item);
				}
			}
		}
	}
}