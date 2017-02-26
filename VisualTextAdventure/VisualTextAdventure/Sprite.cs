using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VisualTextAdventure
{
    class Sprite
    {
        Vector2 position;
        Texture2D image;
        Color color;
        public Sprite(Vector2 Position, Texture2D Image, Color Color)
        {
            position = Position;
            image = Image;
            color = Color;
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(image, position, color);
        }
    }
}
