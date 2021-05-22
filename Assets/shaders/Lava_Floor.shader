// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/Lava_Floor"
{
	Properties
	{
		_MainTex("Sprite texture", 2D) = "white" {}
		_Mask("Mask texture", 2D) = "white" {}
		_MaskBloom("Mask bloom", 2D) = "white" {}
		_Color("Tint", Color) = (1,1,1,1)
		[HDR] _Emission("Emission", Color) = (1,1,1,1)
		_Factor("Factor",Range(1,2))= 1
		_Alphe("Factor",Range(0,1)) = 1
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
				//OUT.vertex = UnityPixelSnap(OUT.vertex); // OPCIONAL
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
				float4 mask = tex2D(_Mask, IN.texcoord);
				mask.rgb *= mask.a;
				float4 maskBloom = tex2D(_MaskBloom, IN.texcoord);
				maskBloom.rgb *= maskBloom.a;

				float4 combinacion = main * mask;
				combinacion.rgb *= combinacion.a;

				float4 combinacion2 = mask * maskBloom;
				float4 combinacion3 = mask * (1-maskBloom);

				float4 combinacionMain = combinacion2 * main;
				//combinacionMain.rgb *= combinacionMain.a;
				float4 combinacionMain2 = combinacion3 * main;
				//combinacionMain2.rgb *= combinacionMain2.a;

				float4 output = float4(0,0,0,main.a);

				
				//output.rgb = lerp(combinacion.rgb,_Emission.rgb, maskBloom.x);
				output.rgb = (combinacionMain.rgb*_Factor+ combinacionMain2.rgb);
				return output;

			}
		ENDCG
		}
	}
}