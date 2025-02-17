
/***************************************************************************
*                                                                          *
*  Copyright (c) Raphaël Ernaelsten (@RaphErnaelsten)                      *
*  All Rights Reserved.                                                    *
*                                                                          *
*  NOTICE: Aura 2 is a commercial project.                                 * 
*  All information contained herein is, and remains the property of        *
*  Raphaël Ernaelsten.                                                     *
*  The intellectual and technical concepts contained herein are            *
*  proprietary to Raphaël Ernaelsten and are protected by copyright laws.  *
*  Dissemination of this information or reproduction of this material      *
*  is strictly forbidden.                                                  *
*                                                                          *
***************************************************************************/

struct appdata_t
{
	float4 vertex : POSITION;
	fixed4 color : COLOR;
	float2 texcoord : TEXCOORD0;
};

struct v2f
{
	float4 vertex : SV_POSITION;
	fixed4 color : COLOR;
	float2 texcoord : TEXCOORD0;
	float4 projPos : TEXCOORD2;
    float3 frustumSpacePosition : TEXCOORD3;
};

v2f vert(appdata_t v)
{
	v2f o;

	o.vertex = UnityObjectToClipPos(v.vertex);
	
    o.projPos = ComputeScreenPos(o.vertex);
	COMPUTE_EYEDEPTH(o.projPos.z);
	
	o.color = v.color;

	o.texcoord = TRANSFORM_TEX(v.texcoord, _MainTex);

	#if defined(AURA)
    o.frustumSpacePosition = Aura2_GetFrustumSpaceCoordinates(v.vertex);
		
	#if defined(_USAGESTAGE_VERTEX)
		#if defined(_USAGETYPE_LIGHT) || defined(_USAGETYPE_BOTH)
			Aura2_ApplyLighting(o.color.xyz, o.frustumSpacePosition, _LightingFactor);
		#endif
		#if defined(_USAGETYPE_FOG) || defined(_USAGETYPE_BOTH)
	
			//// Debug fog only
			#if defined(AURA_DISPLAY_VOLUMETRIC_LIGHTING_ONLY)
			o.color.xyz = float3(0.0f,0.0f,0.0f);
			#endif

			Aura2_ApplyFog(o.color, o.frustumSpacePosition);
		#endif
	#endif
	#endif

	return o;
}

fixed4 frag(v2f i) : SV_Target
{
	float sceneZ = LinearEyeDepth(SAMPLE_DEPTH_TEXTURE_PROJ(_CameraDepthTexture, UNITY_PROJ_COORD(i.projPos)));
	float partZ = i.projPos.z;
	float fade = saturate(InverseLerp( 0, _SoftParticleDistanceFade, (sceneZ - partZ)));
	i.color.a *= fade;
	
	fixed4 textureColor = tex2D(_MainTex, i.texcoord);
	#if defined(ALPHA_TEST)
	clip(textureColor.w - _ClipValue);
	#endif
	fixed4 col = i.color * _TintColor * textureColor;

	#if defined(AURA)
	#if defined(_USAGESTAGE_PIXEL)
        #if defined(_USAGETYPE_LIGHT) || defined(_USAGETYPE_BOTH)
				Aura2_ApplyLighting(col.xyz, i.frustumSpacePosition, _LightingFactor);
        #endif
        #if defined(_USAGETYPE_FOG) || defined(_USAGETYPE_BOTH)
	
				//// Debug fog only
				#if defined(AURA_DISPLAY_VOLUMETRIC_LIGHTING_ONLY)
				col.xyz = float3(0.0f,0.0f,0.0f);
				#endif

				Aura2_ApplyFog(col, i.frustumSpacePosition);
        #endif
	#endif
	#endif

	PREMULTIPLY_ALPHA(col)

	return col;
}