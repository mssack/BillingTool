﻿// Copyright (c) 2014, 2015 All Right Reserved Christian Sack
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
	public class Int32Editor : NumberEditor<Int32?>
	{
		static Int32Editor()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof (Int32Editor), new FrameworkPropertyMetadata(typeof (Int32Editor)));
		}
	}
}