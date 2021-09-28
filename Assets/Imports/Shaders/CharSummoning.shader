Shader "Custom/CharSummoning"
{
    Properties
    {
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _OtherTex ("Albedo (RGB)", 2D) = "white" {}
        _Color ("Color", Color) = (1,0,0,1)
        _Value("Value", Range(0,1)) = 0
        _Limits("Limits", Range(0,1)) = 0
        _Cut("Cut", Range(0,5)) = 0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        #pragma surface surf Standard fullforwardshadows
        #pragma target 3.0

        sampler2D _MainTex;
        sampler2D _OtherTex;
        half4 _Color;
        float _Value;
        float _Limits;
        float _Cut;

        struct Input
        {
            float2 uv_MainTex;
            float2 uv_OtherTex;
            float3 worldPos;
        };


        UNITY_INSTANCING_BUFFER_START(Props)
        UNITY_INSTANCING_BUFFER_END(Props)

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            float v = _Value;
            float l = _Limits;
            float k = _Cut;
            float2 UVMovStat = IN.uv_OtherTex;
            float distx = _Time.y/2;
            float disty = -_Time.y/2;
            UVMovStat += float2(distx, disty);
            if(IN.worldPos.y < 0 - l*k)
            {
                v = 0;
			}
            else if (IN.worldPos.y > k - l*k)
            {
                v = 1;   
			}
            else
            {
                v = IN.worldPos.y / (k*(1-l));
			}

            fixed4 c = ((tex2D (_MainTex, IN.uv_MainTex)* v * _Color)+(tex2D (_OtherTex, UVMovStat) * (1-v)));
            o.Albedo = c.rgb;
        }
        ENDCG
    }
    FallBack "Diffuse"
}