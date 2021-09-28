// Directorio /Nombre del shader
Shader "Custom/Base/Ring" {

	// Variables disponibles en el inspector (Propiedades)
	Properties { 
		_Color ("Color", Color) = (1,1,1,1)
		_ColorE ("Color Emi", Color) = (1,1,1,1)
		_Textura1 ("Tex1", 2D) = "white" {}
		_Textura2 ("Tex2", 2D) = "white" {}
		_FactorA ("FacTex", Range(0,1))=0
		_FactorB ("FacVel", Range(0,5))=0
		_Mask ("Mask", 2D) = "white" {}
	}

	// Primer subshader
	SubShader { 
		LOD 200
		
		CGPROGRAM
		// Método para el cálculo de la luz
		#pragma surface surf Standard fullforwardshadows alpha:blend
		#pragma target 3.0

		// Información adicional provista por el juego
		struct Input {

			float2 uv_MainTex;
			float2 uv_Textura1;
			float2 uv_Textura2;
			float2 uv_Mask;

		};

		// Declaración de variables
		float4 _Color;
		float4 _ColorE;
		sampler2D _Textura1;
		sampler2D _Textura2;
		sampler2D _Mask;
		float _FactorA;
		float _FactorB;

		// Nucleo del programa
		void surf (Input IN, inout SurfaceOutputStandard o) {

			float2 UVtext2 = IN.uv_Textura2;
			UVtext2+= float2(_FactorB*_Time.y,_FactorB*_Time.y);

			float4 m =tex2D(_Mask,IN.uv_Mask);
			float4 b =tex2D(_Textura1,IN.uv_Textura1)*_Color;
			float4 c =tex2D(_Textura2,UVtext2);
		



			float4 d =c*m*(1-b*_FactorA)+(_FactorA*b);

			o.Emission= _ColorE;
			o.Albedo = d.rgb;
			o.Alpha = c.a;
		}
		ENDCG

	}// Final del primer subshader

	// Segundo subshader si existe alguno
	// Tercer subshader...

	// Si no es posible ejecutar ningún subshader ejecute Diffuse
	FallBack "Diffuse"
}
