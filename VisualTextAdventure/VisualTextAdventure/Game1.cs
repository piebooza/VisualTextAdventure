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




namespace VisualTextAdventure
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;


        Wizard wizard;
        Sprite EndScreen;
        List<Thwomps> enemy1 = new List<Thwomps>();
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
        Sprite startbutton;
        int timer = 0;
        int hittimer = 0;
        MouseState mouseState;
        bool invul = false;
        float speed = 5;
        float ground;

        Random rand;
        float jumpPower;
        float gravity = 0.3f;

        bool isAir = false;
        bool moving = false;

        Texture2D pixel;

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
            IsMouseVisible = true;
            spriteBatch = new SpriteBatch(GraphicsDevice);
            rand = new Random();
            Background = new ScrollingBackground(new Sprite(new Vector2(0, 112), Content.Load<Texture2D>("shovel knight")), new Sprite(new Vector2(0, 112), Content.Load<Texture2D>("shovel knight")), 2, new Vector2(1.5f, 1.2f));

            EndScreen = new Sprite(new Vector2(GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 2), Content.Load<Texture2D>("dead"), 0f, new Vector2(1.7f, 1.7f), Color.LightGray, SpriteEffects.None);

            int frameWidth = 22;
            int frameHeight = 26;

            List<Frame> fireballFrames = new List<Frame>();
            List<Frame> frames = new List<Frame>();
            List<Frame> EnemyFrames = new List<Frame>();

            Hearts = new List<Sprite>();
            HalfHearts = new List<Sprite>();
            NoHearts = new List<Sprite>();

            startbutton = new Sprite(new Vector2(375, 200), Content.Load<Texture2D>("start button"),0f, new Vector2( .15f, .15f), Color.DarkGray, SpriteEffects.None);

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

            fireballFrames.Add(new Frame(new Rectangle(16, 55, 53, 52)));
            fireballFrames.Add(new Frame(new Rectangle(87, 49, 53, 58)));
            fireballFrames.Add(new Frame(new Rectangle(159, 55, 53, 52)));
            fireballFrames.Add(new Frame(new Rectangle(229, 49, 53, 58)));

            wizard = new Wizard(new Vector2(300, GraphicsDevice.Viewport.Height - frameHeight / 2 - 90), Content.Load<Texture2D>("New Piskel"), new Vector2(2, 2), frames, 3, TimeSpan.FromMilliseconds(60));
            int x = 0;
            for (int i = 1; i < 60; i++)
            {
                x += rand.Next(300, 600);
                enemy1.Add(new Thwomps(new Vector2(x, GraphicsDevice.Viewport.Height - frameHeight / 2 - 90), Content.Load<Texture2D>("Thwomp"), new Vector2(3, 3), EnemyFrames, 3, TimeSpan.FromMilliseconds(100)));
            }
            fireball.Add(new AnimatedSprite(fireballFrames, new Vector2(wizard.Position.X, wizard.Position.Y), Content.Load<Texture2D>("fireballs"), 0, Vector2.One, SpriteEffects.None, Color.White, TimeSpan.FromMilliseconds(30)));

            jumpPower = 12;
            foreach (Thwomps enemy in enemy1)
            {

                ground = enemy.Position.Y + enemy.FrameHeight / 2;
            }

            pixel = Content.Load<Texture2D>("pixel");
        }

        protected override void UnloadContent()
        {
        }


        protected override void Update(GameTime gameTime)
        {
            keyboardState = Keyboard.GetState();

            if (invul == false)
            {
                wizard.Position.Y = GraphicsDevice.Viewport.Height - wizard.FrameHeight / 2 - 77;
                wizard.scale.X = 2f;
                wizard.scale.Y = 2f;
                foreach (Thwomps enemy in enemy1)
                {
                    if (wizard.Hitbox.Intersects(enemy.Hitbox))
                    {
                        if (LifeTotal > 0)
                        {
                            Hearts.Remove(Hearts[Hearts.Count - 1]);
                            LifeTotal--;
                        }
                        invul = true;
                    }


                }

            }
            if (invul == true)
            {
                hittimer++;
                if (hittimer >= 150)
                {
                    hittimer = 0;
                    invul = false;
                }
                wizard.scale.X = 3f;
                wizard.scale.Y = .5f;
                wizard.Position.Y = GraphicsDevice.Viewport.Height - 83;
            }
            
            if (keyboardState.IsKeyDown(Keys.D) || keyboardState.IsKeyDown(Keys.Right))
            {
                foreach (Thwomps enemy in enemy1)
                {
                    enemy.Position.X -= 2;
                }
            }
            if (keyboardState.IsKeyDown(Keys.A) || keyboardState.IsKeyDown(Keys.Left))
            {
                foreach (Thwomps enemy in enemy1)
                {
                    enemy.Position.X += 2;
                }
            }

            mouseState = Mouse.GetState();
            if(LifeTotal <= 0)
            {
                
                if(mouseState.LeftButton == ButtonState.Pressed)
                {

                }
            }           
            if (!isAir)
            {
                speed = jumpPower;
                isAir = true;

            }
            else if (isAir)
            {
                

                timer++;
                foreach (Thwomps enemy in enemy1)
                {
                    enemy.Position.Y -= speed;
                }
                speed -= gravity;
                foreach (Thwomps enemy in enemy1)
                {

                    if (enemy.Position.Y + enemy.FrameHeight / 2 >= ground)
                    {
                        enemy.Position.Y = ground - enemy.FrameHeight / 2;

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
                        else if (enemy1[x].Position.X == wizard.Position.X)
                        {

                            enemy1[x].Position.X += 0;
                            moving = false;
                        }




                        if (timer >= 200)
                        {
                            isAir = false;
                            timer = 0;
                        }

                    }
                }

            }
            
            wizard.Update(gameTime);
            for (int x = 0; x < enemy1.Count; x++)
            {
                enemy1[x].Update(gameTime);
            }
            fireball[0].Update(gameTime);
            Background.Update(GraphicsDevice.Viewport);

            prevKeyboardState = keyboardState;


            elapsed += gameTime.ElapsedGameTime;
            if (elapsed.TotalMilliseconds >= 1000)
            {
                elapsed = TimeSpan.Zero;

            }

            base.Update(gameTime);

            if (LifeTotal <= 0)
            {
                if(startbutton.Hitbox.Contains(mouseState.X, mouseState.Y))
                //if (mouseState.X < startbutton.hitbox.X + startbutton.hitbox.Width && mouseState.Y < startbutton.hitbox.Y && mouseState.Y > startbutton.hitbox.Y + startbutton.hitbox.Height && mouseState.X > startbutton.hitbox.X)
                {
                    
                        startbutton.color = Color.White;
                        if (mouseState.LeftButton == ButtonState.Pressed)
                        {
                        LifeTotal = 5;
                        Hearts.Add(new Sprite(new Vector2(20, 20), Content.Load<Texture2D>("Heart"), 0, new Vector2(.2f, .2f), Color.White, SpriteEffects.None));
                        Hearts.Add(new Sprite(new Vector2(Hearts[0].Position.X + Hearts[0].Image.Width * Hearts[0].scale.X, 20), Content.Load<Texture2D>("Heart"), 0, new Vector2(.2f, .2f), Color.White, SpriteEffects.None));
                        Hearts.Add(new Sprite(new Vector2(Hearts[1].Position.X + Hearts[1].Image.Width * Hearts[1].scale.X, 20), Content.Load<Texture2D>("Heart"), 0, new Vector2(.2f, .2f), Color.White, SpriteEffects.None));
                        Hearts.Add(new Sprite(new Vector2(Hearts[2].Position.X + Hearts[2].Image.Width * Hearts[2].scale.X, 20), Content.Load<Texture2D>("Heart"), 0, new Vector2(.2f, .2f), Color.White, SpriteEffects.None));
                        Hearts.Add(new Sprite(new Vector2(Hearts[3].Position.X + Hearts[3].Image.Width * Hearts[3].scale.X, 20), Content.Load<Texture2D>("Heart"), 0, new Vector2(.2f, .2f), Color.White, SpriteEffects.None));

                    }


                }
                else
                {
                    startbutton.color = Color.DarkGray;
                }
                
            }
        }



        protected override void Draw(GameTime gameTime)
        {

            if (LifeTotal > 0)
            {

                spriteBatch.Begin();

                Background.Draw(spriteBatch);




                

                foreach (Thwomps enemy in enemy1)
                {
                    enemy.Draw(spriteBatch);
                }
                if (invul)
                {
                    if (hittimer % 10 == 0)
                    {
                        wizard.color = Color.Red;
                    }
                    else if (hittimer % 5 == 0)
                    {

                        wizard.color = Color.White;
                    }
                }
                wizard.Draw(spriteBatch);

                for (int i = 0; i < 5; i++)
                {

                    NoHearts[i].Draw(spriteBatch);
                }

                for (int j = 0; j < Hearts.Count; j++)
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

                /*
                if (wizard.Position.Y - wizard.Image.Height / 2 > enemy1[0].Position.Y + enemy1[0].Image.Height / 2 || wizard.Position.Y - wizard.Image.Height / 2 > enemy2[0].Position.Y + enemy2[0].Image.Height / 2)
                {
                    if (wizard.Position.X - wizard.Image.Width / 2 < enemy1[0].Position.X && wizard.Position.X + wizard.Image.Width / 2 < enemy1[0].Position.X)
                    {
                        for(int i = Hearts.Count; i > 0; i--)
                        {
                            Hearts[i].Draw(spriteBatch);
                        }
                    }
                    if (wizard.Position.X - wizard.Image.Width / 2 < enemy2[0].Position.X && wizard.Position.X + wizard.Image.Width / 2 < enemy2[0].Position.X)
                    {
                        for (int i = Hearts.Count; i > 0; i--)
                        {
                            Hearts[i].Draw(spriteBatch);
                        }
                    }
                }
                */


                spriteBatch.End();
            }
            else
            {
                spriteBatch.Begin();
                EndScreen.Draw(spriteBatch);
                startbutton.Draw(spriteBatch);
                spriteBatch.End();
            }
            base.Draw(gameTime);
        }
    }
}