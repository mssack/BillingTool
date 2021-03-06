//********************************************************************
//
//                 AUTOGENERATED CONTENT DO NOT MODIFY
//                      PRODUCED BY CsWpfBase.Db
//
//********************************************************************
//
//
//Copyright (c) 2014 - 2016 All rights reserved Christian Sack
//<author>Christian Sack</author>
//<email>service.christian@sack.at</email>
//<website>christian.sack.at</website>
//<date>2016-05-27 14:11:39</date>



using System;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Markup;
using CsWpfBase.Ev.Public.Extensions;
using CsWpfBase.Db.attributes;
using CsWpfBase.Db.attributes.columnAttributes;
using CsWpfBase.Db.models;
using CsWpfBase.Db.models.helper;
using CsWpfBase.Db.models.bases;
using CsWpfBase.Db.router;
using BillingDataAccess.sqlcedatabases.billingdatabase.dataset;
using BillingDataAccess.sqlcedatabases.billingdatabase.tables;
using BillingDataAccess.sqlcedatabases.billingdatabase.rowinterfaces;






namespace BillingDataAccess.sqlcedatabases.billingdatabase.rows
{
	#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
	
	///<summary>DataRow([<c>BillingDatabase</c>].[<c>OutputFormats</c>]): row model of <see cref="OutputFormatsTable"/>.</summary>
	[Serializable]
	[DebuggerDisplay("DataRow(BillingDatabase.OutputFormats): Id = '{Id}'")]
	[CsDbDataRow(Database = "BillingDatabase", TableName = "OutputFormats", Generated = "16.05.27 14:11:39", Hash = "775463477EAD7949C18F91D6B5135088")]
	public partial class OutputFormat : CsDbTableRow, IOutputFormat
	{
		#region statics
		private static Dictionary<string, System.Reflection.PropertyInfo> _nativeColumnName_To_Property;
		public static Dictionary<string, System.Reflection.PropertyInfo> NativeColumnName_To_Property
		{
			get
			{
				if (_nativeColumnName_To_Property != null)
					return _nativeColumnName_To_Property;
		
				var type = typeof(IOutputFormat);
				_nativeColumnName_To_Property = new Dictionary<string, System.Reflection.PropertyInfo>
				{
					{ OutputFormatsTable.IdCol, type.GetProperty(nameof(Id)) },
					{ OutputFormatsTable.CreationDateCol, type.GetProperty(nameof(CreationDate)) },
					{ OutputFormatsTable.LastUsedDateCol, type.GetProperty(nameof(LastUsedDate)) },
					{ OutputFormatsTable.NameCol, type.GetProperty(nameof(Name)) },
					{ OutputFormatsTable.BonLayoutNumberCol, type.GetProperty(nameof(BonLayoutNumber)) },
					{ OutputFormatsTable.FirstImageBinaryCol, type.GetProperty(nameof(FirstImageBinary)) },
					{ OutputFormatsTable.FirstImageHashCol, type.GetProperty(nameof(FirstImageHash)) },
					{ OutputFormatsTable.FirstTextCol, type.GetProperty(nameof(FirstText)) },
					{ OutputFormatsTable.SecondImageBinaryCol, type.GetProperty(nameof(SecondImageBinary)) },
					{ OutputFormatsTable.SecondImageHashCol, type.GetProperty(nameof(SecondImageHash)) },
					{ OutputFormatsTable.SecondTextCol, type.GetProperty(nameof(SecondText)) },
					{ OutputFormatsTable.ThirdImageBinaryCol, type.GetProperty(nameof(ThirdImageBinary)) },
					{ OutputFormatsTable.ThirdImageHashCol, type.GetProperty(nameof(ThirdImageHash)) },
					{ OutputFormatsTable.ThirdTextCol, type.GetProperty(nameof(ThirdText)) },
					{ OutputFormatsTable.ScalingCol, type.GetProperty(nameof(Scaling)) },
					{ OutputFormatsTable.BusinessUidCol, type.GetProperty(nameof(BusinessUid)) },
					{ OutputFormatsTable.BusinessNameCol, type.GetProperty(nameof(BusinessName)) },
					{ OutputFormatsTable.BusinessAnschriftCol, type.GetProperty(nameof(BusinessAnschrift)) },
					{ OutputFormatsTable.BusinessMailCol, type.GetProperty(nameof(BusinessMail)) },
					{ OutputFormatsTable.BusinessTelefonCol, type.GetProperty(nameof(BusinessTelefon)) },
					{ OutputFormatsTable.BusinessWebsiteCol, type.GetProperty(nameof(BusinessWebsite)) },
					{ OutputFormatsTable.IsBusinessUidVisibleCol, type.GetProperty(nameof(IsBusinessUidVisible)) },
					{ OutputFormatsTable.IsBusinessNameVisibleCol, type.GetProperty(nameof(IsBusinessNameVisible)) },
					{ OutputFormatsTable.IsBusinessAnschriftVisibleCol, type.GetProperty(nameof(IsBusinessAnschriftVisible)) },
					{ OutputFormatsTable.IsBusinessMailVisibleCol, type.GetProperty(nameof(IsBusinessMailVisible)) },
					{ OutputFormatsTable.IsBusinessTelefonVisibleCol, type.GetProperty(nameof(IsBusinessTelefonVisible)) },
					{ OutputFormatsTable.IsBusinessWebsiteVisibleCol, type.GetProperty(nameof(IsBusinessWebsiteVisible)) },
					{ OutputFormatsTable.IsKassenoperatorVisibleCol, type.GetProperty(nameof(IsKassenoperatorVisible)) },
					{ OutputFormatsTable.CommentCol, type.GetProperty(nameof(Comment)) },
					{ OutputFormatsTable.CommentLastChangedCol, type.GetProperty(nameof(CommentLastChanged)) }
				};
		
				return _nativeColumnName_To_Property;
			}
		}
		#endregion
	
	
		public OutputFormat(DataRowBuilder builder) : base(builder){}
	
		
		///<summary>References the owning data context, this is equal to the database server. Use this to address databases on the same server.</summary>
		public new BillingDataAccess.sqlcedatabases.SqlCeDatabasesContext DataContext => Table.DataContext;
	
