
/***************************************************************************
*                                                                          *
*  Copyright (c) Rapha�l Ernaelsten (@RaphErnaelsten)                      *
*  All Rights Reserved.                                                    *
*                                                                          *
*  NOTICE: Aura 2 is a commercial project.                                 * 
*  All information contained herein is, and remains the property of        *
*  Rapha�l Ernaelsten.                                                     *
*  The intellectual and technical concepts contained herein are            *
*  proprietary to Rapha�l Ernaelsten and are protected by copyright laws.  *
*  Dissemination of this information or reproduction of this material      *
*  is strictly forbidden.                                                  *
*                                                                          *
***************************************************************************/

//-----------------------------------------------------------------------------------------
//			SHEvalLinear
//			Evaluates the spherical harmonics bands
//-----------------------------------------------------------------------------------------
//From UnityCG.cginc
// viewDirection should be normalized, w=1.0
half3 SHEvalLinearL0L1(half4 viewDirection, half4 shAr, half4 shAg, half4 shAb)
{
    // Linear (L1) + constant (L0) polynomial terms
    half3 color;
    color.x = dot(shAr, viewDirection);
    color.y = dot(shAg, viewDirection);
    color.z = dot(shAb, viewDirection);

    return color;
}
// viewDirection should be normalized, w=1.0
half3 SHEvalLinearL2(half4 viewDirection, half4 shBr, half4 shBg, half4 shBb, half4 shC)
{
    // 4 of the quadratic (L2) polynomials
    half4 v = viewDirection.xyzz * viewDirection.yzzx;
    half3 color;
    color.r = dot(shBr, v);
    color.g = dot(shBg, v);
    color.b = dot(shBb, v);
	
    // Final (5th) quadratic (L2) polynomial
    half vC = viewDirection.x * viewDirection.x - viewDirection.y * viewDirection.y;
    color += shC.rgb * vC;

    return color;
}

//-----------------------------------------------------------------------------------------
//			EvaluateSphericalHarmonics
//			Computes the whole spherical harmonics
//-----------------------------------------------------------------------------------------
//From UnityCG.cginc
// viewDirection should be normalized, w=1.0
half3 EvaluateSphericalHarmonics(half4 viewDirection, half4 shAr, half4 shAg, half4 shAb, half4 shBr, half4 shBg, half4 shBb, half4 shC)
{
	// Linear + constant polynomial terms
    half3 color = SHEvalLinearL0L1(viewDirection, shAr, shAg, shAb);
	// Quadratic polynomials
    color += SHEvalLinearL2(viewDirection, shBr, shBg, shBb, shC);
    return color;
}