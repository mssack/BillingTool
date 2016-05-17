// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-05-11</date>

using System;
using System.ComponentModel;
using BillingTool.btScope;
using CsWpfBase.Global;
using CsWpfBase.Themes.Controls.Containers;






namespace BillingTool.Windows
{
	/// <summary>Interaction logic for Window_Options.xaml</summary>
	// ReSharper disable once InconsistentNaming
	public partial class Window_Options : CsWindow
	{
		/// <summary>ctor</summary>
		public Window_Options()
		{
			InitializeComponent();
			CsGlobal.Wpf.Storage.Window.Handle(this, "Window_Options");
			Closing += Window_Options_Closing;
		}


		private void Window_Options_Closing(object sender, CancelEventArgs e)
		{
			Bt.Data.Finalize_Or_Reject_All();
			Bt.Data.SyncChanges();
			Bt.AppOutput.Include_ExitCode(ExitCodes.Options_Saved);
		}
	}
}