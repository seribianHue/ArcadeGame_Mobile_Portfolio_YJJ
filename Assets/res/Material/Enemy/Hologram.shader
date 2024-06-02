Shader "Custom/Hologram"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}

        //테두리 밀도
        _RimDensity("Rim Density", Range(1, 10)) = 3
        //테두리 색
        _RimColor("Rim Color", Color) = (1,1,1,1)

        //노멀맵이 있다면 노멀맵도 같이 사용해서 하면 더 나은결과가 나온다
        _BumpMap("Normal Map", 2D) = "bump"{}
        //노멀맵 비율
        _NormalRate("Normal Rate", Range(0, 1)) = 1

        //깜빡이는 효과
        _FlickerTime("Flicker Time", Range(0, 10)) = 1

        _RimThickness("RimLight Thickness", Float) = 3
        _HoloInterval("Hologram Interval", Float) = 1
        _HoloThickness("Hologram Thickness", Float) = 1
        
    }
    SubShader
    {
        Tags { "RenderType" = "Transparent" "Queue" = "Transparent" }
        LOD 200

        CGPROGRAM

        //lambert lighting 적용 / 반투명 구현
        #pragma surface surf Lambert noambient alpha:fade

        sampler2D _MainTex;
        sampler2D _BumpMap;
        struct Input
        {
            float2 uv_MainTex;

            //시선 벡터 추가(예약어)
            float3 viewDir;

            float2 uv_BumpMap;
        
            //월드 공간상의 좌표 정보
            float3 worldPos;
        };

        fixed4 _Color;

        half _RimDensity;
        float4 _RimColor;

        half _FlickerTime;

        float _RimThickness;
        float _HoloInterval;
        float _HoloThickness;

            //o.Normal : 3D모델에서 표면의 방향을 나타내는 벡터
            //시야벡터와 내적한 값을 적용 (시야 방향이 조명역할)
        void surf (Input IN, inout SurfaceOutput o)
        {
            //노멀맵을 사용한다면
            /*
            float3 nor = UnpackNormal(tex2D(_BumpMap, IN.uv_BumpMap));
            nor = float3(nor.r * _NormalRate, nor.g * _NormalRate, nor.b);
            o.Normal = nor
            */

            // 림 라이트 적용(겉에 빛나기)
            /*
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
            o.Albedo = c.rgb;

            float actRim;
            float rim = dot(o.Normal, IN.viewDir);
            actRim = pow(1-rim, _RimDensity);

            o.Emission = actRim * _RimColor;
            o.Alpha = c.a;
            */

            //림 라이트 가 깜빡이기
            /*
            float rim = saturate(dot(o.Normal, IN.viewDir));
            rim = pow(1-rim, _RimDensity);
            //o.Alpha = rim;
            //o.Alpha = rim * abs(sin(_Time.y * _FlickerTime));
            o.Alpha = rim * (sin(_Time.y * _FlickerTime) * 0.5 + 0.5);

            //o.Emission = pow(1-rim, 3);
            o.Emission = _RimColor;
            */

            
            //o.Emission = _RimColor;
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
            o.Albedo = c.rgb;    
            //saturate 함수 : 0 ~ 1의 범위로 조정하는 함수
            float rim = saturate(dot(o.Normal, IN.viewDir));
            //frac 함수(소수점만 반환하는 함수)
            rim = pow(frac((IN.worldPos.g * _HoloInterval) - (_Time.y * _FlickerTime)), _HoloThickness);

            o.Emission = rim * _RimColor;

            o.Alpha = c.a;

        }
        ENDCG
    }
    FallBack "Diffuse"
}
