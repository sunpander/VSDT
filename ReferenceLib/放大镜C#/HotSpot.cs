///----------------------------------------------------------------------------
/// Class     : HotSpot
/// Purpose   : Hotspot suport.
/// Written by: Ogun TIGLI
/// History   : 22 Dec 2006/Fri starting date.
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
using System.Drawing;
using System.Windows.Forms;

namespace Magnifier20070401
{
    class HotSpot
    {
        public delegate void MouseEventDelegate(Object sender);
        public event MouseEventDelegate OnMouseDown = null;
        public event MouseEventDelegate OnMouseUp = null;
        public event MouseEventDelegate OnMouseMove = null;

        public HotSpot(Rectangle clientRectangle)
        {
            mClientRectangle = clientRectangle;
        }

        public bool ProcessMouseMove(MouseEventArgs e)
        {
            if (mClientRectangle.Contains(e.X, e.Y)) {
           
                if (OnMouseMove != null)
                    OnMouseMove(this);

                return true;
            }
            
            return false;
        }

        public bool ProcessMouseDown(MouseEventArgs e)
        {
            if (mClientRectangle.Contains(e.X, e.Y))
            {

                if (OnMouseDown != null)
                    OnMouseDown(this);

                return true;
            }

            return false;
        }

        public bool ProcessMouseUp(MouseEventArgs e)
        {
            if (mClientRectangle.Contains(e.X, e.Y))
            {

                if (OnMouseUp != null)
                    OnMouseUp(this);

                return true;
            }

            return false;
        }
        
        //--- Data Members ---
        private Rectangle mClientRectangle;
    }
}
