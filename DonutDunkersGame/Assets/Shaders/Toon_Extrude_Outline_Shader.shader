Shader "Custom/Toon_Extrude_Outline_Shader_HLSL"
{
    Properties
    {
        _MainTex ("Albedo Map", 2D) = "white" {}
        _BumpMap ("Normal Map", 2D) = "bump" {}
        _ShadeNum ("Shade Number", Range(1,10)) = 3.0
        _Gloss ("Gloss", Range (1,50)) = 3.0
        _ShadowColor ("Shadow Color", Color) = (0,0,0,1)

      
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        HLSLINCLUDE
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
       

        CBUFFER_START (UnityPerMaterial)
            float _ShadeNum;
            float _Gloss; 
            float3 _ShadowColor;

        CBUFFER_END

        TEXTURE2D (_MainTex);
        SAMPLER (sampler_MainTex);

        TEXTURE2D (_BumpMap);
        SAMPLER (sampler_BumpMap);


        struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                float4 normal : NORMAL;
                //float4 binormal : BINORMAL;
                float4 tangent : TANGENT;
            };

        struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                float4 normal : TEXCOORD1;
                float4 worldPos : TEXCOORD2;
                float4 worldBinormal : TEXCOORD3;
                float4 worldTangent : TEXCOORD4;

            };

        ENDHLSL

        Pass
        {
            HLSLPROGRAM

            #pragma vertex vert
            #pragma fragment frag
            //#pragma target 3.0

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = TransformObjectToHClip (v.vertex.xyz);
                o.uv = v.uv;
                o.normal = mul (unity_ObjectToWorld, float4(v.normal));
                o.worldBinormal = mul (unity_ObjectToWorld,float4((cross(v.normal.xyz, v.tangent.xyz)),1.0));
                o.worldTangent = mul (unity_ObjectToWorld,float4((v.tangent)));
                o.worldPos = mul( unity_ObjectToWorld, v.vertex);

                return o;
            }

            half4 frag (v2f i) : SV_Target
            {
                float3 NormalMap = UnpackNormal (SAMPLE_TEXTURE2D (_BumpMap, sampler_BumpMap, i.uv));
                float3x3 tangentMatrix = float3x3(i.normal.xyz, i.worldBinormal.xyz,  -i.worldTangent.xyz);
                float3 tangentNormal = mul(tangentMatrix, NormalMap);


                // Diffuse Light
                float light = (dot (normalize (tangentNormal),float3(0,1,0))*0.5 + 0.5) * _ShadeNum ;
                float harshLight = round(light)/_ShadeNum;
                float3 colorLight = harshLight + _ShadowColor;

                //Specular Light
                float3 camPos = GetCameraPositionWS();
                float3 fragtocam = camPos - i.worldPos.xyz;
                float3 viewDir = normalize (fragtocam);

                float3 viewReflect = reflect ( -viewDir, normalize(tangentNormal));
                float specularFalloff = dot (viewReflect,float3(0,1,0) )*0.5 +0.5; //UnityWorldSpaceLightDir(i.vertex)
                float specularLight = clamp(pow (abs(specularFalloff), _Gloss),0.0,1.0)* _Gloss * _ShadeNum;
                
                // Outline
                float outline = 1-step(dot(normalize(tangentNormal), normalize (viewDir)), 0.35);


                half4 col =(SAMPLE_TEXTURE2D (_MainTex, sampler_MainTex, i.uv)* half4(colorLight,1) + (round(specularLight)/_ShadeNum));// * outline;

                return col;
            }
            ENDHLSL
        }

        
    }
}
