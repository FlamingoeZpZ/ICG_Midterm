Shader "Custom/EpicToonRampShader"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex("Main Texture", 2D) = "" {}
        _EmissiveTexture("Emissive Texture", 2D) = "" {}
        _ReflectiveTexture("Reflective Texture", 2D) = "" {}
        _ToonRamp("ToonRamp", 2D) = "" {}
        _SpecPow("Specular Power", range(0,100)) = 50
        _Metallic("Metallic Power", range(0,1)) = 1
        _myCube("Reflection Map", Cube) = "grey" {}

    }
    SubShader
    {
        Tags { "Queue"="Geometry" }
        

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard ToonRamp
        half _SpecPow;
        half _Metallic;
        fixed4 _Color;
        sampler2D _MainTex;
        sampler2D _ToonRamp;
        sampler2D _ReflectiveTexture;
        sampler2D _EmissiveTexture;
        samplerCUBE _myCube;

        
        //NOTE: BasicLambert, Basic Blinn too must be in both function and name

        //For flowCharting --> Inputs --> Process --> Output
        
       half4 LightingToonRamp(SurfaceOutputStandard s, half3 lightDir, half atten)
        {
            const half diff = max(0, dot(s.Normal, lightDir));
            const float h = diff*0.5f+0.5f; //Make difference noticable
            const float2 rh = h;

            float4 c; // Why not a fixed 4?
            c.rgb = s.Albedo * _LightColor0.rgb  * tex2D(_ToonRamp, rh).rgb;
            c.a = s.Alpha;
            return c;
        }
        
        struct Input
        {
            float2 uv_MainTex;
            float3 worldRefl;
        };

        void surf(Input IN, inout SurfaceOutputStandard o)
        {
            float3 refl = tex2D(_ReflectiveTexture, IN.uv_MainTex).rgb;
            o.Albedo = tex2D(_MainTex,  IN.uv_MainTex).rgb * _Color.rgb;
            o.Metallic = refl * _Metallic;
            o.Emission =  texCUBE(_myCube, IN.worldRefl).rgb * refl + tex2D(_EmissiveTexture, IN.uv_MainTex).rgb;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
