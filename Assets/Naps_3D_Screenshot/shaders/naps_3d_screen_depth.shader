Shader "Custom/naps_3d_screen_depth" {

	Properties {
	_contrast ("contrast", Range (0, 2)) = 1
	_brightness ("brightness", Range (0, 1)) = 0.5
	}
    SubShader
    {
        Tags { "RenderType"="Opaque" }
		
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"
 
            struct v2f
            {
                float4 pos : SV_POSITION;
                float4 screenuv : TEXCOORD1;
            };
           
            v2f vert (appdata_base v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.screenuv = ComputeScreenPos(o.pos);
                return o;
            }
           
			fixed _contrast;
			fixed _brightness;
            sampler2D _CameraDepthTexture;
 
			half3 AdjustContrast(half3 color, half contrast) 
			{
#if !UNITY_COLORSPACE_GAMMA
				color = LinearToGammaSpace(color);
#endif
				color = saturate(lerp(half3(0.5, 0.5, 0.5), color, contrast));
#if !UNITY_COLORSPACE_GAMMA
				color = GammaToLinearSpace(color);
#endif
				return color;
			}
            fixed4 frag (v2f i) : SV_Target
            {
                float2 uv = i.screenuv.xy / i.screenuv.w;
                float depth = 1 - Linear01Depth(SAMPLE_DEPTH_TEXTURE(_CameraDepthTexture, uv));
                return fixed4(AdjustContrast(fixed3(depth*_brightness, depth*_brightness, depth*_brightness),_contrast), 1);
            }


            ENDCG
        }
    }
}