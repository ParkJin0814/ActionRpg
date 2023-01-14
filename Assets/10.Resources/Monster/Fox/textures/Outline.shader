Shader "YoungJunShader/OutlineRed"
{
    Properties
    {
        _Color("Outline Color", Color) = (1,0,0,1)
        _Width("Outline Width", Range(0.0, 0.1)) = 0.01
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" "Queue" = "Transparent" "IgnoreProjector" = "True"}

        Pass
        {
            Zwrite off
            Cull Front
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 pos : POSITION;
                float3 normal : NORMAL;
            };

            struct v2f
            {
                float4 pos :POSITION;
            };

            float4 _Color;
            float _Width;

            v2f vert (appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.pos);
                float3 normal = mul((float3x3)UNITY_MATRIX_IT_MV, v.normal);
                float2 offset = TransformViewToProjection(normal.xy);
                o.pos.xy += offset * _Width;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                return _Color;
            }
            ENDCG
        }
    }
}
