// Copyright (c) 2014, 2015 All Right Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2015-07-24</date>

using System;
using System.Windows;
using System.Windows.Controls;
using CsWpfBase.Themes.Controls._shared;






namespace CsWpfBase.Themes.Controls.Basics
{
	/// <summary>A default <see cref="ComboBox" /> with a new <see cref="Button" /> inside the context menu of the combo box.</summary>
	public class ComboBoxWithNewButton : ComboBox
	{
		private Button _newButton;

		static ComboBoxWithNewButton()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof (ComboBoxWithNewButton), new FrameworkPropertyMetadata(typeof (ComboBoxWithNewButton)));
		}


		#region Overrides/Interfaces
		/// <summary>Called when <see cref="M:System.Windows.FrameworkElement.ApplyTemplate" /> is called.</summary>
		public override void OnApplyTemplate()
		{
			base.OnApplyTemplate();
			if (Template != null)
			{
				NewButton = Template.GetPart<Button>("PART_NewButton", this);
			}
		}
		#endregion


		private Button NewButton
		{
			get { return _newButton; }
			set
			{
				if (_newButton != null)
				{
					_newButton.Click -= NewButtonOnClick;
				}
				_newButton = value;
				if (_newButton != null)
				{
					_newButton.Click += NewButtonOnClick;
				}
			}
		}
		/// <summary>Occurs when the user presses the new button inside the combobox.</summary>
		public event Action<ComboBoxWithNewButton> NewClicked;

		private void NewButtonOnClick(object sender, RoutedEventArgs routedEventArgs)
		{
			NewClicked?.Invoke(this);
		}
	}
}