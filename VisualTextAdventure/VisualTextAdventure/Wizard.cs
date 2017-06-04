using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VisualTextAdventure
{
    class Wizard : AnimatedSprite
    {
        KeyboardState keyboardState;
        KeyboardState prevKeyboardState;
        float speed = 0;
        float ground;
        float jumpPower;
        float gravity = 0.1f;

        bool isAir = false;

        public Wizard(Vector2 position, Texture2D image, Vector2 scale, List<Frame> frames, float JumpPower)
            : base(frames, position, image, 0, scale, SpriteEffects.None, Color.White)
        {
            jumpPower = JumpPower;
            ground = position.Y + FrameHeight/2;
        }

        public override void Update(GameTime gameTime)
        {

            keyboardState = Keyboard.GetState();
            if (!isAir && keyboardState.IsKeyDown(Keys.Space))
            {
                speed = jumpPower;
                isAir = true;
            }

            if (isAir)
            {
                Position.Y -= speed;
                speed -= gravity;

                if (Position.Y + FrameHeight/2 > ground)
                {
                    Position.Y = ground - FrameHeight/2;
                    isAir = false;
                }
            }
            base.Update(gameTime);
        }

    }
}
