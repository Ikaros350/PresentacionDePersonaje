Shader "Custom/ProyectorGenerico"
{
    Properties{
        [HDR]_Color("Emission", Color) = (1,1,1,1)
        _Alpha("Alpha",Range(0,1)) = 1
        _ShadowTex("Superficie", 2D) = "gray" {}
        _Textura("Main Textura", 2D) = "white" {}
        _Mask("Mask Line", 2D) = "white" {}
        _Factor("Factor",Range(0,1)) = 0
        
    }
        Subshader{
            Tags {"Queue" = "Transparent"
            "RenderType" = "Transparent"
                 }
            Pass {
                ZWrite Off
                Offset -1,-1
                ColorMask RGB
                Fog { Mode Off }
                Cull Off
                //Blend SrcAlpha One // Additive blending
                Blend One OneMinusSrcAlpha

                CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag alpha:blend
                #include "UnityCG.cginc"

                struct v2f {
                    float4 uvShadow : TEXCOORD0;
                    float4 pos : SV_POSITION;
                };

                float4x4 unity_Projector;
                float4x4 unity_ProjectorClip;

                v2f vert(float4 vertex : POSITION)
                {
                    v2f o;
                    o.pos = UnityObjectToClipPos(vertex);
                    o.uvShadow = mul(unity_Projector, vertex);
                    return o;
                }

                float _Factor;
                float _Alpha;
                sampler2D _ShadowTex;
                sampler2D _Textura;
                sampler2D _Mask;
                float4 _Color;

                fixed4 frag(v2f i) : SV_Target
                {
                    float4 texS = tex2Dproj(_ShadowTex, UNITY_PROJ_COORD(i.uvShadow));
                    texS.rgb *= texS.a;
                    float4 texM= tex2Dproj(_Textura, UNITY_PROJ_COORD(i.uvShadow));
                    texM.rgb *= texM.a;
                    float4 texMa = tex2Dproj(_Mask, UNITY_PROJ_COORD(i.uvShadow));
                    texMa.rgb *= texMa.a;

                    float4 combinacion = texS * texM;
                    combinacion.rgb *= combinacion.a;

                    float4 combinacion2 = texM * texMa;
                    float4 combinacion3 = texM * (1 - texMa);

                    float4 combinacionMain = combinacion2 * texS;
                    //combinacionMain.rgb *= combinacionMain.a;
                    float4 combinacionMain2 = combinacion3 * texS;
                    //combinacionMain2.rgb *= combinacionMain2.a;

                    float4 output = float4(0, 0, 0, texS.a);
                    float4 alpha = float4(0, 0, 0, 0);

                    //output.rgb = lerp(combinacion.rgb,_Emission.rgb, maskBloom.x);
                    output.rgb = (combinacionMain.rgb * (_Factor + 1)+ combinacionMain2.rgb);
                    float4 output2 = lerp(alpha, output , _Alpha*0.5);
                    return output2;
                }
                ENDCG
            }
    }
}
