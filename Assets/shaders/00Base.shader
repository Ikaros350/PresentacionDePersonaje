// Directorio /Nombre del shader
Shader "Custom/Base/Base" {

	// Variables disponibles en el inspector (Propiedades)
	Properties { 
		_ColorAmbiente ("Color Ambiente", Color) = (1,1,1,1)
	}

	// Primer subshader
	SubShader { 
		LOD 200
		
		CGPROGRAM
		// Método para el cálculo de la luz
		#pragma surface surf Standard fullforwardshadows
		#pragma target 3.0

		// Información adicional provista por el juego
		struct Input {
			float2 uv_MainTex;
		};

		// Declaración de variables
		float4 _ColorAmbiente;

		// Nucleo del programa
		void surf (Input IN, inout SurfaceOutputStandard o) {

			float4 c = _ColorAmbiente;
			o.Albedo = c.rgb;
		}
		ENDCG

	}// Final del primer subshader

	// Segundo subshader si existe alguno
	// Tercer subshader...

	// Si no es posible ejecutar ningún subshader ejecute Diffuse
	FallBack "Diffuse"
}
