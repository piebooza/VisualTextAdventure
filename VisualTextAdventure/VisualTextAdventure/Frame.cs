using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VisualTextAdventure
{
    class Frame
    {
        Rectangle rect;

        Vector2 origin;
        public Vector2 Origin
        {
            get
            {
                return origin;
            }
        }
        public Rectangle Bounds
        {
            get
            {
                return rect;
            }
        }


        public Frame(Rectangle Rect)
        {
            rect = Rect;
            origin = new Vector2(rect.Width/2, rect.Height/2);
        }
    }
}
