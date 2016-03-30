// Copyright (c) 2014, 2015 All Right Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2015-06-11</date>

using System;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using CsWpfBase.Themes.Controls.ParameterEngine.Base;





namespace CsWpfBase.Themes.Controls.ParameterEngine.Editors
{
#pragma warning disable 1591
	/// <summary>An editor for booleans</summary>
	public class BoolParam : ParameterEngineBase
	{
		#region DependencyProperty Static Keys
		public static readonly DependencyProperty ValueProperty = DependencyProperty.Register("Value", typeof (bool?), typeof (BoolParam), new FrameworkPropertyMetadata {DefaultValue = false, BindsTwoWayByDefault = true, DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged});
		public static readonly DependencyProperty AcceptNullProperty = DependencyProperty.Register("AcceptNull", typeof (bool), typeof (BoolParam), new FrameworkPropertyMetadata {DefaultValue = false, DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged, PropertyChangedCallback = (o, args) => ((BoolParam) o).AcceptNullChanged((bool) args.OldValue, (bool) args.NewValue)});
		#endregion


		static BoolParam()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof (BoolParam), new FrameworkPropertyMetadata(typeof (BoolParam)));
		}
		public BoolParam()
		{
			MouseLeftButtonUp += BoolParam_MouseLeftButtonUp;
		}

		/// <summary>Gets or sets the value.</summary>
		public bool? Value
		{
			get { return (bool?) GetValue(ValueProperty); }
			set { SetValue(ValueProperty, value); }
		}
		/// <summary>Defines if the value can be null.</summary>
		public bool AcceptNull
		{
			get { return (bool) GetValue(AcceptNullProperty); }
			set { SetValue(AcceptNullProperty, value); }
		}
		private void BoolParam_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
		{
			Value = !Value;
		}


		private void AcceptNullChanged(bool oldValue, bool newValue)
		{
			if (newValue == false && Value == null)
				Value = default (bool);
		}
	}
}