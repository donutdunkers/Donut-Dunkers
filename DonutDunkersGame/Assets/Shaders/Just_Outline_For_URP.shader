Shader "Custom/Just_Outline_For_URP"
{
    Properties
    {
        _Amount ("Outline Thickness", Range(0.0,10.0)) = 1.5
        _OutlineCol ("Outline Color", Color) = (0,0,0,1)
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Cull Front

        Pass{
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            


            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                //float2 uv : TEXCOORD0;
                float4 normal : NORMAL;
            };

            struct v2f
            {
                //float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                float3 normal : TEXCOORD1;
                //float4 worldPos : TEXCOORD2;
            };


            float _Amount;
            float4 _OutlineCol;


 

            v2f vert (appdata v)
            {
                v2f o;
                float4 extrudeVert = v.vertex + (normalize (v.normal) * _Amount*0.01);
                o.vertex = UnityObjectToClipPos(extrudeVert);
                //o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.normal = UnityObjectToWorldNormal (v.normal);
                //o.worldPos = mul( unity_ObjectToWorld, extrudeVert);

                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = _OutlineCol;
                return col;
            }
            ENDCG
        }
    }
}
