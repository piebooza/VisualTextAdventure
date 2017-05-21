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
        // determine if these should be protected or private

        public float rotation;
        public Color color;
        public Vector2 Position;
        public Vector2 origin;
        public Vector2 scale;
        public SpriteEffects spriteEffects;


        private Texture2D image;
        public Texture2D Image
        {
            get
            {
                return image;
            }
        }

        public Sprite(Vector2 position, Texture2D image) : this(position, image, 0f, Vector2.One, Color.White, SpriteEffects.None) { }

        public Sprite(Vector2 Position, Texture2D Image, float Rotation, Vector2 Scale, Color Color, SpriteEffects spriteeffects)
        {
            this.Position = Position;
            image = Image;
            rotation = Rotation;
            scale = Scale;
            origin = new Vector2(image.Width / 2, image.Height / 2);
            color = Color;
            spriteEffects = spriteeffects;
        }

        /// <summary>
        /// Draws the sprite
        /// </summary>
        /// <param name="spriteBatch">The thing that draws</param>
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(image, Position, null, color, rotation, origin, scale, spriteEffects, 0f);
        }
    }
}
