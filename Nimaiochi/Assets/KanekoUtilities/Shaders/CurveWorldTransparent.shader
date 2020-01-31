// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/CurveWorldTrsansparent" {
Properties {
    _Color ("Main Color", Color) = (1,1,1,1)
    _MainTex ("Base (RGB) Trans (A)", 2D) = "white" {}
    _Vertical ("Vertical", Float) = 0.0001
    _Horizon ("Horizon", Float) = 0.0001
}

SubShader {
    Tags {"Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent"}
    LOD 100

    //ZWrite Off
    Blend SrcAlpha OneMinusSrcAlpha 

    Pass {  
        CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct appdata_t {
                float4 vertex : POSITION;
                float2 texcoord : TEXCOORD0;
            };

            struct v2f {
                float4 vertex : SV_POSITION;
                half2 texcoord : TEXCOORD0;
                UNITY_FOG_COORDS(1)
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            fixed4 _Color;
            uniform float _Vertical;
            uniform float _Horizon;

            v2f vert (appdata_full v)
            {
                float4 vv = mul( unity_ObjectToWorld, v.vertex );
                vv.xyz -= _WorldSpaceCameraPos.xyz;
                vv = float4( (vv.z * vv.z) * - _Horizon, (vv.z * vv.z) * - _Vertical, 0.0f, 0.0f );
                v.vertex += mul(unity_WorldToObject, vv);

                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.texcoord = TRANSFORM_TEX(v.texcoord, _MainTex);
                UNITY_TRANSFER_FOG(o,o.vertex);

                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.texcoord) * _Color;
                UNITY_APPLY_FOG(i.fogCoord, col);
                return col;
            }
        ENDCG
    }
}

}