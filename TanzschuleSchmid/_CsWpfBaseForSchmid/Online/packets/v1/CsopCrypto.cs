// Copyright (c) 2014, 2015 All Right Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2015-09-16</date>

using System;
using System.IO;
using System.Security.Cryptography;
using CsWpfBase.Ev.Objects;
using CsWpfBase.Ev.Public.Extensions;






namespace CsWpfBase.Online.packets.v1
{
	/// <summary>Crypto Packet V1</summary>
	[Serializable]
	public class CsopCrypto : CsoPacket
	{
		private byte[] _encryptedPacket;
		private byte[] _encryptedSymmetricKey;
		private byte[] _iv;
		private byte _keyVersion;
		


		#region Overrides/Interfaces
		/// <summary>Gets the type code which is used to parse an unknown packet into a specific .Net type.</summary>
		public override Types PacketType
		{
			get { return Types.Crypted; }
		}
		/// <summary>
		///     The initial Value of the <see cref="CsoPacket.PacketVersion" />, this value will be applied to the <see cref="CsoPacket.PacketVersion" />
		///     property whenever the packet is created or no version is defined.
		/// </summary>
		protected override uint InitialVersion
		{
			get { return 1; }
		}

		/// <summary>Interprets a binary into this object.</summary>
		internal override sealed void Parse(Reader reader, int length)
		{
			KeyVersion = reader.Byte();
			Iv = reader.ByteArray();
			EncryptedSymmetricKey = reader.ByteArray();
			EncryptedPacket = reader.ByteArray();
		}

		/// <summary>converts this object into binary and writes the content to the Writer.</summary>
		internal override sealed void Write(Writer writer)
		{
			writer.Byte(KeyVersion);
			writer.ByteArray(Iv);
			writer.ByteArray(EncryptedSymmetricKey);
			writer.ByteArray(EncryptedPacket);
		}
		#endregion


		/// <summary>The associated key pair version</summary>
		public byte KeyVersion
		{
			get { return _keyVersion; }
			set { SetProperty(ref _keyVersion, value); }
		}
		/// <summary>
		///     initialization vector (IV) or starting variable (SV)[1] is a fixed-size input to a cryptographic primitive that is typically required to be
		///     random or pseudorandom.
		/// </summary>
		public byte[] Iv
		{
			get { return _iv; }
			set { SetProperty(ref _iv, value); }
		}
		/// <summary>the encrypted symmetric key.</summary>
		public byte[] EncryptedSymmetricKey
		{
			get { return _encryptedSymmetricKey; }
			set { SetProperty(ref _encryptedSymmetricKey, value); }
		}
		/// <summary>the symmetric encrypted content. usually a packet</summary>
		public byte[] EncryptedPacket
		{
			get { return _encryptedPacket; }
			set { SetProperty(ref _encryptedPacket, value); }
		}

		/// <summary>Parses the cryptographic packet from data.</summary>
		public static CsopCrypto Get(byte[] data)
		{
			var packet = new Reader(data, 0).Packet();
			if (packet.PacketType != Types.Crypted)
				throw new Exception($"The packet is not of type {Types.Crypted}");
			return (CsopCrypto) packet;
		}

		/// <summary>Parses the cryptographic packet from stream.</summary>
		public static CsopCrypto Get(Stream sr, int length)
		{
			return Get(sr.ToByteArray(length));
		}



		/// <summary>Session used for temporary storing encryption details.</summary>
		[Serializable]
		public class Session : Base
		{
			private RSACryptoServiceProvider _asymetricKey;
			private byte[] _encryptedSymetricKey;
			private bool _isKeyLoaded;
			private byte[] _iv;
			private byte[] _symetricKey;

			/// <summary>
			///     initializes a new session by defining the key version used. If you are at a private key environment set the associated
			///     <see cref="CsOnline.Key" /> accordingly
			/// </summary>
			public Session(RSACryptoServiceProvider key, bool createNewSymetricKey)
			{
				AsymetricKey = key;

				if (createNewSymetricKey)
					CreateNewSymetricKey();
			}

