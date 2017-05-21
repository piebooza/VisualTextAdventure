using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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

        public AnimatedSprite(Vector2 postion, Texture2D image, Vector2 scale, List<Frame> frames)
            : this(frames, postion, image, 0f, scale, SpriteEffects.None, Color.White) { }

        public AnimatedSprite(List<Frame> frames, Vector2 Position, Texture2D Image, float Rotation, Vector2 Scale, SpriteEffects spriteEffects, Color Tint) :
            base(Position, Image, Rotation, Scale, Tint, spriteEffects)
        {
             
            this.frames = frames;
            currentFrame = 0;
            frameRate = TimeSpan.FromMilliseconds(60);
        }
        public void Update(GameTime gameTime, Vector2 Speed)
        {
            Position.X += Speed.X;
            Position.Y += Speed.Y;
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
