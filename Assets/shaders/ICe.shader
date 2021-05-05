// Directorio /Nombre del shader
Shader "Custom/Base/Ice" {

	// Variables disponibles en el inspector (Propiedades)
	Properties{
		_ColorAmbiente("Color Ambiente", Color) = (1,1,1,1)
		_Textura("Textura Base",2D)="white"{}
		_Textura2("Textura Hielo",2D)="white"{}
		_Factor("Factor", Range(0,1)) = 0.5
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
			float2 uv_Textura;
			float2 uv_Textura2;
		};

		// Declaración de variables
		float4 _ColorAmbiente;
		sampler2D _Textura;
		sampler2D _Textura2;
		float _Factor;

		// Nucleo del programa
		void surf (Input IN, inout SurfaceOutputStandard o) {

			float4 c = tex2D(_Textura,IN.uv_Textura);
			float4 c1 = tex2D(_Textura2,IN.uv_Textura2);


			o.Albedo = lerp(c,c1,_Factor);
			
		}
		ENDCG

	}// Final del primer subshader

	// Segundo subshader si existe alguno
	// Tercer subshader...

	// Si no es posible ejecutar ningún subshader ejecute Diffuse
	FallBack "Diffuse"
}
