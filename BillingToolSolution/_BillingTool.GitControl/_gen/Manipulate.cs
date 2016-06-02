// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-06-01</date>

using System;
using System.Text.RegularExpressions;






namespace BillingToolGitControl._gen
{
	public static class Manipulate
	{
		public static string Enumeration(string filetext)
		{
			filetext = filetext.Replace("\r\n", "\n");
			filetext = Regex.Replace(filetext, "<see\\s+cref=\"(.*?)\"\\s*/>", match => match.Groups[1].Value); // remove command links
			filetext = Regex.Replace(filetext, "using (?!System).*", match => ""); // remove using's
			filetext = Regex.Replace(filetext, "namespace .*", match => "namespace BillingTool.enumerations"); // adjust namespace
			filetext = Regex.Replace(filetext, "(\\/\\/.*?(?:$|\n))", match => match.Groups[1].Value.StartsWith("///") ? match.Groups[1].Value : ""); // remove comments
			filetext = Regex.Replace(filetext, "\\[\\s*Description.*?\\]\\s*", match => ""); // remove Attributes
			filetext = filetext.Trim('\n');
			filetext = filetext.Replace("\n", "\r\n");
			return filetext;
		}

		public static string Class(string text)
		{
			text = text.Replace("\r\n", "\n");
			text = Regex.Replace(text, "<see\\s+cref=\"(.*?)\"\\s*/>", match => match.Groups[1].Value); // remove command links
			text = Regex.Replace(text, "using (BillingTool)(?!\\.enumerations).*", match => ""); // remove using's
			text = Regex.Replace(text, "namespace .*", match => "namespace BillingTool"); // adjust namespace
			text = Regex.Replace(text, "(\\/\\/.*?(?:$|\n))", match => match.Groups[1].Value.StartsWith("///") ? match.Groups[1].Value : ""); // remove comments
			text = Regex.Replace(text, "{nameof\\((.*?)\\)}", match => match.Groups[1].Value); // remove {nameof()}

			var regex = new Regex("\\$\"(.*?){(.*?)}");// remove {expression} and make ..." + expression + "...   instead
			bool matched = false;
			do
			{
				matched = false;
				text = regex.Replace(text, match =>
				{
					matched = true;
					return $"$\"{match.Groups[1].Value}\" + {match.Groups[2].Value} + \"";
				});
			} while (matched);

			text = Regex.Replace(text, "\\$\"", "\""); // remove {expression} and make ..." + expression + "...   instead
			text = text.Trim('\n');
			text = text.Replace("\n", "\r\n");
			return text;
		}

		public static string BelegDataTypes(string text)
		{
			text = Enumeration(text);
			text = text.Replace("\r\n", "\n");
			text = Regex.Replace(text, ".*\n.*?(?:(?:= 0,)|(?:= 10,)|(?:= 100,)|(?:= 1001,)|(?:= 1002,)|(?:= 1003,))\n", match => ""); // remove invalid members
			text = text.Replace("\n", "\r\n");
			return text;
		}
	}
}