using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VisualTextAdventure
{
    class ScrollingBackground
    {
        Sprite back1;
        Sprite back2;
        float speed;
        Vector2 scale;
        KeyboardState keyboardState;
        KeyboardState prevKeyboardState;
        public ScrollingBackground(Sprite Back1, Sprite Back2, float Speed, Vector2 Scale)
        {
            back1 = Back1;
            back2 = Back2;
            speed = Speed;
            scale = Scale;
            //scale.X *= back1.Image.Width;
            //scale.X *= back2.Image.Width;
            //scale.Y *= back1.Image.Height;
            //scale.Y *= back2.Image.Height;

            back1.scale = scale;
            back2.scale = scale;
        }

        public void Update(Viewport screen)
        {
            keyboardState = Keyboard.GetState();

            if (keyboardState.IsKeyDown(Keys.D) || keyboardState.IsKeyDown(Keys.Right))
            {
                if (prevKeyboardState.IsKeyUp(Keys.A) || prevKeyboardState.IsKeyUp(Keys.Left))
                {
                    back1.Position.X -= speed;
                    back2.Position.X -= speed;
                }
            }
            if (keyboardState.IsKeyDown(Keys.A) || keyboardState.IsKeyDown(Keys.Left))
            {
                if (prevKeyboardState.IsKeyUp(Keys.D) || prevKeyboardState.IsKeyUp(Keys.Right))
                {
                    back1.Position.X += speed;
                    back2.Position.X += speed;
                }
            }
            if (back1.Position.X + back1.Width / 2 <= screen.Width)
            {
                back2.Position.X = (back1.Position.X + back1.Width / 2) + back2.Width / 2;
            }

            if (back2.Position.X + back2.Width / 2 <= screen.Width)
            {
                back1.Position.X = (back2.Position.X + back2.Width / 2) + back1.Width / 2;
            }

            if (back1.Position.X - back1.Width / 2 >= 0)
            {
                back2.Position.X = (back1.Position.X - back1.Width / 2) - back2.Width / 2;
            }

            if (back2.Position.X - back2.Width / 2 >= 0)
            {
                back1.Position.X = (back2.Position.X - back2.Width / 2) - back1.Width / 2;
            }
            //if the left side of the image > 0
            //set the right side of the second image = left side of the first image




        }

        public void Draw(SpriteBatch spriteBatch)
        {
            back1.Draw(spriteBatch);
            back2.Draw(spriteBatch);
        }
    }
}
