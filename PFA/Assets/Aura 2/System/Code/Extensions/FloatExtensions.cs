﻿
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

using UnityEngine;

namespace Aura2API
{
    /// <summary>
    /// Static class containing extension for int type
    /// </summary>
    public static class FloatExtensions
    {
        /// <summary>
        /// Snaps the value to the closest multiple of the parameter
        /// </summary>
        /// <param name="snapValue">The reference snap value</param>
        /// <returns>The snapped value</returns>
        public static float Snap(this float value, float snapValue)
        {
            return Mathf.Round(value / snapValue) * snapValue;
        }

        /// <summary>
        /// Snaps the value to the closest multiple of the parameter but forbids the value to be lower than snapValue
        /// </summary>
        /// <param name="snapValue"></param>
        /// <returns>The snapped value</returns>
        public static float SnapMin(this float value, float snapValue)
        {
            return Mathf.Max(value.Snap(snapValue), snapValue);
        }
    }
}
