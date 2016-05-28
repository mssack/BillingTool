// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-05-28</date>

using System;
using System.Linq;
using System.Windows;
using BillingDataAccess.sqlcedatabases.billingdatabase.rows;
using BillingTool.btScope.configuration.control;
using BillingTool._SharedEnumerations;
using CsWpfBase.Ev.Objects;
using CsWpfBase.Ev.Public.Extensions;






namespace BillingTool.btScope.configuration
{
	/// <summary>Do not use this directly instead use <see cref="Bt" /> class to access instance of this.</summary>
	public sealed class Control : Base
	{
		private static Control _instance;
		private static readonly object SingletonLock = new object();

		/// <summary>Returns the singleton instance</summary>
		internal static Control I
		{
			get
			{
				if (_instance != null)
					return _instance; //Advanced first check to improve performance (no lock needed).
				lock (SingletonLock)
				{
					return _instance ?? (_instance = new Control());
				}
			}
		}

		private string _current;

		private Control()
		{
		}

		/// <summary>Gets the currently active command.</summary>
		public string Current
		{
			get { return _current; }
			private set { SetProperty(ref _current, value); }
		}

		/// <summary>
		///     The <see cref="General" /> sub configuration holds all uncategorize able configurations like <see cref="StartupModes" />, the database location
		///     and other general configurations.
		/// </summary>
		public Control_General General => Control_General.I;


		/// <summary>The <see cref="NewBelegData" /> sub configuration holds all configurable properties for <see cref="BelegData" />'s like default values.</summary>
		public Control_NewBelegData NewBelegData => Control_NewBelegData.I;


		/// <summary>
		///     Interprets command line params passed to the application. Use this method on <see cref="Application.Startup" /> to ensure that the correct
		///     configuration is loaded.
		/// </summary>
		public void Interpret(string[] startParams)
		{
			Current = startParams.Join(" ");
			var concanatedParams = Current.Replace("//", "#######ALÖÄSÖ######").Split("/").Select(x => x.Trim().Replace("#######ALÖÄSÖ######", "//")).Where(x => !string.IsNullOrEmpty(x)).ToList();
			General.Interpret(concanatedParams);
			NewBelegData.Interpret(concanatedParams);
		}
	}



}