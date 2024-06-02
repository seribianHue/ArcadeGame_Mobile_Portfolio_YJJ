Shader "Custom/Damaged"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0

        _Transparency("Transparency", Range(0,1)) = 0.5

        _BlinkTex("BlinkTex", 2D) = "white" {}

        //±ôºýÀÌ´Â È¿°ú
        _FlickerTime("Flicker Time", Range(0, 10)) = 1
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue" = "Transparent" }
        
        //zwrite on
        
        //ColorMask 0

        LOD 200

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard alpha:fade

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        sampler2D _MainTex;
        sampler2D _BlinkTex;

        struct Input
        {
            float2 uv_MainTex;
            float2 uv_BlinkTex;
        };

        half _Glossiness;
        half _Metallic;
        fixed4 _Color;

        float _Transparency;
        float _FlickerTime;

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            // Albedo comes from a texture tinted by color
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;

            //fixed4 blink = tex2D(_BlinkTex, IN.uv_BlinkTex);

            o.Albedo = c.rgb;
            // Metallic and smoothness come from slider variables
            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;

            fixed4 blink = tex2D(_BlinkTex, float2(_Time.y * _FlickerTime, 0.5));

            o.Alpha = c.a * blink.r ;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
