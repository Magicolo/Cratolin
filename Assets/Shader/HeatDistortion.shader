Shader "Orbis/HeatDistortion"
{

    Properties{
		_MainTex("Base (RGBA)", 2D) = "white" {}
        _HeatMap("Heat Map", 2D) = "white" {}
        _Width("Width",Int) = 100
        _Height("Height",Int) = 100
		[MaterialToggle] _PixelSnap ("Pixel snap", Float) = 0
        _WaveForce("Wave Force",Float) = 1
	}

    SubShader
    {

        Tags { "Queue" = "Transparent" }

        GrabPass{ "_BackgroundTexture" }

        // Render the object with the texture generated above, and invert the colors
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct v2f
            {
                float4 grabPos : TEXCOORD0;
                float4 pos : SV_POSITION;
            };

            sampler2D _HeatMap;
            sampler2D _BackgroundTexture;
            float _Width;
            float _Height;
            float _WaveForce;

            v2f vert(appdata_base v) {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.grabPos = ComputeGrabScreenPos(o.pos);

                float4 posNormalized = (o.grabPos - float4(0.5,0.5,0,0)) * sin( _Time.g);        

                fixed4 heatMapCol = tex2D(_HeatMap, o.grabPos + posNormalized / _WaveForce);
                //o.pos = o.pos + float4(heatMapCol.r,1,1,0);
               // o.grabPos.xy = TRANSFORM_TEX(v.grabPos, _HeatMap);
                return o;
            }

            half4 frag(v2f i) : SV_Target
            {
                
                float4 posNormalized = (i.grabPos - float4(0.5,0.5,0,0)) * sin( 2*_Time.g)*2 ;
                float4 posCentered = posNormalized * 10;
                float4 offset = float4(posCentered.x/_Width, posCentered.y/_Height, 0, 0);
                
                fixed4 heatMapNormal = tex2D(_HeatMap, i.grabPos);
                fixed4 heaMapCol = tex2D(_HeatMap, i.grabPos + posNormalized / _WaveForce);
                half4 c = tex2Dproj(_BackgroundTexture, i.grabPos);
                //return float4(0,heaMapCol.b,0,1);
                float4 p = i.grabPos + offset  * heaMapCol.b+ float4(heaMapCol.r, heaMapCol.g,0,0) * 0.005 * (1-heaMapCol.b);
                
                half4 bgcolor = tex2Dproj(_BackgroundTexture, p);
                float r = bgcolor.r;
                return (half4(r,bgcolor.g,bgcolor.b,1) + c)/2;
            }
            ENDCG
        }

    }
}
