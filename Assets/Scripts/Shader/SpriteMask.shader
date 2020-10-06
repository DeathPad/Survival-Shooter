Shader "Unlit/SpriteMask"
{
	Properties
	{
		[PerRendererData] _MainTex("Sprite Texture", 2D) = "white" {}

		_ColorPrim("Primary Color (R)", Color) = (1,1,1,1)
		_ColorSec("Secondary Color (G)", Color) = (1,1,1,1)
		_ColorTert("Tertiary Color (B)", Color) = (1,1,1,1)
		[MaterialToggle] PixelSnap("Pixel snap", Float) = 0
		[MaterialToggle] KEEPBLACK("Keep Black Pixels", Float) = 0
	}

	SubShader
	{
		Tags { "Queue" = "Transparent" "IgnoreProjector" = "True" "RenderType" = "Transparent" "PreviewType" = "Plane" "CanUseSpriteAtlas" = "True" }

		Cull Off
		Lighting Off
		ZWrite Off
		Blend One OneMinusSrcAlpha

		Pass
		{
			CGPROGRAM
			
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile _ PIXELSNAP_ON
			#pragma multi_compile _ KEEPBLACK_ON
			#include "UnityCG.cginc"

			struct appdata_t
			{
				float4 vertex   : POSITION;
				float4 color    : COLOR;
				float2 texcoord : TEXCOORD0;
			};

			struct v2f
			{
				float4 vertex   : SV_POSITION;
				fixed4 color : COLOR;
				float2 texcoord  : TEXCOORD0;
			};

			fixed4 _Color;

			v2f vert(appdata_t IN)
			{
				v2f OUT;
				OUT.vertex = UnityObjectToClipPos(IN.vertex);
				OUT.texcoord = IN.texcoord;
				OUT.color = IN.color * _Color;
				#ifdef PIXELSNAP_ON
				OUT.vertex = UnityPixelSnap(OUT.vertex);
				#endif

				return OUT;
			}

			sampler2D _MainTex;
			sampler2D _AlphaTex;
			float _AlphaSplitEnabled;
			float4 _ColorPrim, _ColorSec, _ColorTert;

			fixed4 SampleSpriteTexture(float2 uv)
			{
				fixed4 color = tex2D(_MainTex, uv);

	#if UNITY_TEXTURE_ALPHASPLIT_ALLOWED
				if (_AlphaSplitEnabled)
					color.a = tex2D(_AlphaTex, uv).r;
	#endif //UNITY_TEXTURE_ALPHASPLIT_ALLOWED

				return color;
			}

			fixed4 frag(v2f IN) : SV_Target
			{
				// sprite texture
				fixed4 c = SampleSpriteTexture(IN.texcoord);

			// all color channels multiplied with a new color
				float4 colorPrim = c.r * _ColorPrim;
				float4 colorSec = c.g * _ColorSec;
				float4 colorTert = c.b * _ColorTert;
				// added together
				float4 colorResult = colorPrim + colorSec + colorTert;
				// keep black color for outlines
#ifdef KEEPBLACK_ON
				colorResult += c * (1 - (c.r + c.g + c.b));
#endif
				// multiply with alpha
				colorResult *= c.a;
				return colorResult;
			}
			
			ENDCG
		}
	}
}