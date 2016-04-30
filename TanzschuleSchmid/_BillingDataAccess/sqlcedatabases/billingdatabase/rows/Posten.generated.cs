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
//<date>2016-04-20 11:49:50</date>



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
	///<summary>DataRow([<c>BillingDatabase</c>].[<c>Postens</c>]): row model of <see cref="PostensTable"/>.</summary>
	[Serializable]
	[DebuggerDisplay("DataRow(BillingDatabase.Postens): Id = '{Id}'")]
	[CsDbDataRow(Database = "BillingDatabase", TableName = "Postens", Generated = "16.04.20 11:49:50", Hash = "02CCA5ACC646543876BAC1C44260C6B6")]
	public partial class Posten : CsDbTableRow, IPosten
	{
		#region statics
		private static Dictionary<string, System.Reflection.PropertyInfo> _nativeColumnName_To_Property;
		public static Dictionary<string, System.Reflection.PropertyInfo> NativeColumnName_To_Property
		{
			get
			{
				if (_nativeColumnName_To_Property != null)
					return _nativeColumnName_To_Property;
		
				var type = typeof(IPosten);
				_nativeColumnName_To_Property = new Dictionary<string, System.Reflection.PropertyInfo>
				{
					{ PostensTable.IdCol, type.GetProperty(nameof(Id)) },
					{ PostensTable.CreationDateCol, type.GetProperty(nameof(CreationDate)) },
					{ PostensTable.LastUsedDateCol, type.GetProperty(nameof(LastUsedDate)) },
					{ PostensTable.NameCol, type.GetProperty(nameof(Name)) },
					{ PostensTable.PreisBruttoCol, type.GetProperty(nameof(PreisBrutto)) },
					{ PostensTable.DimensionCol, type.GetProperty(nameof(Dimension)) },
					{ PostensTable.AnzahlGekauftCol, type.GetProperty(nameof(AnzahlGekauft)) },
					{ PostensTable.AnzahlStorniertCol, type.GetProperty(nameof(AnzahlStorniert)) },
					{ PostensTable.CommentCol, type.GetProperty(nameof(Comment)) },
					{ PostensTable.CommentLastChangedCol, type.GetProperty(nameof(CommentLastChanged)) }
				};
		
				return _nativeColumnName_To_Property;
			}
		}
		#endregion
	
	
		public Posten(DataRowBuilder builder) : base(builder){}
	
		
		///<summary>References the owning data context, this is equal to the database server. Use this to address databases on the same server.</summary>
		public new BillingDataAccess.sqlcedatabases.SqlCeDatabasesContext DataContext => Table.DataContext;
	
		///<summary>References the owning dataset. Use this to address tables in the same database</summary>
		public new BillingDatabase DataSet => Table.DataSet;
	
		///	<summary>Gets the owning table of type <see cref="PostensTable"/>.</summary>
		public new PostensTable Table => (PostensTable) base.Table;
	
	
	
	
		#region COLUMNS
		///<summary>
		///		[<c>BillingDatabase</c>].[<c>Postens</c>].[<c>Id</c>] (Type = <c>uniqueidentifier</c>, Default = '<c>newid()</c>')
		///</summary>
		[CsDbDataColumn(Default = "Guid.NewGuid()")]
		[CsDbNativeDataColumn(Name = "Id", Type = "uniqueidentifier", Default = "newid()", IsNullable = "NO")]
		public Guid Id
		{
			get { return GetDbValue<Guid>(PostensTable.IdCol); }
			set 
			{ 
				if (!SetDbValue(value, PostensTable.IdCol)) return;
			}
		}
		///<summary>
		///		[<c>BillingDatabase</c>].[<c>Postens</c>].[<c>CreationDate</c>] (Type = <c>datetime</c>)
		///</summary>
		[CsDbDataColumn]
		[CsDbNativeDataColumn(Name = "CreationDate", Type = "datetime", IsNullable = "NO")]
		public DateTime CreationDate
		{
			get { return GetDbValue<DateTime>(PostensTable.CreationDateCol); }
			set 
			{ 
				if (!SetDbValue(value, PostensTable.CreationDateCol)) return;
			}
		}
		///<summary>
		///		[<c>BillingDatabase</c>].[<c>Postens</c>].[<c>LastUsedDate</c>] (Type = <c>datetime</c>, <c>NULLABLE</c>)
		///</summary>
		[CsDbDataColumn(IsNullable = true)]
		[CsDbNativeDataColumn(Name = "LastUsedDate", Type = "datetime", IsNullable = "YES")]
		public DateTime? LastUsedDate
		{
			get { return GetDbValue<DateTime?>(PostensTable.LastUsedDateCol); }
			set 
			{ 
				if (!SetDbValue(value, PostensTable.LastUsedDateCol)) return;
			}
		}
		///<summary>
		///		[<c>BillingDatabase</c>].[<c>Postens</c>].[<c>Name</c>] (Type = <c>ntext</c>, MaxLength = <c>536870911</c>)
		///</summary>
		[CsDbDataColumn(MaxLength = 536870911)]
		[CsDbNativeDataColumn(Name = "Name", Type = "ntext", MaxLength = "536870911", IsNullable = "NO")]
		public String Name
		{
			get { return GetDbValue<String>(PostensTable.NameCol); }
			set 
			{ 
				if (!SetDbValue(value, PostensTable.NameCol)) return;
			}
		}
		///<summary>
		///		[<c>BillingDatabase</c>].[<c>Postens</c>].[<c>PreisBrutto</c>] (Type = <c>money</c>)
		///</summary>
		[CsDbDataColumn]
		[CsDbNativeDataColumn(Name = "PreisBrutto", Type = "money", IsNullable = "NO")]
		public Decimal PreisBrutto
		{
			get { return GetDbValue<Decimal>(PostensTable.PreisBruttoCol); }
			set 
			{ 
				if (!SetDbValue(value, PostensTable.PreisBruttoCol)) return;
			}
		}
		///<summary>
		///		[<c>BillingDatabase</c>].[<c>Postens</c>].[<c>Dimension</c>] (Type = <c>ntext</c>, <c>NULLABLE</c>, MaxLength = <c>536870911</c>)
		///</summary>
		[CsDbDataColumn(MaxLength = 536870911, IsNullable = true)]
		[CsDbNativeDataColumn(Name = "Dimension", Type = "ntext", MaxLength = "536870911", IsNullable = "YES")]
		public String Dimension
		{
			get { return GetDbValue<String>(PostensTable.DimensionCol); }
			set 
			{ 
				if (!SetDbValue(value, PostensTable.DimensionCol)) return;
			}
		}
		///<summary>
		///		[<c>BillingDatabase</c>].[<c>Postens</c>].[<c>AnzahlGekauft</c>] (Type = <c>int</c>, Default = '<c>(0)</c>')
		///</summary>
		[CsDbDataColumn(Default = 0)]
		[CsDbNativeDataColumn(Name = "AnzahlGekauft", Type = "int", Default = "(0)", IsNullable = "NO")]
		public Int32 AnzahlGekauft
		{
			get { return GetDbValue<Int32>(PostensTable.AnzahlGekauftCol); }
			set 
			{ 
				if (!SetDbValue(value, PostensTable.AnzahlGekauftCol)) return;
			}
		}
		///<summary>
		///		[<c>BillingDatabase</c>].[<c>Postens</c>].[<c>AnzahlStorniert</c>] (Type = <c>int</c>, Default = '<c>(0)</c>')
		///</summary>
		[CsDbDataColumn(Default = 0)]
		[CsDbNativeDataColumn(Name = "AnzahlStorniert", Type = "int", Default = "(0)", IsNullable = "NO")]
		public Int32 AnzahlStorniert
		{
			get { return GetDbValue<Int32>(PostensTable.AnzahlStorniertCol); }
			set 
			{ 
				if (!SetDbValue(value, PostensTable.AnzahlStorniertCol)) return;
			}
		}
		///<summary>
		///		[<c>BillingDatabase</c>].[<c>Postens</c>].[<c>Comment</c>] (Type = <c>ntext</c>, <c>NULLABLE</c>, MaxLength = <c>536870911</c>)
		///</summary>
		[CsDbDataColumn(MaxLength = 536870911, IsNullable = true)]
		[CsDbNativeDataColumn(Name = "Comment", Type = "ntext", MaxLength = "536870911", IsNullable = "YES")]
		public String Comment
		{
			get { return GetDbValue<String>(PostensTable.CommentCol); }
			set 
			{ 
				if (!SetDbValue(value, PostensTable.CommentCol)) return;
			}
		}
		///<summary>
		///		[<c>BillingDatabase</c>].[<c>Postens</c>].[<c>CommentLastChanged</c>] (Type = <c>datetime</c>, <c>NULLABLE</c>)
		///</summary>
		[CsDbDataColumn(IsNullable = true)]
		[CsDbNativeDataColumn(Name = "CommentLastChanged", Type = "datetime", IsNullable = "YES")]
		public DateTime? CommentLastChanged
		{
			get { return GetDbValue<DateTime?>(PostensTable.CommentLastChangedCol); }
			set 
			{ 
				if (!SetDbValue(value, PostensTable.CommentLastChangedCol)) return;
			}
		}
		
		#endregion
	
	
	
	
	
		///	<summary>Reloads the <see cref="Posten"/> row by executing following command:<para/><c>$"SELECT * FROM Postens WHERE [Id] = '<see cref="Id"/>'</c></summary>
		public Posten Reload()
		{
			Table.DataSet.Postens.LoadThenFind(Id);
			return this;
		}
		
		
		
		
		#region PROPERTIES<Many to One>
		private ContractCollection<BelegPosten> _weakBelegPostens;
		
		
		
		#endregion
		
		
		
		
		
		#region PROPERTIES<One to Many>
		///	<summary>
		///		This field has cached Output. <para/>
		///		<c>SELECT * FROM BelegPostens WHERE [PostenId] = '<paramref name="Id"/>'</c><para/>[<c>Postens</c>].[<c>Id</c>] &#60;&#60;&#60;&#60; [<c>BelegPostens</c>].[<c>PostenId</c>]
		///	</summary>
		[CsDbResolvesRelation("FK_BelegPostens_PostenId", PkTable = "Postens", PkColumn = "Id", FkTable = "BelegPostens", FkColumn = "PostenId")]
		public ContractCollection<BelegPosten> BelegPostens
		{
			get	{ return _weakBelegPostens ?? (_weakBelegPostens = Table.DataSet.BelegPostens.FindOrLoad_By_PostenId(Id)); }
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
			OnPropertyChanged("PreisBrutto");
			OnPropertyChanged("Dimension");
			OnPropertyChanged("AnzahlGekauft");
			OnPropertyChanged("AnzahlStorniert");
			OnPropertyChanged("Comment");
			OnPropertyChanged("CommentLastChanged");
		}
		///	<summary> Set all values which have default values inside the database to their defaults. This method is automatically invoked if you call <see cref="PostensTable.NewRow"/>. </summary>
		public override void ApplyDefaults()
		{
			Id = Guid.NewGuid();
				AnzahlGekauft = 0;
				AnzahlStorniert = 0;
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
		public void Copy_To(IPosten target, bool includePrimaryKey = false)
		{
			if (includePrimaryKey) target.Id = this.Id;
			target.CreationDate = this.CreationDate;
			target.LastUsedDate = this.LastUsedDate;
			target.Name = this.Name;
			target.PreisBrutto = this.PreisBrutto;
			target.Dimension = this.Dimension;
			target.AnzahlGekauft = this.AnzahlGekauft;
			target.AnzahlStorniert = this.AnzahlStorniert;
			target.Comment = this.Comment;
			target.CommentLastChanged = this.CommentLastChanged;
		}
		///	<summary> This method copy's each database field from the <paramref name="source"/> interface to this data row.</summary>
		public void Copy_From(IPosten source, bool includePrimaryKey = false)
		{
			if (includePrimaryKey) this.Id = source.Id;
			this.CreationDate = source.CreationDate;
			this.LastUsedDate = source.LastUsedDate;
			this.Name = source.Name;
			this.PreisBrutto = source.PreisBrutto;
			this.Dimension = source.Dimension;
			this.AnzahlGekauft = source.AnzahlGekauft;
			this.AnzahlStorniert = source.AnzahlStorniert;
			this.Comment = source.Comment;
			this.CommentLastChanged = source.CommentLastChanged;
		}
		///	<summary> 
		///		This method copy's each database field which is not in the <paramref name="excludedColumns"/> 
		///		from the <paramref name="source"/> interface to this data row.
		/// </summary>
		public void Copy_From_But_Ignore(IPosten source, params string[] excludedColumns)
		{
			if (!excludedColumns.Contains(PostensTable.IdCol)) this.Id = source.Id;
			if (!excludedColumns.Contains(PostensTable.CreationDateCol)) this.CreationDate = source.CreationDate;
			if (!excludedColumns.Contains(PostensTable.LastUsedDateCol)) this.LastUsedDate = source.LastUsedDate;
			if (!excludedColumns.Contains(PostensTable.NameCol)) this.Name = source.Name;
			if (!excludedColumns.Contains(PostensTable.PreisBruttoCol)) this.PreisBrutto = source.PreisBrutto;
			if (!excludedColumns.Contains(PostensTable.DimensionCol)) this.Dimension = source.Dimension;
			if (!excludedColumns.Contains(PostensTable.AnzahlGekauftCol)) this.AnzahlGekauft = source.AnzahlGekauft;
			if (!excludedColumns.Contains(PostensTable.AnzahlStorniertCol)) this.AnzahlStorniert = source.AnzahlStorniert;
			if (!excludedColumns.Contains(PostensTable.CommentCol)) this.Comment = source.Comment;
			if (!excludedColumns.Contains(PostensTable.CommentLastChangedCol)) this.CommentLastChanged = source.CommentLastChanged;
		}
		///	<summary> 
		///		This method copy's each database field which is in the <paramref name="includedColumns"/> 
		///		from the <paramref name="source"/> interface to this data row.
		/// </summary>
		public void Copy_From_But_TakeOnly(IPosten source, params string[] includedColumns)
		{
			if (includedColumns.Contains(PostensTable.IdCol)) this.Id = source.Id;
			if (includedColumns.Contains(PostensTable.CreationDateCol)) this.CreationDate = source.CreationDate;
			if (includedColumns.Contains(PostensTable.LastUsedDateCol)) this.LastUsedDate = source.LastUsedDate;
			if (includedColumns.Contains(PostensTable.NameCol)) this.Name = source.Name;
			if (includedColumns.Contains(PostensTable.PreisBruttoCol)) this.PreisBrutto = source.PreisBrutto;
			if (includedColumns.Contains(PostensTable.DimensionCol)) this.Dimension = source.Dimension;
			if (includedColumns.Contains(PostensTable.AnzahlGekauftCol)) this.AnzahlGekauft = source.AnzahlGekauft;
			if (includedColumns.Contains(PostensTable.AnzahlStorniertCol)) this.AnzahlStorniert = source.AnzahlStorniert;
			if (includedColumns.Contains(PostensTable.CommentCol)) this.Comment = source.Comment;
			if (includedColumns.Contains(PostensTable.CommentLastChangedCol)) this.CommentLastChanged = source.CommentLastChanged;
		}
	}
}