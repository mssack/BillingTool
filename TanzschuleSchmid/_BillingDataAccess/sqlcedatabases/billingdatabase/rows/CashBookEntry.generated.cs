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
//<date>2016-04-01 15:22:10</date>



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
	///<summary>DataRow([<c>BillingDatabase</c>].[<c>CashBook</c>]): row model of <see cref="CashBookTable"/>.</summary>
	[Serializable]
	[DebuggerDisplay("DataRow(BillingDatabase.CashBook): Id = '{Id}'")]
	[CsDbDataRow(Database = "BillingDatabase", TableName = "CashBook", Generated = "16.04.01 15:22:10", Hash = "F615CE0FDA9A8799FF2F755D55296814")]
	public partial class CashBookEntry : CsDbTableRow, ICashBookEntry
	{
		#region statics
		private static Dictionary<string, System.Reflection.PropertyInfo> _nativeColumnName_To_Property;
		public static Dictionary<string, System.Reflection.PropertyInfo> NativeColumnName_To_Property
		{
			get
			{
				if (_nativeColumnName_To_Property != null)
					return _nativeColumnName_To_Property;
		
				var type = typeof(ICashBookEntry);
				_nativeColumnName_To_Property = new Dictionary<string, System.Reflection.PropertyInfo>
				{
					{ CashBookTable.IdCol, type.GetProperty(nameof(Id)) },
					{ CashBookTable.TypNameCol, type.GetProperty(nameof(TypName)) },
					{ CashBookTable.BelegNummerCol, type.GetProperty(nameof(BelegNummer)) },
					{ CashBookTable.KassenIdCol, type.GetProperty(nameof(KassenId)) },
					{ CashBookTable.KassenOperatorCol, type.GetProperty(nameof(KassenOperator)) },
					{ CashBookTable.DatumCol, type.GetProperty(nameof(Datum)) },
					{ CashBookTable.BetragBruttoCol, type.GetProperty(nameof(BetragBrutto)) },
					{ CashBookTable.UmsatzZählerCol, type.GetProperty(nameof(UmsatzZähler)) },
					{ CashBookTable.SteuersatzCol, type.GetProperty(nameof(Steuersatz)) },
					{ CashBookTable.LeistungsBeschreibungCol, type.GetProperty(nameof(LeistungsBeschreibung)) },
					{ CashBookTable.BelegTextCol, type.GetProperty(nameof(BelegText)) },
					{ CashBookTable.InternEmpfängerCol, type.GetProperty(nameof(InternEmpfänger)) },
					{ CashBookTable.InterneEmpfängerIdCol, type.GetProperty(nameof(InterneEmpfängerId)) },
					{ CashBookTable.InterneBeschreibungCol, type.GetProperty(nameof(InterneBeschreibung)) },
					{ CashBookTable.ZuletztGeändertCol, type.GetProperty(nameof(ZuletztGeändert)) }
				};
		
				return _nativeColumnName_To_Property;
			}
		}
		#endregion
	
	
		public CashBookEntry(DataRowBuilder builder) : base(builder){}
	
		
		///<summary>References the owning data context, this is equal to the database server. Use this to address databases on the same server.</summary>
		public new BillingDataAccess.sqlcedatabases.SqlCeDatabasesContext DataContext => Table.DataContext;
	
		///<summary>References the owning dataset. Use this to address tables in the same database</summary>
		public new BillingDatabase DataSet => Table.DataSet;
	
		///	<summary>Gets the owning table of type <see cref="CashBookTable"/>.</summary>
		public new CashBookTable Table => (CashBookTable) base.Table;
	
	
	
	
		#region COLUMNS
		///<summary>
		///		[<c>BillingDatabase</c>].[<c>CashBook</c>].[<c>Id</c>] (Type = <c>uniqueidentifier</c>, Default = '<c>newid()</c>')
		///</summary>
		[CsDbDataColumn(Default = "Guid.NewGuid()")]
		[CsDbNativeDataColumn(Name = "Id", Type = "uniqueidentifier", Default = "newid()", IsNullable = "NO")]
		public Guid Id
		{
			get { return GetDbValue<Guid>(CashBookTable.IdCol); }
			set 
			{ 
				if (!SetDbValue(value, CashBookTable.IdCol)) return;
			}
		}
		///<summary>
		///		[<c>BillingDatabase</c>].[<c>CashBook</c>].[<c>TypName</c>] (Type = <c>ntext</c>, MaxLength = <c>536870911</c>)
		///</summary>
		[CsDbDataColumn(MaxLength = 536870911)]
		[CsDbNativeDataColumn(Name = "TypName", Type = "ntext", MaxLength = "536870911", IsNullable = "NO")]
		public String TypName
		{
			get { return GetDbValue<String>(CashBookTable.TypNameCol); }
			set 
			{ 
				if (!SetDbValue(value, CashBookTable.TypNameCol)) return;
			}
		}
		///<summary>
		///		[<c>BillingDatabase</c>].[<c>CashBook</c>].[<c>BelegNummer</c>] (Type = <c>int</c>)
		///</summary>
		[CsDbDataColumn]
		[CsDbNativeDataColumn(Name = "BelegNummer", Type = "int", IsNullable = "NO")]
		public Int32 BelegNummer
		{
			get { return GetDbValue<Int32>(CashBookTable.BelegNummerCol); }
			set 
			{ 
				if (!SetDbValue(value, CashBookTable.BelegNummerCol)) return;
			}
		}
		///<summary>
		///		[<c>BillingDatabase</c>].[<c>CashBook</c>].[<c>KassenId</c>] (Type = <c>int</c>)
		///</summary>
		[CsDbDataColumn]
		[CsDbNativeDataColumn(Name = "KassenId", Type = "int", IsNullable = "NO")]
		public Int32 KassenId
		{
			get { return GetDbValue<Int32>(CashBookTable.KassenIdCol); }
			set 
			{ 
				if (!SetDbValue(value, CashBookTable.KassenIdCol)) return;
			}
		}
		///<summary>
		///		[<c>BillingDatabase</c>].[<c>CashBook</c>].[<c>KassenOperator</c>] (Type = <c>ntext</c>, MaxLength = <c>536870911</c>)
		///</summary>
		[CsDbDataColumn(MaxLength = 536870911)]
		[CsDbNativeDataColumn(Name = "KassenOperator", Type = "ntext", MaxLength = "536870911", IsNullable = "NO")]
		public String KassenOperator
		{
			get { return GetDbValue<String>(CashBookTable.KassenOperatorCol); }
			set 
			{ 
				if (!SetDbValue(value, CashBookTable.KassenOperatorCol)) return;
			}
		}
		///<summary>
		///		[<c>BillingDatabase</c>].[<c>CashBook</c>].[<c>Datum</c>] (Type = <c>datetime</c>, Default = '<c>getdate()</c>')
		///</summary>
		[CsDbDataColumn(Default = "DateTime.Now")]
		[CsDbNativeDataColumn(Name = "Datum", Type = "datetime", Default = "getdate()", IsNullable = "NO")]
		public DateTime Datum
		{
			get { return GetDbValue<DateTime>(CashBookTable.DatumCol); }
			set 
			{ 
				if (!SetDbValue(value, CashBookTable.DatumCol)) return;
			}
		}
		///<summary>
		///		[<c>BillingDatabase</c>].[<c>CashBook</c>].[<c>BetragBrutto</c>] (Type = <c>money</c>, Default = '<c>(0)</c>')
		///</summary>
		[CsDbDataColumn(Default = 0)]
		[CsDbNativeDataColumn(Name = "BetragBrutto", Type = "money", Default = "(0)", IsNullable = "NO")]
		public Decimal BetragBrutto
		{
			get { return GetDbValue<Decimal>(CashBookTable.BetragBruttoCol); }
			set 
			{ 
				if (!SetDbValue(value, CashBookTable.BetragBruttoCol)) return;
			}
		}
		///<summary>
		///		[<c>BillingDatabase</c>].[<c>CashBook</c>].[<c>UmsatzZähler</c>] (Type = <c>money</c>)
		///</summary>
		[CsDbDataColumn]
		[CsDbNativeDataColumn(Name = "UmsatzZähler", Type = "money", IsNullable = "NO")]
		public Decimal UmsatzZähler
		{
			get { return GetDbValue<Decimal>(CashBookTable.UmsatzZählerCol); }
			set 
			{ 
				if (!SetDbValue(value, CashBookTable.UmsatzZählerCol)) return;
			}
		}
		///<summary>
		///		[<c>BillingDatabase</c>].[<c>CashBook</c>].[<c>Steuersatz</c>] (Type = <c>money</c>, Default = '<c>(0)</c>')
		///</summary>
		[CsDbDataColumn(Default = 0)]
		[CsDbNativeDataColumn(Name = "Steuersatz", Type = "money", Default = "(0)", IsNullable = "NO")]
		public Decimal Steuersatz
		{
			get { return GetDbValue<Decimal>(CashBookTable.SteuersatzCol); }
			set 
			{ 
				if (!SetDbValue(value, CashBookTable.SteuersatzCol)) return;
			}
		}
		///<summary>
		///		[<c>BillingDatabase</c>].[<c>CashBook</c>].[<c>LeistungsBeschreibung</c>] (Type = <c>ntext</c>, MaxLength = <c>536870911</c>)
		///</summary>
		[CsDbDataColumn(MaxLength = 536870911)]
		[CsDbNativeDataColumn(Name = "LeistungsBeschreibung", Type = "ntext", MaxLength = "536870911", IsNullable = "NO")]
		public String LeistungsBeschreibung
		{
			get { return GetDbValue<String>(CashBookTable.LeistungsBeschreibungCol); }
			set 
			{ 
				if (!SetDbValue(value, CashBookTable.LeistungsBeschreibungCol)) return;
			}
		}
		///<summary>
		///		[<c>BillingDatabase</c>].[<c>CashBook</c>].[<c>BelegText</c>] (Type = <c>ntext</c>, <c>NULLABLE</c>, MaxLength = <c>536870911</c>)
		///</summary>
		[CsDbDataColumn(MaxLength = 536870911, IsNullable = true)]
		[CsDbNativeDataColumn(Name = "BelegText", Type = "ntext", MaxLength = "536870911", IsNullable = "YES")]
		public String BelegText
		{
			get { return GetDbValue<String>(CashBookTable.BelegTextCol); }
			set 
			{ 
				if (!SetDbValue(value, CashBookTable.BelegTextCol)) return;
			}
		}
		///<summary>
		///		[<c>BillingDatabase</c>].[<c>CashBook</c>].[<c>InternEmpfänger</c>] (Type = <c>ntext</c>, <c>NULLABLE</c>, MaxLength = <c>536870911</c>)
		///</summary>
		[CsDbDataColumn(MaxLength = 536870911, IsNullable = true)]
		[CsDbNativeDataColumn(Name = "InternEmpfänger", Type = "ntext", MaxLength = "536870911", IsNullable = "YES")]
		public String InternEmpfänger
		{
			get { return GetDbValue<String>(CashBookTable.InternEmpfängerCol); }
			set 
			{ 
				if (!SetDbValue(value, CashBookTable.InternEmpfängerCol)) return;
			}
		}
		///<summary>
		///		[<c>BillingDatabase</c>].[<c>CashBook</c>].[<c>InterneEmpfängerId</c>] (Type = <c>ntext</c>, <c>NULLABLE</c>, MaxLength = <c>536870911</c>)
		///</summary>
		[CsDbDataColumn(MaxLength = 536870911, IsNullable = true)]
		[CsDbNativeDataColumn(Name = "InterneEmpfängerId", Type = "ntext", MaxLength = "536870911", IsNullable = "YES")]
		public String InterneEmpfängerId
		{
			get { return GetDbValue<String>(CashBookTable.InterneEmpfängerIdCol); }
			set 
			{ 
				if (!SetDbValue(value, CashBookTable.InterneEmpfängerIdCol)) return;
			}
		}
		///<summary>
		///		[<c>BillingDatabase</c>].[<c>CashBook</c>].[<c>InterneBeschreibung</c>] (Type = <c>ntext</c>, <c>NULLABLE</c>, MaxLength = <c>536870911</c>)
		///</summary>
		[CsDbDataColumn(MaxLength = 536870911, IsNullable = true)]
		[CsDbNativeDataColumn(Name = "InterneBeschreibung", Type = "ntext", MaxLength = "536870911", IsNullable = "YES")]
		public String InterneBeschreibung
		{
			get { return GetDbValue<String>(CashBookTable.InterneBeschreibungCol); }
			set 
			{ 
				if (!SetDbValue(value, CashBookTable.InterneBeschreibungCol)) return;
			}
		}
		///<summary>
		///		[<c>BillingDatabase</c>].[<c>CashBook</c>].[<c>ZuletztGeändert</c>] (Type = <c>datetime</c>, Default = '<c>getdate()</c>')
		///</summary>
		[CsDbDataColumn(Default = "DateTime.Now")]
		[CsDbNativeDataColumn(Name = "ZuletztGeändert", Type = "datetime", Default = "getdate()", IsNullable = "NO")]
		public DateTime ZuletztGeändert
		{
			get { return GetDbValue<DateTime>(CashBookTable.ZuletztGeändertCol); }
			set 
			{ 
				if (!SetDbValue(value, CashBookTable.ZuletztGeändertCol)) return;
			}
		}
		
		#endregion
	
	
	
	
	
		///	<summary>Reloads the <see cref="CashBookEntry"/> row by executing following command:<para/><c>$"SELECT * FROM CashBook WHERE [Id] = '<see cref="Id"/>'</c></summary>
		public CashBookEntry Reload()
		{
			Table.DataSet.CashBook.LoadThenFind(Id);
			return this;
		}
		
		
		
		
		#region PROPERTIES<Many to One>
		
		
		
		
		#endregion
		
		
		
		
		
		#region PROPERTIES<One to Many>
		
		#endregion
		
		
		
		
		
		#region FUNC<Overrides>
		protected void Invalidate()
		{
			if (!IsPropertyChangedHandled)
				return;
		
			OnPropertyChanged("Id");
			OnPropertyChanged("TypName");
			OnPropertyChanged("BelegNummer");
			OnPropertyChanged("KassenId");
			OnPropertyChanged("KassenOperator");
			OnPropertyChanged("Datum");
			OnPropertyChanged("BetragBrutto");
			OnPropertyChanged("UmsatzZähler");
			OnPropertyChanged("Steuersatz");
			OnPropertyChanged("LeistungsBeschreibung");
			OnPropertyChanged("BelegText");
			OnPropertyChanged("InternEmpfänger");
			OnPropertyChanged("InterneEmpfängerId");
			OnPropertyChanged("InterneBeschreibung");
			OnPropertyChanged("ZuletztGeändert");
		}
		///	<summary> Set all values which have default values inside the database to their defaults. This method is automatically invoked if you call <see cref="CashBookTable.NewRow"/>. </summary>
		public override void ApplyDefaults()
		{
			Id = Guid.NewGuid();
				Datum = DateTime.Now;
				BetragBrutto = 0;
				Steuersatz = 0;
				ZuletztGeändert = DateTime.Now;
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
		public void Copy_To(ICashBookEntry target, bool includePrimaryKey = false)
		{
			if (includePrimaryKey) target.Id = this.Id;
			target.TypName = this.TypName;
			target.BelegNummer = this.BelegNummer;
			target.KassenId = this.KassenId;
			target.KassenOperator = this.KassenOperator;
			target.Datum = this.Datum;
			target.BetragBrutto = this.BetragBrutto;
			target.UmsatzZähler = this.UmsatzZähler;
			target.Steuersatz = this.Steuersatz;
			target.LeistungsBeschreibung = this.LeistungsBeschreibung;
			target.BelegText = this.BelegText;
			target.InternEmpfänger = this.InternEmpfänger;
			target.InterneEmpfängerId = this.InterneEmpfängerId;
			target.InterneBeschreibung = this.InterneBeschreibung;
			target.ZuletztGeändert = this.ZuletztGeändert;
		}
		///	<summary> This method copy's each database field from the <paramref name="source"/> interface to this data row.</summary>
		public void Copy_From(ICashBookEntry source, bool includePrimaryKey = false)
		{
			if (includePrimaryKey) this.Id = source.Id;
			this.TypName = source.TypName;
			this.BelegNummer = source.BelegNummer;
			this.KassenId = source.KassenId;
			this.KassenOperator = source.KassenOperator;
			this.Datum = source.Datum;
			this.BetragBrutto = source.BetragBrutto;
			this.UmsatzZähler = source.UmsatzZähler;
			this.Steuersatz = source.Steuersatz;
			this.LeistungsBeschreibung = source.LeistungsBeschreibung;
			this.BelegText = source.BelegText;
			this.InternEmpfänger = source.InternEmpfänger;
			this.InterneEmpfängerId = source.InterneEmpfängerId;
			this.InterneBeschreibung = source.InterneBeschreibung;
			this.ZuletztGeändert = source.ZuletztGeändert;
		}
		///	<summary> 
		///		This method copy's each database field which is not in the <paramref name="excludedColumns"/> 
		///		from the <paramref name="source"/> interface to this data row.
		/// </summary>
		public void Copy_From(ICashBookEntry source, params string[] excludedColumns)
		{
			if (!excludedColumns.Contains(CashBookTable.IdCol)) this.Id = source.Id;
			if (!excludedColumns.Contains(CashBookTable.TypNameCol)) this.TypName = source.TypName;
			if (!excludedColumns.Contains(CashBookTable.BelegNummerCol)) this.BelegNummer = source.BelegNummer;
			if (!excludedColumns.Contains(CashBookTable.KassenIdCol)) this.KassenId = source.KassenId;
			if (!excludedColumns.Contains(CashBookTable.KassenOperatorCol)) this.KassenOperator = source.KassenOperator;
			if (!excludedColumns.Contains(CashBookTable.DatumCol)) this.Datum = source.Datum;
			if (!excludedColumns.Contains(CashBookTable.BetragBruttoCol)) this.BetragBrutto = source.BetragBrutto;
			if (!excludedColumns.Contains(CashBookTable.UmsatzZählerCol)) this.UmsatzZähler = source.UmsatzZähler;
			if (!excludedColumns.Contains(CashBookTable.SteuersatzCol)) this.Steuersatz = source.Steuersatz;
			if (!excludedColumns.Contains(CashBookTable.LeistungsBeschreibungCol)) this.LeistungsBeschreibung = source.LeistungsBeschreibung;
			if (!excludedColumns.Contains(CashBookTable.BelegTextCol)) this.BelegText = source.BelegText;
			if (!excludedColumns.Contains(CashBookTable.InternEmpfängerCol)) this.InternEmpfänger = source.InternEmpfänger;
			if (!excludedColumns.Contains(CashBookTable.InterneEmpfängerIdCol)) this.InterneEmpfängerId = source.InterneEmpfängerId;
			if (!excludedColumns.Contains(CashBookTable.InterneBeschreibungCol)) this.InterneBeschreibung = source.InterneBeschreibung;
			if (!excludedColumns.Contains(CashBookTable.ZuletztGeändertCol)) this.ZuletztGeändert = source.ZuletztGeändert;
		}
	}
}