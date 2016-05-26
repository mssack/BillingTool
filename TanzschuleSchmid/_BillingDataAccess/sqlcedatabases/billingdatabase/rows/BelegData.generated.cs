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
//<date>2016-05-18 21:29:58</date>



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
	
	///<summary>DataRow([<c>BillingDatabase</c>].[<c>BelegDaten</c>]): row model of <see cref="BelegDatenTable"/>.</summary>
	[Serializable]
	[DebuggerDisplay("DataRow(BillingDatabase.BelegDaten): Id = '{Id}'")]
	[CsDbDataRow(Database = "BillingDatabase", TableName = "BelegDaten", Generated = "16.05.18 21:29:58", Hash = "76EEEDE0EA7100EB16D296B848DA6751")]
	public partial class BelegData : CsDbTableRow, IBelegData
	{
		#region statics
		private static Dictionary<string, System.Reflection.PropertyInfo> _nativeColumnName_To_Property;
		public static Dictionary<string, System.Reflection.PropertyInfo> NativeColumnName_To_Property
		{
			get
			{
				if (_nativeColumnName_To_Property != null)
					return _nativeColumnName_To_Property;
		
				var type = typeof(IBelegData);
				_nativeColumnName_To_Property = new Dictionary<string, System.Reflection.PropertyInfo>
				{
					{ BelegDatenTable.IdCol, type.GetProperty(nameof(Id)) },
					{ BelegDatenTable.StateNameCol, type.GetProperty(nameof(StateName)) },
					{ BelegDatenTable.TypNameCol, type.GetProperty(nameof(TypName)) },
					{ BelegDatenTable.DatumCol, type.GetProperty(nameof(Datum)) },
					{ BelegDatenTable.KassenIdCol, type.GetProperty(nameof(KassenId)) },
					{ BelegDatenTable.KassenOperatorCol, type.GetProperty(nameof(KassenOperator)) },
					{ BelegDatenTable.NummerCol, type.GetProperty(nameof(Nummer)) },
					{ BelegDatenTable.UmsatzZählerCol, type.GetProperty(nameof(UmsatzZähler)) },
					{ BelegDatenTable.StornoBelegIdCol, type.GetProperty(nameof(StornoBelegId)) },
					{ BelegDatenTable.BonNummerVonCol, type.GetProperty(nameof(BonNummerVon)) },
					{ BelegDatenTable.BonNummerBisCol, type.GetProperty(nameof(BonNummerBis)) },
					{ BelegDatenTable.BetragBruttoCol, type.GetProperty(nameof(BetragBrutto)) },
					{ BelegDatenTable.BetragNettoCol, type.GetProperty(nameof(BetragNetto)) },
					{ BelegDatenTable.ZusatzTextCol, type.GetProperty(nameof(ZusatzText)) },
					{ BelegDatenTable.PrintCountCol, type.GetProperty(nameof(PrintCount)) },
					{ BelegDatenTable.MailCountCol, type.GetProperty(nameof(MailCount)) },
					{ BelegDatenTable.EmpfängerCol, type.GetProperty(nameof(Empfänger)) },
					{ BelegDatenTable.EmpfängerIdCol, type.GetProperty(nameof(EmpfängerId)) },
					{ BelegDatenTable.ZahlungsReferenzCol, type.GetProperty(nameof(ZahlungsReferenz)) },
					{ BelegDatenTable.CommentCol, type.GetProperty(nameof(Comment)) },
					{ BelegDatenTable.CommentLastChangedCol, type.GetProperty(nameof(CommentLastChanged)) }
				};
		
				return _nativeColumnName_To_Property;
			}
		}
		#endregion
	
	
		public BelegData(DataRowBuilder builder) : base(builder){}
	
		
		///<summary>References the owning data context, this is equal to the database server. Use this to address databases on the same server.</summary>
		public new BillingDataAccess.sqlcedatabases.SqlCeDatabasesContext DataContext => Table.DataContext;
	
		///<summary>References the owning dataset. Use this to address tables in the same database</summary>
		public new BillingDatabase DataSet => Table.DataSet;
	
		///	<summary>Gets the owning table of type <see cref="BelegDatenTable"/>.</summary>
		public new BelegDatenTable Table => (BelegDatenTable) base.Table;
	
	
	
	
		#region COLUMNS
		///<summary>
		///		[<c>BillingDatabase</c>].[<c>BelegDaten</c>].[<c>Id</c>] (Type = <c>uniqueidentifier</c>, Default = '<c>newid()</c>')
		///</summary>
		[CsDbDataColumn(Default = "Guid.NewGuid()")]
		[CsDbNativeDataColumn(Name = "Id", Type = "uniqueidentifier", Default = "newid()", IsNullable = "NO")]
		public Guid Id
		{
			get { return GetDbValue<Guid>(BelegDatenTable.IdCol); }
			set 
			{ 
				if (!SetDbValue(value, BelegDatenTable.IdCol)) return;
			}
		}
		///<summary>
		///		[<c>BillingDatabase</c>].[<c>BelegDaten</c>].[<c>StateName</c>] (Type = <c>nvarchar</c>, Default = '<c>('Unknown')</c>', MaxLength = <c>255</c>)
		///</summary>
		[CsDbDataColumn(MaxLength = 255, Default = "Unknown")]
		[CsDbNativeDataColumn(Name = "StateName", Type = "nvarchar", Default = "('Unknown')", MaxLength = "255", IsNullable = "NO")]
		public String StateName
		{
			get { return GetDbValue<String>(BelegDatenTable.StateNameCol); }
			set 
			{ 
				if (!SetDbValue(value, BelegDatenTable.StateNameCol)) return;
			}
		}
		///<summary>
		///		[<c>BillingDatabase</c>].[<c>BelegDaten</c>].[<c>TypName</c>] (Type = <c>ntext</c>, Default = '<c>('Bar')</c>', MaxLength = <c>536870911</c>)
		///</summary>
		[CsDbDataColumn(MaxLength = 536870911, Default = "Bar")]
		[CsDbNativeDataColumn(Name = "TypName", Type = "ntext", Default = "('Bar')", MaxLength = "536870911", IsNullable = "NO")]
		public String TypName
		{
			get { return GetDbValue<String>(BelegDatenTable.TypNameCol); }
			set 
			{ 
				if (!SetDbValue(value, BelegDatenTable.TypNameCol)) return;
			}
		}
		///<summary>
		///		[<c>BillingDatabase</c>].[<c>BelegDaten</c>].[<c>Datum</c>] (Type = <c>datetime</c>, Default = '<c>getdate()</c>')
		///</summary>
		[CsDbDataColumn(Default = "DateTime.Now")]
		[CsDbNativeDataColumn(Name = "Datum", Type = "datetime", Default = "getdate()", IsNullable = "NO")]
		public DateTime Datum
		{
			get { return GetDbValue<DateTime>(BelegDatenTable.DatumCol); }
			set 
			{ 
				if (!SetDbValue(value, BelegDatenTable.DatumCol)) return;
			}
		}
		///<summary>
		///		[<c>BillingDatabase</c>].[<c>BelegDaten</c>].[<c>KassenId</c>] (Type = <c>nvarchar</c>, MaxLength = <c>255</c>)
		///</summary>
		[CsDbDataColumn(MaxLength = 255)]
		[CsDbNativeDataColumn(Name = "KassenId", Type = "nvarchar", MaxLength = "255", IsNullable = "NO")]
		public String KassenId
		{
			get { return GetDbValue<String>(BelegDatenTable.KassenIdCol); }
			set 
			{ 
				if (!SetDbValue(value, BelegDatenTable.KassenIdCol)) return;
			}
		}
		///<summary>
		///		[<c>BillingDatabase</c>].[<c>BelegDaten</c>].[<c>KassenOperator</c>] (Type = <c>ntext</c>, MaxLength = <c>536870911</c>)
		///</summary>
		[CsDbDataColumn(MaxLength = 536870911)]
		[CsDbNativeDataColumn(Name = "KassenOperator", Type = "ntext", MaxLength = "536870911", IsNullable = "NO")]
		public String KassenOperator
		{
			get { return GetDbValue<String>(BelegDatenTable.KassenOperatorCol); }
			set 
			{ 
				if (!SetDbValue(value, BelegDatenTable.KassenOperatorCol)) return;
			}
		}
		///<summary>
		///		[<c>BillingDatabase</c>].[<c>BelegDaten</c>].[<c>Nummer</c>] (Type = <c>int</c>)
		///</summary>
		[CsDbDataColumn]
		[CsDbNativeDataColumn(Name = "Nummer", Type = "int", IsNullable = "NO")]
		public Int32 Nummer
		{
			get { return GetDbValue<Int32>(BelegDatenTable.NummerCol); }
			set 
			{ 
				if (!SetDbValue(value, BelegDatenTable.NummerCol)) return;
			}
		}
		///<summary>
		///		[<c>BillingDatabase</c>].[<c>BelegDaten</c>].[<c>UmsatzZähler</c>] (Type = <c>money</c>)
		///</summary>
		[CsDbDataColumn]
		[CsDbNativeDataColumn(Name = "UmsatzZähler", Type = "money", IsNullable = "NO")]
		public Decimal UmsatzZähler
		{
			get { return GetDbValue<Decimal>(BelegDatenTable.UmsatzZählerCol); }
			set 
			{ 
				if (!SetDbValue(value, BelegDatenTable.UmsatzZählerCol)) return;
			}
		}
		///<summary>
		///		[<c>BillingDatabase</c>].[<c>BelegDaten</c>].[<c>StornoBelegId</c>] (Type = <c>uniqueidentifier</c>, <c>NULLABLE</c>)
		///</summary>
		[CsDbDataColumn(IsNullable = true)]
		[CsDbNativeDataColumn(Name = "StornoBelegId", Type = "uniqueidentifier", IsNullable = "YES")]
		public Guid? StornoBelegId
		{
			get { return GetDbValue<Guid?>(BelegDatenTable.StornoBelegIdCol); }
			set 
			{ 
				if (!SetDbValue(value, BelegDatenTable.StornoBelegIdCol)) return;
			}
		}
		///<summary>
		///		[<c>BillingDatabase</c>].[<c>BelegDaten</c>].[<c>BonNummerVon</c>] (Type = <c>int</c>, <c>NULLABLE</c>)
		///</summary>
		[CsDbDataColumn(IsNullable = true)]
		[CsDbNativeDataColumn(Name = "BonNummerVon", Type = "int", IsNullable = "YES")]
		public Int32? BonNummerVon
		{
			get { return GetDbValue<Int32?>(BelegDatenTable.BonNummerVonCol); }
			set 
			{ 
				if (!SetDbValue(value, BelegDatenTable.BonNummerVonCol)) return;
			}
		}
		///<summary>
		///		[<c>BillingDatabase</c>].[<c>BelegDaten</c>].[<c>BonNummerBis</c>] (Type = <c>int</c>, <c>NULLABLE</c>)
		///</summary>
		[CsDbDataColumn(IsNullable = true)]
		[CsDbNativeDataColumn(Name = "BonNummerBis", Type = "int", IsNullable = "YES")]
		public Int32? BonNummerBis
		{
			get { return GetDbValue<Int32?>(BelegDatenTable.BonNummerBisCol); }
			set 
			{ 
				if (!SetDbValue(value, BelegDatenTable.BonNummerBisCol)) return;
			}
		}
		///<summary>
		///		[<c>BillingDatabase</c>].[<c>BelegDaten</c>].[<c>BetragBrutto</c>] (Type = <c>money</c>, Default = '<c>(0)</c>')
		///</summary>
		[CsDbDataColumn(Default = 0)]
		[CsDbNativeDataColumn(Name = "BetragBrutto", Type = "money", Default = "(0)", IsNullable = "NO")]
		public Decimal BetragBrutto
		{
			get { return GetDbValue<Decimal>(BelegDatenTable.BetragBruttoCol); }
			set 
			{ 
				if (!SetDbValue(value, BelegDatenTable.BetragBruttoCol)) return;
			}
		}
		///<summary>
		///		[<c>BillingDatabase</c>].[<c>BelegDaten</c>].[<c>BetragNetto</c>] (Type = <c>money</c>, Default = '<c>(0)</c>')
		///</summary>
		[CsDbDataColumn(Default = 0)]
		[CsDbNativeDataColumn(Name = "BetragNetto", Type = "money", Default = "(0)", IsNullable = "NO")]
		public Decimal BetragNetto
		{
			get { return GetDbValue<Decimal>(BelegDatenTable.BetragNettoCol); }
			set 
			{ 
				if (!SetDbValue(value, BelegDatenTable.BetragNettoCol)) return;
			}
		}
		///<summary>
		///		[<c>BillingDatabase</c>].[<c>BelegDaten</c>].[<c>ZusatzText</c>] (Type = <c>ntext</c>, <c>NULLABLE</c>, MaxLength = <c>536870911</c>)
		///</summary>
		[CsDbDataColumn(MaxLength = 536870911, IsNullable = true)]
		[CsDbNativeDataColumn(Name = "ZusatzText", Type = "ntext", MaxLength = "536870911", IsNullable = "YES")]
		public String ZusatzText
		{
			get { return GetDbValue<String>(BelegDatenTable.ZusatzTextCol); }
			set 
			{ 
				if (!SetDbValue(value, BelegDatenTable.ZusatzTextCol)) return;
			}
		}
		///<summary>
		///		[<c>BillingDatabase</c>].[<c>BelegDaten</c>].[<c>PrintCount</c>] (Type = <c>int</c>, Default = '<c>(0)</c>')
		///</summary>
		[CsDbDataColumn(Default = 0)]
		[CsDbNativeDataColumn(Name = "PrintCount", Type = "int", Default = "(0)", IsNullable = "NO")]
		public Int32 PrintCount
		{
			get { return GetDbValue<Int32>(BelegDatenTable.PrintCountCol); }
			set 
			{ 
				if (!SetDbValue(value, BelegDatenTable.PrintCountCol)) return;
			}
		}
		///<summary>
		///		[<c>BillingDatabase</c>].[<c>BelegDaten</c>].[<c>MailCount</c>] (Type = <c>int</c>, Default = '<c>(0)</c>')
		///</summary>
		[CsDbDataColumn(Default = 0)]
		[CsDbNativeDataColumn(Name = "MailCount", Type = "int", Default = "(0)", IsNullable = "NO")]
		public Int32 MailCount
		{
			get { return GetDbValue<Int32>(BelegDatenTable.MailCountCol); }
			set 
			{ 
				if (!SetDbValue(value, BelegDatenTable.MailCountCol)) return;
			}
		}
		///<summary>
		///		[<c>BillingDatabase</c>].[<c>BelegDaten</c>].[<c>Empfänger</c>] (Type = <c>ntext</c>, <c>NULLABLE</c>, MaxLength = <c>536870911</c>)
		///</summary>
		[CsDbDataColumn(MaxLength = 536870911, IsNullable = true)]
		[CsDbNativeDataColumn(Name = "Empfänger", Type = "ntext", MaxLength = "536870911", IsNullable = "YES")]
		public String Empfänger
		{
			get { return GetDbValue<String>(BelegDatenTable.EmpfängerCol); }
			set 
			{ 
				if (!SetDbValue(value, BelegDatenTable.EmpfängerCol)) return;
			}
		}
		///<summary>
		///		[<c>BillingDatabase</c>].[<c>BelegDaten</c>].[<c>EmpfängerId</c>] (Type = <c>ntext</c>, <c>NULLABLE</c>, MaxLength = <c>536870911</c>)
		///</summary>
		[CsDbDataColumn(MaxLength = 536870911, IsNullable = true)]
		[CsDbNativeDataColumn(Name = "EmpfängerId", Type = "ntext", MaxLength = "536870911", IsNullable = "YES")]
		public String EmpfängerId
		{
			get { return GetDbValue<String>(BelegDatenTable.EmpfängerIdCol); }
			set 
			{ 
				if (!SetDbValue(value, BelegDatenTable.EmpfängerIdCol)) return;
			}
		}
		///<summary>
		///		[<c>BillingDatabase</c>].[<c>BelegDaten</c>].[<c>ZahlungsReferenz</c>] (Type = <c>ntext</c>, <c>NULLABLE</c>, MaxLength = <c>536870911</c>)
		///</summary>
		[CsDbDataColumn(MaxLength = 536870911, IsNullable = true)]
		[CsDbNativeDataColumn(Name = "ZahlungsReferenz", Type = "ntext", MaxLength = "536870911", IsNullable = "YES")]
		public String ZahlungsReferenz
		{
			get { return GetDbValue<String>(BelegDatenTable.ZahlungsReferenzCol); }
			set 
			{ 
				if (!SetDbValue(value, BelegDatenTable.ZahlungsReferenzCol)) return;
			}
		}
		///<summary>
		///		[<c>BillingDatabase</c>].[<c>BelegDaten</c>].[<c>Comment</c>] (Type = <c>ntext</c>, <c>NULLABLE</c>, MaxLength = <c>536870911</c>)
		///</summary>
		[CsDbDataColumn(MaxLength = 536870911, IsNullable = true)]
		[CsDbNativeDataColumn(Name = "Comment", Type = "ntext", MaxLength = "536870911", IsNullable = "YES")]
		public String Comment
		{
			get { return GetDbValue<String>(BelegDatenTable.CommentCol); }
			set 
			{ 
				if (!SetDbValue(value, BelegDatenTable.CommentCol)) return;
			}
		}
		///<summary>
		///		[<c>BillingDatabase</c>].[<c>BelegDaten</c>].[<c>CommentLastChanged</c>] (Type = <c>datetime</c>, <c>NULLABLE</c>)
		///</summary>
		[CsDbDataColumn(IsNullable = true)]
		[CsDbNativeDataColumn(Name = "CommentLastChanged", Type = "datetime", IsNullable = "YES")]
		public DateTime? CommentLastChanged
		{
			get { return GetDbValue<DateTime?>(BelegDatenTable.CommentLastChangedCol); }
			set 
			{ 
				if (!SetDbValue(value, BelegDatenTable.CommentLastChangedCol)) return;
			}
		}
		
		#endregion
	
	
	
	
	
		///	<summary>Reloads the <see cref="BelegData"/> row by executing following command:<para/><c>$"SELECT * FROM BelegDaten WHERE [Id] = '<see cref="Id"/>'</c></summary>
		public BelegData Reload()
		{
			Table.DataSet.BelegDaten.LoadThenFind(Id);
			return this;
		}
		
		
		
		
		#region PROPERTIES<Many to One>
		private BelegData _stornoBeleg;
		private ContractCollection<BelegData> _weakStornierendeBelege;
		private ContractCollection<BelegPosten> _weakPostens;
		private ContractCollection<MailedBeleg> _weakMailedBelege;
		private ContractCollection<PrintedBeleg> _weakPrintedBelege;
		public bool IsStornoBelegLoaded => Equals(_stornoBeleg?.Id, StornoBelegId);
		
		///	<summary>
		///		This field has cached Output.<para/>
		///		<c>SELECT * FROM BelegDaten WHERE [Id] = '<paramref name="StornoBelegId"/>'</c><para/>[<c>BelegDaten</c>].[<c>StornoBelegId</c>] &#62;&#62;&#62;&#62; [<c>BelegDaten</c>].[<c>Id</c>]
		///	</summary>
		[CsDbResolvesRelation("FK_BelegData_StornoBelegId", PkTable = "BelegDaten", PkColumn = "Id", FkTable = "BelegDaten", FkColumn = "StornoBelegId")][DependsOn("StornoBelegId")]
		public BelegData StornoBeleg
		{
			get 
			{
				if (IsStornoBelegLoaded)
					return _stornoBeleg;
				if (StornoBelegId == null) _stornoBeleg = null; else
				_stornoBeleg = Table.DataSet.BelegDaten.FindOrLoad(StornoBelegId.Value);
				return _stornoBeleg;
			}
			set 
			{
				
				if (value != null && value.Table.DataSet != Table.DataSet) throw new InvalidOperationException("The owning data set have to be equal.");
				if (value == _stornoBeleg) return;
		
				_stornoBeleg = value;
		
				if (value == null)
					SetDbValue(default(Guid?), BelegDatenTable.StornoBelegIdCol, "StornoBelegId");
				else
					SetDbValue(value.Id, BelegDatenTable.StornoBelegIdCol, "StornoBelegId");
			}
		}
		#endregion
		
		
		
		
		
		#region PROPERTIES<One to Many>
		///	<summary>
		///		This field has cached Output. <para/>
		///		<c>SELECT * FROM BelegDaten WHERE [StornoBelegId] = '<paramref name="Id"/>'</c><para/>[<c>BelegDaten</c>].[<c>Id</c>] &#60;&#60;&#60;&#60; [<c>BelegDaten</c>].[<c>StornoBelegId</c>]
		///	</summary>
		[CsDbResolvesRelation("FK_BelegData_StornoBelegId", PkTable = "BelegDaten", PkColumn = "Id", FkTable = "BelegDaten", FkColumn = "StornoBelegId")]
		public ContractCollection<BelegData> StornierendeBelege
		{
			get	{ return _weakStornierendeBelege ?? (_weakStornierendeBelege = Table.DataSet.BelegDaten.FindOrLoad_By_StornoBelegId(Id)); }
		}
		///	<summary>
		///		This field has cached Output. <para/>
		///		<c>SELECT * FROM BelegPostens WHERE [BelegDataId] = '<paramref name="Id"/>'</c><para/>[<c>BelegDaten</c>].[<c>Id</c>] &#60;&#60;&#60;&#60; [<c>BelegPostens</c>].[<c>BelegDataId</c>]
		///	</summary>
		[CsDbResolvesRelation("FK_BelegPostens_BelegDataId", PkTable = "BelegDaten", PkColumn = "Id", FkTable = "BelegPostens", FkColumn = "BelegDataId")]
		public ContractCollection<BelegPosten> Postens
		{
			get	{ return _weakPostens ?? (_weakPostens = Table.DataSet.BelegPostens.FindOrLoad_By_BelegDataId(Id)); }
		}
		///	<summary>
		///		This field has cached Output. <para/>
		///		<c>SELECT * FROM MailedBelege WHERE [BelegDataId] = '<paramref name="Id"/>'</c><para/>[<c>BelegDaten</c>].[<c>Id</c>] &#60;&#60;&#60;&#60; [<c>MailedBelege</c>].[<c>BelegDataId</c>]
		///	</summary>
		[CsDbResolvesRelation("FK_MailedBelege_BelegDataId", PkTable = "BelegDaten", PkColumn = "Id", FkTable = "MailedBelege", FkColumn = "BelegDataId")]
		public ContractCollection<MailedBeleg> MailedBelege
		{
			get	{ return _weakMailedBelege ?? (_weakMailedBelege = Table.DataSet.MailedBelege.FindOrLoad_By_BelegDataId(Id)); }
		}
		///	<summary>
		///		This field has cached Output. <para/>
		///		<c>SELECT * FROM PrintedBelege WHERE [BelegDataId] = '<paramref name="Id"/>'</c><para/>[<c>BelegDaten</c>].[<c>Id</c>] &#60;&#60;&#60;&#60; [<c>PrintedBelege</c>].[<c>BelegDataId</c>]
		///	</summary>
		[CsDbResolvesRelation("FK_PrintedBelege_BelegDataId", PkTable = "BelegDaten", PkColumn = "Id", FkTable = "PrintedBelege", FkColumn = "BelegDataId")]
		public ContractCollection<PrintedBeleg> PrintedBelege
		{
			get	{ return _weakPrintedBelege ?? (_weakPrintedBelege = Table.DataSet.PrintedBelege.FindOrLoad_By_BelegDataId(Id)); }
		}
		#endregion
		
		
		
		
		
		#region FUNC<Overrides>
		protected void Invalidate()
		{
			if (!IsPropertyChangedHandled)
				return;
		
			OnPropertyChanged("Id");
			OnPropertyChanged("StateName");
			OnPropertyChanged("TypName");
			OnPropertyChanged("Datum");
			OnPropertyChanged("KassenId");
			OnPropertyChanged("KassenOperator");
			OnPropertyChanged("Nummer");
			OnPropertyChanged("UmsatzZähler");
			OnPropertyChanged("StornoBelegId");
			OnPropertyChanged("BonNummerVon");
			OnPropertyChanged("BonNummerBis");
			OnPropertyChanged("BetragBrutto");
			OnPropertyChanged("BetragNetto");
			OnPropertyChanged("ZusatzText");
			OnPropertyChanged("PrintCount");
			OnPropertyChanged("MailCount");
			OnPropertyChanged("Empfänger");
			OnPropertyChanged("EmpfängerId");
			OnPropertyChanged("ZahlungsReferenz");
			OnPropertyChanged("Comment");
			OnPropertyChanged("CommentLastChanged");
		}
		///	<summary> Set all values which have default values inside the database to their defaults. This method is automatically invoked if you call <see cref="BelegDatenTable.NewRow"/>. </summary>
		public override void ApplyDefaults()
		{
			Id = Guid.NewGuid();
				StateName = "Unknown";
				TypName = "Bar";
				Datum = DateTime.Now;
				BetragBrutto = 0;
				BetragNetto = 0;
				PrintCount = 0;
				MailCount = 0;
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
		public void Copy_To(IBelegData target, bool includePrimaryKey = false)
		{
			if (includePrimaryKey) target.Id = this.Id;
			target.StateName = this.StateName;
			target.TypName = this.TypName;
			target.Datum = this.Datum;
			target.KassenId = this.KassenId;
			target.KassenOperator = this.KassenOperator;
			target.Nummer = this.Nummer;
			target.UmsatzZähler = this.UmsatzZähler;
			target.StornoBelegId = this.StornoBelegId;
			target.BonNummerVon = this.BonNummerVon;
			target.BonNummerBis = this.BonNummerBis;
			target.BetragBrutto = this.BetragBrutto;
			target.BetragNetto = this.BetragNetto;
			target.ZusatzText = this.ZusatzText;
			target.PrintCount = this.PrintCount;
			target.MailCount = this.MailCount;
			target.Empfänger = this.Empfänger;
			target.EmpfängerId = this.EmpfängerId;
			target.ZahlungsReferenz = this.ZahlungsReferenz;
			target.Comment = this.Comment;
			target.CommentLastChanged = this.CommentLastChanged;
		}
		///	<summary> This method copy's each database field from the <paramref name="source"/> interface to this data row.</summary>
		public void Copy_From(IBelegData source, bool includePrimaryKey = false)
		{
			if (includePrimaryKey) this.Id = source.Id;
			this.StateName = source.StateName;
			this.TypName = source.TypName;
			this.Datum = source.Datum;
			this.KassenId = source.KassenId;
			this.KassenOperator = source.KassenOperator;
			this.Nummer = source.Nummer;
			this.UmsatzZähler = source.UmsatzZähler;
			this.StornoBelegId = source.StornoBelegId;
			this.BonNummerVon = source.BonNummerVon;
			this.BonNummerBis = source.BonNummerBis;
			this.BetragBrutto = source.BetragBrutto;
			this.BetragNetto = source.BetragNetto;
			this.ZusatzText = source.ZusatzText;
			this.PrintCount = source.PrintCount;
			this.MailCount = source.MailCount;
			this.Empfänger = source.Empfänger;
			this.EmpfängerId = source.EmpfängerId;
			this.ZahlungsReferenz = source.ZahlungsReferenz;
			this.Comment = source.Comment;
			this.CommentLastChanged = source.CommentLastChanged;
		}
		///	<summary> 
		///		This method copy's each database field which is not in the <paramref name="excludedColumns"/> 
		///		from the <paramref name="source"/> interface to this data row.
		/// </summary>
		public void Copy_From_But_Ignore(IBelegData source, params string[] excludedColumns)
		{
			if (!excludedColumns.Contains(BelegDatenTable.IdCol)) this.Id = source.Id;
			if (!excludedColumns.Contains(BelegDatenTable.StateNameCol)) this.StateName = source.StateName;
			if (!excludedColumns.Contains(BelegDatenTable.TypNameCol)) this.TypName = source.TypName;
			if (!excludedColumns.Contains(BelegDatenTable.DatumCol)) this.Datum = source.Datum;
			if (!excludedColumns.Contains(BelegDatenTable.KassenIdCol)) this.KassenId = source.KassenId;
			if (!excludedColumns.Contains(BelegDatenTable.KassenOperatorCol)) this.KassenOperator = source.KassenOperator;
			if (!excludedColumns.Contains(BelegDatenTable.NummerCol)) this.Nummer = source.Nummer;
			if (!excludedColumns.Contains(BelegDatenTable.UmsatzZählerCol)) this.UmsatzZähler = source.UmsatzZähler;
			if (!excludedColumns.Contains(BelegDatenTable.StornoBelegIdCol)) this.StornoBelegId = source.StornoBelegId;
			if (!excludedColumns.Contains(BelegDatenTable.BonNummerVonCol)) this.BonNummerVon = source.BonNummerVon;
			if (!excludedColumns.Contains(BelegDatenTable.BonNummerBisCol)) this.BonNummerBis = source.BonNummerBis;
			if (!excludedColumns.Contains(BelegDatenTable.BetragBruttoCol)) this.BetragBrutto = source.BetragBrutto;
			if (!excludedColumns.Contains(BelegDatenTable.BetragNettoCol)) this.BetragNetto = source.BetragNetto;
			if (!excludedColumns.Contains(BelegDatenTable.ZusatzTextCol)) this.ZusatzText = source.ZusatzText;
			if (!excludedColumns.Contains(BelegDatenTable.PrintCountCol)) this.PrintCount = source.PrintCount;
			if (!excludedColumns.Contains(BelegDatenTable.MailCountCol)) this.MailCount = source.MailCount;
			if (!excludedColumns.Contains(BelegDatenTable.EmpfängerCol)) this.Empfänger = source.Empfänger;
			if (!excludedColumns.Contains(BelegDatenTable.EmpfängerIdCol)) this.EmpfängerId = source.EmpfängerId;
			if (!excludedColumns.Contains(BelegDatenTable.ZahlungsReferenzCol)) this.ZahlungsReferenz = source.ZahlungsReferenz;
			if (!excludedColumns.Contains(BelegDatenTable.CommentCol)) this.Comment = source.Comment;
			if (!excludedColumns.Contains(BelegDatenTable.CommentLastChangedCol)) this.CommentLastChanged = source.CommentLastChanged;
		}
		///	<summary> 
		///		This method copy's each database field which is in the <paramref name="includedColumns"/> 
		///		from the <paramref name="source"/> interface to this data row.
		/// </summary>
		public void Copy_From_But_TakeOnly(IBelegData source, params string[] includedColumns)
		{
			if (includedColumns.Contains(BelegDatenTable.IdCol)) this.Id = source.Id;
			if (includedColumns.Contains(BelegDatenTable.StateNameCol)) this.StateName = source.StateName;
			if (includedColumns.Contains(BelegDatenTable.TypNameCol)) this.TypName = source.TypName;
			if (includedColumns.Contains(BelegDatenTable.DatumCol)) this.Datum = source.Datum;
			if (includedColumns.Contains(BelegDatenTable.KassenIdCol)) this.KassenId = source.KassenId;
			if (includedColumns.Contains(BelegDatenTable.KassenOperatorCol)) this.KassenOperator = source.KassenOperator;
			if (includedColumns.Contains(BelegDatenTable.NummerCol)) this.Nummer = source.Nummer;
			if (includedColumns.Contains(BelegDatenTable.UmsatzZählerCol)) this.UmsatzZähler = source.UmsatzZähler;
			if (includedColumns.Contains(BelegDatenTable.StornoBelegIdCol)) this.StornoBelegId = source.StornoBelegId;
			if (includedColumns.Contains(BelegDatenTable.BonNummerVonCol)) this.BonNummerVon = source.BonNummerVon;
			if (includedColumns.Contains(BelegDatenTable.BonNummerBisCol)) this.BonNummerBis = source.BonNummerBis;
			if (includedColumns.Contains(BelegDatenTable.BetragBruttoCol)) this.BetragBrutto = source.BetragBrutto;
			if (includedColumns.Contains(BelegDatenTable.BetragNettoCol)) this.BetragNetto = source.BetragNetto;
			if (includedColumns.Contains(BelegDatenTable.ZusatzTextCol)) this.ZusatzText = source.ZusatzText;
			if (includedColumns.Contains(BelegDatenTable.PrintCountCol)) this.PrintCount = source.PrintCount;
			if (includedColumns.Contains(BelegDatenTable.MailCountCol)) this.MailCount = source.MailCount;
			if (includedColumns.Contains(BelegDatenTable.EmpfängerCol)) this.Empfänger = source.Empfänger;
			if (includedColumns.Contains(BelegDatenTable.EmpfängerIdCol)) this.EmpfängerId = source.EmpfängerId;
			if (includedColumns.Contains(BelegDatenTable.ZahlungsReferenzCol)) this.ZahlungsReferenz = source.ZahlungsReferenz;
			if (includedColumns.Contains(BelegDatenTable.CommentCol)) this.Comment = source.Comment;
			if (includedColumns.Contains(BelegDatenTable.CommentLastChangedCol)) this.CommentLastChanged = source.CommentLastChanged;
		}
	}
}