		///<summary>References the owning dataset. Use this to address tables in the same database</summary>
		public new BillingDatabase DataSet => Table.DataSet;
	
		///	<summary>Gets the owning table of type <see cref="OutputFormatsTable"/>.</summary>
		public new OutputFormatsTable Table => (OutputFormatsTable) base.Table;
	
	
	
	
		#region COLUMNS
		///<summary>
		///		[<c>BillingDatabase</c>].[<c>OutputFormats</c>].[<c>Id</c>] (Type = <c>uniqueidentifier</c>, Default = '<c>newid()</c>')
		///</summary>
		[CsDbDataColumn(Default = "Guid.NewGuid()")]
		[CsDbNativeDataColumn(Name = "Id", Type = "uniqueidentifier", Default = "newid()", IsNullable = "NO")]
		public Guid Id
		{
			get { return GetDbValue<Guid>(OutputFormatsTable.IdCol); }
			set 
			{ 
				if (!SetDbValue(value, OutputFormatsTable.IdCol)) return;
			}
		}
		///<summary>
		///		[<c>BillingDatabase</c>].[<c>OutputFormats</c>].[<c>CreationDate</c>] (Type = <c>datetime</c>, Default = '<c>getdate()</c>')
		///</summary>
		[CsDbDataColumn(Default = "DateTime.Now")]
		[CsDbNativeDataColumn(Name = "CreationDate", Type = "datetime", Default = "getdate()", IsNullable = "NO")]
		public DateTime CreationDate
		{
			get { return GetDbValue<DateTime>(OutputFormatsTable.CreationDateCol); }
			set 
			{ 
				if (!SetDbValue(value, OutputFormatsTable.CreationDateCol)) return;
			}
		}
		///<summary>
		///		[<c>BillingDatabase</c>].[<c>OutputFormats</c>].[<c>LastUsedDate</c>] (Type = <c>datetime</c>, <c>NULLABLE</c>)
		///</summary>
		[CsDbDataColumn(IsNullable = true)]
		[CsDbNativeDataColumn(Name = "LastUsedDate", Type = "datetime", IsNullable = "YES")]
		public DateTime? LastUsedDate
		{
			get { return GetDbValue<DateTime?>(OutputFormatsTable.LastUsedDateCol); }
			set 
			{ 
				if (!SetDbValue(value, OutputFormatsTable.LastUsedDateCol)) return;
			}
		}
		///<summary>
		///		[<c>BillingDatabase</c>].[<c>OutputFormats</c>].[<c>Name</c>] (Type = <c>ntext</c>, MaxLength = <c>536870911</c>)
		///</summary>
		[CsDbDataColumn(MaxLength = 536870911)]
		[CsDbNativeDataColumn(Name = "Name", Type = "ntext", MaxLength = "536870911", IsNullable = "NO")]
		public String Name
		{
			get { return GetDbValue<String>(OutputFormatsTable.NameCol); }
			set 
			{ 
				if (!SetDbValue(value, OutputFormatsTable.NameCol)) return;
			}
		}
		///<summary>
		///		[<c>BillingDatabase</c>].[<c>OutputFormats</c>].[<c>BonLayoutNumber</c>] (Type = <c>int</c>, Default = '<c>(0)</c>')
		///</summary>
		[CsDbDataColumn(Default = 0)]
		[CsDbNativeDataColumn(Name = "BonLayoutNumber", Type = "int", Default = "(0)", IsNullable = "NO")]
		public Int32 BonLayoutNumber
		{
			get { return GetDbValue<Int32>(OutputFormatsTable.BonLayoutNumberCol); }
			set 
			{ 
				if (!SetDbValue(value, OutputFormatsTable.BonLayoutNumberCol)) return;
			}
		}
		///<summary>
		///		[<c>BillingDatabase</c>].[<c>OutputFormats</c>].[<c>FirstImageBinary</c>] (Type = <c>image</c>, <c>NULLABLE</c>, MaxLength = <c>1073741823</c>)
		///</summary>
		[CsDbDataColumn(MaxLength = 1073741823, IsNullable = true)]
		[CsDbNativeDataColumn(Name = "FirstImageBinary", Type = "image", MaxLength = "1073741823", IsNullable = "YES")]
		public Byte[] FirstImageBinary
		{
			get { return GetDbValue<Byte[]>(OutputFormatsTable.FirstImageBinaryCol); }
			set 
			{ 
				if (!SetDbValue(value, OutputFormatsTable.FirstImageBinaryCol)) return;
			}
		}
		///<summary>
		///		[<c>BillingDatabase</c>].[<c>OutputFormats</c>].[<c>FirstImageHash</c>] (Type = <c>ntext</c>, <c>NULLABLE</c>, MaxLength = <c>536870911</c>)
		///</summary>
		[CsDbDataColumn(MaxLength = 536870911, IsNullable = true)]
		[CsDbNativeDataColumn(Name = "FirstImageHash", Type = "ntext", MaxLength = "536870911", IsNullable = "YES")]
		public String FirstImageHash
		{
			get { return GetDbValue<String>(OutputFormatsTable.FirstImageHashCol); }
			set 
			{ 
				if (!SetDbValue(value, OutputFormatsTable.FirstImageHashCol)) return;
			}
		}
		///<summary>
		///		[<c>BillingDatabase</c>].[<c>OutputFormats</c>].[<c>FirstText</c>] (Type = <c>ntext</c>, <c>NULLABLE</c>, MaxLength = <c>536870911</c>)
		///</summary>
		[CsDbDataColumn(MaxLength = 536870911, IsNullable = true)]
		[CsDbNativeDataColumn(Name = "FirstText", Type = "ntext", MaxLength = "536870911", IsNullable = "YES")]
		public String FirstText
		{
			get { return GetDbValue<String>(OutputFormatsTable.FirstTextCol); }
			set 
			{ 
				if (!SetDbValue(value, OutputFormatsTable.FirstTextCol)) return;
			}
		}
		///<summary>
		///		[<c>BillingDatabase</c>].[<c>OutputFormats</c>].[<c>SecondImageBinary</c>] (Type = <c>image</c>, <c>NULLABLE</c>, MaxLength = <c>1073741823</c>)
		///</summary>
		[CsDbDataColumn(MaxLength = 1073741823, IsNullable = true)]
		[CsDbNativeDataColumn(Name = "SecondImageBinary", Type = "image", MaxLength = "1073741823", IsNullable = "YES")]
		public Byte[] SecondImageBinary
		{
			get { return GetDbValue<Byte[]>(OutputFormatsTable.SecondImageBinaryCol); }
			set 
			{ 
				if (!SetDbValue(value, OutputFormatsTable.SecondImageBinaryCol)) return;
			}
		}
		///<summary>
		///		[<c>BillingDatabase</c>].[<c>OutputFormats</c>].[<c>SecondImageHash</c>] (Type = <c>ntext</c>, <c>NULLABLE</c>, MaxLength = <c>536870911</c>)
		///</summary>
		[CsDbDataColumn(MaxLength = 536870911, IsNullable = true)]
		[CsDbNativeDataColumn(Name = "SecondImageHash", Type = "ntext", MaxLength = "536870911", IsNullable = "YES")]
		public String SecondImageHash
		{
			get { return GetDbValue<String>(OutputFormatsTable.SecondImageHashCol); }
			set 
			{ 
				if (!SetDbValue(value, OutputFormatsTable.SecondImageHashCol)) return;
			}
		}
		///<summary>
		///		[<c>BillingDatabase</c>].[<c>OutputFormats</c>].[<c>SecondText</c>] (Type = <c>ntext</c>, <c>NULLABLE</c>, MaxLength = <c>536870911</c>)
		///</summary>
		[CsDbDataColumn(MaxLength = 536870911, IsNullable = true)]
		[CsDbNativeDataColumn(Name = "SecondText", Type = "ntext", MaxLength = "536870911", IsNullable = "YES")]
		public String SecondText
		{
			get { return GetDbValue<String>(OutputFormatsTable.SecondTextCol); }
			set 
			{ 
				if (!SetDbValue(value, OutputFormatsTable.SecondTextCol)) return;
			}
		}
		///<summary>
		///		[<c>BillingDatabase</c>].[<c>OutputFormats</c>].[<c>ThirdImageBinary</c>] (Type = <c>image</c>, <c>NULLABLE</c>, MaxLength = <c>1073741823</c>)
		///</summary>
		[CsDbDataColumn(MaxLength = 1073741823, IsNullable = true)]
		[CsDbNativeDataColumn(Name = "ThirdImageBinary", Type = "image", MaxLength = "1073741823", IsNullable = "YES")]
		public Byte[] ThirdImageBinary
		{
			get { return GetDbValue<Byte[]>(OutputFormatsTable.ThirdImageBinaryCol); }
			set 
			{ 
				if (!SetDbValue(value, OutputFormatsTable.ThirdImageBinaryCol)) return;
			}
		}
		///<summary>
		///		[<c>BillingDatabase</c>].[<c>OutputFormats</c>].[<c>ThirdImageHash</c>] (Type = <c>ntext</c>, <c>NULLABLE</c>, MaxLength = <c>536870911</c>)
		///</summary>
		[CsDbDataColumn(MaxLength = 536870911, IsNullable = true)]
		[CsDbNativeDataColumn(Name = "ThirdImageHash", Type = "ntext", MaxLength = "536870911", IsNullable = "YES")]
		public String ThirdImageHash
		{
			get { return GetDbValue<String>(OutputFormatsTable.ThirdImageHashCol); }
			set 
			{ 
				if (!SetDbValue(value, OutputFormatsTable.ThirdImageHashCol)) return;
			}
		}
		///<summary>
		///		[<c>BillingDatabase</c>].[<c>OutputFormats</c>].[<c>ThirdText</c>] (Type = <c>ntext</c>, <c>NULLABLE</c>, MaxLength = <c>536870911</c>)
		///</summary>
		[CsDbDataColumn(MaxLength = 536870911, IsNullable = true)]
		[CsDbNativeDataColumn(Name = "ThirdText", Type = "ntext", MaxLength = "536870911", IsNullable = "YES")]
		public String ThirdText
		{
			get { return GetDbValue<String>(OutputFormatsTable.ThirdTextCol); }
			set 
			{ 
				if (!SetDbValue(value, OutputFormatsTable.ThirdTextCol)) return;
			}
		}
		///<summary>
		///		[<c>BillingDatabase</c>].[<c>OutputFormats</c>].[<c>Scaling</c>] (Type = <c>float</c>, Default = '<c>(1)</c>')
		///</summary>
		[CsDbDataColumn(Default = 1)]
		[CsDbNativeDataColumn(Name = "Scaling", Type = "float", Default = "(1)", IsNullable = "NO")]
		public Double Scaling
		{
			get { return GetDbValue<Double>(OutputFormatsTable.ScalingCol); }
			set 
			{ 
				if (!SetDbValue(value, OutputFormatsTable.ScalingCol)) return;
			}
		}
		///<summary>
		///		[<c>BillingDatabase</c>].[<c>OutputFormats</c>].[<c>BusinessUid</c>] (Type = <c>ntext</c>, <c>NULLABLE</c>, MaxLength = <c>536870911</c>)
		///</summary>
		[CsDbDataColumn(MaxLength = 536870911, IsNullable = true)]
		[CsDbNativeDataColumn(Name = "BusinessUid", Type = "ntext", MaxLength = "536870911", IsNullable = "YES")]
		public String BusinessUid
		{
			get { return GetDbValue<String>(OutputFormatsTable.BusinessUidCol); }
			set 
			{ 
				if (!SetDbValue(value, OutputFormatsTable.BusinessUidCol)) return;
			}
		}
		///<summary>
		///		[<c>BillingDatabase</c>].[<c>OutputFormats</c>].[<c>BusinessName</c>] (Type = <c>ntext</c>, <c>NULLABLE</c>, MaxLength = <c>536870911</c>)
		///</summary>
		[CsDbDataColumn(MaxLength = 536870911, IsNullable = true)]
		[CsDbNativeDataColumn(Name = "BusinessName", Type = "ntext", MaxLength = "536870911", IsNullable = "YES")]
		public String BusinessName
		{
			get { return GetDbValue<String>(OutputFormatsTable.BusinessNameCol); }
			set 
			{ 
				if (!SetDbValue(value, OutputFormatsTable.BusinessNameCol)) return;
			}
		}
		///<summary>
		///		[<c>BillingDatabase</c>].[<c>OutputFormats</c>].[<c>BusinessAnschrift</c>] (Type = <c>ntext</c>, <c>NULLABLE</c>, MaxLength = <c>536870911</c>)
		///</summary>
		[CsDbDataColumn(MaxLength = 536870911, IsNullable = true)]
		[CsDbNativeDataColumn(Name = "BusinessAnschrift", Type = "ntext", MaxLength = "536870911", IsNullable = "YES")]
		public String BusinessAnschrift
		{
			get { return GetDbValue<String>(OutputFormatsTable.BusinessAnschriftCol); }
			set 
			{ 
				if (!SetDbValue(value, OutputFormatsTable.BusinessAnschriftCol)) return;
			}
		}
		///<summary>
		///		[<c>BillingDatabase</c>].[<c>OutputFormats</c>].[<c>BusinessMail</c>] (Type = <c>ntext</c>, <c>NULLABLE</c>, MaxLength = <c>536870911</c>)
		///</summary>
		[CsDbDataColumn(MaxLength = 536870911, IsNullable = true)]
		[CsDbNativeDataColumn(Name = "BusinessMail", Type = "ntext", MaxLength = "536870911", IsNullable = "YES")]
		public String BusinessMail
		{
			get { return GetDbValue<String>(OutputFormatsTable.BusinessMailCol); }
			set 
			{ 
				if (!SetDbValue(value, OutputFormatsTable.BusinessMailCol)) return;
			}
		}
		///<summary>
		///		[<c>BillingDatabase</c>].[<c>OutputFormats</c>].[<c>BusinessTelefon</c>] (Type = <c>ntext</c>, <c>NULLABLE</c>, MaxLength = <c>536870911</c>)
		///</summary>
		[CsDbDataColumn(MaxLength = 536870911, IsNullable = true)]
		[CsDbNativeDataColumn(Name = "BusinessTelefon", Type = "ntext", MaxLength = "536870911", IsNullable = "YES")]
		public String BusinessTelefon
		{
			get { return GetDbValue<String>(OutputFormatsTable.BusinessTelefonCol); }
			set 
			{ 
				if (!SetDbValue(value, OutputFormatsTable.BusinessTelefonCol)) return;
			}
		}
		///<summary>
		///		[<c>BillingDatabase</c>].[<c>OutputFormats</c>].[<c>BusinessWebsite</c>] (Type = <c>ntext</c>, <c>NULLABLE</c>, MaxLength = <c>536870911</c>)
		///</summary>
		[CsDbDataColumn(MaxLength = 536870911, IsNullable = true)]
		[CsDbNativeDataColumn(Name = "BusinessWebsite", Type = "ntext", MaxLength = "536870911", IsNullable = "YES")]
		public String BusinessWebsite
		{
			get { return GetDbValue<String>(OutputFormatsTable.BusinessWebsiteCol); }
			set 
			{ 
				if (!SetDbValue(value, OutputFormatsTable.BusinessWebsiteCol)) return;
			}
		}
		///<summary>
		///		[<c>BillingDatabase</c>].[<c>OutputFormats</c>].[<c>IsBusinessUidVisible</c>] (Type = <c>bit</c>, Default = '<c>(1)</c>')
		///</summary>
		[CsDbDataColumn(Default = true)]
		[CsDbNativeDataColumn(Name = "IsBusinessUidVisible", Type = "bit", Default = "(1)", IsNullable = "NO")]
		public Boolean IsBusinessUidVisible
		{
			get { return GetDbValue<Boolean>(OutputFormatsTable.IsBusinessUidVisibleCol); }
			set 
			{ 
				if (!SetDbValue(value, OutputFormatsTable.IsBusinessUidVisibleCol)) return;
			}
		}
		///<summary>
		///		[<c>BillingDatabase</c>].[<c>OutputFormats</c>].[<c>IsBusinessNameVisible</c>] (Type = <c>bit</c>, Default = '<c>(1)</c>')
		///</summary>
		[CsDbDataColumn(Default = true)]
		[CsDbNativeDataColumn(Name = "IsBusinessNameVisible", Type = "bit", Default = "(1)", IsNullable = "NO")]
		public Boolean IsBusinessNameVisible
		{
			get { return GetDbValue<Boolean>(OutputFormatsTable.IsBusinessNameVisibleCol); }
			set 
			{ 
				if (!SetDbValue(value, OutputFormatsTable.IsBusinessNameVisibleCol)) return;
			}
		}
		///<summary>
		///		[<c>BillingDatabase</c>].[<c>OutputFormats</c>].[<c>IsBusinessAnschriftVisible</c>] (Type = <c>bit</c>, Default = '<c>(1)</c>')
		///</summary>
		[CsDbDataColumn(Default = true)]
		[CsDbNativeDataColumn(Name = "IsBusinessAnschriftVisible", Type = "bit", Default = "(1)", IsNullable = "NO")]
		public Boolean IsBusinessAnschriftVisible
		{
			get { return GetDbValue<Boolean>(OutputFormatsTable.IsBusinessAnschriftVisibleCol); }
			set 
			{ 
				if (!SetDbValue(value, OutputFormatsTable.IsBusinessAnschriftVisibleCol)) return;
			}
		}
		///<summary>
		///		[<c>BillingDatabase</c>].[<c>OutputFormats</c>].[<c>IsBusinessMailVisible</c>] (Type = <c>bit</c>, Default = '<c>(1)</c>')
		///</summary>
		[CsDbDataColumn(Default = true)]
		[CsDbNativeDataColumn(Name = "IsBusinessMailVisible", Type = "bit", Default = "(1)", IsNullable = "NO")]
		public Boolean IsBusinessMailVisible
		{
			get { return GetDbValue<Boolean>(OutputFormatsTable.IsBusinessMailVisibleCol); }
			set 
			{ 
				if (!SetDbValue(value, OutputFormatsTable.IsBusinessMailVisibleCol)) return;
			}
		}
		///<summary>
		///		[<c>BillingDatabase</c>].[<c>OutputFormats</c>].[<c>IsBusinessTelefonVisible</c>] (Type = <c>bit</c>, Default = '<c>(1)</c>')
		///</summary>
		[CsDbDataColumn(Default = true)]
		[CsDbNativeDataColumn(Name = "IsBusinessTelefonVisible", Type = "bit", Default = "(1)", IsNullable = "NO")]
		public Boolean IsBusinessTelefonVisible
		{
			get { return GetDbValue<Boolean>(OutputFormatsTable.IsBusinessTelefonVisibleCol); }
			set 
			{ 
				if (!SetDbValue(value, OutputFormatsTable.IsBusinessTelefonVisibleCol)) return;
			}
		}
		///<summary>
		///		[<c>BillingDatabase</c>].[<c>OutputFormats</c>].[<c>IsBusinessWebsiteVisible</c>] (Type = <c>bit</c>, Default = '<c>(1)</c>')
		///</summary>
		[CsDbDataColumn(Default = true)]
		[CsDbNativeDataColumn(Name = "IsBusinessWebsiteVisible", Type = "bit", Default = "(1)", IsNullable = "NO")]
		public Boolean IsBusinessWebsiteVisible
		{
			get { return GetDbValue<Boolean>(OutputFormatsTable.IsBusinessWebsiteVisibleCol); }
			set 
			{ 
				if (!SetDbValue(value, OutputFormatsTable.IsBusinessWebsiteVisibleCol)) return;
			}
		}
		///<summary>
		///		[<c>BillingDatabase</c>].[<c>OutputFormats</c>].[<c>IsKassenoperatorVisible</c>] (Type = <c>bit</c>, Default = '<c>(1)</c>')
		///</summary>
		[CsDbDataColumn(Default = true)]
		[CsDbNativeDataColumn(Name = "IsKassenoperatorVisible", Type = "bit", Default = "(1)", IsNullable = "NO")]
		public Boolean IsKassenoperatorVisible
		{
			get { return GetDbValue<Boolean>(OutputFormatsTable.IsKassenoperatorVisibleCol); }
			set 
			{ 
				if (!SetDbValue(value, OutputFormatsTable.IsKassenoperatorVisibleCol)) return;
			}
		}
		///<summary>
		///		[<c>BillingDatabase</c>].[<c>OutputFormats</c>].[<c>Comment</c>] (Type = <c>ntext</c>, <c>NULLABLE</c>, MaxLength = <c>536870911</c>)
		///</summary>
		[CsDbDataColumn(MaxLength = 536870911, IsNullable = true)]
		[CsDbNativeDataColumn(Name = "Comment", Type = "ntext", MaxLength = "536870911", IsNullable = "YES")]
		public String Comment
		{
			get { return GetDbValue<String>(OutputFormatsTable.CommentCol); }
			set 
			{ 
				if (!SetDbValue(value, OutputFormatsTable.CommentCol)) return;
			}
		}
		///<summary>
		///		[<c>BillingDatabase</c>].[<c>OutputFormats</c>].[<c>CommentLastChanged</c>] (Type = <c>datetime</c>, <c>NULLABLE</c>)
		///</summary>
		[CsDbDataColumn(IsNullable = true)]
		[CsDbNativeDataColumn(Name = "CommentLastChanged", Type = "datetime", IsNullable = "YES")]
		public DateTime? CommentLastChanged
		{
			get { return GetDbValue<DateTime?>(OutputFormatsTable.CommentLastChangedCol); }
			set 
			{ 
				if (!SetDbValue(value, OutputFormatsTable.CommentLastChangedCol)) return;
			}
		}
		
