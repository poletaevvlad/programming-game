Shader "Unlit/GridShader"{
	Properties{
		_MainTex ("Texture", 2D) = "white" {}
		_BackgroundColor ("Background color", Color) = (1, 1, 1, 1)
		_GridColor ("Grid color", Color) = (0, 0, 0, 1)
	}

	SubShader{
		Tags { "RenderType" = "Opaque" }
		LOD 100

		Pass{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"

			struct vertexInput {
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct fragmentInput {
				float2 uv : TEXCOORD0;
				float4 vertex : SV_POSITION;
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;
			half4 _BackgroundColor;
			half4 _GridColor;
			
			fragmentInput vert (vertexInput v) {
				fragmentInput o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				return o;
			}
			
			fixed4 frag (fragmentInput i) : SV_Target {
				half ratio = tex2D(_MainTex, i.uv).r;
				half4 col = lerp(_BackgroundColor, _GridColor, 1 - ratio);
				return col;
			}

			ENDCG
		}
	}
}
