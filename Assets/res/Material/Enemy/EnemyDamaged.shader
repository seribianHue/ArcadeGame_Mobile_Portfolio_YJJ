Shader "Custom/EnemyDamaged"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        // 디졸브용 노이즈 이미지
        _NoiseTex("Noise Tex (RGB)", 2D) = "white" {}
        // 알파 테스트용 수치
        _Cut("Alpha Cut", Range(0, 1)) = 0
        //알파 테스트 비율 조정
        _CutRate("Alpha Cut Rate", Range(0.01, 0.1)) = 0.05
        
        // 외곽 색
        [HDR]_OutColor("OutColor", Color) = (1,1,1,1)
        // 외곽선 두께 설정
        _OutThickness("OutThickness", Range(1, 1.5)) = 1.15

        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue" = "Transparent" }
        LOD 200

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Lambert alpha:fade

        sampler2D _MainTex;
        sampler2D _NoiseTex;

        float _Cut;
        float _CutRate;

        float4 _OutColor;

        float _OutThickness;

        struct Input
        {
            float2 uv_MainTex;
            float2 uv_NoiseTex;
        };

        half _Glossiness;
        half _Metallic;
        fixed4 _Color;

        void surf (Input IN, inout SurfaceOutput o)
        {
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
            o.Albedo = c.rgb;

            float4 noise = tex2D(_NoiseTex, IN.uv_NoiseTex);

            float alpha = 0;
            if (noise.r >= _Cut * _CutRate)
                alpha = 1;

            float outline = 1;
            if (noise.r >= _Cut * _CutRate * _OutThickness)
                outline = 0;

            o.Albedo = o.Albedo + (outline * _OutColor.rgb);

            o.Alpha = alpha;

            // Metallic and smoothness come from slider variables
            //o.Metallic = _Metallic;
            //o.Smoothness = _Glossiness;
            //o.Alpha = c.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
