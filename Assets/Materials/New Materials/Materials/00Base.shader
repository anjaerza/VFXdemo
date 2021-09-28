Shader "Custom/Movimiento/Base" {

    Properties {
        _Color ("Color principal", Color) = (1,1,1,1)
        _TexturaMovimiento ("Textura movimiento", 2D) = "white" {}
        _Alpha ("Alpha", 2D) = "white" {}
        _Mask ("Máscara",2D) = "white" {}
        _FactorA ("VelAng", Range(0,5))=0
        _FactorB ("Ampli", Range(0,5))=0
    }


    SubShader {
        LOD 200

        CGPROGRAM
        #pragma surface surf Standard fullforwardshadows alpha:blend
        #pragma target 3.0

        sampler2D _TexturaMovimiento;
        sampler2D _Mask;
        sampler2D _Alpha;
        float4 _Color;
        float _FactorA;
		float _FactorB;

        struct Input {
            float2 uv_TexturaMovimiento;
            float2 uv_Mask;
            float2 uv_Alpha;
        };

        void surf (Input IN, inout SurfaceOutputStandard o) {
            float2 UVMovimiento = IN.uv_TexturaMovimiento;
            float distanciaX =_FactorB*sin(_FactorA*_Time.y);
            float distanciaY =_FactorB*sin(_FactorA*_Time.y);
            UVMovimiento += float2(distanciaX, distanciaY);

            float4 m = tex2D(_Mask,IN.uv_Mask);
            float4 p = tex2D(_Alpha,IN.uv_Alpha);

            float4 c = (tex2D(_TexturaMovimiento, UVMovimiento))*m;

            o.Albedo = c.rgb;
            o.Alpha=p.a;
            o.Emission=_Color*m;
        }
        ENDCG
    }
    FallBack "Diffuse"
}