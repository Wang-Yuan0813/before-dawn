// 示例：简单的高斯模糊 Shader
Shader "UI/Blur"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _BlurSize ("Blur Size", Range(0, 10)) = 1
    }
    SubShader
    {
        CGINCLUDE
        #include "UnityCG.cginc"
        sampler2D _MainTex;
        float _BlurSize;
        ENDCG

        // 水平模糊 Pass
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag_horizontal
            struct appdata { float4 vertex : POSITION; float2 uv : TEXCOORD0; };
            struct v2f { float2 uv : TEXCOORD0; float4 vertex : SV_POSITION; };
            v2f vert(appdata v) { v2f o; o.vertex = UnityObjectToClipPos(v.vertex); o.uv = v.uv; return o; }
            float4 frag_horizontal(v2f i) : SV_Target
            {
                float4 color = 0;
                float2 uv = i.uv;
                // 采样左右像素（高斯权重简化版）
                color += tex2D(_MainTex, uv + float2(_BlurSize * 0.001, 0)) * 0.25;
                color += tex2D(_MainTex, uv) * 0.5;
                color += tex2D(_MainTex, uv - float2(_BlurSize * 0.001, 0)) * 0.25;
                return color;
            }
            ENDCG
        }
        // 垂直模糊 Pass（类似水平，方向改为 y 轴）
    }
}