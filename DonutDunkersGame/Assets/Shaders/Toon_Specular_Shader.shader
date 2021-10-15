Shader "Custom/Toon_Specular_Shader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _ShadeNum ("Shade Number", Range(1,10)) = 3.0
        _Gloss ("Gloss", Range (1,50)) = 3.0
      
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
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                float3 normal : TEXCOORD1;
                float4 worldPos : TEXCOORD2;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float _ShadeNum;
            float _Gloss;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.normal = UnityObjectToWorldNormal (v.normal);
                o.worldPos = mul( unity_ObjectToWorld, v.vertex);

                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // Diffuse Light
                float light = (dot (normalize (i.normal),_WorldSpaceLightPos0.xyz)*0.5 + 0.5) * _ShadeNum ;

                //Specular Light
                float3 camPos = _WorldSpaceCameraPos;
                float3 fragtocam = camPos - i.worldPos;
                float3 viewDir = normalize (fragtocam);

                float3 viewReflect = reflect ( -viewDir, normalize(i.normal));
                float specularFalloff = dot (viewReflect, _WorldSpaceLightPos0.xyz)*0.5 +0.5;
                float specularLight = clamp(pow (specularFalloff, _Gloss),0.0,1.0)*_ShadeNum;


                fixed4 col = (tex2D(_MainTex, i.uv)* (round(light))/_ShadeNum) + (round(specularLight)/_ShadeNum);

                return col;
            }
            ENDCG
        }
    }
}
