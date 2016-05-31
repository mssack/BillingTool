// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-05-27</date>

using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Windows.Media.Imaging;






namespace CsWpfBase.Ev.Public.Extensions
{
	/// <summary>Wraps a bunch of reading and writing functions.</summary>
	//[DebuggerStepThrough]
	public static class FileWritingAndReadingExtensions
	{
		/// <summary>Creates the owning directory if it's not exists.</summary>
		public static void CreateDirectory_IfNotExists(this FileInfo file)
		{
			if (file.Directory == null)
				throw new DirectoryNotFoundException();
			if (!file.Directory.Exists)
				file.Directory.Create();
		}

		/// <summary>Deletes existing file if exists.</summary>
		public static void DeleteFile_IfExists(this FileInfo file)
		{
			if (file.Exists)
				file.Delete();
		}

		/// <summary>Appends an index to an file.</summary>
		public static FileInfo AppendIndexToFile_IfExists(this FileInfo file)
		{
			var dir = file.Directory;
			if (dir == null)
				throw new InvalidOperationException();

			var ext = file.Extension;

			var filenameWithNumber = file.Name.Replace(ext, "");
			var filename = filenameWithNumber;

			if (file.Exists)
			{
				var lastIndexOf = filenameWithNumber.LastIndexOf("_", StringComparison.Ordinal);
				var number = 1;


				if (lastIndexOf == -1)
				{
					file = new FileInfo(Path.Combine(dir.FullName, filename + "_" + number + ext));
				}
				else
				{
					filename = filenameWithNumber.Substring(0, lastIndexOf);
					var numberText = filenameWithNumber.Substring(lastIndexOf + 1);
					if (!int.TryParse(numberText, out number))
					{
						file = new FileInfo(Path.Combine(dir.FullName, filename + "_" + number + ext));
					}
				}
				while (file.Exists)
				{
					number++;
					file = new FileInfo(Path.Combine(dir.FullName, filename + "_" + number + ext));
				}
			}
			return file;
		}

