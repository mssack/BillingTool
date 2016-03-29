sampler2D input : register(s0);
float brightness : register(c0);
float contrast : register(c1);

float4 main(float2 uv : TEXCOORD) : COLOR
{
    float4 color = tex2D(input, uv); 
    float4 result;

	result.r = ((color.r / color.a) + brightness) * (1.0 + contrast) / 1.0;
	result.g = ((color.g / color.a) + brightness) * (1.0 + contrast) / 1.0;
	result.b = ((color.b / color.a) + brightness) * (1.0 + contrast) / 1.0;
	result.a = color.a;
	result.rgb *= color.a;
	result = result;
    
    return result;
}