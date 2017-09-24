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

        //AnimatedSprite boss;

        List<Fireball> fireball = new List<Fireball>();
        List<Frame> fireballFrames = new List<Frame>();

        Label MainText;

        Button startScreen;

        ScrollingBackground Background;

        List<Thwomps> enemy1 = new List<Thwomps>();

        List<Sprite> Hearts;
        List<Sprite> HalfHearts;
        List<Sprite> NoHearts;

        Sprite startbutton;
        Sprite secretbutton;
        Sprite EndScreen;

        int LifeTotal = 5;
        int timer = 0;
        int hittimer = 0;

        TimeSpan elapsed = TimeSpan.Zero;
        TimeSpan fireballtimer = TimeSpan.Zero;

        KeyboardState prevKeyboardState;
        KeyboardState keyboardState;
        MouseState mouseState;
        MouseState prevmouseState;
        Random rand;

        float jumpPower;
        float gravity = 0.3f;
        float speed = 5;
        float ground;

        bool isAir = false;
        bool moving = false;
        bool invul = false;
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



            List<Frame> frames = new List<Frame>();
            List<Frame> EnemyFrames = new List<Frame>();
            List<Frame> BossFrames = new List<Frame>();

            Hearts = new List<Sprite>();
            HalfHearts = new List<Sprite>();
            NoHearts = new List<Sprite>();

            startbutton = new Sprite(new Vector2(375, 200), Content.Load<Texture2D>("start button"), 0f, new Vector2(.15f, .15f), Color.DarkGray, SpriteEffects.None);
            secretbutton = new Sprite(new Vector2(790, 470), Content.Load<Texture2D>("secret heart"), 0f, new Vector2(.01f, .01f), Color.DarkGray, SpriteEffects.None);

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

            fireballFrames.Add(new Frame(new Rectangle(132, 101, 54, 57)));
            fireballFrames.Add(new Frame(new Rectangle(202, 96, 56, 61)));
            fireballFrames.Add(new Frame(new Rectangle(273, 100, 56, 57)));
            fireballFrames.Add(new Frame(new Rectangle(345, 95, 56, 59)));

            /*
                        BossFrames.Add(new Frame(new Rectangle(18, 16, 128, 178)));
                        BossFrames.Add(new Frame(new Rectangle(157, 17, 131, 176)));
                        BossFrames.Add(new Frame(new Rectangle(445, 12, 131, 183)));
                        */

            wizard = new Wizard(new Vector2(300, GraphicsDevice.Viewport.Height - frameHeight / 2 - 89), Content.Load<Texture2D>("New Piskel"), new Vector2(2, 2), frames, 3, TimeSpan.FromMilliseconds(60));

            //boss = new AnimatedSprite(BossFrames, new Vector2(4000, GraphicsDevice.Viewport.Height - frameHeight / 2 - 89), Content.Load<Texture2D>("boss"), 0f, new Vector2(2, 2), SpriteEffects.FlipHorizontally, Color.White, TimeSpan.FromMilliseconds(1000));


            int x = 0;

            for (int i = 1; i < 60; i++)
            {
                x += rand.Next(300, 600);
                enemy1.Add(new Thwomps(new Vector2(x, GraphicsDevice.Viewport.Height - frameHeight / 2 - 90), Content.Load<Texture2D>("Thwomp"), new Vector2(3, 3), EnemyFrames, 3, TimeSpan.FromMilliseconds(100)));
            }

            jumpPower = 12;
            foreach (Thwomps enemy in enemy1)
            {

                ground = enemy.Position.Y + enemy.FrameHeight / 2;
            }


        }

        protected override void UnloadContent()
        {
        }


        protected override void Update(GameTime gameTime)
        {
            keyboardState = Keyboard.GetState();
            prevmouseState = mouseState;
            mouseState = Mouse.GetState();

            if (prevmouseState.LeftButton == ButtonState.Released && mouseState.LeftButton == ButtonState.Pressed)
            {
                fireball.Add(new Fireball(new Vector2((int)(wizard.Position.X + 25), (int)(wizard.Position.Y - 25 )), Content.Load<Texture2D>("fireballs"), new Vector2(0.1f), fireballFrames, new TimeSpan(0, 0, 0, 0, 60)));
            }
            else if (prevmouseState.LeftButton == ButtonState.Pressed && mouseState.LeftButton == ButtonState.Pressed)
            {
                fireball[fireball.Count - 1].state = Fireball.State.Growing;
            }
            else if (prevmouseState.LeftButton == ButtonState.Pressed && mouseState.LeftButton == ButtonState.Released)
            {

                fireball[fireball.Count - 1].state = Fireball.State.Moving;
            }
            if (invul == false)
            {
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
            }

            if (keyboardState.IsKeyDown(Keys.D) || keyboardState.IsKeyDown(Keys.Right))
            {
                foreach (Thwomps enemy in enemy1)
                {
                    enemy.Position.X -= 2;
                }
                foreach(Fireball ball in fireball)
                {
                    ball.Position.X -= 2;
                }
            }
            if (keyboardState.IsKeyDown(Keys.A) || keyboardState.IsKeyDown(Keys.Left))
            {
                foreach (Thwomps enemy in enemy1)
                {
                    enemy.Position.X += 2;
                }
                foreach (Fireball ball in fireball)
                {
                    ball.Position.X += 2;
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

            for (int x = 0; x < fireball.Count; x++)
            {
                fireball[x].Update(gameTime);
            }
            for (int x = 0; x < enemy1.Count; x++)
            {
                enemy1[x].Update(gameTime);
            }
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
                if (startbutton.Hitbox.Contains(mouseState.X, mouseState.Y))
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

                if (secretbutton.Hitbox.Contains(mouseState.X, mouseState.Y))
                //if (mouseState.X < startbutton.hitbox.X + startbutton.hitbox.Width && mouseState.Y < startbutton.hitbox.Y && mouseState.Y > startbutton.hitbox.Y + startbutton.hitbox.Height && mouseState.X > startbutton.hitbox.X)
                {

                    secretbutton.color = Color.White;
                    if (mouseState.LeftButton == ButtonState.Pressed)
                    {
                        LifeTotal = 10;
                        Hearts.Add(new Sprite(new Vector2(20, 20), Content.Load<Texture2D>("Heart"), 0, new Vector2(.2f, .2f), Color.White, SpriteEffects.None));
                        Hearts.Add(new Sprite(new Vector2(Hearts[0].Position.X + Hearts[0].Image.Width * Hearts[0].scale.X, 20), Content.Load<Texture2D>("Heart"), 0, new Vector2(.2f, .2f), Color.White, SpriteEffects.None));
                        Hearts.Add(new Sprite(new Vector2(Hearts[1].Position.X + Hearts[1].Image.Width * Hearts[1].scale.X, 20), Content.Load<Texture2D>("Heart"), 0, new Vector2(.2f, .2f), Color.White, SpriteEffects.None));
                        Hearts.Add(new Sprite(new Vector2(Hearts[2].Position.X + Hearts[2].Image.Width * Hearts[2].scale.X, 20), Content.Load<Texture2D>("Heart"), 0, new Vector2(.2f, .2f), Color.White, SpriteEffects.None));
                        Hearts.Add(new Sprite(new Vector2(Hearts[3].Position.X + Hearts[3].Image.Width * Hearts[3].scale.X, 20), Content.Load<Texture2D>("Heart"), 0, new Vector2(.2f, .2f), Color.White, SpriteEffects.None));
                        Hearts.Add(new Sprite(new Vector2(Hearts[4].Position.X + Hearts[4].Image.Width * Hearts[4].scale.X, 20), Content.Load<Texture2D>("Heart"), 0, new Vector2(.2f, .2f), Color.White, SpriteEffects.None));
                        Hearts.Add(new Sprite(new Vector2(Hearts[5].Position.X + Hearts[5].Image.Width * Hearts[5].scale.X, 20), Content.Load<Texture2D>("Heart"), 0, new Vector2(.2f, .2f), Color.White, SpriteEffects.None));
                        Hearts.Add(new Sprite(new Vector2(Hearts[6].Position.X + Hearts[6].Image.Width * Hearts[6].scale.X, 20), Content.Load<Texture2D>("Heart"), 0, new Vector2(.2f, .2f), Color.White, SpriteEffects.None));
                        Hearts.Add(new Sprite(new Vector2(Hearts[7].Position.X + Hearts[7].Image.Width * Hearts[7].scale.X, 20), Content.Load<Texture2D>("Heart"), 0, new Vector2(.2f, .2f), Color.White, SpriteEffects.None));
                        Hearts.Add(new Sprite(new Vector2(Hearts[8].Position.X + Hearts[8].Image.Width * Hearts[8].scale.X, 20), Content.Load<Texture2D>("Heart"), 0, new Vector2(.2f, .2f), Color.White, SpriteEffects.None));

                        NoHearts.Add(new Sprite(new Vector2(NoHearts[4].Position.X + NoHearts[4].Image.Width * NoHearts[4].scale.X, 20), Content.Load<Texture2D>("Emp_Heart"), 0, new Vector2(.095f, .095f), Color.White, SpriteEffects.None));
                        NoHearts.Add(new Sprite(new Vector2(NoHearts[5].Position.X + NoHearts[5].Image.Width * NoHearts[5].scale.X, 20), Content.Load<Texture2D>("Emp_Heart"), 0, new Vector2(.095f, .095f), Color.White, SpriteEffects.None));
                        NoHearts.Add(new Sprite(new Vector2(NoHearts[6].Position.X + NoHearts[6].Image.Width * NoHearts[6].scale.X, 20), Content.Load<Texture2D>("Emp_Heart"), 0, new Vector2(.095f, .095f), Color.White, SpriteEffects.None));
                        NoHearts.Add(new Sprite(new Vector2(NoHearts[7].Position.X + NoHearts[7].Image.Width * NoHearts[7].scale.X, 20), Content.Load<Texture2D>("Emp_Heart"), 0, new Vector2(.095f, .095f), Color.White, SpriteEffects.None));
                        NoHearts.Add(new Sprite(new Vector2(NoHearts[8].Position.X + NoHearts[8].Image.Width * NoHearts[8].scale.X, 20), Content.Load<Texture2D>("Emp_Heart"), 0, new Vector2(.095f, .095f), Color.White, SpriteEffects.None));
                    }


                }
                else
                {
                    secretbutton.color = Color.DarkGray;
                }
            }
        }



        protected override void Draw(GameTime gameTime)
        {

            if (LifeTotal > 0)
            {

                spriteBatch.Begin();

                Background.Draw(spriteBatch);

                //boss.Draw(spriteBatch);




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

                for (int i = 0; i < NoHearts.Count; i++)
                {

                    NoHearts[i].Draw(spriteBatch);
                }

                for (int j = 0; j < Hearts.Count; j++)
                {
                    Hearts[j].Draw(spriteBatch);
                }

                foreach (Fireball ball in fireball)
                {
                    ball.Draw(spriteBatch);
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
                secretbutton.Draw(spriteBatch);
                spriteBatch.End();
            }
            base.Draw(gameTime);
        }
    }
}