// Copyright (c) 2014, 2015 All Right Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2015-06-11</date>

using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using CsWpfBase.Ev.Objects;
using CsWpfBase.Ev.Public.Extensions;
using CsWpfBase.Utilitys;





namespace CsWpfBase.Themes.Controls.Editors.Base
{
#pragma warning disable 1591
	public class NumberEditor : EditorBase<string>
	{
		#region DependencyProperty Static Keys
		public static readonly DependencyProperty AllowNullProperty = DependencyProperty.Register("AllowNull", typeof (bool), typeof (NumberEditor), new FrameworkPropertyMetadata {DefaultValue = default(bool), BindsTwoWayByDefault = true, DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged, Inherits = true});
		private static readonly DependencyPropertyKey IsValidNumberPropertyKey = DependencyProperty.RegisterReadOnly("IsValidNumber", typeof (bool), typeof (NumberEditor), new FrameworkPropertyMetadata {DefaultValue = default(bool), DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged});
		public static readonly DependencyProperty IsValidNumberProperty = IsValidNumberPropertyKey.DependencyProperty;
		private static readonly DependencyPropertyKey ErrorMessagePropertyKey = DependencyProperty.RegisterReadOnly("ErrorMessage", typeof (string), typeof (NumberEditor), new FrameworkPropertyMetadata {DefaultValue = default(string), BindsTwoWayByDefault = true, DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged, Inherits = true});
		public static readonly DependencyProperty ErrorMessageProperty = ErrorMessagePropertyKey.DependencyProperty;
		#endregion
	

		protected static readonly HashSet<Key> NumberKeys = new HashSet<Key>(new[] {Key.NumPad0, Key.NumPad1, Key.NumPad2, Key.NumPad3, Key.NumPad4, Key.NumPad5, Key.NumPad6, Key.NumPad7, Key.NumPad8, Key.NumPad9, Key.D0, Key.D1, Key.D2, Key.D3, Key.D4, Key.D5, Key.D6, Key.D7, Key.D8, Key.D9});
		protected static readonly HashSet<Key> NavigationKeys = new HashSet<Key>(new[] {Key.Left, Key.Right, Key.Tab});
		protected static readonly HashSet<Key> RemoveKeys = new HashSet<Key>(new[] {Key.Back, Key.Delete});


		private ICommand _decreaseCommand;
		private ICommand _increaseCommand;
		private TextBox _inputTextBox;


		#region Overrides
		public override void OnApplyTemplate()
		{
			base.OnApplyTemplate();
			if (Template == null)
			{
				InputTextBox = null;
				return;
			}
			InputTextBox = Template.FindName("PART_InputTextBox", this) as TextBox;
		}
		#endregion


		#region Virtuals
		protected virtual void IncreaseRequired()
		{
		}
		protected virtual void DecreaseRequired()
		{
		}


		protected virtual void InputTextBoxChanged(TextBox old, TextBox newValue)
		{
		}
		#endregion


		public bool IsValidNumber
		{
			get { return (bool) GetValue(IsValidNumberProperty); }
			protected set { SetValue(IsValidNumberPropertyKey, value); }
		}
		public string ErrorMessage
		{
			get { return (string) GetValue(ErrorMessageProperty); }
			protected set { SetValue(ErrorMessagePropertyKey, value); }
		}
		public bool AllowNull
		{
			get { return (bool) GetValue(AllowNullProperty); }
			set { SetValue(AllowNullProperty, value); }
		}

		public ICommand IncreaseCommand
		{
			get { return _increaseCommand ?? (_increaseCommand = new RelayCommand(IncreaseRequired)); }
		}
		public ICommand DecreaseCommand
		{
			get { return _decreaseCommand ?? (_decreaseCommand = new RelayCommand(DecreaseRequired)); }
		}

		protected TextBox InputTextBox
		{
			get { return _inputTextBox; }
			set
			{
				var old = _inputTextBox;
				_inputTextBox = value;
				InputTextBoxChanged(old, value);
			}
		}
	}





