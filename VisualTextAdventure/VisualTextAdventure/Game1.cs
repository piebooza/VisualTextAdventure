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
        Sprite EndScreen;
        List<AnimatedSprite> Thwomps = new List<AnimatedSprite>();
        Label MainText;
        Button startScreen;
        ScrollingBackground Background;
        KeyboardState prevKeyboardState;
        KeyboardState keyboardState;

        List<Sprite> Hearts;
        List<Sprite> HalfHearts;
        List<Sprite> NoHearts;
        int LifeTotal = 5;
        TimeSpan elapsed = TimeSpan.Zero;

        float speed = 5;
        float ground;

        float jumpPower;
        float gravity = 0.1f;

        bool isAir = false;

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

            Background = new ScrollingBackground(new Sprite(new Vector2(0, 112), Content.Load<Texture2D>("shovel knight")), new Sprite(new Vector2(0, 112), Content.Load<Texture2D>("shovel knight")), 2, new Vector2(1.5f, 1.2f));

            EndScreen = new Sprite(new Vector2(GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 2), Content.Load<Texture2D>("dead"), 0f, new Vector2(1.7f, 1.7f), Color.White, SpriteEffects.None);

            int frameWidth = 22;
            int frameHeight = 26;

            List<Frame> frames = new List<Frame>();
            List<Frame> EnemyFrames = new List<Frame>();
            Hearts = new List<Sprite>();
            HalfHearts = new List<Sprite>();
            NoHearts = new List<Sprite>();

            Hearts.Add(new Sprite(new Vector2(20, 20), Content.Load<Texture2D>("Heart"), 0, new Vector2(.2f, .2f), Color.White, SpriteEffects.None));
            Hearts.Add(new Sprite(new Vector2(Hearts[0].Position.X + Hearts[0].Image.Width * Hearts[0].scale.X, 20), Content.Load<Texture2D>("Heart"), 0, new Vector2(.2f, .2f), Color.White, SpriteEffects.None));
            Hearts.Add(new Sprite(new Vector2(Hearts[1].Position.X + Hearts[1].Image.Width * Hearts[1].scale.X, 20), Content.Load<Texture2D>("Heart"), 0, new Vector2(.2f, .2f), Color.White, SpriteEffects.None));
            Hearts.Add(new Sprite(new Vector2(Hearts[2].Position.X + Hearts[2].Image.Width * Hearts[2].scale.X, 20), Content.Load<Texture2D>("Heart"), 0, new Vector2(.2f, .2f), Color.White, SpriteEffects.None));
            Hearts.Add(new Sprite(new Vector2(Hearts[3].Position.X + Hearts[3].Image.Width * Hearts[3].scale.X, 20), Content.Load<Texture2D>("Heart"), 0, new Vector2(.2f, .2f), Color.White, SpriteEffects.None));

            NoHearts.Add(new Sprite(new Vector2(20, 20), Content.Load<Texture2D>("Emp_Heart"), 0, new Vector2(.095f, .095f), Color.White, SpriteEffects.None));
            NoHearts.Add(new Sprite(new Vector2(NoHearts[0].Position.X + NoHearts[0].Image.Width * NoHearts[0].scale.X, 20), Content.Load<Texture2D>("Emp_Heart"), 0, new Vector2(.095f, .095f), Color.White, SpriteEffects.None));
            NoHearts.Add(new Sprite(new Vector2(NoHearts[1].Position.X + NoHearts[1].Image.Width * NoHearts[1].scale.X, 20), Content.Load<Texture2D>("Emp_Heart"), 0, new Vector2(.095f, .095f), Color.White, SpriteEffects.None));
            NoHearts.Add(new Sprite(new Vector2(NoHearts[2].Position.X + NoHearts[2].Image.Width * NoHearts[2].scale.X, 20), Content.Load<Texture2D>("Emp_Heart"), 0, new Vector2(.095f, .095f), Color.White, SpriteEffects.None));
            NoHearts.Add(new Sprite(new Vector2(NoHearts[3].Position.X + NoHearts[3].Image.Width * NoHearts[3].scale.X, 20), Content.Load<Texture2D>("Emp_Heart"), 0, new Vector2(.095f, .095f), Color.White, SpriteEffects.None));

            frames.Add(new Frame(new Rectangle(9, 4, frameWidth, frameHeight)));
            frames.Add(new Frame(new Rectangle(41, 4, frameWidth, frameHeight)));
            frames.Add(new Frame(new Rectangle(73, 4, frameWidth, frameHeight)));
            frames.Add(new Frame(new Rectangle(105, 4, frameWidth, frameHeight)));
            frames.Add(new Frame(new Rectangle(137, 4, frameWidth, frameHeight)));
            frames.Add(new Frame(new Rectangle(169, 4, frameWidth, frameHeight)));
            frames.Add(new Frame(new Rectangle(201, 4, frameWidth, frameHeight)));
            frames.Add(new Frame(new Rectangle(233, 4, frameWidth, frameHeight)));
            frames.Add(new Frame(new Rectangle(265, 4, frameWidth, frameHeight)));

            EnemyFrames.Add(new Frame(new Rectangle(15, 88, 15, 18)));
            EnemyFrames.Add(new Frame(new Rectangle(37, 94, 20, 12)));
            EnemyFrames.Add(new Frame(new Rectangle(64, 91, 19, 15)));
            EnemyFrames.Add(new Frame(new Rectangle(90, 91, 18, 15)));
            EnemyFrames.Add(new Frame(new Rectangle(115, 89, 17, 17)));
            EnemyFrames.Add(new Frame(new Rectangle(139, 89, 17, 17)));


            wizard = new Wizard(new Vector2(100, GraphicsDevice.Viewport.Height - frameHeight / 2 - 90), Content.Load<Texture2D>("New Piskel"), new Vector2(2, 2), frames, 3, TimeSpan.FromMilliseconds(60));

            Thwomps.Add(new AnimatedSprite(EnemyFrames, new Vector2(400, GraphicsDevice.Viewport.Height - frameHeight / 2 - 90), Content.Load<Texture2D>("Thwomp"), 0, new Vector2(3, 3), SpriteEffects.None, Color.White, TimeSpan.FromMilliseconds(100)));





            ground = Thwomps[0].Position.Y + Thwomps[0].FrameHeight / 2;

        }

        protected override void UnloadContent()
        {
        }


        protected override void Update(GameTime gameTime)
        {
            if(keyboardState.IsKeyDown(Keys.A) || keyboardState.IsKeyDown(Keys.Left))
            {
                for (int x = 0; x < Thwomps.Count; x++)
                {
                    Thwomps[x].Position.X++;
                }
            }
            if (keyboardState.IsKeyDown(Keys.D) || keyboardState.IsKeyDown(Keys.Right))
            {
                for (int x = 0; x < Thwomps.Count; x++)
                {
                    Thwomps[x].Position.X--;
                }
            }



            if (isAir)
            {
                Thwomps[0].Position.Y -= speed;
                speed -= gravity;

                if (Thwomps[0].Position.Y + Thwomps[0].FrameHeight / 2 >= ground)
                {
                    Thwomps[0].Position.Y = ground - Thwomps[0].FrameHeight / 2 ;
                    isAir = false;
                }
            }

            if (!isAir)
            {
                jumpPower = speed;
                isAir = true;
            }
            wizard.Update(gameTime);
            Thwomps[0].Update(gameTime);
            Background.Update(GraphicsDevice.Viewport);

            prevKeyboardState = keyboardState;


            elapsed += gameTime.ElapsedGameTime;
            if (elapsed.TotalMilliseconds >= 1000)
            {
                elapsed = TimeSpan.Zero;

            }

            base.Update(gameTime);
        }




        protected override void Draw(GameTime gameTime)
        {

            if (LifeTotal > 0)
            {

                spriteBatch.Begin();

                Background.Draw(spriteBatch);

                wizard.Draw(spriteBatch);

                

                if (isAir == false)
                {
                    Thwomps[0].Draw(spriteBatch);
                }
                
                if(isAir == true)
                {
                    for (int x = 0; x < Thwomps.Count; x++)
                    {
                        Thwomps[x].Draw(spriteBatch);
                    }
                }

                for (int i = 0; i < 5; i++)
                {

                    NoHearts[i].Draw(spriteBatch);
                }

                for (int j = 0; j < LifeTotal; j++)
                {
                    Hearts[j].Draw(spriteBatch);
                }


           

                spriteBatch.End();
            }
            else
            {
                spriteBatch.Begin();
                EndScreen.Draw(spriteBatch);
                spriteBatch.End();
            }
            base.Draw(gameTime);
        }
    }
}
