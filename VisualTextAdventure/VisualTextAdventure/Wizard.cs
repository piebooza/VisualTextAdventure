using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

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

        WizardState wizardState;

        public Wizard(Vector2 position, Texture2D image, Vector2 scale, List<Frame> frames, float JumpPower, TimeSpan frameRate)
            : base(frames, position, image, 0, scale, SpriteEffects.None, Color.White, frameRate)
        {
            jumpPower = JumpPower;
            ground = position.Y + FrameHeight/2;
            wizardState = WizardState.Idle;

            
        }

        public override void Update(GameTime gameTime)
        {

            keyboardState = Keyboard.GetState();
            if (keyboardState.IsKeyDown(Keys.D) || keyboardState.IsKeyDown(Keys.Right))
            {
                if (prevKeyboardState.IsKeyUp(Keys.A) || prevKeyboardState.IsKeyUp(Keys.Left))
                {


                    spriteEffects = SpriteEffects.None;
                    wizardState = WizardState.Walking;

                }

            }
            else if (keyboardState.IsKeyDown(Keys.A) || keyboardState.IsKeyDown(Keys.Left))
            {

                spriteEffects = SpriteEffects.FlipHorizontally;
                wizardState = WizardState.Walking;
                

                if (prevKeyboardState.IsKeyUp(Keys.D) || prevKeyboardState.IsKeyUp(Keys.Right))
                {
                    spriteEffects = SpriteEffects.FlipHorizontally;

                }

            }
            else
            {
                wizardState = WizardState.Idle;
            }
            if (!isAir && keyboardState.IsKeyDown(Keys.Space))
            {
                wizardState = WizardState.Jumping;
                speed = jumpPower;
                isAir = true;
            }



            if (isAir)
            {
                Position.Y -= speed;
                speed -= gravity;

                if (Position.Y + FrameHeight / 2 > ground)
                {
                    Position.Y = ground - FrameHeight / 2;
                    isAir = false;
                }
            }

            if (wizardState.Equals(WizardState.Jumping) || wizardState.Equals(WizardState.Walking))
            {
                base.Update(gameTime);
            }
        }
    }
}
