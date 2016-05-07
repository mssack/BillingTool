// Copyright (c) 2016 All rights reserved Christian Sack, Michael Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2016-04-19</date>

using System;
using System.Linq;
using System.Windows.Markup;
using CsWpfBase.Ev.Public.Extensions;






namespace CsWpfBase.Themes.AttachedProperties
{
	/// <summary>
	///     Usage:
	///     <ComboBox SelectedValue="{Binding Path=AutoReorder}" SelectedValuePath="Value" DisplayMemberPath="Description" ItemsSource="{Binding Source={EnumerationExtension {x:Type local:Ipv4LanScannerView+Option+AutoReorderingTypes}}}"></ComboBox>
	/// </summary>
#pragma warning disable 1591
	public class EnumerationExtension : MarkupExtension
	{
		private Type _enumType;


		public EnumerationExtension(Type enumType)
		{
			if (enumType == null)
				throw new ArgumentNullException("enumType");

			EnumType = enumType;
		}


		#region Overrides/Interfaces
		public override object ProvideValue(IServiceProvider serviceProvider)
		{
			var enumValues = Enum.GetValues(EnumType);
			return enumValues.OfType<Enum>().Where(x => !IgnoredEnums.Contains(x)).Select(x => new EnumerationMember() {Value = x, Name = x.GetName(), Description = x.GetDescription()}).ToArray();
		}
		#endregion


		public Type EnumType
		{
			get { return _enumType; }
			private set
			{
				if (_enumType == value)
					return;

				var enumType = Nullable.GetUnderlyingType(value) ?? value;

				if (enumType.IsEnum == false)
					throw new ArgumentException("Type must be an Enum.");

				_enumType = value;
			}
		}
		public Enum[] IgnoredEnums { get; set; }




		public class EnumerationMember
		{
			public string Description { get; set; }
			public string Name { get; set; }
			public object Value { get; set; }
		}
	}
}