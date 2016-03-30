// Copyright (c) 2014, 2015 All Right Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2015-06-11</date>

using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Markup;





namespace CsWpfBase.Themes.AttachedProperties
{
	/// <summary>Usage:
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

		public override object ProvideValue(IServiceProvider serviceProvider)
		{
			var enumValues = Enum.GetValues(EnumType);

			return (
				from object enumValue in enumValues
				select new EnumerationMember
						{
							Value = enumValue,
							Description = GetDescription((Enum) enumValue)
						}).ToArray();
		}

		public static string GetDescription(Enum enumValue)
		{
			var descriptionAttribute = enumValue.GetType()
												.GetField(enumValue.ToString())
												.GetCustomAttributes(typeof (DescriptionAttribute), false)
												.FirstOrDefault() as DescriptionAttribute;


			return descriptionAttribute != null
				? descriptionAttribute.Description
				: enumValue.ToString();
		}





		public class EnumerationMember
		{
			public string Description { get; set; }
			public object Value { get; set; }
		}
	}
}