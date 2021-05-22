Shader "Custom/Proyector_Ice" 
{
	Properties{
		[HDR] _Emission("Emission", Color) = (1,1,1,1)
		_ShadowTex("Cookie", 2D) = "" {}
		_FalloffTex("FallOff", 2D) = "" {}
		_MaskTextura("Main Textura", 2D) = "white" {}
		_MaskBloom("Mask Line", 2D) = "white" {}
		_Factor("Factor",Range(0,1)) = 0
	}

	Subshader
	{
		Tags {"Queue" = "Transparent"
			//"IgnoreProjector" = "True"
			"RenderType" = "Transparent"
			"PreviewType" = "Plane"
			}
		
		ZWrite Off
		ColorMask RGB
		Blend DstColor One
		Offset -1,-1
		Cull Off
		Lighting Off
		Fog { Mode Off }
		//Blend One OneMinusSrcAlpha
		
		Pass
		{
		CGPROGRAM
		#pragma vertex vert
		#pragma fragment frag
		//#pragma multi_compile_fog
		#include "UnityCG.cginc"

		struct v2f {
			float4 uvShadow : TEXCOORD0;
			float4 uvFalloff : TEXCOORD1;
			UNITY_FOG_COORDS(2)
			float4 pos : SV_POSITION;
		};
		
		float4x4 unity_Projector;
		float4x4 unity_ProjectorClip;

		v2f vert(float4 vertex : POSITION)
		{
			v2f o;
			o.pos = UnityObjectToClipPos(vertex);
			o.uvShadow = mul(unity_Projector, vertex);
			o.uvFalloff = mul(unity_ProjectorClip, vertex);
			//UNITY_TRANSFER_FOG(o,o.pos);
			return o;
		}

		float4 _Emission;
		sampler2D _ShadowTex;
		sampler2D _FalloffTex;
		sampler2D _MaskBloom;
		sampler2D _MaskTextura;

		fixed4 frag(v2f i) : SV_Target
		{
			float4 texM = tex2Dproj(_ShadowTex, UNITY_PROJ_COORD(i.uvShadow));
			float4 texT = tex2Dproj(_MaskTextura, UNITY_PROJ_COORD(i.uvShadow));
			//texS.rgb *= _Emission.rgb;
			texM.rgb *= texM.a;
			texT.rgb *= texT.a;

			//float4 texF = tex2Dproj(_FalloffTex, UNITY_PROJ_COORD(i.uvFalloff));
			//float4 output = texM;
			float4 output = texT* texM;

			//UNITY_APPLY_FOG_COLOR(i.fogCoord, res, fixed4(0,0,0,0));
			return output;
		}
		ENDCG
		}
	}
}

