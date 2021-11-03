Shader "Custom/Toon_Extrude_Outline_Shader"
{
    Properties
    {
        _MainTex ("Albedo Map", 2D) = "white" {}
        [NoScaleOffset]_BumpMap ("Normal Map", 2D) = "bump" {}
        _ShadeNum ("Shade Number", Range(1,10)) = 3.0
        _Gloss ("Gloss", Range (0,1)) = 0.5
        _ShadowColor ("Shadow Color", Color) = (0,0,0,1)

      
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
                float4 normal : NORMAL;
                //float4 binormal : BINORMAL;
                float4 tangent : TANGENT;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                float3 normal : TEXCOORD1;
                float4 worldPos : TEXCOORD2;
                float3 worldBinormal : TEXCOORD3;
                float3 worldTangent : TEXCOORD4;

            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            sampler2D _BumpMap;
            float4 _BumpMap_ST;
            float _ShadeNum;
            float _Gloss; 
            float3 _ShadowColor;
            float3 _SpecularIntensity;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.normal = UnityObjectToWorldNormal (v.normal);
                o.worldTangent = UnityObjectToWorldDir (v.tangent.xyz);
                o.worldBinormal = cross(o.normal, o.worldTangent) * (v.tangent.w * unity_WorldTransformParams.w); //correctly handle flipping and negative scale
                o.worldPos = mul( unity_ObjectToWorld, v.vertex);

                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float3 NormalMap = UnpackNormal (tex2D (_BumpMap, i.uv));
                float3x3 tangentMatrix = {
                    i.worldTangent.x, i.worldBinormal.x, i.normal.x,
                    i.worldTangent.y, i.worldBinormal.y, i.normal.y,
                    i.worldTangent.z, i.worldBinormal.z, i.normal.z
                };
                float3 tangentNormal = normalize(mul(tangentMatrix, NormalMap));


                // Diffuse Light
                float light = (dot (normalize (tangentNormal),_WorldSpaceLightPos0.xyz)*0.5 + 0.5) * _ShadeNum ;
                float harshLight = round(light)/_ShadeNum;
                float3 colorLight = harshLight + _ShadowColor;

                //Specular Light
                float3 camPos = _WorldSpaceCameraPos;
                float3 fragtocam = camPos - i.worldPos;
                float3 viewDir = normalize (fragtocam);

                float3 viewReflect = reflect ( -viewDir, normalize(tangentNormal));
                float specularFalloff = dot (viewReflect, _WorldSpaceLightPos0.xyz)*0.5 +0.5;

                float specularExponent = exp2( _Gloss * 11) + 2; 
                float specularLight = pow (specularFalloff, specularExponent) * _Gloss * _ShadeNum;
                
                // Outline
                //float outline = 1-step(dot(normalize(tangentNormal), normalize (viewDir)), 0.35);


                fixed4 col =(tex2D(_MainTex, i.uv)* fixed4(colorLight,1) + (round(specularLight)/_ShadeNum));

                return col;
            }
            ENDCG
        }

        
    }
}