	public class NumberEditor<TValue> : NumberEditor
	{
		#region DependencyProperty Static Keys
		public static readonly DependencyProperty ValueNumberProperty = DependencyProperty.Register("ValueNumber", typeof (TValue), typeof (NumberEditor<TValue>), new FrameworkPropertyMetadata {DefaultValue = default(TValue), BindsTwoWayByDefault = true, DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged, PropertyChangedCallback = (o, args) =>
		{
			((NumberEditor<TValue>) o).ValueNumberChanged((TValue) args.OldValue, (TValue) args.NewValue);
		}});
		public static readonly DependencyProperty MinimumProperty = DependencyProperty.Register("Minimum", typeof (TValue), typeof (NumberEditor<TValue>), new FrameworkPropertyMetadata {DefaultValue = default(TValue), DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged, Inherits = true, PropertyChangedCallback = (o, args) => { ((NumberEditor<TValue>) o).BoundaryChanged((TValue) args.OldValue, (TValue) args.NewValue); }});
		public static readonly DependencyProperty MaximumProperty = DependencyProperty.Register("Maximum", typeof (TValue), typeof (NumberEditor<TValue>), new FrameworkPropertyMetadata {DefaultValue = default(TValue), DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged, Inherits = true, PropertyChangedCallback = (o, args) => { ((NumberEditor<TValue>) o).BoundaryChanged((TValue) args.OldValue, (TValue) args.NewValue); }});
		#endregion


		private readonly ProcessLock _changeLock = new ProcessLock();
		private readonly Utilitys.DynamicNumber _dynamicNumber = new Utilitys.DynamicNumber(typeof (TValue));
		private bool? _acceptCommaNumber;


		private bool? _acceptNevativeNumber;
		private bool? _isNullable;
		private Type _valueType;
		

		#region Overrides
		protected override void InputTextBoxChanged(TextBox old, TextBox newValue)
		{
			if (old != null)
			{
				old.PreviewKeyDown -= InputTextBox_PreviewKeyDown;
				old.TextChanged -= InputTextBox_TextChanged;
			}
			if (newValue != null)
			{
				newValue.PreviewKeyDown += InputTextBox_PreviewKeyDown;
				newValue.TextChanged += InputTextBox_TextChanged;
			}
		}
		protected override void ValueChanged(string oldValue, string newValue)
		{
			if (_changeLock.Active)
				return;
			using (_changeLock.Activate())
			{
				if ((String.IsNullOrEmpty(newValue) || newValue == "-") && AllowNull)
					ValueNumber = default(TValue);
				else if (String.IsNullOrEmpty(newValue) || newValue == "-")
					ValueNumber = (TValue) Activator.CreateInstance(ValueType);
				else
					try
					{
						var valueNumber = (TValue) Convert.ChangeType(newValue, ValueType);

						_dynamicNumber.Value = valueNumber;
						ValueNumber = valueNumber;

						if (_dynamicNumber > Maximum)
						{
							ErrorMessage = "The number is too big the maximum is " + Maximum + ".";
							IsValidNumber = false;
							return;
						}
						if (_dynamicNumber < Minimum)
						{
							ErrorMessage = "The number is too small the minimum is " + Minimum + ".";
							IsValidNumber = false;
							return;
						}

						IsValidNumber = true;
					}
					catch (Exception)
					{
						ErrorMessage = "The number is too small or too big or contains invalid characters.";
						IsValidNumber = false;
					}
			}
		}
		protected override void IncreaseRequired()
		{
			_dynamicNumber.Value = ValueNumber;
			if (!(new Utilitys.DynamicNumber(typeof (TValue)) {Value = _dynamicNumber + 1} > Maximum))
				ValueNumber = (TValue) (_dynamicNumber + 1);
		}
		protected override void DecreaseRequired()
		{
			_dynamicNumber.Value = ValueNumber;
			if (!(new Utilitys.DynamicNumber(typeof (TValue)) {Value = _dynamicNumber - 1} < Minimum))
				ValueNumber = (TValue) (_dynamicNumber - 1);
		}
		#endregion


