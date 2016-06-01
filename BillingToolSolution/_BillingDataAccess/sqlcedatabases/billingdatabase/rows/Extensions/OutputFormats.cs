// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-05-27</date>

using System;
using System.Linq;
using System.Windows.Markup;
using System.Windows.Media.Imaging;
using BillingDataAccess.sqlcedatabases.billingdatabase.tables;
using BillingDataAccess.sqlcedatabases.billingdatabase._Extensions.enumerations;
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
		/// <summary>Applys the database extended default values, described by developer, to the row.</summary>
		public override void ApplyExtendedDefaults()
		{
			base.ApplyExtendedDefaults();
			ApplyBusinessInfos();
		}

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


		/// <summary>The wrapper property for column property <see cref="BonLayoutNumber" />.</summary>
		[DependsOn(nameof(BonLayoutNumber))]
		public BonLayouts BonLayout
		{
			get { return EnumWrapper.Get(BonLayoutNumber, BonLayouts.Unknown); }
			set { EnumWrapper.Set(() => BonLayoutNumber = (int) value); }
		}

		/// <summary>The wrapper property for column property <see cref="BonLayoutNumber" />.</summary>
		[DependsOn(nameof(BonLayoutNumber))]
		public BonLayoutTypes BonLayoutType => BonLayout.GetBonLayoutType();

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



		/// <summary>returns true if this element has been used before.</summary>
		[DependsOn(nameof(LastUsedDate))]
		public bool HasBeenUsed => LastUsedDate != null;

		/// <summary>
		///     returns true if this <see cref="OutputFormat" /> is a default format.
		///     <remarks>OnPropertyChanged will be invoked from table <see cref="OutputFormatsTable" />.</remarks>
		/// </summary>
		public bool IsDefault => new[]
		{
			IsDefault_PrintFormat(),
			IsDefault_MailFormat(),
			IsDefault_StornoFormat(),
			IsDefault_TagesbonFormat(),
			IsDefault_MonatsbonFormat(),
			IsDefault_JahresbonFormat()
		}.Any(x => x);


		/// <summary>returns true if this <see cref="OutputFormat" /> can be modified.</summary>
		[DependsOn(nameof(HasBeenUsed))]
		public bool CanBeModified => !HasBeenUsed;

		/// <summary>returns true if this <see cref="OutputFormat" /> can be deleted.</summary>
		[DependsOn(nameof(HasBeenUsed))]
		[DependsOn(nameof(IsDefault))]
		public bool CanBeDeleted => !HasBeenUsed && !IsDefault;


		/// <summary>returns true if this element is the <see cref="OutputFormatsTable.Default_PrintFormat" />.</summary>
		public bool IsDefault_PrintFormat() => Table.Default_PrintFormat == this;

		/// <summary>returns true if this element is the <see cref="OutputFormatsTable.Default_MailFormat" />.</summary>
		public bool IsDefault_MailFormat() => Table.Default_MailFormat == this;

		/// <summary>returns true if this element is the <see cref="OutputFormatsTable.Default_StornoFormat" />.</summary>
		public bool IsDefault_StornoFormat() => Table.Default_StornoFormat == this;

		/// <summary>returns true if this element is the <see cref="OutputFormatsTable.Default_TagesBonFormat" />.</summary>
		public bool IsDefault_TagesbonFormat() => Table.Default_TagesBonFormat == this;

		/// <summary>returns true if this element is the <see cref="OutputFormatsTable.Default_MonatsBonFormat" />.</summary>
		public bool IsDefault_MonatsbonFormat() => Table.Default_MonatsBonFormat == this;

		/// <summary>returns true if this element is the <see cref="OutputFormatsTable.Default_JahresBonFormat" />.</summary>
		public bool IsDefault_JahresbonFormat() => Table.Default_JahresBonFormat == this;

		/// <summary>sets the <see cref="OutputFormat" /> as the associated default in the <see cref="OutputFormatsTable" />.</summary>
		public void SetAsDbStandard()
		{
			if (BonLayout.IsPrintLayout())
				Table.Default_PrintFormat = this;
			else if (BonLayout.IsMailLayout())
				Table.Default_MailFormat = this;
			else if (BonLayout.IsStornoLayout())
				Table.Default_StornoFormat = this;
			else if (BonLayout.IsTagesBonLayout())
				Table.Default_TagesBonFormat = this;
			else if (BonLayout.IsMonatsBonLayout())
				Table.Default_MonatsBonFormat = this;
			else if (BonLayout.IsJahresBonLayout())
				Table.Default_JahresBonFormat = this;
		}


		/// <summary>Applys the business informations.</summary>
		public void ApplyBusinessInfos()
		{
			BusinessUid = DataSet.Configurations.Business.Uid;
			BusinessName = DataSet.Configurations.Business.Name;
			BusinessAnschrift = DataSet.Configurations.Business.Anschrift;
			BusinessMail = DataSet.Configurations.Business.Mail;
			BusinessTelefon = DataSet.Configurations.Business.Telefon;
			BusinessWebsite = DataSet.Configurations.Business.Website;
		}



		private class ImageHandler
		{
			private BitmapSource _value;

			public ImageHandler(Func<byte[]> getDbValue, Action<byte[]> setDbValue, Func<string> getDbHash, Action<string> setDbHash)
			{
				GetDbValue = getDbValue;
				SetDbValue = setDbValue;
				GetDbHash = getDbHash;
				SetDbHash = setDbHash;

			}

			private BitmapSource Value
			{
				get { return _value; }
				set
				{
					_value = value;
					value?.Freeze();
				}
			}
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