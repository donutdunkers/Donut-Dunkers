Shader "Custom/Just_Outline_Wiggle"
{
    Properties
    {
        _Amount ("Outline Thickness", Range(0.0,10.0)) = 1.5
        _OutlineCol ("Outline Color", Color) = (0,0,0,1)
        _WiggleIntensity ("Wiggle Intensity", Range (0,1)) = 1.0
        _WiggleSpeed ("Wiggle Speed", Range (0,10)) = 1.0
        _WiggleDir ("Wiggle Direction", Vector) = (1,0,0)
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
            float _WiggleIntensity;
            float _WiggleSpeed;
            float3 _WiggleDir; 


 

            v2f vert (appdata v)
            {
                v2f o;
                float4 extrudeVert = v.vertex + (normalize (v.normal) * _Amount*0.01) ;
                o.vertex = UnityObjectToClipPos(extrudeVert + (normalize(_WiggleDir) * ((v.vertex.y+0.5) * _WiggleIntensity * sin(_Time.z * _WiggleSpeed))));
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