		/// <summary>returns a <see cref="FileInfo" /> with the same file name but the directory will be the desktop.</summary>
		public static FileInfo In_Desktop_Directory(this FileInfo file)
		{
			if (file == null)
				return null;

			return new FileInfo(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), file.Name));
		}

		/// <summary>
		///     Saves a string to file using <see cref="UTF8Encoding" />.
		///     <para>If file exists then file is deleted.</para>
		///     <para>If directory not exists then directory is created</para>
		/// </summary>
		public static void SaveAs_Utf8String(this string input, FileInfo file)
		{
			if (file == null)
				throw new ArgumentException("The file is null. You must specify a file.", nameof(file));
			using (var writer = new StreamWriter(file.FullName, false, new UTF8Encoding()))
			{
				writer.Write(input);
				writer.Close();
			}
		}

		/// <summary>
		///     Saves a string to file using <see cref="UnicodeEncoding" />.
		///     <para>If file exists then file is deleted.</para>
		///     <para>If directory not exists then directory is created</para>
		/// </summary>
		public static void SaveAs_UnicodeString(this string input, FileInfo file)
		{
			if (file == null)
				throw new ArgumentException("The file is null. You must specify a file.", nameof(file));
			using (var writer = new StreamWriter(file.FullName, false, new UnicodeEncoding()))
			{
				writer.Write(input);
				writer.Close();
			}
		}

		/// <summary>
		///     Serialize a file using <see cref="BinaryFormatter" />.
		///     <para>If file exists then file is deleted.</para>
		///     <para>If directory not exists then directory is created</para>
		/// </summary>
		public static void SaveAs_SerializedBinary(this object ob, FileInfo file)
		{
			if (ob == null)
				throw new ArgumentException("The object is null.", nameof(ob));
			if (file == null)
				throw new ArgumentException("The file is null. You must specify a file.", nameof(file));
			if (ob is byte[])
				throw new ArgumentException($"The object can not be of type byte[]. Use {nameof(SaveTo_File)}() method instead.");


			file.CreateDirectory_IfNotExists();
			file.DeleteFile_IfExists();

			var binaryFormatter = new BinaryFormatter();
			using (var sr = file.Open(FileMode.CreateNew))
			{
				binaryFormatter.Serialize(sr, ob);
			}
		}

		/// <summary>
		///     Saves the binary to a file using the .NET Framework (File.WriteAllBytes).
		///     <para>If file exists then file is deleted.</para>
		///     <para>If directory not exists then directory is created</para>
		/// </summary>
		public static void SaveTo_File(this byte[] ob, FileInfo file)
		{
			if (file == null)
				throw new ArgumentException("The file is null. You must specify a file.", nameof(file));

			file.CreateDirectory_IfNotExists();
			file.DeleteFile_IfExists();
			File.WriteAllBytes(file.FullName, ob ?? new byte[0]);
		}

		/// <summary>
		///     Saves the binary to a file using the .NET Framework (File.WriteAllBytes).
		///     <para>If file exists then file is deleted.</para>
		///     <para>If directory not exists then directory is created</para>
		/// </summary>
		public static void SaveTo_File(this byte[] ob, string file)
		{
			if (file == null)
				throw new ArgumentException("The file is null. You must specify a file.", nameof(file));
			SaveTo_File(ob, new FileInfo(file));
		}

		/// <summary>Saves a image as jpg file.
		///     <para>If file exists then file is deleted.</para>
		///     <para>If directory not exists then directory is created</para>
		/// </summary>
		public static void SaveAs_JpgFile(this BitmapSource img, FileInfo file, int qualityLevel = 100)
		{
			if (img == null)
				throw new ArgumentException("The image is null. You must specify an image to save it to a file.", nameof(img));
			if (file == null)
				throw new ArgumentException("The file path is null. You must specify a file path.", nameof(file));

			var jpegBitmapEncoder = new JpegBitmapEncoder {QualityLevel = qualityLevel};
			img.SaveAs_Img(jpegBitmapEncoder, file);
		}

		/// <summary>Saves a image as png file.
		///     <para>If file exists then file is deleted.</para>
		///     <para>If directory not exists then directory is created</para>
		/// </summary>
		public static void SaveAs_PngFile(this BitmapSource img, FileInfo file, PngInterlaceOption interlace = PngInterlaceOption.Default)
		{
			if (img == null)
				throw new ArgumentException("The image is null. You must specify an image to save it to a file.", nameof(img));
			if (file == null)
				throw new ArgumentException("The file path is null. You must specify a file path.", nameof(file));

			var pngBitmapEncoder = new PngBitmapEncoder {Interlace = interlace};
			img.SaveAs_Img(pngBitmapEncoder, file);
		}

		/// <summary>Saves a image as gif file.
		///     <para>If file exists then file is deleted.</para>
		///     <para>If directory not exists then directory is created</para>
		/// </summary>
		public static void SaveAs_GifFile(this BitmapSource img, FileInfo file)
		{
			if (img == null)
				throw new ArgumentException("The image is null. You must specify an image to save it to a file.", nameof(img));
			if (file == null)
				throw new ArgumentException("The file path is null. You must specify a file path.", nameof(file));

			var gifBitmapEncoder = new GifBitmapEncoder();
			img.SaveAs_Img(gifBitmapEncoder, file);
		}

		/// <summary>Saves a image as bmp file.
		///     <para>If file exists then file is deleted.</para>
		///     <para>If directory not exists then directory is created</para>
		/// </summary>
		public static void SaveAs_BmpFile(this BitmapSource img, FileInfo file)
		{
			if (img == null)
				throw new ArgumentException("The image is null. You must specify an image to save it to a file.", nameof(img));
			if (file == null)
				throw new ArgumentException("The file path is null. You must specify a file path.", nameof(file));

			var bmpBitmapEncoder = new BmpBitmapEncoder();
			img.SaveAs_Img(bmpBitmapEncoder, file);
		}

		/// <summary>Saves a image as tiff file.
		///     <para>If file exists then file is deleted.</para>
		///     <para>If directory not exists then directory is created</para>
		/// </summary>
		public static void SaveAs_TiffFile(this BitmapSource img, FileInfo file, TiffCompressOption compressOption = TiffCompressOption.None)
		{
			if (img == null)
				throw new ArgumentException("The image is null. You must specify an image to save it to a file.", nameof(img));
			if (file == null)
				throw new ArgumentException("The file path is null. You must specify a file path.", nameof(file));

			var tiffBitmapEncoder = new TiffBitmapEncoder {Compression = compressOption};
			img.SaveAs_Img(tiffBitmapEncoder, file);
		}

		/// <summary>Saves a image as wmp file.
		///     <para>If file exists then file is deleted.</para>
		///     <para>If directory not exists then directory is created</para>
		/// </summary>
		public static void SaveAs_WmpFile(this BitmapSource img, FileInfo file)
		{
			if (img == null)
				throw new ArgumentException("The image is null. You must specify an image to save it to a file.", nameof(img));
			if (file == null)
				throw new ArgumentException("The file path is null. You must specify a file path.", nameof(file));

			var wmpBitmapEncoder = new WmpBitmapEncoder();
			//TODO include Options
			img.SaveAs_Img(wmpBitmapEncoder, file);
		}

		/// <summary>Deserialize a file using <see cref="BinaryFormatter" />.</summary>
		public static T LoadAs_Object_From_SerializedBinary<T>(this FileInfo file) where T : class
		{
			if (!file.Exists)
				throw new FileNotFoundException();
			var binaryFormatter = new BinaryFormatter();
			using (var sr = file.Open(FileMode.Open))
			{
				var deserialized = binaryFormatter.Deserialize(sr);
				return (T) deserialized;
			}
		}

		/// <summary>Uses UTF8 encoding</summary>
		public static string LoadAs_UTF8String(this FileInfo file)
		{
			if (!file.Exists)
				throw new FileNotFoundException();
			using (var reader = new StreamReader(file.FullName, Encoding.UTF8))
			{
				return reader.ReadToEnd();
			}
		}

		/// <summary>Uses UTF8 encoding</summary>
		public static string LoadAs_UnicodeString(this FileInfo file)
		{
			if (!file.Exists)
				throw new FileNotFoundException();
			using (var reader = new StreamReader(file.FullName, Encoding.Unicode))
			{
				return reader.ReadToEnd();
			}
		}

		/// <summary>Loads an image from a file.</summary>
		public static BitmapSource LoadAs_Image(this FileInfo file)
		{
			if (!file.Exists)
				throw new FileNotFoundException();
			BitmapImage img;
			using (var fs = file.Open(FileMode.Open))
			{
				var sr = new MemoryStream();
				fs.CopyTo(sr);
				sr.Seek(0, SeekOrigin.Begin);
				img = new BitmapImage();
				img.BeginInit();
				img.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
				img.CacheOption = BitmapCacheOption.OnLoad;
				img.StreamSource = sr;
				img.EndInit();
				sr.Close();
				fs.Close();
			}
			return img;
		}

		/// <summary>Reads the complete content of a file as byte array. Using the File.ReadAllBytes(string filename) method (.NET 2.0).</summary>
		public static byte[] LoadAs_ByteArray(this FileInfo file)
		{
			if (file == null)
				throw new ArgumentException("The file path cannot be null", nameof(file));
			if (!file.Exists)
				throw new FileNotFoundException();

			return File.ReadAllBytes(file.FullName);
		}

		private static void SaveAs_Img(this BitmapSource img, BitmapEncoder encoder, FileInfo file)
		{
			if (img == null)
				throw new NullReferenceException();
			using (var stream = new FileStream(file.FullName, FileMode.OpenOrCreate))
			{
				encoder.Frames.Add(BitmapFrame.Create(img));
				encoder.Save(stream);
				stream.Close();
			}
		}
	}
}