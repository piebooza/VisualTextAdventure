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
        List<Thwomps> enemy1 = new List<Thwomps>();
        List<Thwomps> enemy2 = new List<Thwomps>();
        List<AnimatedSprite> fireball = new List<AnimatedSprite>();
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

        int timer = 0;
        int timer2 = 0;

        MouseState mouseState;

        float speed = 5;
        float ground;

        Random rand;
        float jumpPower;
        float gravity = 0.1f;

        bool isAir = false;
        bool moving = false;

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
            rand = new Random();
            Background = new ScrollingBackground(new Sprite(new Vector2(0, 112), Content.Load<Texture2D>("shovel knight")), new Sprite(new Vector2(0, 112), Content.Load<Texture2D>("shovel knight")), 2, new Vector2(1.5f, 1.2f));

            EndScreen = new Sprite(new Vector2(GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 2), Content.Load<Texture2D>("dead"), 0f, new Vector2(1.7f, 1.7f), Color.White, SpriteEffects.None);

            int frameWidth = 22;
            int frameHeight = 26;

            List<Frame> fireballFrames = new List<Frame>();
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

            fireballFrames.Add(new Frame(new Rectangle(16,55,53,52)));
            fireballFrames.Add(new Frame(new Rectangle(87,49,53,58)));
            fireballFrames.Add(new Frame(new Rectangle(159,55,53,52)));
            fireballFrames.Add(new Frame(new Rectangle(229,49,53,58)));

            wizard = new Wizard(new Vector2(300, GraphicsDevice.Viewport.Height - frameHeight / 2 - 90), Content.Load<Texture2D>("New Piskel"), new Vector2(2, 2), frames, 3, TimeSpan.FromMilliseconds(60));
            enemy1.Add(new Thwomps( new Vector2(400, GraphicsDevice.Viewport.Height - frameHeight / 2 - 90), Content.Load<Texture2D>("Thwomp"), new Vector2(3,3), EnemyFrames, 3, TimeSpan.FromMilliseconds(100)));
            enemy2.Add(new Thwomps( new Vector2(100, GraphicsDevice.Viewport.Height - frameHeight / 2 - 90), Content.Load<Texture2D>("Thwomp"), new Vector2(3, 3), EnemyFrames, 3, TimeSpan.FromMilliseconds(300)));

            fireball.Add(new AnimatedSprite(fireballFrames, new Vector2(wizard.Position.X, wizard.Position.Y), Content.Load<Texture2D>("fireballs"), 0, Vector2.One, SpriteEffects.None, Color.White, TimeSpan.FromMilliseconds(30)));

            jumpPower = 7;

            ground = enemy1[0].Position.Y + enemy1[0].FrameHeight / 2;

        }

        protected override void UnloadContent()
        {
        }


        protected override void Update(GameTime gameTime)
        {
            keyboardState = Keyboard.GetState();
            if (keyboardState.IsKeyDown(Keys.D) || keyboardState.IsKeyDown(Keys.Right))
            {
                    enemy1[0].Position.X++;
                    enemy2[0].Position.X++;
                
                
                
            }
            if (keyboardState.IsKeyDown(Keys.A) || keyboardState.IsKeyDown(Keys.Left))
            {
                    enemy1[0].Position.X--;
                    enemy2[0].Position.X--;
                
            }
            
                mouseState = Mouse.GetState();
            if (!isAir)
            {
                speed = jumpPower;
                isAir = true;
          
            }
            else if (isAir)
            {


              //  timer++;
              //  timer2++;

                enemy1[0].Position.Y -= speed;
                enemy2[0].Position.Y -= speed;
                speed -= gravity;

                if (enemy1[0].Position.Y + enemy1[0].FrameHeight / 2 >= ground)
                {
                    enemy1[0].Position.Y = ground - enemy1[0].FrameHeight / 2;

                }
                if (enemy2[0].Position.Y + enemy2[0].FrameHeight / 2 >= ground)
                {
                    enemy2[0].Position.Y = ground - enemy2[0].FrameHeight / 2;

                }
                for (int x = 0; x < enemy1.Count; x++)
                {

                     if (enemy1[x].Position.X > wizard.Position.X && moving == true)
                     {

                         enemy1[x].Position.X -= 12;
                         moving = true;
                     }

                     else if (enemy1[x].Position.X < wizard.Position.X && moving == true)
                     {

                         enemy1[x].Position.X += 12;
                         moving = true;
                     }
                     else if (enemy1[x].Position.X == wizard.Position.X )
                     {

                         enemy1[x].Position.X += 0;
                         moving = false;
                     }       
                      

                

                    if (timer >= 200)
                    {
                        isAir = false;
                        timer = 0;
                    }
                    if (timer2 >= 200)
                    {
                        isAir = false;
                        timer2 = 0;
                    }
                }
            }

            wizard.Update(gameTime);
            enemy1[0].Update(gameTime);
            enemy2[0].Update(gameTime);
            fireball[0].Update(gameTime);
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

                

                if (!isAir)
                {
                    enemy1[0].Draw(spriteBatch);
                    enemy2[0].Draw(spriteBatch);
                }
                
                if(isAir)
                {
                    for (int x = 0; x < enemy1.Count; x++)
                    {
                        enemy1[x].Draw(spriteBatch);
                        enemy2[x].Draw(spriteBatch);
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

                if (mouseState.LeftButton == ButtonState.Pressed)
                {
                    for (int k = 0; k < LifeTotal; k++)
                    {
                        fireball[0].Draw(spriteBatch);
                    }
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