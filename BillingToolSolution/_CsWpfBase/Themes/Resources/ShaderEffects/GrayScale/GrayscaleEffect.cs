// Copyright (c) 2014, 2015 All Right Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2015-06-11</date>

using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Effects;





namespace CsWpfBase.Themes.Resources.ShaderEffects.GrayScale
{
#pragma warning disable 1591
	/// <summary>A gray scale effect to use in effects on a ui element.</summary>
	public class GrayscaleEffect : ShaderEffect
	{
		#region DependencyProperty Static Keys
		public static readonly DependencyProperty InputProperty = RegisterPixelShaderSamplerProperty("Input", typeof (GrayscaleEffect), 0);
		public static readonly DependencyProperty DesaturationFactorProperty = DependencyProperty.Register("DesaturationFactor", typeof (double), typeof (GrayscaleEffect), new UIPropertyMetadata(0.0, PixelShaderConstantCallback(0), CoerceDesaturationFactor));
		#endregion


		private static readonly PixelShader Shader = new PixelShader {UriSource = new Uri(@"pack://application:,,,/CsWpfBase;component/Themes/Resources/ShaderEffects/GrayScale/GrayscaleEffect.ps", UriKind.RelativeOrAbsolute)};

		/// <summary>ctor</summary>
		public GrayscaleEffect()
		{
			PixelShader = Shader;

			UpdateShaderValue(InputProperty);
			UpdateShaderValue(DesaturationFactorProperty);
		}
		/// <summary>The brush to modify.</summary>
		public Brush Input
		{
			get { return (Brush) GetValue(InputProperty); }
			set { SetValue(InputProperty, value); }
		}
		/// <summary>Grayscale</summary>
		public double DesaturationFactor
		{
			get { return (double) GetValue(DesaturationFactorProperty); }
			set { SetValue(DesaturationFactorProperty, value); }
		}

		private static object CoerceDesaturationFactor(DependencyObject d, object value)
		{
			var effect = (GrayscaleEffect) d;
			var newFactor = (double) value;

			if (newFactor < 0.0 || newFactor > 1.0)
			{
				return effect.DesaturationFactor;
			}

			return newFactor;
		}
	}
}