		#endregion
	
	
	
	
	
		///	<summary>Reloads the <see cref="OutputFormat"/> row by executing following command:<para/><c>$"SELECT * FROM OutputFormats WHERE [Id] = '<see cref="Id"/>'</c></summary>
		public OutputFormat Reload()
		{
			Table.DataSet.OutputFormats.LoadThenFind(Id);
			return this;
		}
		
		
		
		
		#region PROPERTIES<Many to One>
		private ContractCollection<MailedBeleg> _weakMailedBelege;
		private ContractCollection<PrintedBeleg> _weakPrintedBelege;
		
		
		
		#endregion
		
		
		
		
		
		#region PROPERTIES<One to Many>
		///	<summary>
		///		This field has cached Output. <para/>
		///		<c>SELECT * FROM MailedBelege WHERE [OutputFormatId] = '<paramref name="Id"/>'</c><para/>[<c>OutputFormats</c>].[<c>Id</c>] &#60;&#60;&#60;&#60; [<c>MailedBelege</c>].[<c>OutputFormatId</c>]
		///	</summary>
		[CsDbResolvesRelation("FK_MailedBelege_OutputFormatId", PkTable = "OutputFormats", PkColumn = "Id", FkTable = "MailedBelege", FkColumn = "OutputFormatId")]
		public ContractCollection<MailedBeleg> MailedBelege
		{
			get	{ return _weakMailedBelege ?? (_weakMailedBelege = Table.DataSet.MailedBelege.FindOrLoad_By_OutputFormatId(Id)); }
		}
		///	<summary>
		///		This field has cached Output. <para/>
		///		<c>SELECT * FROM PrintedBelege WHERE [OutputFormatId] = '<paramref name="Id"/>'</c><para/>[<c>OutputFormats</c>].[<c>Id</c>] &#60;&#60;&#60;&#60; [<c>PrintedBelege</c>].[<c>OutputFormatId</c>]
		///	</summary>
		[CsDbResolvesRelation("FK_PrintedBelege_OutputFormatId", PkTable = "OutputFormats", PkColumn = "Id", FkTable = "PrintedBelege", FkColumn = "OutputFormatId")]
		public ContractCollection<PrintedBeleg> PrintedBelege
		{
			get	{ return _weakPrintedBelege ?? (_weakPrintedBelege = Table.DataSet.PrintedBelege.FindOrLoad_By_OutputFormatId(Id)); }
		}
		#endregion
		
		
		
		
		
