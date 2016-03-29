// Copyright (c) 2014, 2015 All Right Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2015-11-29</date>

using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Web;






namespace CsWpfBase.Global.transmission.secured.http
{
	/// <summary>
	///     A <see cref="HttpCryptoServerSession" />e is used whenever there is a client and a server where the client has the public key and the server holds the
	///     private key. Use this like an http request handler with a http response.
	/// </summary>
	public class HttpCryptoServerSession
	{
		private readonly RSACryptoServiceProvider _privateKey;
		private readonly HttpRequestBase _request;
		private byte[] _inputData;
		private byte[] _iv;
		private byte[] _symmetricKey;

		internal HttpCryptoServerSession(RSACryptoServiceProvider privateKey, HttpRequestBase httpRequest)
		{
			if (privateKey == null)
				throw new ArgumentException("The parameter cannot be null", nameof(privateKey));
			if (privateKey.PublicOnly)
				throw new ArgumentException($"The {nameof(HttpCryptoServerSession)} accepts private keys only.", nameof(privateKey));
			if (httpRequest == null)
				throw new ArgumentException("The parameter cannot be null", nameof(httpRequest));

			_request = httpRequest;
			_privateKey = privateKey;
		}

		/// <summary>The decrypted input data.</summary>
		public byte[] RequestData
		{
			get
			{
				if (_inputData == null)
					DecryptData();
				return _inputData;
			}
		}



		/// <summary>Encrypts data which needs to be sent back to the client as a response.</summary>
		/// <param name="rawData">the data which needs to be encrypted.</param>
		public byte[] CreateResponse(byte[] rawData)
		{
			DecryptData();

			using (Aes aes = new AesCryptoServiceProvider())
			{
				aes.Padding = PaddingMode.Zeros;
				aes.Key = _symmetricKey;
				aes.IV = _iv;

				using (var ciphertext = new MemoryStream())
				{
					ciphertext.Write(BitConverter.GetBytes(rawData.Length),0,4);

					using (var cs = new CryptoStream(ciphertext, aes.CreateEncryptor(), CryptoStreamMode.Write))
					{
						cs.Write(rawData, 0, rawData.Length);
						cs.FlushFinalBlock();
					}
					
					return ciphertext.ToArray();
				}
			}
		}




		/// <summary>Decrypt a message sent by the sender (<see cref="HttpCryptoClientSession" />) and stores the encryption key and iv.</summary>
		private void DecryptData()
		{
			if (_inputData != null)
				return;

			var rawData = GetRawData();

			if (rawData.Length < 8)
				throw new InvalidDataException("The data which needs to be decrypted contains an invalid data length.");



			var p = 0;
			var keyLength = BitConverter.ToInt32(rawData, p);
			p = p + 4;
			var ivLength = BitConverter.ToInt32(rawData, p);
			p = p + 4;
			var dataLength = BitConverter.ToInt32(rawData, p);
			p = p + 4;

			if (rawData.Length < p + keyLength + ivLength + dataLength)
				throw new InvalidDataException("The data which needs to be decrypted contains an invalid data length.");


			try
			{
				using (Aes aes = new AesCryptoServiceProvider())
				{
					aes.Padding = PaddingMode.Zeros;

					aes.Key = _symmetricKey = new RSAOAEPKeyExchangeDeformatter(_privateKey).DecryptKeyExchange(rawData.Skip(p).Take(keyLength).ToArray());
					p = p + keyLength;
					aes.IV = _iv = rawData.Skip(p).Take(ivLength).ToArray();
					p = p + ivLength;


					// Decrypt the message 
					using (var plainData = new MemoryStream())
					{
						using (var cs = new CryptoStream(plainData, aes.CreateDecryptor(), CryptoStreamMode.Write))
						{
							cs.Write(rawData, p, rawData.Length - p);
							cs.FlushFinalBlock();
						}

						_inputData = plainData.ToArray();
						Array.Resize(ref _inputData, dataLength);
					}
				}
			}
			catch (Exception exp)
			{
				throw new InvalidDataException("The data couldn't be decrypted see inner Exception", exp);
			}
		}

		private byte[] GetRawData()
		{
			var inputData = new byte[_request.ContentLength];
			var pos = 0;
			while (pos < _request.InputStream.Length)
			{
				var bytesRead = _request.InputStream.Read(inputData, pos, inputData.Length - pos);
				if (bytesRead == 0)
				{
					// End of data and we didn't finish reading. Oops.
					throw new IOException("Premature end of data");
				}
				pos += bytesRead;
			}
			return inputData;
		}
	}
}