Shader "Unlit/GridShader"{
	Properties{
		_MainTex ("Texture", 2D) = "white" {}
		_BackgroundColor ("Background color", Color) = (1, 1, 1, 1)
		_GridColor ("Grid color", Color) = (0, 0, 0, 1)

		_ShowAnimationTime ("Show animation time", range(0, 1)) = 0
		_ObjectXScale("X scale", float) = 1
		_ObjectYScale("Y scale", float) = 1
		_AnimationX("Animation X", range(0, 1)) = 0.5
		_AnimationY("Animation Y", range(0, 1)) = 0.5
		_MinVisibility("Min. visibility", range(0, 1)) = 0
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
				float2 coord : TEXCOORD1;
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;
			half4 _BackgroundColor;
			half4 _GridColor;
			float _ShowAnimationTime;
			float _ObjectXScale, _ObjectYScale;
			float _AnimationX, _AnimationY;
			float _MinVisibility;
			
			fragmentInput vert (vertexInput v) {
				fragmentInput o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				o.coord = float2(1 - v.uv.x * _ObjectXScale, 1 - (1 - v.uv.y) * _ObjectYScale);
				return o;
			}
			
			fixed4 frag (fragmentInput i) : SV_Target {
				half textureFactor = 1 - tex2D(_MainTex, i.uv).r;
				float animationFactor = 3 * pow((1 - length(float2(i.coord.x - _AnimationX, i.coord.y - _AnimationY))), 3) * _ShowAnimationTime;
				float visibility = lerp(_MinVisibility, 1, clamp(animationFactor, 0, 1)) * textureFactor;
				half4 col = lerp(_BackgroundColor, _GridColor, visibility);
				return col;
			}

			ENDCG
		}
	}
}
