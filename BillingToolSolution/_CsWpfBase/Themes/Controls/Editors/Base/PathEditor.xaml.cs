// Copyright (c) 2014, 2015 All Right Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2015-06-11</date>

using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Interop;
using CsWpfBase.Ev.Objects;
using CsWpfBase.Ev.Public.Extensions;
using CsWpfBase.Global;
using CsWpfBase.Utilitys;
using IWin32Window = System.Windows.Forms.IWin32Window;





namespace CsWpfBase.Themes.Controls.Editors.Base
{
#pragma warning disable 1591
	/// <summary>The <see cref="PathEditor" /> is the style based control for the <see cref="FPathEditor" /> and the
	///     <see cref="DPathEditor" />. This type is styled.</summary>
	public class PathEditor : EditorBase<string>
	{
		#region DependencyProperty Static Keys
		private static readonly DependencyPropertyKey IsValidPathPropertyKey = DependencyProperty.RegisterReadOnly("IsValidPath", typeof (bool), typeof (PathEditor), new FrameworkPropertyMetadata {DefaultValue = default(bool), DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged, Inherits = true});
		public static readonly DependencyProperty IsValidPathProperty = IsValidPathPropertyKey.DependencyProperty;
		public static readonly DependencyProperty AllowNonExistingPathProperty = DependencyProperty.Register("AllowNonExistingPath", typeof (bool), typeof (PathEditor), new FrameworkPropertyMetadata {DefaultValue = default(bool), DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged, PropertyChangedCallback = (o, args) => ((PathEditor) o).AllowNonExistingPathChanged((bool) args.OldValue, (bool) args.NewValue)});
		public static readonly DependencyProperty AllowNullProperty = DependencyProperty.Register("AllowNull", typeof (bool), typeof (PathEditor), new FrameworkPropertyMetadata {DefaultValue = default(bool), DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged, PropertyChangedCallback = (o, args) => ((PathEditor) o).AllowNullChanged((bool) args.OldValue, (bool) args.NewValue)});
		public static readonly DependencyProperty NullTextProperty = DependencyProperty.Register("NullText", typeof (string), typeof (PathEditor), new FrameworkPropertyMetadata {DefaultValue = default(string), DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged});
		public static readonly DependencyProperty InitialPathProperty = DependencyProperty.Register("InitialPath", typeof (string), typeof (PathEditor), new FrameworkPropertyMetadata {DefaultValue = default(string), DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged});
		#endregion


		private TextBox _inputTextBox;
		protected ICommand _openDialogCommand;


		#region Overrides
		public override void OnApplyTemplate()
		{
			base.OnApplyTemplate();
			if (Template != null)
				InputTextBox = Template.FindName("PART_InputTextBox", this) as TextBox;
		}
		#endregion


		public bool IsValidPath
		{
			get { return (bool) GetValue(IsValidPathProperty); }
			protected set { SetValue(IsValidPathPropertyKey, value); }
		}
		public bool AllowNonExistingPath
		{
			get { return (bool) GetValue(AllowNonExistingPathProperty); }
			set { SetValue(AllowNonExistingPathProperty, value); }
		}
		public bool AllowNull
		{
			get { return (bool) GetValue(AllowNullProperty); }
			set { SetValue(AllowNullProperty, value); }
		}
		public string NullText
		{
			get { return (string) GetValue(NullTextProperty); }
			set { SetValue(NullTextProperty, value); }
		}
		public string InitialPath
		{
			get { return (string) GetValue(InitialPathProperty); }
			set { SetValue(InitialPathProperty, value); }
		}
		public ICommand OpenDialogCommand
		{
			get { return _openDialogCommand; }
		}

		private TextBox InputTextBox
		{
			get { return _inputTextBox; }
			set
			{
				if (_inputTextBox != null)
					_inputTextBox.PreviewKeyDown -= InputTextBoxOnPreviewKeyDown;
				_inputTextBox = value;
				if (_inputTextBox != null)
					_inputTextBox.PreviewKeyDown += InputTextBoxOnPreviewKeyDown;
			}
		}
		private void InputTextBoxOnPreviewKeyDown(object sender, KeyEventArgs keyEventArgs)
		{
			if (keyEventArgs.Key == Key.Enter)
			{
				var expression = ((TextBox) sender).GetBindingExpression(TextBox.TextProperty);
				if (expression != null)
					expression.UpdateSource();
			}
		}