		public TValue ValueNumber
		{
			get { return (TValue) GetValue(ValueNumberProperty); }
			set { SetValue(ValueNumberProperty, value); }
		}
		public TValue Minimum
		{
			get { return (TValue) GetValue(MinimumProperty); }
			set { SetValue(MinimumProperty, value); }
		}
		public TValue Maximum
		{
			get { return (TValue) GetValue(MaximumProperty); }
			set { SetValue(MaximumProperty, value); }
		}
		private bool AcceptNegativeNumber
		{
			get
			{
				if (_acceptNevativeNumber != null)
					return _acceptNevativeNumber.Value;

				_acceptNevativeNumber = (ValueType != typeof (byte) && ValueType != typeof (UInt16) && ValueType != typeof (UInt32) && ValueType != typeof (UInt64));
				return _acceptNevativeNumber.Value;
			}
		}
		private bool AcceptCommaNumber
		{
			get
			{
				if (_acceptCommaNumber != null)
					return _acceptCommaNumber.Value;

				_acceptCommaNumber = ValueType == typeof (double) || ValueType == typeof (decimal);
				return _acceptCommaNumber.Value;
			}
		}
		private bool IsNullable
		{
			get
			{
				if (_isNullable != null)
					return _isNullable.Value;
				_isNullable = Nullable.GetUnderlyingType(typeof (TValue)) != null;
				return _isNullable.Value;
			}
		}
		private Type ValueType
		{
			get
			{
				if (_valueType != null)
					return _valueType;
				var type = typeof (TValue);
				var underlyingType = Nullable.GetUnderlyingType(type);

				_valueType = underlyingType ?? type;
				return _valueType;
			}
		}
		private void ValueNumberChanged(TValue oldValue, TValue newValue)
		{
			if (_changeLock.Active)
				return;
			using (_changeLock.Activate())
			{
				Value = newValue.ToString();
				IsValidNumber = true;
			}
		}
		private void RemoveStringWithoutSelectionChange(string remove)
		{
			var selectionStart = InputTextBox.SelectionStart;
			var selectionLength = InputTextBox.SelectionLength;
			var text = InputTextBox.Text;
			var occurance = text.IndexOf(remove, StringComparison.Ordinal);



			if (occurance == -1)
				return;

			InputTextBox.Text = text.Replace(remove, "");
			if (occurance + remove.Length > selectionStart && occurance < selectionStart)
				InputTextBox.Select(selectionStart - remove.Length, occurance + remove.Length - selectionStart);
			else if (occurance < selectionStart + selectionLength && occurance > selectionStart)
				InputTextBox.Select(selectionStart, selectionLength - (selectionStart + selectionLength - occurance));
			else if (occurance < selectionStart)
				InputTextBox.Select(selectionStart - remove.Length, selectionLength);
			else if (occurance > selectionStart)
				InputTextBox.Select(selectionStart, selectionLength);
		}


		private void InputTextBox_PreviewKeyDown(object sender, KeyEventArgs args)
		{
			args.Handled = true;
			var key = args.Key;
			if (key == Key.OemMinus && AcceptNegativeNumber)
			{
				if (InputTextBox.Text.StartsWith("-"))
					RemoveStringWithoutSelectionChange("-");
				else
				{
					var start = InputTextBox.SelectionStart;
					var length = InputTextBox.SelectionLength;
					InputTextBox.Text = "-" + InputTextBox.Text;
					InputTextBox.Select(start + 1, length);
				}
			}
			else if (key == Key.OemComma && AcceptCommaNumber)
			{
				RemoveStringWithoutSelectionChange(",");
				args.Handled = false;
			}
			else if (NumberKeys.Contains(key))
			{
				if (InputTextBox.Text.StartsWith("-") && InputTextBox.SelectionStart == 0 && InputTextBox.SelectionLength == 0)
					RemoveStringWithoutSelectionChange("-");

				args.Handled = false;
			}
			else if (RemoveKeys.Contains(key))
			{
				args.Handled = false;
			}
			else if (NavigationKeys.Contains(key))
			{
				args.Handled = false;
			}
		}
		private void InputTextBox_TextChanged(object sender, TextChangedEventArgs args)
		{
			if (_changeLock.Active)
				return;

			if (String.IsNullOrEmpty(InputTextBox.Text) && !AllowNull)
			{
				using (_changeLock.Activate())
				{
					InputTextBox.Text = ValueNumber.ToString();
					IsValidNumber = true;
					InputTextBox.SelectAll();
				}
			}
			if (InputTextBox.Text == "-")
			{
				using (_changeLock.Activate())
				{
					InputTextBox.Text = ValueNumber.ToString();
					IsValidNumber = true;
					InputTextBox.SelectAll();
				}
			}
		}

		private void BoundaryChanged(TValue oldValue, TValue newValue)
		{
			if (_changeLock.Active)
				return;
			using (_changeLock.Activate())
			{
				try
				{
					if (_dynamicNumber > Maximum)
					{
						ErrorMessage = "The number is too big the maximum is " + Maximum + ".";
						IsValidNumber = false;
						return;
					}
					if (_dynamicNumber < Minimum)
					{
						ErrorMessage = "The number is too small the minimum is " + Minimum + ".";
						IsValidNumber = false;
						return;
					}
					IsValidNumber = true;
				}
				catch (Exception)
				{
					ErrorMessage = "The number is too small or too big or contains invalid characters.";
					IsValidNumber = false;
				}
			}
		}
	}
}