		#region FUNC<Overrides>
		protected void Invalidate()
		{
			if (!IsPropertyChangedHandled)
				return;
		
			OnPropertyChanged("Id");
			OnPropertyChanged("CreationDate");
			OnPropertyChanged("LastUsedDate");
			OnPropertyChanged("Name");
			OnPropertyChanged("BonLayoutNumber");
			OnPropertyChanged("FirstImageBinary");
			OnPropertyChanged("FirstImageHash");
			OnPropertyChanged("FirstText");
			OnPropertyChanged("SecondImageBinary");
			OnPropertyChanged("SecondImageHash");
			OnPropertyChanged("SecondText");
			OnPropertyChanged("ThirdImageBinary");
			OnPropertyChanged("ThirdImageHash");
			OnPropertyChanged("ThirdText");
			OnPropertyChanged("Scaling");
			OnPropertyChanged("BusinessUid");
			OnPropertyChanged("BusinessName");
			OnPropertyChanged("BusinessAnschrift");
			OnPropertyChanged("BusinessMail");
			OnPropertyChanged("BusinessTelefon");
			OnPropertyChanged("BusinessWebsite");
			OnPropertyChanged("IsBusinessUidVisible");
			OnPropertyChanged("IsBusinessNameVisible");
			OnPropertyChanged("IsBusinessAnschriftVisible");
			OnPropertyChanged("IsBusinessMailVisible");
			OnPropertyChanged("IsBusinessTelefonVisible");
			OnPropertyChanged("IsBusinessWebsiteVisible");
			OnPropertyChanged("IsKassenoperatorVisible");
			OnPropertyChanged("Comment");
			OnPropertyChanged("CommentLastChanged");
		}
		///	<summary> Set all values which have default values inside the database to their defaults. This method is automatically invoked if you call <see cref="OutputFormatsTable.NewRow"/>. </summary>
		public override void ApplyDefaults()
		{
			Id = Guid.NewGuid();
				CreationDate = DateTime.Now;
				BonLayoutNumber = 0;
				Scaling = 1;
				IsBusinessUidVisible = true;
				IsBusinessNameVisible = true;
				IsBusinessAnschriftVisible = true;
				IsBusinessMailVisible = true;
				IsBusinessTelefonVisible = true;
				IsBusinessWebsiteVisible = true;
				IsKassenoperatorVisible = true;
		}
		/// <summary>
		///     Loads the complete data bundle of the current row into a target data set.
		///     <para>A data bundle is defined as a set of all rows inside a database which are connected via relations.</para>
		///     <para>The currently selected row is the root of the bundle</para>
		/// </summary>
		public void Copy_BundledData_Into_DataSet(BillingDatabase target)
		{
			base.Copy_BundledData_Into_DataSet(target);
		}
		
