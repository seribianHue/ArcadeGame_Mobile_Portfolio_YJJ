Shader "Custom/FadeOut"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0

        _DisappearPart("Dissapear Part", Range(0, 1)) = 0.5

        // ¿Ü°û »ö
        [HDR]_OutColor("OutColor", Color) = (1,1,1,1)
        // ¿Ü°û¼± µÎ²² ¼³Á¤
        _OutThickness("OutThickness", float) = 1.15

        _BlinkTex("BlinkTex", 2D) = "white" {}

        _HoloInterval("Hologram Interval", Float) = 1

    }
    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue" = "Transparent" }
        LOD 200

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Lambert alpha:fade

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        sampler2D _MainTex;
        sampler2D _BlinkTex;

        struct Input
        {
            float2 uv_MainTex;
            float2 uv_BlinkTex;

            float3 viewDir;
            float3 worldPos;
        };

        half _Glossiness;
        half _Metallic;
        fixed4 _Color;

        float _DisappearPart;

        float4 _OutColor;
        float _OutThickness;

        float _HoloInterval;


        void surf (Input IN, inout SurfaceOutput o)
        {
            // Albedo comes from a texture tinted by color
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
            o.Albedo = c.rgb;

            float rim = pow(saturate(IN.worldPos.g + _DisappearPart), _OutThickness);

            rim = 1-rim;

            o.Alpha = rim;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
