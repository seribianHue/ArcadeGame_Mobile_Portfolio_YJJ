Shader "Custom/Hologram"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}

        //�׵θ� �е�
        _RimDensity("Rim Density", Range(1, 10)) = 3
        //�׵θ� ��
        _RimColor("Rim Color", Color) = (1,1,1,1)

        //��ָ��� �ִٸ� ��ָʵ� ���� ����ؼ� �ϸ� �� ��������� ���´�
        _BumpMap("Normal Map", 2D) = "bump"{}
        //��ָ� ����
        _NormalRate("Normal Rate", Range(0, 1)) = 1

        //�����̴� ȿ��
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

        //lambert lighting ���� / ������ ����
        #pragma surface surf Lambert noambient alpha:fade

        sampler2D _MainTex;
        sampler2D _BumpMap;
        struct Input
        {
            float2 uv_MainTex;

            //�ü� ���� �߰�(�����)
            float3 viewDir;

            float2 uv_BumpMap;
        
            //���� �������� ��ǥ ����
            float3 worldPos;
        };

        fixed4 _Color;

        half _RimDensity;
        float4 _RimColor;

        half _FlickerTime;

        float _RimThickness;
        float _HoloInterval;
        float _HoloThickness;

            //o.Normal : 3D�𵨿��� ǥ���� ������ ��Ÿ���� ����
            //�þߺ��Ϳ� ������ ���� ���� (�þ� ������ ������)
        void surf (Input IN, inout SurfaceOutput o)
        {
            //��ָ��� ����Ѵٸ�
            /*
            float3 nor = UnpackNormal(tex2D(_BumpMap, IN.uv_BumpMap));
            nor = float3(nor.r * _NormalRate, nor.g * _NormalRate, nor.b);
            o.Normal = nor
            */

            // �� ����Ʈ ����(�ѿ� ������)
            /*
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
            o.Albedo = c.rgb;

            float actRim;
            float rim = dot(o.Normal, IN.viewDir);
            actRim = pow(1-rim, _RimDensity);

            o.Emission = actRim * _RimColor;
            o.Alpha = c.a;
            */

            //�� ����Ʈ �� �����̱�
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
            //saturate �Լ� : 0 ~ 1�� ������ �����ϴ� �Լ�
            float rim = saturate(dot(o.Normal, IN.viewDir));
            //frac �Լ�(�Ҽ����� ��ȯ�ϴ� �Լ�)
            rim = pow(frac((IN.worldPos.g * _HoloInterval) - (_Time.y * _FlickerTime)), _HoloThickness);

            o.Emission = rim * _RimColor;

            o.Alpha = c.a;

        }
        ENDCG
    }
    FallBack "Diffuse"
}
