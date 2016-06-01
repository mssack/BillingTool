// Copyright (c) 2014, 2015 All Right Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2015-06-11</date>

using System;
using System.Windows;
using System.Windows.Data;
using CsWpfBase.Themes.Controls.Editors.Base;
using CsWpfBase.Utilitys;





namespace CsWpfBase.Themes.Controls.Editors
{
#pragma warning disable 1591
	public class DateEditor : EditorBase<DateTime?>
	{
		#region DependencyProperty Static Keys
		public static readonly DependencyProperty AllowNullProperty = DependencyProperty.Register("AllowNull", typeof (bool), typeof (DateEditor), new FrameworkPropertyMetadata {DefaultValue = default(bool), DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged, PropertyChangedCallback = (o, args) => ((DateEditor) o).AllowNullChanged((bool) args.OldValue, (bool) args.NewValue)});
		internal static readonly DependencyProperty InternalValueProperty = DependencyProperty.Register("InternalValue", typeof (DateTime?), typeof (DateEditor), new FrameworkPropertyMetadata {DefaultValue = default(DateTime?), BindsTwoWayByDefault = true, DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged, PropertyChangedCallback = (o, args) => ((DateEditor) o).InternalValueChanged((DateTime?) args.OldValue, (DateTime?) args.NewValue)});
		#endregion


		private readonly ProcessLock _changeLock = new ProcessLock();
		static DateEditor()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof (DateEditor), new FrameworkPropertyMetadata(typeof (DateEditor)));
		}


		#region Overrides
		protected override void ValueChanged(DateTime? oldValue, DateTime? newValue)
		{
			if (_changeLock.Active)
				return;
			using (_changeLock.Activate())
			{
				InternalValue = newValue;
			}
		}
		#endregion


		public bool AllowNull
		{
			get { return (bool) GetValue(AllowNullProperty); }
			set { SetValue(AllowNullProperty, value); }
		}
		/// <summary>This property is used to create an bypass and adapt the changes to the original value see
		///     <see cref="InternalValueChanged" />
		/// </summary>
		internal DateTime? InternalValue
		{
			get { return (DateTime?) GetValue(InternalValueProperty); }
			set { SetValue(InternalValueProperty, value); }
		}
		private void InternalValueChanged(DateTime? oldValue, DateTime? newValue)
		{
			if (_changeLock.Active)
				return;
			using (_changeLock.Activate())
			{
				if (newValue == null)
				{
					Value = null;
					return;
				}
				if (Value == null)
				{
					Value = new DateTime(newValue.Value.Year, newValue.Value.Month, newValue.Value.Day, 0, 0, 0, 0);
					return;
				}
				Value = new DateTime(newValue.Value.Year, newValue.Value.Month, newValue.Value.Day, Value.Value.Hour, Value.Value.Minute, Value.Value.Second, Value.Value.Millisecond);
			}
		}
		private void AllowNullChanged(bool oldValue, bool newValue)
		{
			if (ReadLocalValue(ValueProperty) == DependencyProperty.UnsetValue)
				Value = newValue ? null : (DateTime?) DateTime.MinValue;
			else if (Value == null && newValue == false)
				Value = DateTime.MinValue;
		}
	}
}