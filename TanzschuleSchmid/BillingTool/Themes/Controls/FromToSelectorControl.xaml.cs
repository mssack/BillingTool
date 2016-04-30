// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-04-30</date>

using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;






namespace BillingTool.Themes.Controls
{
	/// <summary>Used to select a date range.</summary>
	public class FromToSelectorControl : Control
	{


		static FromToSelectorControl()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof(FromToSelectorControl), new FrameworkPropertyMetadata(typeof(FromToSelectorControl)));
		}

		/// <summary>The datetime from.</summary>
		public DateTime From
		{
			get { return (DateTime) GetValue(FromProperty); }
			set { SetValue(FromProperty, value); }
		}
		/// <summary>The datetime to.</summary>
		public DateTime To
		{
			get { return (DateTime) GetValue(ToProperty); }
			set { SetValue(ToProperty, value); }
		}

		/// <summary>Occurs whenever the date selection changed</summary>
		public event Action SelectionChanged;

		private void FromDateChanged()
		{
			if (To < From)
				To = From.AddDays(1);
			OnSelectionChanged();
		}

		private void ToDateChanged()
		{
			if (From > To)
				From = To.AddDays(-1);
			OnSelectionChanged();
		}

		private void OnSelectionChanged()
		{
			SelectionChanged?.Invoke();
		}



#pragma warning disable 1591
		public static readonly DependencyProperty FromProperty = DependencyProperty.Register("From", typeof(DateTime), typeof(FromToSelectorControl), new FrameworkPropertyMetadata {DefaultValue = default(DateTime), BindsTwoWayByDefault = true, DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged, PropertyChangedCallback = (o, args) => ((FromToSelectorControl) o).FromDateChanged()});
		public static readonly DependencyProperty ToProperty = DependencyProperty.Register("To", typeof(DateTime), typeof(FromToSelectorControl), new FrameworkPropertyMetadata {DefaultValue = default(DateTime), BindsTwoWayByDefault = true, DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged, PropertyChangedCallback = (o, args) => ((FromToSelectorControl) o).ToDateChanged()});
#pragma warning restore 1591
	}
}