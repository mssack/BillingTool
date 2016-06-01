// Copyright (c) 2014, 2015 All Right Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2015-07-24</date>

using System;
using System.Windows;
using System.Windows.Data;
using CsWpfBase.Themes.Controls.Editors.Base;






namespace CsWpfBase.Themes.Controls.Editors
{
	/// <summary>An editor for date times.</summary>
	public class DateTimeEditor : EditorBase<DateTime?>
	{
		#region DP Keys
		/// <summary></summary>
		public static readonly DependencyProperty AllowNullProperty = DependencyProperty.Register("AllowNull", typeof (bool), typeof (DateTimeEditor), new FrameworkPropertyMetadata {DefaultValue = default(bool), DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged});
		#endregion


		static DateTimeEditor()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof (DateTimeEditor), new FrameworkPropertyMetadata(typeof (DateTimeEditor)));
		}

		/// <summary>Specify's whether the editor allows null as value.</summary>
		public bool AllowNull
		{
			get { return (bool) GetValue(AllowNullProperty); }
			set { SetValue(AllowNullProperty, value); }
		}
	}
}