///----------------------------------------------------------------------------
/// Class     : Configuration
/// Purpose   : Configuratin for magnifier. 
/// Written by: Ogun TIGLI
/// History   : 31 May 2006/Wed starting date.
///             22 Dec 2006/Fri minor code fixes and hotsot support addition.
///             01 Apr 2007/Sun XML serialization support added.
///             
/// Notes: 
/// This software is provided 'as-is', without any express or implied 
/// warranty. In no event will the author be held liable for any damages 
/// arising from the use of this software.
/// 
/// Permission is granted to anyone to use this software for any purpose, 
/// including commercial applications, and to alter it and redistribute it 
/// freely, subject to the following restrictions:
///     1. The origin of this software must not be misrepresented; 
///        you must not claim that you wrote the original software. 
///        If you use this software in a product, an acknowledgment 
///        in the product documentation would be appreciated. 
///     2. Altered source versions must be plainly marked as such, and 
///        must not be misrepresented as being the original software.
///     3. This notice cannot be removed, changed or altered from any source 
///        code distribution.
/// 
///        (c) 2006-2007 Ogun TIGLI. All rights reserved. 
///----------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;

namespace Magnifier20070401
{
    /// <summary>
    /// Configuration class for the magnifier.
    /// </summary>
    public class Configuration
    {
        public Configuration()
        {
        }

        public float ZoomFactor
        {
            get { return mZoomFactor; }
            set
            {
                if (value > ZOOM_FACTOR_MAX)
                {
                    mZoomFactor = ZOOM_FACTOR_MAX;
                }
                else if (value < ZOOM_FACTOR_MIN)
                {
                    mZoomFactor = ZOOM_FACTOR_MIN;
                }
                else
                {
                    mZoomFactor = value;
                }
            }
        } private float mZoomFactor = ZOOM_FACTOR_DEFAULT;

        public float SpeedFactor
        {
            get { return mSpeedFactor; }
            set
            {
                if (value > SPEED_FACTOR_MAX)
                {
                    mSpeedFactor = SPEED_FACTOR_MAX;
                }
                else if (value < SPEED_FACTOR_MIN)
                {
                    mSpeedFactor = SPEED_FACTOR_MIN;
                }
                else
                {
                    mSpeedFactor = value;
                }
            }
        } private float mSpeedFactor = SPEED_FACTOR_DEFAULT;


        public int LocationX = -1;
        public int LocationY = -1;

        public bool CloseOnMouseUp = true;
        public bool DoubleBuffered = true;
        public bool HideMouseCursor = true;
        public bool RememberLastPoint = true;
        public bool ReturnToOrigin = true;
        public bool ShowInTaskbar = false;
        public bool TopMostWindow = true;

        public int MagnifierWidth = 150;
        public int MagnifierHeight = 150;

        public static readonly float ZOOM_FACTOR_MAX = 10.0f;
        public static readonly float ZOOM_FACTOR_MIN = 1.0f;
        public static readonly float ZOOM_FACTOR_DEFAULT = 3.0f;

        public static readonly float SPEED_FACTOR_MAX = 1.0f;
        public static readonly float SPEED_FACTOR_MIN = 0.05f;
        public static readonly float SPEED_FACTOR_DEFAULT = 0.35f;
    }
}
