/*
	Shader that make a distortion effect masked by an alpha texture
*/
Shader "Explosion/Explosion_VFX_Distortion_Shader"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)							// ** Additional Color
        _AlphaTex ("Alpha", 2D) = "white" {}						// ** Alpha Texture
		_NormalTex ("Normal", 2D) = "bump" {}						// ** Normal Texure
		_Magnitude("Noise Magnitude", Range(0, 1)) = 0.05			// ** Noise Magnitude
		_Distortion("Distortion", Range(0, 1)) = 0.25				// ** Distortion Intensity
	}
	SubShader
	{
		// The distortion effect must be transparent or at least semi-transparent
		Tags { "Queue" = "Transparent" "ignoreProjector" = "True" "RenderType" = "Transparent" }
		Blend SrcAlpha OneMinusSrcAlpha

		// The shader will use a Grab Pass, creating a texture with the content of the frame buffer
		GrabPass {}

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			#include "UnityCG.cginc"

			// --- textures
			sampler2D _GrabTexture;
			sampler2D _AlphaTex;
			sampler2D _NormalTex;

			// --- parameters
			fixed4 _Color;
			fixed _Magnitude;
			fixed _Distortion;

			struct vertInput
			{
				float4 vertex : POSITION;
				float2 texcoord : TEXCOORD0;
			};

			struct vertOutput
			{
				float4 vertex : POSITION;
				float2 texcoord : TEXCOORD0;
				float4 uvgrab : TEXCOORD1;					
			};

			vertOutput vert(vertInput v)
			{
				vertOutput o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uvgrab = ComputeGrabScreenPos(o.vertex);
				o.texcoord = v.texcoord;
				return o;
			}

			half4 frag(vertOutput o) : COLOR
			{
				// --- Alpha Texture
				half4 alpha_channel = tex2D(_AlphaTex, o.texcoord);

				// --- Normal Texture
				half4 normal_channel = tex2D(_NormalTex, o.texcoord);

				// --- Distortion Effect
				half2 distortion = UnpackNormal(normal_channel).rg;
				o.uvgrab.x += _Distortion*cos(_Time * (10 * _Magnitude));
				o.uvgrab.y += _Distortion*sin(_Time * (10 * _Magnitude));
				o.uvgrab.xy += 0.5*distortion * _Magnitude;
				half4 distortion_channel = tex2Dproj(_GrabTexture, UNITY_PROJ_COORD(o.uvgrab));
				distortion_channel.a = alpha_channel.a;

				// --- Final Result
				return distortion_channel*_Color;
			}
			ENDCG
		}
    }
    FallBack "Diffuse"
}