			/// <summary>the asymetric key provider. If the key provided is a private Key this provider will be either.</summary>
			public RSACryptoServiceProvider AsymetricKey
			{
				get { return _asymetricKey; }
				private set { SetProperty(ref _asymetricKey, value); }
			}
			/// <summary>
			///     initialization vector (IV) or starting variable (SV)[1] is a fixed-size input to a cryptographic primitive that is typically required to be
			///     random or pseudorandom.
			/// </summary>
			public byte[] Iv
			{
				get { return _iv; }
				private set { SetProperty(ref _iv, value); }
			}
			/// <summary>The unencrypted symmetric key.</summary>
			public byte[] SymetricKey
			{
				get { return _symetricKey; }
				private set { SetProperty(ref _symetricKey, value); }
			}
			/// <summary>The asymmetric encrypted Key.</summary>
			public byte[] EncryptedSymetricKey
			{
				get { return _encryptedSymetricKey; }
				private set { SetProperty(ref _encryptedSymetricKey, value); }
			}
			/// <summary>Defines if the key is already loaded.</summary>
			public bool IsKeyLoaded
			{
				get { return _isKeyLoaded; }
				private set { SetProperty(ref _isKeyLoaded, value); }
			}

			/// <summary>Get the unencrypted key by reading a <see cref="CsopCrypto" /> packet. Needs the private key version</summary>
			public void GetKeys(CsopCrypto packet)
			{
				if (AsymetricKey.PublicOnly)
					throw new InvalidOperationException("a private key is needed");
				if (packet.Iv == null || packet.EncryptedSymmetricKey == null)
					throw new InvalidOperationException("no key information in packet. Wrong communication sequence.");

				Iv = packet.Iv;
				SymetricKey = new RSAOAEPKeyExchangeDeformatter(AsymetricKey).DecryptKeyExchange(packet.EncryptedSymmetricKey);
				IsKeyLoaded = true;
			}

			/// <summary>encrypts a packet</summary>
			public CsopCrypto EncryptPacket(CsoPacket unencryptedPacket, byte keyVersion)
			{
				var unencryptedData = unencryptedPacket.GetData();



				using (Aes aes = new AesCryptoServiceProvider())
				{
					aes.Padding = PaddingMode.Zeros;
					aes.IV = Iv;
					aes.Key = SymetricKey;

					using (var ciphertext = new MemoryStream())
					{
						using (var cs = new CryptoStream(ciphertext, aes.CreateEncryptor(), CryptoStreamMode.Write))
						{
							cs.Write(unencryptedData, 0, unencryptedData.Length);
							cs.FlushFinalBlock();
						}
						var cryptoPacket = new CsopCrypto();
						if (EncryptedSymetricKey != null)
						{
							cryptoPacket.Iv = Iv;
							cryptoPacket.EncryptedSymmetricKey = EncryptedSymetricKey;
						}
						cryptoPacket.EncryptedPacket = ciphertext.ToArray();
						cryptoPacket.KeyVersion = keyVersion;

						return cryptoPacket;
					}
				}
			}

			/// <summary>Decrypts the packet</summary>
			public CsoPacket DecryptPacket(CsopCrypto cryptedPacket)
			{
				using (Aes aes = new AesCryptoServiceProvider())
				{
					aes.Padding = PaddingMode.Zeros;
					aes.IV = Iv;
					aes.Key = SymetricKey;

					// Decrypt the message 
					using (var plainData = new MemoryStream())
					{
						using (var cs = new CryptoStream(plainData, aes.CreateDecryptor(), CryptoStreamMode.Write))
						{
							cs.Write(cryptedPacket.EncryptedPacket, 0, cryptedPacket.EncryptedPacket.Length);
							cs.FlushFinalBlock();
						}
						return CsOnline.Parser.ParsePacket(plainData.ToArray(), 0);
					}
				}
			}

			private void CreateNewSymetricKey()
			{
				using (Aes aes = new AesCryptoServiceProvider())
				{
					aes.Padding = PaddingMode.Zeros;
					SymetricKey = aes.Key;
					Iv = aes.IV;
				}
				EncryptedSymetricKey = new RSAOAEPKeyExchangeFormatter(AsymetricKey).CreateKeyExchange(SymetricKey, typeof (Aes));
				IsKeyLoaded = true;
			}
		}
	}
}