// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

 Shader "Projector/AdditiveTint" {
     Properties {
         _Attenuation ("Falloff", Range(0.0, 2.0)) = 0
         _ShadowTex ("Cookie", 2D) = "gray" {}
         _Color("Color",Color)=(0,0,0,0)
     }
     Subshader {
         Tags {"Queue"="Transparent"}
         Pass {
             ZWrite Off
             ColorMask RGB
             Blend SrcAlpha One 
             Offset -1, -1
 
             CGPROGRAM
             #pragma vertex vert
             #pragma fragment frag
             #include "UnityCG.cginc"
             
             struct v2f {
                 float4 uvShadow : TEXCOORD0;
                 float4 pos : SV_POSITION;
             };
             
             float4x4 unity_Projector;
             float4x4 unity_ProjectorClip;
             
             v2f vert (float4 vertex : POSITION)
             {
                 v2f o;
                 o.pos = UnityObjectToClipPos (vertex);
                 o.uvShadow = mul (unity_Projector, vertex);
                 return o;
             }
             
             sampler2D _ShadowTex;
             fixed4 _Color;
             float _Attenuation;
             
             fixed4 frag (v2f i) : SV_Target
             {

                 fixed4 texCookie = tex2Dproj (_ShadowTex, UNITY_PROJ_COORD(i.uvShadow));
                 fixed4 outColor = texCookie.a;
                
                 return outColor *_Attenuation*_Color;
             }
             ENDCG
         }
     }
 }