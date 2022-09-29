Shader "Custom/MonoColorSurface"
{
	Properties
	{
		_Color("Color", Color) = (1,1,1,1)
	}
		SubShader
	{
		Tags
		{
			"RenderType" = "Transparent"
			"IgnoreProjector" = "True"
		}
		Cull Back

		CGPROGRAM

		#pragma surface surf Standard

		#pragma target 3.0

		struct Input
		{
			float2 uv_MainTex;
		};

		UNITY_INSTANCING_BUFFER_START(Props)
			UNITY_DEFINE_INSTANCED_PROP(fixed4, _Color)
		UNITY_INSTANCING_BUFFER_END(Props)

		void surf(Input IN, inout SurfaceOutputStandard o)
		{
			o.Alpha = UNITY_ACCESS_INSTANCED_PROP(Props, _Color).a;
			o.Albedo = UNITY_ACCESS_INSTANCED_PROP(Props, _Color).rgb;
			o.Emission = UNITY_ACCESS_INSTANCED_PROP(Props, _Color).rgb;
		}
		ENDCG
	}
		FallBack "Diffuse"
}