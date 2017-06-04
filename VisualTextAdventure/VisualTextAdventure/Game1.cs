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
 * 
 * Fix Screen scrolling
 * Add Enemy Jump physics
 */


namespace VisualTextAdventure
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;


        Wizard wizard;
        List<AnimatedSprite> Blocks = new List<AnimatedSprite>();
        Label MainText;
        Button startScreen;
        ScrollingBackground Background;
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

            Background = new ScrollingBackground(new Sprite(new Vector2(0, 112), Content.Load<Texture2D>("shovel knight")), new Sprite(new Vector2(0, 112), Content.Load<Texture2D>("shovel knight")), 3, new Vector2(1.5f, 1.2f));

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

            EnemyFrames.Add(new Frame(new Rectangle(12, 5, 11, 10)));
            EnemyFrames.Add(new Frame(new Rectangle(44, 5, 11, 10)));
            EnemyFrames.Add(new Frame(new Rectangle(11, 36, 12, 11)));

            wizard = new Wizard(new Vector2(100, GraphicsDevice.Viewport.Height - frameHeight / 2 - 85), Content.Load<Texture2D>("New Piskel"), new Vector2(2, 2), frames, 3);
            //Blocks.Add(new AnimatedSprite(new Vector2(600, GraphicsDevice.Viewport.Height - 90), Content.Load<Texture2D>("New Piskel (1)"), new Vector2(3, 3), EnemyFrames, 3));





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
                    //Blocks[0].Position.X--;

                    wizard.spriteEffects = SpriteEffects.None;

                }
                
            }
            else if (keyboardState.IsKeyDown(Keys.A) || keyboardState.IsKeyDown(Keys.Left))
            {

                wizard.spriteEffects = SpriteEffects.FlipHorizontally;



                if (prevKeyboardState.IsKeyUp(Keys.D) || prevKeyboardState.IsKeyUp(Keys.Right))
                {
                    //Blocks[0].Position.X++; ;

                    wizard.spriteEffects = SpriteEffects.FlipHorizontally;

                }
                
            }
            wizard.Update(gameTime);
            Background.Update(GraphicsDevice.Viewport);
            base.Update(gameTime);
            prevKeyboardState = keyboardState;

        }
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            Background.Draw(spriteBatch);
            //Blocks[0].Draw(spriteBatch);
            wizard.Draw(spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
