Shader "Custom/CameraShader"
{
    Properties
    {
        _MainTex ("Textura", 2D) = "white" {}
        _Factor ("Factor", Range(0,0.5)) = 0
    }
    SubShader
    {
        Pass
        {
        	CGPROGRAM
        	#pragma vertex vert_img
        	#pragma fragment frag
        	#pragma fragmentoption ARB_precision_hint_fastest
        	#include "UnityCG.cginc"

        	uniform sampler2D _MainTex;
        	float _Factor;

        	float4 frag(v2f_img i) : COLOR
        	{
        		float4 c1 = tex2D(_MainTex, i.uv);
        		float4 c2 =c1*(_Factor+1);
        		return c2;
        	}
        	ENDCG
        }
    }
    FallBack "Diffuse"
}
