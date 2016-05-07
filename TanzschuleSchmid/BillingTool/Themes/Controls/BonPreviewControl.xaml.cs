// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-05-07</date>

using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using BillingDataAccess.sqlcedatabases.billingdatabase.rows;






namespace BillingTool.Themes.Controls
{
	/// <summary>Interaction logic for BonPreviewControl.xaml</summary>
	public partial class BonPreviewControl : UserControl
	{

		/// <summary>ctor</summary>
		public BonPreviewControl()
		{
			InitializeComponent();
		}

		/// <summary>The data item.</summary>
		public BelegData Item
		{
			get { return (BelegData) GetValue(ItemProperty); }
			set { SetValue(ItemProperty, value); }
		}

		/// <summary>The currently displayed <see cref="OutputFormat" />.</summary>
		public OutputFormat SelectedPreviewFormat
		{
			get { return (OutputFormat) GetValue(SelectedPreviewFormatProperty); }
			set { SetValue(SelectedPreviewFormatProperty, value); }
		}

		/// <summary>The currently selectable <see cref="OutputFormat" />.</summary>
		public OutputFormat[] SelectablePreviewFormats
		{
			get { return (OutputFormat[]) GetValue(SelectablePreviewFormatsProperty); }
			set { SetValue(SelectablePreviewFormatsProperty, value); }
		}

		/// <summary>Reloads the selectable preview formats.</summary>
		public void ReloadSelectablePreviewFormats()
		{
			var currentSelection = SelectedPreviewFormat;
			SelectablePreviewFormats = Item?.PrintedBelege.Select(x => x.OutputFormat).Union(Item.MailedBelege.Select(x => x.OutputFormat)).ToArray();
			if (SelectablePreviewFormats != null && SelectablePreviewFormats.Length != 0)
			{
				if (SelectablePreviewFormats.Contains(currentSelection))
					return;
				SelectedPreviewFormat = SelectablePreviewFormats[0];
				return;
			}
			SelectedPreviewFormat = null;
		}

#pragma warning disable 1591
		public static readonly DependencyProperty ItemProperty = DependencyProperty.Register("Item", typeof(BelegData), typeof(BonPreviewControl), new FrameworkPropertyMetadata {DefaultValue = default(BelegData), BindsTwoWayByDefault = true, DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged, PropertyChangedCallback = (o, args) => ((BonPreviewControl)o).ReloadSelectablePreviewFormats()});
		public static readonly DependencyProperty SelectedPreviewFormatProperty = DependencyProperty.Register("SelectedPreviewFormat", typeof(OutputFormat), typeof(BonPreviewControl), new FrameworkPropertyMetadata {DefaultValue = default(OutputFormat), BindsTwoWayByDefault = true, DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged});
		public static readonly DependencyProperty SelectablePreviewFormatsProperty = DependencyProperty.Register("SelectablePreviewFormats", typeof(OutputFormat[]), typeof(BonPreviewControl), new FrameworkPropertyMetadata {DefaultValue = default(OutputFormat[]), BindsTwoWayByDefault = true, DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged});
#pragma warning restore 1591
	}
}