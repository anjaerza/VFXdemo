Shader "Hidden/CamStat"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Mask ("Mask", 2D) = "white" {}
        _EffTex ("Effect", 2D) = "white" {}
    }
    SubShader
    {
        // No culling or depth
        Cull Off ZWrite Off ZTest Always

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                float2 uv_Mask : TEXCOORD1;
                float2 uv_EffTex : TEXCOORD2;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float2 uv_Mask : TEXCOORD1;
                float2 uv_EffTex : TEXCOORD2;
                float4 vertex : SV_POSITION;
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                o.uv_Mask = v.uv_Mask;
				o.uv_EffTex = v.uv_EffTex;
                return o;
            }

            sampler2D _MainTex;
            sampler2D _Mask;
			sampler2D _EffTex;

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);
                fixed4 output = col;
                float2 UVMovStat = i.uv_EffTex;
                float distx = -10*tan(_Time.y*7);
                float disty = 2*sin(_Time.y*0.5f);
                UVMovStat += float2(distx, disty);
                float4 mask = tex2D(_Mask, i.uv_Mask);
				float4 eff = tex2D(_EffTex, UVMovStat);
                //output.rgb = Luminance(col.rgb) * (1-mask) + eff*mask;
                output.rgb = col.rgb * (1-mask) + eff*mask;
                return output;
            }
            ENDCG
        }
    }
}
