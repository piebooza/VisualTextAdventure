using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VisualTextAdventure
{

    class Fireball : AnimatedSprite
    {
        public enum State
        {
            Moving,
            Growing,
            Idle
        }

        public State state;
        public Fireball(Vector2 position, Texture2D image, Vector2 scale, List<Frame> frames, TimeSpan frameRate)
            : base(frames, position, image, 0f, scale, SpriteEffects.None, Color.White, frameRate)
        {
            state = State.Idle;
        }
        public override void Update(GameTime gameTime)
        {


            if (state == State.Growing)
            {
                if (scale.X <= 2 || scale.Y <= 2)
                {
                    scale.X += .01f;
                    scale.Y += .01f;
                    Position.X += .2f;
                    Position.Y -= .2f;
                }
            }
            if (state == State.Moving)
            {
                Position.X += 1;
                Position.Y += .01f;
            }

            base.Update(gameTime);
        }
    }
}