		/// <summary>
		///     Loads the complete data bundle of the current row into a new data set.
		///     <para>A data bundle is defined as a set of rows inside a database which are connected via relations.</para>
		///     <para>The currently selected row is the root of the bundle</para>
		/// </summary>
		public new BillingDatabase Copy_BundledData_In_New_DataSet()
		{
			return (BillingDatabase) base.Copy_BundledData_In_New_DataSet();
		}
		#endregion
		
		
		///	<summary> This method copy's each database field into the <paramref name="target"/> interface. </summary>
		public void Copy_To(IOutputFormat target, bool includePrimaryKey = false)
		{
			if (includePrimaryKey) target.Id = this.Id;
			target.CreationDate = this.CreationDate;
			target.LastUsedDate = this.LastUsedDate;
			target.Name = this.Name;
			target.BonLayoutNumber = this.BonLayoutNumber;
			target.FirstImageBinary = this.FirstImageBinary;
			target.FirstImageHash = this.FirstImageHash;
			target.FirstText = this.FirstText;
			target.SecondImageBinary = this.SecondImageBinary;
			target.SecondImageHash = this.SecondImageHash;
			target.SecondText = this.SecondText;
			target.ThirdImageBinary = this.ThirdImageBinary;
			target.ThirdImageHash = this.ThirdImageHash;
			target.ThirdText = this.ThirdText;
			target.Scaling = this.Scaling;
			target.BusinessUid = this.BusinessUid;
			target.BusinessName = this.BusinessName;
			target.BusinessAnschrift = this.BusinessAnschrift;
			target.BusinessMail = this.BusinessMail;
			target.BusinessTelefon = this.BusinessTelefon;
			target.BusinessWebsite = this.BusinessWebsite;
			target.IsBusinessUidVisible = this.IsBusinessUidVisible;
			target.IsBusinessNameVisible = this.IsBusinessNameVisible;
			target.IsBusinessAnschriftVisible = this.IsBusinessAnschriftVisible;
			target.IsBusinessMailVisible = this.IsBusinessMailVisible;
			target.IsBusinessTelefonVisible = this.IsBusinessTelefonVisible;
			target.IsBusinessWebsiteVisible = this.IsBusinessWebsiteVisible;
			target.IsKassenoperatorVisible = this.IsKassenoperatorVisible;
			target.Comment = this.Comment;
			target.CommentLastChanged = this.CommentLastChanged;
		}
		///	<summary> This method copy's each database field from the <paramref name="source"/> interface to this data row.</summary>
		public void Copy_From(IOutputFormat source, bool includePrimaryKey = false)
		{
			if (includePrimaryKey) this.Id = source.Id;
			this.CreationDate = source.CreationDate;
			this.LastUsedDate = source.LastUsedDate;
			this.Name = source.Name;
			this.BonLayoutNumber = source.BonLayoutNumber;
			this.FirstImageBinary = source.FirstImageBinary;
			this.FirstImageHash = source.FirstImageHash;
			this.FirstText = source.FirstText;
			this.SecondImageBinary = source.SecondImageBinary;
			this.SecondImageHash = source.SecondImageHash;
			this.SecondText = source.SecondText;
			this.ThirdImageBinary = source.ThirdImageBinary;
			this.ThirdImageHash = source.ThirdImageHash;
			this.ThirdText = source.ThirdText;
			this.Scaling = source.Scaling;
			this.BusinessUid = source.BusinessUid;
			this.BusinessName = source.BusinessName;
			this.BusinessAnschrift = source.BusinessAnschrift;
			this.BusinessMail = source.BusinessMail;
			this.BusinessTelefon = source.BusinessTelefon;
			this.BusinessWebsite = source.BusinessWebsite;
			this.IsBusinessUidVisible = source.IsBusinessUidVisible;
			this.IsBusinessNameVisible = source.IsBusinessNameVisible;
			this.IsBusinessAnschriftVisible = source.IsBusinessAnschriftVisible;
			this.IsBusinessMailVisible = source.IsBusinessMailVisible;
			this.IsBusinessTelefonVisible = source.IsBusinessTelefonVisible;
			this.IsBusinessWebsiteVisible = source.IsBusinessWebsiteVisible;
			this.IsKassenoperatorVisible = source.IsKassenoperatorVisible;
			this.Comment = source.Comment;
			this.CommentLastChanged = source.CommentLastChanged;
		}
		///	<summary> 
		///		This method copy's each database field which is not in the <paramref name="excludedColumns"/> 
		///		from the <paramref name="source"/> interface to this data row.
		/// </summary>
		public void Copy_From_But_Ignore(IOutputFormat source, params string[] excludedColumns)
		{
			if (!excludedColumns.Contains(OutputFormatsTable.IdCol)) this.Id = source.Id;
			if (!excludedColumns.Contains(OutputFormatsTable.CreationDateCol)) this.CreationDate = source.CreationDate;
			if (!excludedColumns.Contains(OutputFormatsTable.LastUsedDateCol)) this.LastUsedDate = source.LastUsedDate;
			if (!excludedColumns.Contains(OutputFormatsTable.NameCol)) this.Name = source.Name;
			if (!excludedColumns.Contains(OutputFormatsTable.BonLayoutNumberCol)) this.BonLayoutNumber = source.BonLayoutNumber;
			if (!excludedColumns.Contains(OutputFormatsTable.FirstImageBinaryCol)) this.FirstImageBinary = source.FirstImageBinary;
			if (!excludedColumns.Contains(OutputFormatsTable.FirstImageHashCol)) this.FirstImageHash = source.FirstImageHash;
			if (!excludedColumns.Contains(OutputFormatsTable.FirstTextCol)) this.FirstText = source.FirstText;
			if (!excludedColumns.Contains(OutputFormatsTable.SecondImageBinaryCol)) this.SecondImageBinary = source.SecondImageBinary;
			if (!excludedColumns.Contains(OutputFormatsTable.SecondImageHashCol)) this.SecondImageHash = source.SecondImageHash;
			if (!excludedColumns.Contains(OutputFormatsTable.SecondTextCol)) this.SecondText = source.SecondText;
			if (!excludedColumns.Contains(OutputFormatsTable.ThirdImageBinaryCol)) this.ThirdImageBinary = source.ThirdImageBinary;
			if (!excludedColumns.Contains(OutputFormatsTable.ThirdImageHashCol)) this.ThirdImageHash = source.ThirdImageHash;
			if (!excludedColumns.Contains(OutputFormatsTable.ThirdTextCol)) this.ThirdText = source.ThirdText;
			if (!excludedColumns.Contains(OutputFormatsTable.ScalingCol)) this.Scaling = source.Scaling;
			if (!excludedColumns.Contains(OutputFormatsTable.BusinessUidCol)) this.BusinessUid = source.BusinessUid;
			if (!excludedColumns.Contains(OutputFormatsTable.BusinessNameCol)) this.BusinessName = source.BusinessName;
			if (!excludedColumns.Contains(OutputFormatsTable.BusinessAnschriftCol)) this.BusinessAnschrift = source.BusinessAnschrift;
			if (!excludedColumns.Contains(OutputFormatsTable.BusinessMailCol)) this.BusinessMail = source.BusinessMail;
			if (!excludedColumns.Contains(OutputFormatsTable.BusinessTelefonCol)) this.BusinessTelefon = source.BusinessTelefon;
			if (!excludedColumns.Contains(OutputFormatsTable.BusinessWebsiteCol)) this.BusinessWebsite = source.BusinessWebsite;
			if (!excludedColumns.Contains(OutputFormatsTable.IsBusinessUidVisibleCol)) this.IsBusinessUidVisible = source.IsBusinessUidVisible;
			if (!excludedColumns.Contains(OutputFormatsTable.IsBusinessNameVisibleCol)) this.IsBusinessNameVisible = source.IsBusinessNameVisible;
			if (!excludedColumns.Contains(OutputFormatsTable.IsBusinessAnschriftVisibleCol)) this.IsBusinessAnschriftVisible = source.IsBusinessAnschriftVisible;
			if (!excludedColumns.Contains(OutputFormatsTable.IsBusinessMailVisibleCol)) this.IsBusinessMailVisible = source.IsBusinessMailVisible;
			if (!excludedColumns.Contains(OutputFormatsTable.IsBusinessTelefonVisibleCol)) this.IsBusinessTelefonVisible = source.IsBusinessTelefonVisible;
			if (!excludedColumns.Contains(OutputFormatsTable.IsBusinessWebsiteVisibleCol)) this.IsBusinessWebsiteVisible = source.IsBusinessWebsiteVisible;
			if (!excludedColumns.Contains(OutputFormatsTable.IsKassenoperatorVisibleCol)) this.IsKassenoperatorVisible = source.IsKassenoperatorVisible;
			if (!excludedColumns.Contains(OutputFormatsTable.CommentCol)) this.Comment = source.Comment;
			if (!excludedColumns.Contains(OutputFormatsTable.CommentLastChangedCol)) this.CommentLastChanged = source.CommentLastChanged;
		}
		///	<summary> 
		///		This method copy's each database field which is in the <paramref name="includedColumns"/> 
		///		from the <paramref name="source"/> interface to this data row.
		/// </summary>
		public void Copy_From_But_TakeOnly(IOutputFormat source, params string[] includedColumns)
		{
			if (includedColumns.Contains(OutputFormatsTable.IdCol)) this.Id = source.Id;
			if (includedColumns.Contains(OutputFormatsTable.CreationDateCol)) this.CreationDate = source.CreationDate;
			if (includedColumns.Contains(OutputFormatsTable.LastUsedDateCol)) this.LastUsedDate = source.LastUsedDate;
			if (includedColumns.Contains(OutputFormatsTable.NameCol)) this.Name = source.Name;
			if (includedColumns.Contains(OutputFormatsTable.BonLayoutNumberCol)) this.BonLayoutNumber = source.BonLayoutNumber;
			if (includedColumns.Contains(OutputFormatsTable.FirstImageBinaryCol)) this.FirstImageBinary = source.FirstImageBinary;
			if (includedColumns.Contains(OutputFormatsTable.FirstImageHashCol)) this.FirstImageHash = source.FirstImageHash;
			if (includedColumns.Contains(OutputFormatsTable.FirstTextCol)) this.FirstText = source.FirstText;
			if (includedColumns.Contains(OutputFormatsTable.SecondImageBinaryCol)) this.SecondImageBinary = source.SecondImageBinary;
			if (includedColumns.Contains(OutputFormatsTable.SecondImageHashCol)) this.SecondImageHash = source.SecondImageHash;
			if (includedColumns.Contains(OutputFormatsTable.SecondTextCol)) this.SecondText = source.SecondText;
			if (includedColumns.Contains(OutputFormatsTable.ThirdImageBinaryCol)) this.ThirdImageBinary = source.ThirdImageBinary;
			if (includedColumns.Contains(OutputFormatsTable.ThirdImageHashCol)) this.ThirdImageHash = source.ThirdImageHash;
			if (includedColumns.Contains(OutputFormatsTable.ThirdTextCol)) this.ThirdText = source.ThirdText;
			if (includedColumns.Contains(OutputFormatsTable.ScalingCol)) this.Scaling = source.Scaling;
			if (includedColumns.Contains(OutputFormatsTable.BusinessUidCol)) this.BusinessUid = source.BusinessUid;
			if (includedColumns.Contains(OutputFormatsTable.BusinessNameCol)) this.BusinessName = source.BusinessName;
			if (includedColumns.Contains(OutputFormatsTable.BusinessAnschriftCol)) this.BusinessAnschrift = source.BusinessAnschrift;
			if (includedColumns.Contains(OutputFormatsTable.BusinessMailCol)) this.BusinessMail = source.BusinessMail;
			if (includedColumns.Contains(OutputFormatsTable.BusinessTelefonCol)) this.BusinessTelefon = source.BusinessTelefon;
			if (includedColumns.Contains(OutputFormatsTable.BusinessWebsiteCol)) this.BusinessWebsite = source.BusinessWebsite;
			if (includedColumns.Contains(OutputFormatsTable.IsBusinessUidVisibleCol)) this.IsBusinessUidVisible = source.IsBusinessUidVisible;
			if (includedColumns.Contains(OutputFormatsTable.IsBusinessNameVisibleCol)) this.IsBusinessNameVisible = source.IsBusinessNameVisible;
			if (includedColumns.Contains(OutputFormatsTable.IsBusinessAnschriftVisibleCol)) this.IsBusinessAnschriftVisible = source.IsBusinessAnschriftVisible;
			if (includedColumns.Contains(OutputFormatsTable.IsBusinessMailVisibleCol)) this.IsBusinessMailVisible = source.IsBusinessMailVisible;
			if (includedColumns.Contains(OutputFormatsTable.IsBusinessTelefonVisibleCol)) this.IsBusinessTelefonVisible = source.IsBusinessTelefonVisible;
			if (includedColumns.Contains(OutputFormatsTable.IsBusinessWebsiteVisibleCol)) this.IsBusinessWebsiteVisible = source.IsBusinessWebsiteVisible;
			if (includedColumns.Contains(OutputFormatsTable.IsKassenoperatorVisibleCol)) this.IsKassenoperatorVisible = source.IsKassenoperatorVisible;
			if (includedColumns.Contains(OutputFormatsTable.CommentCol)) this.Comment = source.Comment;
			if (includedColumns.Contains(OutputFormatsTable.CommentLastChangedCol)) this.CommentLastChanged = source.CommentLastChanged;
		}
	}
}