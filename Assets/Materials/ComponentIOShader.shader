Shader "Unlit/ComponentIOShader" {
	Properties{
		_BgColor ("Background Color", Color) = (1, 1, 1, 1)
		_FgColor ("Foreground Color", Color) = (0, 0, 0, 1)
		_Radius ("Radius", range (0, 0.5)) = 0.2
		_Padding ("Circle AA radius", range (0, 0.5)) = 0.1
	}
	SubShader {
		Tags { "RenderType" = "Opaque" }
		LOD 100

		Pass {
			CGPROGRAM

			#pragma vertex vert
			#pragma fragment frag

			#include "UnityCG.cginc"

			half4 _BgColor;
			half4 _FgColor;
			float _Radius;
			float _Padding;

			struct appdata {
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f {
				float4 vertex : SV_POSITION;
				float2 uv : TEXCOORD0;
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;
			
			v2f vert (appdata v) {
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv;
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target {
				float radCenter = distance (float2(0.5, 0.5), i.uv);
				float c = saturate((radCenter - _Radius) / 2.0 / _Padding + 0.5);
			
				return lerp (_FgColor, _BgColor, c);
			}
			ENDCG
		}
	}
}
