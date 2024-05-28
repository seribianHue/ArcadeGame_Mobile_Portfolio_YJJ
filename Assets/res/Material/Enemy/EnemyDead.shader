Shader "Custom/EnemyDead"
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
        _OutThickness("OutThickness", Range(0, 1.5)) = 1.15

        _BlinkTex("BlinkTex", 2D) = "white" {}

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
        };

        half _Glossiness;
        half _Metallic;
        fixed4 _Color;

        float _DisappearPart;

        float4 _OutColor;
        float _OutThickness;

        void surf (Input IN, inout SurfaceOutput o)
        {
            // Albedo comes from a texture tinted by color
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
            fixed4 blink = tex2D(_BlinkTex, IN.uv_BlinkTex) * _OutColor * _Time.y;

            o.Albedo = c.rgb;
            // Metallic and smoothness come from slider variables
            //o.Metallic = _Metallic;
            //o.Smoothness = _Glossiness;

            float alpha = 0;
            if (IN.uv_MainTex.y <= _DisappearPart)
            {
                //visable
                alpha = 1;
                if(IN.uv_MainTex.y >= _DisappearPart - 0.01) alpha = 0.1;
                else if(IN.uv_MainTex.y >= _DisappearPart - 0.02) alpha = 0.2;
                else if(IN.uv_MainTex.y >= _DisappearPart - 0.03) alpha = 0.3;
                else if(IN.uv_MainTex.y >= _DisappearPart - 0.04) alpha = 0.4;
                else if(IN.uv_MainTex.y >= _DisappearPart - 0.05) alpha = 0.5;
                else if(IN.uv_MainTex.y >= _DisappearPart - 0.06) alpha = 0.63;
                else if(IN.uv_MainTex.y >= _DisappearPart - 0.07) alpha = 0.75;
                else if(IN.uv_MainTex.y >= _DisappearPart - 0.08) alpha = 0.87;
                else if(IN.uv_MainTex.y >= _DisappearPart - 0.09) alpha = 0.95;
                else if(IN.uv_MainTex.y >= _DisappearPart - 0.1) alpha = 1; 

                if (IN.uv_MainTex.y <= _DisappearPart - _OutThickness)
                {
                    //alpha = 1;
                }
                else
                {
                    //alpha = lerp(1, 0, IN.uv_MainTex.y/(_DisappearPart));
                    //alpha = lerp(0, 1, _DisappearPart);
                    //alpha = pow(alpha, 0.3);
                }
                //alpha = lerp(1, 0, IN.uv_MainTex.y / _DisappearPart);
                //alpha = pow(alpha, 0.3);
            }
            else alpha = 0;

            float outline = 0;
            if (IN.uv_MainTex.y >= _DisappearPart - _OutThickness)
                outline = 1;

            //outline = lerp(0, 1, 0.1);

            //alpha = lerp(0, 1, alpha);

            //o.Albedo = o.Albedo + (outline * _OutColor.rgb);
            o.Albedo = o.Albedo;
            o.Alpha = c.a * alpha;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
