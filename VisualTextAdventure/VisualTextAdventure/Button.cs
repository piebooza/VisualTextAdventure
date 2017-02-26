using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VisualTextAdventure
{
    class Button : Sprite
    {
        Vector2 position;
        Rectangle hitbox;
        Texture2D image;
        Color color;
        public Button(Vector2 Position,Texture2D Image, Color Color) :
        base(Position, Image, Color)
        {
            position = Position;
            image = Image;
            hitbox = new Rectangle((int) position.X,(int) position.Y, image.Width, image.Height);
            color = Color;
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(image, position, hitbox, color);
        }
    }
}
