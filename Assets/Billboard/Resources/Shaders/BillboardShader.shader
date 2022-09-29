Shader "Custom/BillboardShader"
{
	Properties
	{
		_Offset("Offset", Vector) = (0, 0, 0, 0)
		_MainTex("Texture Image", 2D) = "white" {}
		_ScaleX("Scale X", Float) = 1.0
		_ScaleY("Scale Y", Float) = 1.0
		_Cutoff("Alpha Cutoff", Range(0,1)) = 0.0
		[Toggle] _IsMonoColor("Is Mono Color", Float) = 0
		_MonoColor("Mono Color", Color) = (255,0,0,0)
	}

		SubShader
		{
			Tags
			{
				"Queue" = "AlphaTest"
				"RenderType" = "TransparentCutout"
				"IgnoreProjector" = "True"
			//"DisableBatching" = "True" //In order to avoid "flickering"
			}

			Pass
			{
				Cull Back

				CGPROGRAM
				#pragma vertex vert
				#pragma fragment frag
				#pragma multi_compile_instancing
				#pragma target 3.0
				#include "UnityCG.cginc"

		uniform sampler2D _MainTex;
		//uniform float4 _Offset;
		//uniform float _ScaleX;
		//uniform float _ScaleY;
		//uniform float _Cutoff;
		//uniform fixed4 _MonoColor;
		//uniform fixed _IsMonoColor;

		struct vertexInput
		{
			float4 vertex : POSITION;
			float4 tex : TEXCOORD0;
			UNITY_VERTEX_INPUT_INSTANCE_ID
		};

		struct vertexOutput
		{
			float4 pos : SV_POSITION;
			float4 tex : TEXCOORD0;
			UNITY_VERTEX_INPUT_INSTANCE_ID
		};

		UNITY_INSTANCING_BUFFER_START(Props)
			UNITY_DEFINE_INSTANCED_PROP(float4, _Offset)
			UNITY_DEFINE_INSTANCED_PROP(float, _ScaleX)
			UNITY_DEFINE_INSTANCED_PROP(float, _ScaleY)
			UNITY_DEFINE_INSTANCED_PROP(float, _Cutoff)
			UNITY_DEFINE_INSTANCED_PROP(fixed4, _MonoColor)
			UNITY_DEFINE_INSTANCED_PROP(fixed, _IsMonoColor)
		UNITY_INSTANCING_BUFFER_END(Props)

			vertexOutput vert(vertexInput input)
			{
				vertexOutput output;

				UNITY_SETUP_INSTANCE_ID(input);
				UNITY_TRANSFER_INSTANCE_ID(input, output);

				output.pos = mul(UNITY_MATRIX_P, mul(UNITY_MATRIX_MV, float4(0.0, 0.0, 0.0, 1.0)) +
					float4(input.vertex.x, input.vertex.y, 0.0, 0.0) * float4(UNITY_ACCESS_INSTANCED_PROP(Props, _ScaleX), UNITY_ACCESS_INSTANCED_PROP(Props, _ScaleY), 1.0, 1.0) +
					UNITY_ACCESS_INSTANCED_PROP(Props, _Offset));
				output.tex = input.tex;
				return output;
			}

			float4 frag(vertexOutput input) : COLOR
			{
				UNITY_SETUP_INSTANCE_ID(input);

				float4 texColor = tex2D(_MainTex, float2(input.tex.xy));
				if (texColor.a < UNITY_ACCESS_INSTANCED_PROP(Props, _Cutoff))
				{
					discard;
				}

				if (UNITY_ACCESS_INSTANCED_PROP(Props, _IsMonoColor) != 0)
				{
					texColor = UNITY_ACCESS_INSTANCED_PROP(Props,  _MonoColor);
				}

				return texColor;
			}

			ENDCG
		}
		}
}