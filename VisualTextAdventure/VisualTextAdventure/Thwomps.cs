using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VisualTextAdventure
{
    class Thwomps : AnimatedSprite
    {
        float speed = 0;
        float ground;
        float jumpPower;
        float gravity = 0.1f;
        bool isAir = false;
        MouseState mouseState;
        KeyboardState keyboardState;

        public Thwomps(Vector2 position, Texture2D image, Vector2 scale, List<Frame> frames, float JumpPower, TimeSpan frameRate)
            : base(frames, position, image, 0, scale, SpriteEffects.None, Color.White, frameRate)
        {

            jumpPower = JumpPower;
            ground = position.Y + FrameHeight / 2;
        }
        public override void Update(GameTime gameTime)
        {
            keyboardState = Keyboard.GetState();
            mouseState = Mouse.GetState();

            if (!isAir)
            {
                speed = jumpPower;
                isAir = true;

            }
            else if (isAir)
            {   
                 
                  
                }
            }

        }
    }
    

