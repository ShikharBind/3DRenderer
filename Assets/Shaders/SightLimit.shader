Shader "Custom/SightLimit"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        [HideInInspector]_Color ("Color", Color) = (1, 1, 0, 1)
        _Range ("Range", Range(0.01, 1)) = 1.0
        _PlayerRadius ("PlayerRadius", float) = 1.0
    }
    SubShader
    {
        Cull off
        Tags { "RenderType"="Opaque" }
        Blend SrcAlpha OneMinusSrcAlpha
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
                fixed4 color : COLOR;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 color : COLOR;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;

            fixed4 _Color;
            float _Range;
            float _PlayerRadius;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.color = v.color;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                float2 coord = i.uv;
                float4 col = tex2D(_MainTex, i.uv);
                col *= i.color;
                float x = 0.5 - coord.x;
                float y = 0.5 - coord.y;
                float dist = x*x + y*y;
                float playercircle = step(dist, 0.001 / _PlayerRadius);
                // float playercircle = 1 - 10000 * dist / _PlayerRadius;
                float maskcircle = 1 - 100 * dist / _Range;
                float FOVcontroller = 1.5;
                float maskr = step(y, 1.5*x);
                float maskl = step(y, -1.5*x);
                float mask = 1 - maskcircle * maskr * maskl - playercircle;
                return col * (mask);
            }
            ENDCG
        }
    }
}
