/*
	Shader that makes an object become transparent horizontally along the time
*/
Shader "Dynamite/fuse_fade"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)							// ** Additional Color
        _MainTex ("Albedo", 2D) = "white" {}						// ** Albedo Texture
		_AlphaTex("Alpha", 2D) = "white" {}							// ** Alpha Texture
		_NormalTex("Normal Map", 2D) = "bump" {}					// ** Normal Texture
		_ScrollXSpeed("X Scroll Speed", Range(0,10)) = 2			// ** Fading Speed
		_NormalIntensity("Normal Intensity", Range(0, 1)) = 1		// ** Normal Texture Intensity
		_GameTime ("Game Time", Float) = 0.2
    }

    SubShader
    {
		// To make the alpha texture work, the shader must be transparent
		Tags { "Queue" = "Transparent" "IgnoreProjector"="True" "RenderType" = "TransparentCutout" "ForceNoShadowCasting" = "True"}
        LOD 200

        CGPROGRAM

		// The shader must work with the alpha channel hiding the geometry
        #pragma surface surf Lambert alpha:fade

		// --- textures
        sampler2D _MainTex;
		sampler2D _AlphaTex;
		sampler2D _NormalTex;

		// --- parameters
		fixed4 _Color;
		fixed _ScrollXSpeed;
		fixed _NormalIntensity;
		float _GameTime;

        struct Input
        {
            float2 uv_MainTex;
			float2 uv_AlphaTex;
        };       

        UNITY_INSTANCING_BUFFER_START(Props)
        UNITY_INSTANCING_BUFFER_END(Props)

        void surf (Input IN, inout SurfaceOutput o)
        {
			// --- Albedo Texture
            fixed4 albedo_channel = tex2D (_MainTex, IN.uv_MainTex) * _Color;

			// --- Normal Texture
			float3 normal_channel = UnpackNormal(tex2D(_NormalTex, IN.uv_MainTex));
			normal_channel.xy *= _NormalIntensity;
            
			// --- Alpha Texture
			fixed2 alpha_UV = IN.uv_AlphaTex;
			fixed xScrollValue = _ScrollXSpeed * _GameTime;
			alpha_UV += fixed2(xScrollValue, IN.uv_AlphaTex.y);
			fixed4 alpha_channel = tex2D(_AlphaTex, alpha_UV);

			// --- Final Result
			o.Albedo = albedo_channel.rgb;
            o.Alpha = albedo_channel.a * alpha_channel.rgb;
			o.Normal = normalize(normal_channel.rgb);
        }
        ENDCG
    }
    FallBack "Diffuse"
}
