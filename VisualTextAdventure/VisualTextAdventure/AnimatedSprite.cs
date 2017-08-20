using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VisualTextAdventure
{
    class AnimatedSprite : Sprite
    {
        List<Frame> frames;
        int currentFrame;
        TimeSpan frameTimer;
        TimeSpan frameRate;

        //List Frames
        //current frame: Frame object or index
        //udpate to update animation
        //override sprites draw function

        public float FrameHeight
        {
            get
            {
                return frames[currentFrame].Bounds.Height * scale.Y;
            }
        }

        public Rectangle Hitbox
        {
            get
            {
                return new Rectangle((int)(Position.X - frames[currentFrame].Origin.X * scale.X), (int)(Position.Y - frames[currentFrame].Origin.Y * scale.Y), (int)(frames[currentFrame].Bounds.Width * scale.X), (int)(frames[currentFrame].Bounds.Height * scale.Y));
            }
        }


        public AnimatedSprite(List<Frame> frames, Vector2 Position, Texture2D Image, float Rotation, Vector2 Scale, SpriteEffects spriteEffects, Color Tint, TimeSpan frameRate) :
            base(Position, Image, Rotation, Scale, Tint, spriteEffects)
        {

            this.frames = frames;
            currentFrame = 0;
            this.frameRate = frameRate;
        }
        public virtual void Update(GameTime gameTime)
        {

            frameTimer += gameTime.ElapsedGameTime;
            if (frameTimer >= frameRate)
            {
                frameTimer = TimeSpan.Zero;
                currentFrame++;
                if (currentFrame >= frames.Count)
                {
                    currentFrame = 0;
                }
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Image, Position, frames[currentFrame].Bounds, color, rotation, frames[currentFrame].Origin, scale, spriteEffects, 0f);
        }
    }
}
