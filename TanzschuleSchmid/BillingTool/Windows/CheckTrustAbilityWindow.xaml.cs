// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-04-01</date>

using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Data;
using CsWpfBase.Themes.Controls.Containers;






namespace BillingTool.Windows
{
	/// <summary>Interaction logic for CheckTrustAbilityWindow.xaml</summary>
	public partial class CheckTrustAbilityWindow : CsWindow
	{
		private string _verificationAnswerSolution;

		/// <summary>ctor</summary>
		public CheckTrustAbilityWindow(string title, string text)
		{
			InitializeComponent();
			Title = title;
			Text = text;
			Topmost = true;

			GetVerificationPropertys();
		}

		/// <summary>The message text of the window.</summary>
		public string Text
		{
			get { return (string) GetValue(TextProperty); }
			set { SetValue(TextProperty, value); }
		}

		/// <summary>Returns true if the verification code is correct.</summary>
		public bool IsValid
		{
			get { return (bool) GetValue(IsValidProperty); }
			set { SetValue(IsValidProperty, value); }
		}

		/// <summary>the verification question.</summary>
		public string VerificationQuestion
		{
			get { return (string) GetValue(VerificationQuestionProperty); }
			set { SetValue(VerificationQuestionProperty, value); }
		}

		/// <summary>the correct verification answer.</summary>
		public string VerificationAnswer
		{
			get { return (string) GetValue(VerificationAnswerProperty); }
			set { SetValue(VerificationAnswerProperty, value); }
		}

		/// <summary>True if the user entered the correct verification code and pressed accept.</summary>
		public bool HasBeenValidated { get; private set; }



		private void GetVerificationPropertys()
		{
			var regex = new Regex("\\b[a-zA-Z]+\\b", RegexOptions.Compiled);
			var matches = regex.Matches(Text);

			var limitedCount = 4;
			if (matches.Count < limitedCount)
				limitedCount = matches.Count;

			if (limitedCount == 0)
				throw new InvalidOperationException("There is no text which is not possible.");


			var rand = new Random();
			var wordIndex = rand.Next(1, limitedCount + 1);

			_verificationAnswerSolution = matches[matches.Count - wordIndex].Value;
			VerificationAnswer = _verificationAnswerSolution.Substring(0, 1);

			var pre = "Geben Sie aus der obigen Nachricht";
			if (wordIndex == 1)
				VerificationQuestion = $"{pre} das letzte Wort an.";
			else if (wordIndex == 2)
				VerificationQuestion = $"{pre} das vorletzte Wort an.";
			else if (wordIndex == 3)
				VerificationQuestion = $"{pre} das dritte Wort von hinten an.";
			else if (wordIndex == 4)
				VerificationQuestion = $"{pre} das vierte Wort von hinten an.";
			IsValid = false;
		}

		private void VerificationAnswerChanged()
		{
			IsValid = VerificationAnswer!=null &&_verificationAnswerSolution != null && string.Equals(VerificationAnswer.ToLower().Trim(), _verificationAnswerSolution.ToLower());
		}

		private void AbbrechenClicked(object sender, RoutedEventArgs e)
		{
			HasBeenValidated = false;
			Close();
		}

		private void FortfahrenClicked(object sender, RoutedEventArgs e)
		{
			HasBeenValidated = true;
			Close();
		}




#pragma warning disable 1591
		public static readonly DependencyProperty TextProperty = DependencyProperty.Register("Text", typeof (string), typeof (CheckTrustAbilityWindow), new FrameworkPropertyMetadata {DefaultValue = default(string), BindsTwoWayByDefault = true, DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged});
		public static readonly DependencyProperty IsValidProperty = DependencyProperty.Register("IsValid", typeof (bool), typeof (CheckTrustAbilityWindow), new FrameworkPropertyMetadata {DefaultValue = default(bool), DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged});
		public static readonly DependencyProperty VerificationAnswerProperty = DependencyProperty.Register("VerificationAnswer", typeof (string), typeof (CheckTrustAbilityWindow), new FrameworkPropertyMetadata {DefaultValue = default(string), DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged, PropertyChangedCallback = (o, args) => { ((CheckTrustAbilityWindow)o).VerificationAnswerChanged(); } });
		public static readonly DependencyProperty VerificationQuestionProperty = DependencyProperty.Register("VerificationQuestion", typeof (string), typeof (CheckTrustAbilityWindow), new FrameworkPropertyMetadata {DefaultValue = default(string),  DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged});
#pragma warning restore 1591
	}
}