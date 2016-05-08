// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-05-07</date>

using System;
using System.Windows.Markup;
using System.Windows.Media.Imaging;
using BillingDataAccess.sqlcedatabases.billingdatabase._Extensions;
using CsWpfBase.Ev.Public.Extensions;
using CsWpfBase.Utilitys;






namespace BillingDataAccess.sqlcedatabases.billingdatabase.rows
{
	partial class OutputFormat
	{
		private ImageHandler _firstImageCash;
		private ImageHandler _secondImageCash;
		private ImageHandler _thirdImageCash;


		#region Overrides/Interfaces
		/// <summary>sets the value of a column and notify property changed.</summary>
		public override bool SetDbValue<T>(T m, string columnName, string propName = "")
		{

			var changed = base.SetDbValue(m, columnName, propName);
			if (!changed)
				return false;

			if (columnName == nameof(FirstImageBinary))
				FirstImageHash = FirstImageBinary.Sha1Hash().ConvertTo_Hex();
			if (columnName == nameof(SecondImageBinary))
				SecondImageHash = SecondImageBinary.Sha1Hash().ConvertTo_Hex();
			if (columnName == nameof(ThirdImageBinary))
				ThirdImageHash = ThirdImageBinary.Sha1Hash().ConvertTo_Hex();

			return true;
		}
		#endregion


		/// <summary>The wrapper property for column property <see cref="BonLayoutName" />.</summary>
		[DependsOn(nameof(BonLayoutName))]
		public BonLayouts BonLayout
		{
			get
			{
				BonLayouts val;
				return Enum.TryParse(BonLayoutName, true, out val) ? val : BonLayouts.Unknown;
			}
			set { BonLayoutName = value.ToString(); }
		}

		/// <summary>The cashed image.</summary>
		[DependsOn(nameof(FirstImageBinary))]
		public BitmapSource FirstImage
		{
			get
			{
				if (_firstImageCash == null)
					_firstImageCash = new ImageHandler(() => FirstImageBinary, val => FirstImageBinary = val, () => FirstImageHash, val => FirstImageHash = val);

				return _firstImageCash.GetValue();
			}
			set
			{
				if (_firstImageCash == null)
					_firstImageCash = new ImageHandler(() => FirstImageBinary, val => FirstImageBinary = val, () => FirstImageHash, val => FirstImageHash = val);

				_firstImageCash.SetValue(value);
			}
		}

		/// <summary>The cashed image.</summary>
		[DependsOn(nameof(SecondImageBinary))]
		public BitmapSource SecondImage
		{
			get
			{
				if (_secondImageCash == null)
					_secondImageCash = new ImageHandler(() => SecondImageBinary, val => SecondImageBinary = val, () => SecondImageHash, val => SecondImageHash = val);

				return _secondImageCash.GetValue();
			}
			set
			{
				if (_secondImageCash == null)
					_secondImageCash = new ImageHandler(() => SecondImageBinary, val => SecondImageBinary = val, () => SecondImageHash, val => SecondImageHash = val);

				_secondImageCash.SetValue(value);
			}
		}

		/// <summary>The cashed image.</summary>
		[DependsOn(nameof(ThirdImageBinary))]
		public BitmapSource ThirdImage
		{
			get
			{
				if (_thirdImageCash == null)
					_thirdImageCash = new ImageHandler(() => ThirdImageBinary, val => ThirdImageBinary = val, () => ThirdImageHash, val => ThirdImageHash = val);

				return _thirdImageCash.GetValue();
			}
			set
			{
				if (_thirdImageCash == null)
					_thirdImageCash = new ImageHandler(() => ThirdImageBinary, val => ThirdImageBinary = val, () => ThirdImageHash, val => ThirdImageHash = val);

				_thirdImageCash.SetValue(value);
			}
		}



		/// <summary>The cashed image.</summary>
		[DependsOn(nameof(FirstImageBinary))]
		public BitmapSource FirstImageThreadSafe
		{
			get
			{
				if (_firstImageCash == null)
					_firstImageCash = new ImageHandler(() => FirstImageBinary, val => FirstImageBinary = val, () => FirstImageHash, val => FirstImageHash = val);

				return _firstImageCash.GetThreadSafeValue();
			}
		}

		/// <summary>The cashed image.</summary>
		[DependsOn(nameof(SecondImageBinary))]
		public BitmapSource SecondImageThreadSafe
		{
			get
			{
				if (_secondImageCash == null)
					_secondImageCash = new ImageHandler(() => SecondImageBinary, val => SecondImageBinary = val, () => SecondImageHash, val => SecondImageHash = val);

				return _secondImageCash.GetThreadSafeValue();
			}
		}

		/// <summary>The cashed image.</summary>
		[DependsOn(nameof(ThirdImageBinary))]
		public BitmapSource ThirdImageThreadSafe
		{
			get
			{
				if (_thirdImageCash == null)
					_thirdImageCash = new ImageHandler(() => ThirdImageBinary, val => ThirdImageBinary = val, () => ThirdImageHash, val => ThirdImageHash = val);

				return _thirdImageCash.GetThreadSafeValue();
			}
		}

		/// <summary>returns true if this element has been used before.</summary>
		[DependsOn(nameof(LastUsed))]
		public bool HasBeenUsed => LastUsed != null;
		



		private class ImageHandler
		{

			public ImageHandler(Func<byte[]> getDbValue, Action<byte[]> setDbValue, Func<string> getDbHash, Action<string> setDbHash)
			{
				GetDbValue = getDbValue;
				SetDbValue = setDbValue;
				GetDbHash = getDbHash;
				SetDbHash = setDbHash;

			}

			private BitmapSource Value { get; set; }
			private string Hash { get; set; }

			private Func<byte[]> GetDbValue { get; }
			private Action<byte[]> SetDbValue { get; }

			private Func<string> GetDbHash { get; }
			private Action<string> SetDbHash { get; }

			private ProcessLock Locker { get; } = new ProcessLock();

			private byte[] DbValue
			{
				get { return GetDbValue(); }
				set { SetDbValue(value); }
			}
			private string DbHash
			{
				get { return GetDbHash(); }
				set { SetDbHash(value); }
			}


			public BitmapSource GetValue()
			{
				if (Locker.Active)
					return Value;

				if (string.IsNullOrEmpty(DbHash))
					return null;
				if (Hash != null && Hash.Equals(DbHash))
					return Value;
				Hash = DbHash;
				Value = DbValue.ConvertTo_Image();
				return Value;
			}
			public BitmapSource GetThreadSafeValue()
			{
				if (string.IsNullOrEmpty(DbHash))
					return null;
				return DbValue.ConvertTo_Image();
			}

			public void SetValue(BitmapSource value)
			{
				if (value == null)
				{
					DbValue = null;
					DbHash = null;
					return;
				}

				Value = value;
				using (Locker.Activate())
				{
					DbValue = Value.ResizeToMaximum(400, 800).ConvertTo_PngByteArray();

					//by setting the db value the hash will be automatically created.
					Hash = DbHash;
				}
			}
		}
	}
}