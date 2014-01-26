Shader "Hidden/FadeToColor"
{
    Properties
    {
        _MainTex ("Base", 2D) = "white" {}
        _Color ("Color", Color) = (0, 0, 0, 0)
        _Opacity ("Opacity", Float) = 0
    }

    CGINCLUDE

    #include "UnityCG.cginc"

    sampler2D _MainTex;
    half4 _Color;
    half _Opacity;
    
    half4 frag (v2f_img i) : COLOR
    {
        half4 c = tex2D (_MainTex, i.uv);
        return lerp (c, _Color, _Opacity);
    }

    ENDCG

    SubShader
    {
        ZTest Always Cull Off ZWrite Off
        Fog { Mode off }  
        Pass
        {
            CGPROGRAM
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma vertex vert_img
            #pragma fragment frag
            ENDCG
        }
    } 
    FallBack off
}
