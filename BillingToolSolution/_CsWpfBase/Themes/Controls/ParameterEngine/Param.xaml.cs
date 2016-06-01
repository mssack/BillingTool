// Copyright (c) 2014, 2015 All Right Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2015-06-11</date>

using System;
using System.Windows;
using CsWpfBase.Themes.Controls.ParameterEngine.Base;





namespace CsWpfBase.Themes.Controls.ParameterEngine
{
#pragma warning disable 1591
	public class Param : ParameterEngineBase
	{
		static Param()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof (Param), new FrameworkPropertyMetadata(typeof (Param)));
		}
	}
}