		#region Virtuals
		protected virtual void AllowNonExistingPathChanged(bool oldValue, bool newValue)
		{
		}
		protected virtual void AllowNullChanged(bool oldValue, bool newValue)
		{
		}
		#endregion
	}





	/// <summary>The generic logic for the editor</summary>
	public abstract class PathEditor<TPathType> : PathEditor
		where TPathType : class
	{
		#region DependencyProperty Static Keys
		public static readonly DependencyProperty ValuePathProperty = DependencyProperty.Register("ValuePath", typeof (TPathType), typeof (PathEditor<TPathType>), new FrameworkPropertyMetadata {DefaultValue = default(TPathType), BindsTwoWayByDefault = true, DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged, PropertyChangedCallback = (o, args) => ((PathEditor<TPathType>) o).ValuePathChanged((TPathType) args.OldValue, (TPathType) args.NewValue)});
		#endregion


		private readonly ProcessLock _changeLock = new ProcessLock();
		public PathEditor()
		{
			_openDialogCommand = new RelayCommand(OpenDialog);
			PreviewKeyDown += (sender, args) =>
			{
				if (args.Key == Key.F && (Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl)))
				{
					_openDialogCommand.Execute(null);
					args.Handled = true;
				}
			};
		}


		#region Abstracts
		public abstract string Convert(TPathType path);
		public abstract TPathType Convert(string path);
		public abstract bool Exists(TPathType path);
		public abstract string OpenDialog(IWin32Window owner, string initialPath, out bool canceled);
		#endregion


		#region Overrides
		protected override void ValueChanged(string oldValue, string newValue)
		{
			if (_changeLock.Active)
				return;
			using (_changeLock.Activate())
			{
				ValuePath = String.IsNullOrEmpty(newValue) ? null : Convert(newValue);
				CheckValidState();
			}
		}
		protected override void AllowNullChanged(bool oldValue, bool newValue)
		{
			CheckValidState();
		}
		protected override void AllowNonExistingPathChanged(bool oldValue, bool newValue)
		{
			CheckValidState();
		}
		#endregion


		public TPathType ValuePath
		{
			get { return (TPathType) GetValue(ValuePathProperty); }
			set { SetValue(ValuePathProperty, value); }
		}
		private void OpenDialog()
		{
			bool canceled;
			var path = OpenDialog(Win.Hwnd(Window.GetWindow(this)), InitialPath ?? Value, out canceled);
			if (canceled)
				return;

			var isNullOrEmpty = String.IsNullOrEmpty(path);
			if (isNullOrEmpty && AllowNull)
			{
				ValuePath = null;
				return;
			}
			if (isNullOrEmpty && !AllowNull)
			{
				CsGlobal.Message.Push("Es muss ein Pfad angegeben werden.");
				return;
			}


			var valuePath = Convert(path);
			if (!Exists(valuePath) && AllowNonExistingPath == false)
			{
				CsGlobal.Message.Push("Der Pfad '" + path + "' existiert nicht.");
				return;
			}
			ValuePath = valuePath;
		}
		private void ValuePathChanged(TPathType oldValue, TPathType newValue)
		{
			if (_changeLock.Active)
				return;
			using (_changeLock.Activate())
			{
				Value = newValue == null ? null : Convert(newValue);
				CheckValidState();
			}
		}
		private void CheckValidState()
		{
			var isNullOrEmpty = String.IsNullOrEmpty(Value);
			if (isNullOrEmpty && AllowNull)
			{
				IsValidPath = true;
			}
			else if (isNullOrEmpty && !AllowNull)
			{
				IsValidPath = false;
			}
			else
			{
				var exists = Exists(ValuePath);
				if (!exists && AllowNonExistingPath == false)
					IsValidPath = false;
				else
					IsValidPath = true;
			}
		}





		private static class Win
		{
			public static IWin32Window Hwnd(Window w)
			{
				return new Impl(new WindowInteropHelper(w).Handle);
			}





			private class Impl : IWin32Window
			{
				private readonly IntPtr _handle;
				public Impl(IntPtr handle)
				{
					_handle = handle;
				}
				public IntPtr Handle
				{
					get { return _handle; }
				}
			}
		}
	}
}