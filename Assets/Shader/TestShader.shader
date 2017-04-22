// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "RGB -> TestShader" {
  
 Properties{
     _MainTex ("Sprite Texture", 2D) = ""
      [MaterialToggle] PixelSnap ("Pixel snap", Float) = 1
 }
  
 Subshader {
     Tags {"Queue"="Transparent"}
     ZWrite Off
     Blend SrcAlpha OneMinusSrcAlpha
     Cull Off
     Lighting Off
     Fog { Mode Off }
     
     Pass {
         CGPROGRAM
         #pragma vertex vert
         #pragma fragment frag
             #pragma multi_compile DUMMY PIXELSNAP_ON
             #pragma target 3.0
             #include "UnityCG.cginc"
             
         struct v2f {
             float4 position : SV_POSITION;
             float2 uv_mainTex : TEXCOORD;                
         };
         
 
        
         uniform float4 _MainTex_ST;
         
         v2f vert(float4 position : POSITION, float2 uv : TEXCOORD0) {
             v2f o;
             o.position = UnityObjectToClipPos(position);
             o.uv_mainTex = uv * _MainTex_ST.xy + _MainTex_ST.zw;
             
             #ifdef PIXELSNAP_ON
             o.position = UnityPixelSnap (o.position);
             #endif
             
             return o;
         }
        
         uniform sampler2D _MainTex;
         fixed4 frag(float2 uv_mainTex : TEXCOORD) : COLOR {
             fixed4 mainTex = tex2D(_MainTex, uv_mainTex);
             fixed4 fragColor;
             fragColor.rgb = dot(mainTex.rgb, fixed3(.222, .707, .071));
             fragColor.a = mainTex.a;
             return fragColor;
         }
         ENDCG
     }
 }
  
 }