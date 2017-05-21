using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

/*
 * ScreenManager -> handles which screen is updating & drawing (contains list of screens)
 * Screen -> each screen
 * TextBox
 * XML to save the players current position in game
 * Multiple accounts
 */


namespace VisualTextAdventure
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;


        List<AnimatedSprite> Wizards = new List<AnimatedSprite>();
        List<AnimatedSprite> Blocks = new List<AnimatedSprite>();
        Label MainText;
        Button startScreen;
        Sprite Background;
        Sprite BC2;
        KeyboardState prevKeyboardState;
        KeyboardState keyboardState;
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }
        protected override void Initialize()
        {

            base.Initialize();
        }
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            
            Background = new Sprite(new Vector2(425, 160), Content.Load<Texture2D>("shovel knight"), 0f, Vector2.One, Color.White, SpriteEffects.None);
            //Finished Wizard Animation on 3/26/17
            //Next class work on scaling because origin currently doesn't compensate for scale
            //Next class get a background and an enemy working

            BC2 = new Sprite(new Vector2(Background.Position.X + Background.Image.Width, Background.Position.Y), Content.Load<Texture2D>("shovel knight"), 0f, Vector2.One, Color.White, SpriteEffects.None);

            int frameWidth = 22;
            int frameHeight = 26;

            List<Frame> frames = new List<Frame>();
            List<Frame> EnemyFrames = new List<Frame>();

            frames.Add(new Frame(new Rectangle(9, 4, frameWidth, frameHeight)));
            frames.Add(new Frame(new Rectangle(41, 4, frameWidth, frameHeight)));
            frames.Add(new Frame(new Rectangle(73, 4, frameWidth, frameHeight)));
            frames.Add(new Frame(new Rectangle(105, 4, frameWidth, frameHeight)));
            frames.Add(new Frame(new Rectangle(137, 4, frameWidth, frameHeight)));
            frames.Add(new Frame(new Rectangle(169, 4, frameWidth, frameHeight)));
            frames.Add(new Frame(new Rectangle(201, 4, frameWidth, frameHeight)));
            frames.Add(new Frame(new Rectangle(233, 4, frameWidth, frameHeight)));
            frames.Add(new Frame(new Rectangle(265, 4, frameWidth, frameHeight)));

            EnemyFrames.Add(new Frame(new Rectangle(12,5,11,10)));
            EnemyFrames.Add(new Frame(new Rectangle(44,5,11,10)));
            EnemyFrames.Add(new Frame(new Rectangle(11, 36, 12, 11)));

            Wizards.Add(new AnimatedSprite(new Vector2(100, GraphicsDevice.Viewport.Height - frameHeight / 2 - 85), Content.Load<Texture2D>("New Piskel"),new Vector2(1,1), frames));
            Blocks.Add(new AnimatedSprite(new Vector2(600, GraphicsDevice.Viewport.Height - 100), Content.Load<Texture2D>("New Piskel (1)"),new Vector2(5,5), EnemyFrames));
            
            for (int i = 0; i < Wizards.Count; i++)
            {
                Wizards[i].scale = new Vector2(2, 2);
            }

        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {

            keyboardState = Keyboard.GetState();

            if (keyboardState.IsKeyDown(Keys.D) || keyboardState.IsKeyDown(Keys.Right))
            {
                if (prevKeyboardState.IsKeyUp(Keys.A) || prevKeyboardState.IsKeyUp(Keys.Left))
                {
                    Blocks[0].Position.X--;
                    for (int i = 0; i < Wizards.Count; i++)
                    {
                        Wizards[i].spriteEffects = SpriteEffects.None;
                    }
                    Background.Position.X--;
                    BC2.Position.X--;
                    
                    if(Background.Position.X + Background.Image.Width < 0)
                    {
                        Background.Position.X = BC2.Position.X + BC2.Image.Width;
                    }


                }
                Wizards[0].Update(gameTime, new Vector2(0, 0));
            }
            else if (keyboardState.IsKeyDown(Keys.A) || keyboardState.IsKeyDown(Keys.Left))
            {
                for (int i = 0; i < Wizards.Count; i++)
                {
                    Wizards[i].spriteEffects = SpriteEffects.FlipHorizontally;
                }
                

                if (prevKeyboardState.IsKeyUp(Keys.D) || prevKeyboardState.IsKeyUp(Keys.Right))
                {
                    Blocks[0].Position.X++;
                    for (int i = 0; i < Wizards.Count; i++)
                    {
                        Wizards[i].spriteEffects = SpriteEffects.FlipHorizontally;
                    }
                    Background.Position.X++;
                    BC2.Position.X++;
                }
                Wizards[0].Update(gameTime, new Vector2(0, 0));
            }

            base.Update(gameTime);
            prevKeyboardState = keyboardState;

        }
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            Background.Draw(spriteBatch);
            BC2.Draw(spriteBatch);
            Blocks[0].Draw(spriteBatch);
            Wizards[0].Draw(spriteBatch);
            spriteBatch.End();
            
            base.Draw(gameTime);
        }
    }
}
