Shader "Hidden/BWShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
	    _ColorMask ("Main Color", Color) = (1,1,1,1)
	    _Dithering ("Dithering", Range(0.0, 1.0)) = 0.6
		_Noise("Noise", Range(0.0, 1.0)) = 0.05
		_ColorContribution("Color Contribution", Range(0.0, 1.0)) = 0.35
		_Color1 ("Color 1", Color) = (1, 1, 1, 1)
		_Color2 ("Color 2", Color) = (1, 0.75, 1, 1)
    }
    SubShader
    {
        // No culling or depth
        Cull Off ZWrite Off ZTest Always

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
				float4 color : COLOR;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
				float4 color : COLOR;
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
				o.color = v.color;
                return o;
            }

            sampler2D _MainTex;
			float4 _ColorMask;
			float _ColorContribution;
			float _Dithering;
			float _Noise;
			float4 _Color1;
			float4 _Color2;

			float random(float2 uv)
			{
				return frac(sin(dot(uv, float2(12.9898 + _SinTime[1], 78.233)))*43758.5453123);
			}

			float brightness(float3 rgb)
			{
				float bmax = max(max(rgb[0], rgb[1]), rgb[2]);
				float bmin = min(min(rgb[0], rgb[1]), rgb[2]);
				return bmax;
			}

			float2 toScreenSpace(float2 uv, float width, float height)
			{
				return float2(int(uv[0] * width), int(uv[1] * height));
			}

			float dither(float2 xy, float v, int minor, int major)
			{
				bool evenRow = xy[0] % 2;
				bool evenCol = xy[1] % 2;
				if ((evenRow == true && evenCol == true) || (evenRow == false && evenCol == false)) {
					return float(int(v * minor)) / minor;
				}
				else {
					return float(int(v * major)) / major;
				}
			}

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);

                // calculate lightness
				float b = brightness(col.rgb);
				float2 xy = toScreenSpace(i.uv, 320, 240);
				float ditherValue = dither(xy, b, 8, 4);
				float randomValue = random(xy);
				float3 baseColor1 = float3(1, 1, 1);
				float3 baseColor2 = float3(1, .5, 1);
				col.rgb = col.rgb * _ColorContribution;
				bool evenRow = xy[0] % 2;
				bool evenCol = xy[1] % 2;
				if ((evenRow == true && evenCol == true) || (evenRow == false && evenCol == false)) {
					col.rgb = col.rgb + _Dithering * ditherValue * _Color2;
				}
				else {
					col.rgb = col.rgb + _Dithering * ditherValue * _Color1;
				}
				col.rgb = col.rgb + _Noise * randomValue;
				col.rgb = i.color[3] * col.rgb;
                return col;
            }
            ENDCG
        }
    }
}
