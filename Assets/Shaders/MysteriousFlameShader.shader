Shader"Custom/MysteriousCloudShader" {
Properties {
_MainTex ("Texture", 2D) = "white" {}
_Speed ("Speed", Float) = 1.0
_Distortion ("Distortion", Float) = 1.0
}
SubShader {
Tags { "Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent" }
Blend SrcAlpha
OneMinusSrcAlpha
ZWriteOff

Pass {
CGPROGRAM
#pragma vertex vert
#pragma fragment frag
#include "UnityCG.cginc"

struct appdata_t
{
    float4 vertex : POSITION;
    float2 uv : TEXCOORD0;
};

struct v2f
{
    float2 uv : TEXCOORD0;
    float4 vertex : SV_POSITION;
};

sampler2D _MainTex;
float _Speed;
float _Distortion;

v2f vert(appdata_t v)
{
    v2f o;
    o.vertex = UnityObjectToClipPos(v.vertex);
    o.uv = v.uv;
    return o;
}

fixed4 frag(v2f i) : SV_Target
{
    float2 offset = float2(cos(_Time.y * _Speed), sin(_Time.y * _Speed)) * _Distortion;
    float2 uv = i.uv + offset;
    fixed4 col = tex2D(_MainTex, uv);
    return col;
}
ENDCG
}
}
}