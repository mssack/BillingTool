// Copyright (c) 2014, 2015 All Right Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2015-06-11</date>

using System;
using System.Windows;
using CsWpfBase.Themes.Controls.Editors.Base;





namespace CsWpfBase.Themes.Controls.Editors.Number
{
#pragma warning disable 1591
	public class UInt16Editor : NumberEditor<UInt16?>
	{
		static UInt16Editor()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof (UInt16Editor), new FrameworkPropertyMetadata(typeof (UInt16Editor)));
		}
	}
}