// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/SpriteShaderBase"
{
	Properties
	{
		_MainTex("Sprite texture", 2D) = "white" {}
		_Mask("Mask texture", 2D) = "white" {}
		_MaskBloom("Mask bloom", 2D) = "white" {}
		_Color("Tint", Color) = (1,1,1,1)
		[HDR] _Emission("Emission", Color) = (1,1,1,1)
		_Factor("Factor",Range(0,1))= 0
	}

	SubShader
	{
		Tags
		{
			"Queue" = "Transparent"
			"IgnoreProjector" = "True"
			"RenderType" = "Transparent"
			"PreviewType" = "Plane"
			"CanUseSpriteAtlas" = "True"
		}

		Cull Off
		Lighting Off
		ZWrite Off
		Fog { Mode Off }
		Blend One OneMinusSrcAlpha

		Pass
		{
		CGPROGRAM
			#pragma vertex vert // Qué método va a operar los vertices
			#pragma fragment frag // Qué método va a operar los píxeles
			#include "UnityCG.cginc" // Esto tiene estructuras definidas e información del modelo

			struct appdata_t
			{
				float4 vertex   : POSITION; // v.vertex.xyz
				float4 color    : COLOR; // Color de cada vertice
				float2 texcoord : TEXCOORD0; // UV
			};

			struct v2f
			{
				float4 vertex   : SV_POSITION;
				float4 color : COLOR;
				half2 texcoord  : TEXCOORD0; // UV
			};

				
			float4 _Color;
			float4 _Emission;
			half3 _Position;
			float _Factor;

			v2f vert(appdata_t IN)
			{
				v2f OUT;
				OUT.vertex = UnityObjectToClipPos(IN.vertex);
				OUT.texcoord = IN.texcoord;
				OUT.color = IN.color * _Color;
				OUT.vertex = UnityPixelSnap(OUT.vertex); // OPCIONAL
				return OUT;
			}

			sampler2D _Mask;
			sampler2D _MaskBloom;
			sampler2D _MainTex;
			float4 _MainTex_TexelSize;

			float4 frag(v2f IN) : SV_Target
			{
				float4 main = tex2D(_MainTex, IN.texcoord) * IN.color;
				main.rgb *= main.a;
				float4 output = float4(0,0,0,main.a);

				float gray = (main.r + main.g + main.b)/3.0;
				output.rgb = lerp(main.rgb,gray.xxx,_Factor);

				return output;

			}
		ENDCG
		}
	}
}