Shader "Custom/Sprite Outline" {
    Properties {
        _MainTex ("Base (RGB)", 2D) = "white" {}
        _Color ("Color", Color) = (1, 1, 1, 1)
        [MaterialToggle] _RenderObject("Render Object", Float) = 0
        _Distance ("Distance", Float) = 1
    }
    SubShader {
        Tags {"Queue"="Transparent" "RenderType"="Transparent"}
        Cull Off
        Blend One OneMinusSrcAlpha
       
        Pass {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"
 
            sampler2D _MainTex;
 
            struct v2f {
                float4 pos : SV_POSITION;
                half2 uv : TEXCOORD0;
            };
 
            v2f vert(appdata_base v) {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv = v.texcoord;
                return o;
            }
 
            fixed4 _Color;
            float4 _MainTex_TexelSize;
            float _RenderObject;
            float _Distance;
            
            fixed4 _transparent = fixed4(0,0,0,0);
 
            fixed4 frag(v2f i) : COLOR
            {
                half4 c = tex2D(_MainTex, i.uv);
                c.rgb *= c.a;
                half4 outlineC = _Color;
                
                fixed is_transparent = 1-floor(c.a);
 
                fixed2 north = fixed2(0, _MainTex_TexelSize.y * _Distance);
                fixed2 south = fixed2(0, -_MainTex_TexelSize.y * _Distance);
                fixed2 east = fixed2(_MainTex_TexelSize.x * _Distance, 0);
                fixed2 west = fixed2(-_MainTex_TexelSize.x * _Distance, 0);
 
                fixed has_opaque_neighbor = 1 - tex2D(_MainTex, i.uv + north).a;
                has_opaque_neighbor *= 1 - tex2D(_MainTex, i.uv + south).a;
                has_opaque_neighbor *= 1 - tex2D(_MainTex, i.uv + east).a;
                has_opaque_neighbor *= 1 - tex2D(_MainTex, i.uv + west).a;
                has_opaque_neighbor *= 1 - tex2D(_MainTex, i.uv + north + east).a;
                has_opaque_neighbor *= 1 - tex2D(_MainTex, i.uv + north + west).a;
                has_opaque_neighbor *= 1 - tex2D(_MainTex, i.uv + south + east).a;
                has_opaque_neighbor *= 1 - tex2D(_MainTex, i.uv + south + west).a;
                has_opaque_neighbor = 1 - has_opaque_neighbor;
                
                fixed is_outline = is_transparent * has_opaque_neighbor;
 
                c = lerp(_transparent, c, _RenderObject);
                return lerp(c, outlineC, is_outline);
            }  
 
            ENDCG
        }
    }
    FallBack "Diffuse"
}