// Directorio /Nombre del shader
Shader "Custom/Base/MovText" {

	// Variables disponibles en el inspector (Propiedades)
	Properties { 
		[HDR]_ColorAmbiente ("Color Ambiente", Color) = (1,1,1,1)
		_Texture("Textura Base",2D) = "white"{}
		_Speed("Velocidad",Vector) = (0,0,0,0)
		_Factor("Factor", Range(0,1)) = 0
	}

	// Primer subshader
	SubShader { 
		Tags {"Queue" = "Transparent" "IgnoreProjector" = "True" "RenderType" = "TransparentCutOut"}
		//ZWrite Off
		//Blend SrcAlpha OneMinusSrcAlpha
		LOD 100
		
		CGPROGRAM
		// Método para el cálculo de la luz
		#pragma surface surf Standard fullforwardshadows Alpha:fade
		#pragma target 3.0

		// Información adicional provista por el juego
		struct Input {
			float2 uv_Texture;
			
		};

		// Declaración de variables
		float4 _ColorAmbiente;
		float4 _Speed;
		sampler2D _Texture;
		float _Factor;

		// Nucleo del programa
		void surf (Input IN, inout SurfaceOutputStandard o) {

			float2 Uv = IN.uv_Texture;
			float distanciaX = _Speed.x * _Time.y;
			float distanciaY = _Speed.y * _Time.y;
			Uv += float2 (distanciaX, distanciaY);
			float4 output = tex2D(_Texture, Uv);
			o.Albedo = (output * _ColorAmbiente).rgb;
			output.a = _Factor;
			o.Alpha = output.a;
		}
		ENDCG

	}// Final del primer subshader

	// Segundo subshader si existe alguno
	// Tercer subshader...

	// Si no es posible ejecutar ningún subshader ejecute Diffuse
	FallBack "Diffuse"
}
