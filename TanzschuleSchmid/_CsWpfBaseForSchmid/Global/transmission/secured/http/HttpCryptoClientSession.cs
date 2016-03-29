// Copyright (c) 2014, 2015 All Right Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2015-11-29</date>

using System;
using System.IO;
using System.Net;
using System.Net.Cache;
using System.Security.Cryptography;






namespace CsWpfBase.Global.transmission.secured.http
{
	/// <summary>
	///     A <see cref="HttpCryptoClientSession"/> is used whenever there is a Server and a Client where the Client has the public key and the Server holds the private
	///     key. Use this like an http request with a http response.
	/// </summary>
	public class HttpCryptoClientSession
	{
		private readonly RSACryptoServiceProvider _publicKey;

		internal HttpCryptoClientSession(RSACryptoServiceProvider publicKey, string website)
		{
			if (publicKey == null || !publicKey.PublicOnly)
				throw new ArgumentException("There have to be an public asymmetric key!", nameof(publicKey));
			if (string.IsNullOrEmpty(website))
				throw new ArgumentException("The website must be specified and cannot be null or empty.");


			Website = website;
			_publicKey = publicKey;
		}

		/// <summary>The target website.</summary>
		public string Website { get; private set; }


		/// <summary>Uploads the specified <paramref name="data" /> encrypted and returns the decrypted response from the server.</summary>
		/// <param name="data">The data to upload</param>
		/// <returns>The decrypted response</returns>
		public byte[] Upload(byte[] data)
		{
			using (Aes aes = new AesCryptoServiceProvider())
			{
				aes.Padding = PaddingMode.Zeros;
				byte[] encryptedData = Encrypt(data, aes);


				var request = (HttpWebRequest) WebRequest.Create(Website);
				request.Method = "POST";
				request.CachePolicy = new HttpRequestCachePolicy(HttpRequestCacheLevel.NoCacheNoStore);
				request.KeepAlive = false;
				request.UserAgent = null;
				request.Referer = null;
				request.Timeout = 60000;
				using (var stream = request.GetRequestStream())
				{
					stream.Write(encryptedData, 0, encryptedData.Length);
				}
				var webResponse = request.GetResponse();
				var responseData = ReadResponseData(webResponse);
				
				return Decrypt(responseData,aes);
			}

		}

		private byte[] Encrypt(byte[] data, Aes aes)
		{
			var encryptedKey = new RSAOAEPKeyExchangeFormatter(_publicKey).CreateKeyExchange(aes.Key, typeof(Aes));
			using (var ciphertext = new MemoryStream())
			{
				using (var cs = new CryptoStream(ciphertext, aes.CreateEncryptor(), CryptoStreamMode.Write))
				{
					cs.Write(data, 0, data.Length);
					cs.FlushFinalBlock();
				}


				var cipherArray = ciphertext.ToArray();
				int p = 0;

				var requestData = new byte[4 + 4 + 4 + encryptedKey.Length + aes.IV.Length + cipherArray.Length];

				Buffer.BlockCopy(BitConverter.GetBytes(encryptedKey.Length), 0, requestData, p, 4);
				p = p + 4;
				Buffer.BlockCopy(BitConverter.GetBytes(aes.IV.Length), 0, requestData, p, 4);
				p = p + 4;
				Buffer.BlockCopy(BitConverter.GetBytes(data.Length), 0, requestData, p, 4);
				p = p + 4;

				Buffer.BlockCopy(encryptedKey, 0, requestData, p, encryptedKey.Length);
				p = p + encryptedKey.Length;

				Buffer.BlockCopy(aes.IV, 0, requestData, p, aes.IV.Length);
				p = p + aes.IV.Length;

				Buffer.BlockCopy(cipherArray, 0, requestData, p, cipherArray.Length);
				return requestData;
			}
		}

		private byte[] ReadResponseData(WebResponse response)
		{

			var responseData = new byte[response.ContentLength];
			var pos = 0;
			using (var stream = response.GetResponseStream())
			{
				while (pos < responseData.Length)
				{
					var bytesRead = stream.Read(responseData, pos, responseData.Length - pos);
					if (bytesRead == 0)
					{
						// End of data and we didn't finish reading. Oops.
						throw new IOException("Premature end of data");
					}
					pos += bytesRead;
				}
			}
			return responseData;
		}

		private byte[] Decrypt(byte[] data, Aes aes)
		{
			using (var plainData = new MemoryStream())
			{
				using (var cs = new CryptoStream(plainData, aes.CreateDecryptor(), CryptoStreamMode.Write))
				{
					cs.Write(data, 4, data.Length-4);
					cs.FlushFinalBlock();
				}
				var dataArray = plainData.ToArray();
				Array.Resize(ref dataArray, BitConverter.ToInt32(data, 0));
				return dataArray;
			}
		}
	}
}