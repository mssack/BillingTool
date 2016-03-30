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
	public class ByteEditor : NumberEditor<Byte?>
	{
		static ByteEditor()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof (ByteEditor), new FrameworkPropertyMetadata(typeof (ByteEditor)));
		}
	}
}