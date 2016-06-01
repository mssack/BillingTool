// Copyright (c) 2014, 2015 All Right Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2015-07-24</date>

using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Data;
using CsWpfBase.Themes.Controls.Containers;
using CsWpfBase.Utilitys;






namespace CsWpfBase.Global.app.install.agreement
{
	/// <summary>Interaction logic for AgreementWindow.xaml</summary>
	public partial class CsgAgreementWindow : CsWindow
	{
		#region DP Keys
		/// <summary></summary>
		public static readonly DependencyProperty AgreementProperty = DependencyProperty.Register("Agreement", typeof (string), typeof (CsgAgreementWindow), new FrameworkPropertyMetadata {DefaultValue = default(string), DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged});
		/// <summary></summary>
		public static readonly DependencyProperty PrivacyAgreementProperty = DependencyProperty.Register("PrivacyAgreement", typeof (string), typeof (CsgAgreementWindow), new FrameworkPropertyMetadata {DefaultValue = default(string), DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged});
		/// <summary></summary>
		public static readonly DependencyProperty ApplicationNameProperty = DependencyProperty.Register("ApplicationName", typeof (string), typeof (CsgAgreementWindow), new FrameworkPropertyMetadata {DefaultValue = default(string), DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged});
		#endregion


		private readonly ProcessLock _validCloseLock = new ProcessLock();

		/// <summary>ctor</summary>
		public CsgAgreementWindow()
		{
			InitializeComponent();
			Closing += OnClosing;
		}

		/// <summary>Gets or sets the application name.</summary>
		public string ApplicationName
		{
			get { return (string) GetValue(ApplicationNameProperty); }
			set { SetValue(ApplicationNameProperty, value); }
		}
		/// <summary>Gets or sets the agreement the window will present.</summary>
		public string Agreement
		{
			get { return (string) GetValue(AgreementProperty); }
			set { SetValue(AgreementProperty, value); }
		}
		/// <summary>Gets or sets the privacy agreement the window will present.</summary>
		public string PrivacyAgreement
		{
			get { return (string) GetValue(PrivacyAgreementProperty); }
			set { SetValue(PrivacyAgreementProperty, value); }
		}

		private void OnClosing(object sender, CancelEventArgs cancelEventArgs)
		{
			if (_validCloseLock.Active)
				return;

			Environment.Exit(0);
		}

		private void DenyClicked(object sender, RoutedEventArgs e)
		{
			Environment.Exit(0);
		}

		private void AcceptClicked(object sender, RoutedEventArgs e)
		{
			_validCloseLock.Activate();
			DialogResult = true;
			Close();
		}
	}
}