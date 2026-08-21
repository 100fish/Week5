Shader "Unlit/UnlitScreenspaceTexture"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _ScreenSpaceTex ("Screenspace Texture", 2D) = "white" {}
        _UnscaledScreenSpaceText ("Unscaled Screenspace Texture", 2D) = "white" {}
        _Color ("Color", Color) = (1,1,1,1)
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

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
                float4 screenUV : TEXCOORD1;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 screenUV : TEXCOORD1;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex, _ScreenSpaceTex, _UnscaledScreenSpaceText;
            float4 _MainTex_ST, _ScreenSpaceTex_ST, _UnscaledScreenSpaceText_ST;
            fixed4 _Color;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.screenUV = ComputeScreenPos (o.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv); //apply main texture;
                
                float2 screenspaceUV = i.screenUV.xy / i.screenUV.w; //get screenspace UV;
                
                float2 unscaledScreenspaceUV = TRANSFORM_TEX (screenspaceUV, _UnscaledScreenSpaceText); //get transformed unscaled screenspace uv;
                
                col *= tex2D (_UnscaledScreenSpaceText, screenspaceUV);
                
                float aspect = _ScreenParams.x / _ScreenParams.y; //get screen aspect;
                
                screenspaceUV.x *= aspect; //multiply screenspace uv by aspect to remove aspect distortion;
                
                screenspaceUV = TRANSFORM_TEX(screenspaceUV, _ScreenSpaceTex);
                
                col *= tex2D (_ScreenSpaceTex, screenspaceUV);
                
                col *= _Color;
                
                return col;
            }
            ENDCG
        }
    }
}
