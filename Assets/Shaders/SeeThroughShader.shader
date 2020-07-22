Shader "Custom/SeeThroughShader"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0
    }

	CGINCLUDE
	bool IsPlaneBetweenPoints(float3 planeCenter, float3 planeNormal, float3 p1, float3 p2)
	{
		float s1 = dot(p1 - planeCenter, planeNormal);
		float s2 = dot(p2 - planeCenter, planeNormal);
		return !(s1 > 0.0f && s2 > 0.0f || s1 < 0.0f && s2 < 0.0f);
	}
	ENDCG

    SubShader
    {
        Tags { "Queue"="Transparent" "RenderType"="Transparent" }
        LOD 200

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard fullforwardshadows alpha:fade

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        sampler2D _MainTex;

        struct Input
        {
            float2 uv_MainTex;
			float3 worldPos;
			float3 worldNormal;
        };

        half _Glossiness;
        half _Metallic;
        fixed4 _Color;
		fixed4 _PlayerPosition;

        // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
        // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
        // #pragma instancing_options assumeuniformscaling
        UNITY_INSTANCING_BUFFER_START(Props)
            // put more per-instance properties here
        UNITY_INSTANCING_BUFFER_END(Props)

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            // Albedo comes from a texture tinted by color
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
            o.Albedo = c.rgb;
            // Metallic and smoothness come from slider variables
            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;

			float3 planeCenter = IN.worldPos;
			float3 planeNormal = IN.worldNormal;
			float3 p1 = _PlayerPosition;
			float3 p2 = _WorldSpaceCameraPos;
			float s1 = dot(p1 - planeCenter, planeNormal);
			float s2 = dot(p2 - planeCenter, planeNormal);
			bool betweenPoints = !(s1 > 0.0f && s2 > 0.0f || s1 < 0.0f && s2 < 0.0f);

			// only apply this logic to walls that are sort of close to the player.
			float playerDistance = distance(IN.worldPos, _PlayerPosition);
			float t = clamp(playerDistance / 30.0f, 0.0f, 1.0f);
			t = pow(t, 3);

			float minAlpha = 0.0f;// 0.25f;
			minAlpha = minAlpha * (1.0f - t) + t;
			float maxAlpha = 1.0f;
			if (betweenPoints) {
				o.Alpha = minAlpha;
			}
			else {
				// s1 is the distance from the plane, so we'll LERP based on that
				t = abs(s1 / 5.0f);
				if (t > 1.0f)
					t = 1.0f;
				o.Alpha = maxAlpha * t + minAlpha * (1.0f - t);
			}
        }
        ENDCG
    }
    FallBack "Diffuse"
}
