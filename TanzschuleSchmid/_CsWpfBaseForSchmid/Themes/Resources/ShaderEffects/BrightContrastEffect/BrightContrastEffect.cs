// Copyright (c) 2014, 2015 All Right Reserved Christian Sack
// <author>Christian Sack</author>
// <email>christian@sack.at</email>
// <website>christian.sack.at</website>
// <date>2015-07-24</date>

using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Effects;






//Use following line to compile shader:
//"C:\Program Files (x86)\Microsoft DirectX SDK (June 2010)\Utilities\bin\x86\fxc.exe" /T ps_2_0 /E main /Fo  "$(ProjectDir)Themes\Resources\ShaderEffects\BrightContrastEffect\bricon.ps" "$(ProjectDir)Themes\Resources\ShaderEffects\BrightContrastEffect\bricon.fx"

namespace CsWpfBase.Themes.Resources.ShaderEffects.BrightContrastEffect
{
#pragma warning disable 1591
	/// <summary>Effect for adjusting the brightness or the contrast.</summary>
	public class BrightContrastEffect : ShaderEffect
	{
		#region DP Keys
		public static readonly DependencyProperty InputProperty = RegisterPixelShaderSamplerProperty("Input", typeof (BrightContrastEffect), 0);
		public static readonly DependencyProperty BrightnessProperty = DependencyProperty.Register("Brightness", typeof (double), typeof (BrightContrastEffect), new UIPropertyMetadata(0.0, PixelShaderConstantCallback(0)));
		public static readonly DependencyProperty ContrastProperty = DependencyProperty.Register("Contrast", typeof (double), typeof (BrightContrastEffect), new UIPropertyMetadata(0.0, PixelShaderConstantCallback(1)));
		#endregion


		private static readonly PixelShader MShader = new PixelShader {UriSource = new Uri(@"pack://application:,,,/CsWpfBase;component/Themes/Resources/ShaderEffects/BrightContrastEffect/bricon.ps")};

		/// <summary>ctor</summary>
		public BrightContrastEffect()
		{
			PixelShader = MShader;
			UpdateShaderValue(InputProperty);
			UpdateShaderValue(BrightnessProperty);
			UpdateShaderValue(ContrastProperty);
		}

		/// <summary>The brush to adjust.</summary>
		public Brush Input
		{
			get { return (Brush) GetValue(InputProperty); }
			set { SetValue(InputProperty, value); }
		}
		/// <summary>Adjust the brightness with this property.</summary>
		public double Brightness
		{
			get { return (double) GetValue(BrightnessProperty); }
			set { SetValue(BrightnessProperty, value); }
		}
		/// <summary>Adjust the contrast with this property.</summary>
		public double Contrast
		{
			get { return (double) GetValue(ContrastProperty); }
			set { SetValue(ContrastProperty, value); }
		}
	}
}