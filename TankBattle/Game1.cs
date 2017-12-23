using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using Tank_Game;

namespace TankBattle
{

    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        // Textures & Sprities
        Texture2D TankTexture;
        Texture2D BulletTexture;
        Texture2D CastleBulletTexture;
        Texture2D TurretTexture;
        Texture2D CastleTurretTexture;
        Texture2D CastleTurretTextureForShow;
        Texture2D CastleTurretBaseTexture;
        Texture2D dot;
        SpriteFont Andy40, Andy20;

        Texture2D backgroundtexture;

        int ScreenWidth = 1920;
        int ScreenHeight = 1080;

        Rectangle SafeArea;

        public static Random rand = new Random();
        public static Block block;
        public Sprite Tank;
        public Sprite Bullet;
        public Sprite CastleBullet;
        public Sprite Turret;
        public Sprite CastleTurret;
        public Sprite CastleTurretBase;
        public float tankSpeed = 900f;
        public bool fisttime = true;
        public float bulletSpeed = 1200f;
        public float castleBulletSpeed = 500f;
        public float castleBulletY = 1;
        public static Random rando = new Random();
        public float tankLoc = 0f;
        public int specialCounter = 0;
        public static bool forceField = true;
        public bool dead = false;
        public float lives = 50f;
        public int score = 0;
        public bool won = false;
        public static int turretHealth = 50;
        public static bool specialHit = false;
        public static float castleShootMod = 0f;
        public static int round = 1;

        // Timers
        float keyPressSpeed = .1f;
        float keyPressTimer = 0.0f;
        float CastleShootSpeed = 4.5f;
        float CastleShootTimer = 0.0f;
        float ForceFieldVibSpeed = .5f;
        float ForceFieldVibTimer = 0.0f;
        float gameOverSpeed = 3f;
        float gameOverTimer = 0.0f;
        float specialTimerHitLength = 5f;
        float specialHitTimer = 0.0f;
        float forceFeildTimerEndFlashLength = 1.2f;
        float forceFeildTimerEndTimer = 0.0f;
        float roundOverSpeed = 3f;
        float roundOverTimer = 0.0f;


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            this.graphics.PreferredBackBufferWidth = ScreenWidth;
            this.graphics.PreferredBackBufferHeight = ScreenHeight;
            this.graphics.ApplyChanges();

            SafeArea = Window.ClientBounds;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            Texture2D TileTexture;
            int x = rand.Next(-2, 3);

            // Load Fonts
            Andy20 = Content.Load<SpriteFont>(@"Andy20");
            Andy40 = Content.Load<SpriteFont>(@"Andy40");

            // Load Textures
            TankTexture = Content.Load<Texture2D>(@"TankTexture");
            BulletTexture = Content.Load<Texture2D>(@"BulletTexture");
            CastleBulletTexture = Content.Load<Texture2D>(@"BulletTexture");
            TurretTexture = Content.Load<Texture2D>(@"Turret Texture");
            CastleTurretTexture = Content.Load<Texture2D>(@"CastleTurretTexture");
            CastleTurretTextureForShow = Content.Load<Texture2D>(@"CastleTurretTextureForShow");
            CastleTurretBaseTexture = Content.Load<Texture2D>(@"CastleTurretBaseTexture");
            dot = Content.Load<Texture2D>(@"Dot");

            backgroundtexture = Content.Load<Texture2D>("backgroundtexture");


            // Set Camera Properties
            Camera.WorldRectangle = new Rectangle(0, 0, ScreenWidth, ScreenHeight);
            Camera.ViewPortWidth = ScreenWidth;
            Camera.ViewPortHeight = ScreenHeight;

            // Load TileMap
            TileTexture = Content.Load<Texture2D>(@"TileTexture");
            Tank = new Sprite(new Vector2(20, ScreenHeight - TileMap.TileHeight * 4), TankTexture, new Rectangle(0, 0, 64, 32), Vector2.Zero);

            Bullet = new Sprite(new Vector2(0, 0), BulletTexture, new Rectangle(0, 0, 10, 10), Vector2.Zero);
            Bullet.Expired = true;

            CastleBullet = new Sprite(new Vector2(800, 800), CastleBulletTexture, new Rectangle(0, 0, 10, 10), Vector2.Zero);
            CastleBullet.Expired = true;

            Turret = new Sprite(new Vector2(13, ScreenHeight - TileMap.TileHeight * 4 + 2), TurretTexture, new Rectangle(0, 0, 71, 43), Vector2.Zero);
            //CastleTurret = new Sprite(new Vector2(425, 155), CastleTurretTextureF, new Rectangle(0, 0, 71, 43), Vector2.Zero);
            CastleTurret = new Sprite(new Vector2(465, 170), CastleTurretTextureForShow, new Rectangle(0, 0, 24, 2), Vector2.Zero);
            CastleTurretBase = new Sprite(new Vector2(501, 156), CastleTurretBaseTexture, new Rectangle(0, 0, 36, 23), Vector2.Zero);
            CastleTurret.WorldLocation = new Vector2(CastleTurret.WorldLocation.X + 24, CastleTurret.WorldLocation.Y - 14);


            TileMap.Initialize(TileTexture);
            Drawing.Initialize(dot);
        }

        protected override void UnloadContent()
        {
        }

        #region Procedures
        private void handleInput(KeyboardState keyState)
        {
            if (dead == false)
            {
                if (keyState.IsKeyDown(Keys.Escape))
                    this.Exit();

                if ((keyState.IsKeyDown(Keys.Space)) && (Bullet.Expired == true))
                {
                    Bullet.WorldLocation = new Vector2(Tank.WorldLocation.X + 32, Tank.WorldLocation.Y + 12);
                    Bullet.Velocity = new Vector2((float)Math.Cos(Turret.Rotation - .5f), (float)Math.Sin(Turret.Rotation - .5f)) * bulletSpeed;
                    Bullet.Expired = false;
                }

                if ((keyState.IsKeyDown(Keys.Left)) && (keyPressTimer > keyPressSpeed) && (Tank.WorldLocation.X >= 22))
                {
                    Tank.Velocity = new Vector2(-1, 0) * tankSpeed;
                    Turret.Velocity = new Vector2(-1, 0) * tankSpeed;
                    keyPressTimer = 0f;
                }
                else if ((keyState.IsKeyDown(Keys.Right)) && (keyPressTimer > keyPressSpeed) && (Tank.WorldLocation.X <= TileMap.MapWidth + 200))
                {
                    Tank.Velocity = new Vector2(1, 0) * tankSpeed;
                    Turret.Velocity = new Vector2(1, 0) * tankSpeed;
                    keyPressTimer = 0f;
                }
                else if ((keyState.IsKeyDown(Keys.Up)) && (keyPressTimer > keyPressSpeed) && (Turret.Rotation >= -1f))
                {
                    Turret.Rotation = Turret.Rotation - 0.1f;
                    keyPressTimer = 0f;
                }
                else if ((keyState.IsKeyDown(Keys.Down)) && (keyPressTimer > keyPressSpeed) && (Turret.Rotation <= .5))
                {
                    Turret.Rotation = Turret.Rotation + 0.1f;
                    keyPressTimer = 0f;
                }
                else
                {
                    Tank.Velocity = new Vector2(0, 0) * tankSpeed;
                    Turret.Velocity = new Vector2(0, 0) * tankSpeed;
                }
            }
        }

        private void handleAngleInput(KeyboardState keyState)
        {
            if (dead == false)
            {
                if ((keyState.IsKeyDown(Keys.Up)) && (keyPressTimer > keyPressSpeed) && (Turret.Rotation >= -1f))
                {
                    Turret.Rotation = Turret.Rotation - 0.1f;
                    keyPressTimer = 0f;
                }
                else if ((keyState.IsKeyDown(Keys.Down)) && (keyPressTimer > keyPressSpeed) && (Turret.Rotation <= .5))
                {
                    Turret.Rotation = Turret.Rotation + 0.1f;
                    keyPressTimer = 0f;
                }
            }
        }



        Vector2 NewBulletVelocity(Vector2 oldVelo)
        {
            Vector2 newVelo = oldVelo;

            if (oldVelo.Y <= -70)
            {
                newVelo.Y = oldVelo.Y * 0.93f;
            }
            else
            {
                newVelo.Y = Math.Abs(oldVelo.Y * 1.1f);
            }
            return newVelo;
        }


        #endregion

        protected override void Update(GameTime gameTime)
        {
            // Update Timer
            keyPressTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            CastleShootTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (dead == true)
                gameOverTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            // Handle Input
            if (won == false && dead == false)
            {
                handleInput(Keyboard.GetState());
                handleAngleInput(Keyboard.GetState());
            }

            // Update Bullet
            if (Bullet.WorldLocation.Y <= TileMap.TileHeight ||
                Bullet.WorldLocation.Y >= ScreenHeight - 2 * TileMap.TileHeight ||
                Bullet.WorldLocation.X >= ScreenWidth - TileMap.TileHeight)
            {
                Bullet.Velocity = new Vector2(0, 0);
                Bullet.Expired = true;
            }

            if (!Bullet.Expired)
                Bullet.Velocity = NewBulletVelocity(Bullet.Velocity);


            // Update And Shoot Castle Bullet
            if ((CastleShootTimer > CastleShootSpeed) && (won == false))
            {
                //float x = rand.Next(-15, 20);
                float x = rand.Next(0, 0);
                CastleBullet.WorldLocation = new Vector2(CastleTurret.WorldLocation.X + 60, CastleTurret.WorldLocation.Y + 8);
                tankLoc = Tank.WorldLocation.X / 500 - .1f;
                if (Tank.WorldLocation.X > 100 && Tank.WorldLocation.X < 170)
                    CastleBullet.Velocity = new Vector2((float)Math.Cos(3.55f + tankLoc + (x / 100)), (float)Math.Sin(-.5f)) * castleBulletSpeed * 1.1f;
                else if (Tank.WorldLocation.X >= 0 && Tank.WorldLocation.X <= 100)
                    CastleBullet.Velocity = new Vector2((float)Math.Cos(3.55f + tankLoc + (x / 100)), (float)Math.Sin(-.5f)) * castleBulletSpeed * 1.2f;
                else if (Tank.WorldLocation.X >= 170)
                    CastleBullet.Velocity = new Vector2((float)Math.Cos(3.55f + tankLoc + (x / 100)), (float)Math.Sin(-.5f)) * castleBulletSpeed;
                CastleTurret.Rotation = (float)(Math.Tan((CastleBullet.Velocity.Y) / (CastleBullet.Velocity.X)));
                CastleShootTimer = 0f;
                CastleBullet.Expired = false;
            }

            if (CastleBullet.WorldLocation.Y <= TileMap.TileHeight ||
                CastleBullet.WorldLocation.Y >= ScreenHeight - 2 * TileMap.TileHeight ||
                CastleBullet.WorldLocation.X <= TileMap.TileWidth)
            {
                CastleBullet.Velocity = new Vector2(0, 0);
                CastleBullet.Expired = true;
            }

            if (!CastleBullet.Expired)
                CastleBullet.Velocity = NewBulletVelocity(CastleBullet.Velocity);


            // Check for Bullet Colliding w/Castle
            Vector2 mapSq = new Vector2(TileMap.GetSquareByPixelX((int)Bullet.WorldCenter.X), TileMap.GetSquareByPixelY((int)Bullet.WorldCenter.Y));

            if (TileMap.mapSquare[(int)mapSq.X, (int)mapSq.Y].tileType == TileMap.TileType.Castle)
            {
                TileMap.mapSquare[(int)mapSq.X, (int)mapSq.Y].tileType = TileMap.TileType.Empty;
                Bullet.Velocity = new Vector2(0, 0);
                Bullet.Expired = true;
            }
            else if (TileMap.mapSquare[(int)mapSq.X, (int)mapSq.Y].tileType == TileMap.TileType.Border)
            {
                Bullet.Velocity = new Vector2(0, 0);
                Bullet.Expired = true;
            }

            // Check for Bullet Colliding w/Special
            if (TileMap.mapSquare[(int)mapSq.X, (int)mapSq.Y].tileType == TileMap.TileType.Special)
            {
                TileMap.mapSquare[(int)mapSq.X, (int)mapSq.Y].tileType = TileMap.TileType.Empty;
                Bullet.Velocity = new Vector2(0, 0);
                if (Bullet.Expired == false)
                {
                    specialCounter++;
                    specialHit = true;
                }
                Bullet.Expired = true;
            }

            // Check for Bullet Colliding w/Grass

            if (TileMap.mapSquare[(int)mapSq.X, (int)mapSq.Y].tileType == TileMap.TileType.Grass)
            {
                Bullet.Velocity = new Vector2(0, 0);
                Bullet.Expired = true;
            }
            // Check for Castle Bullet Colliding w/Tank
            if (CastleBullet.IsBoxColliding(Tank.BoundingBoxRect))
            {
                lives = lives - 10f;
                CastleBullet.Velocity = new Vector2(0, 0);
                CastleBullet.Expired = true;
                if (lives <= 0)
                {
                    score = Math.Abs(50 - turretHealth);
                    dead = true;
                }
            }

            // Check for Bullet Colliding w/Castle Turret
            if ((Bullet.IsBoxColliding(CastleTurretBase.BoundingBoxRect)) && (specialHit == true))
            {
                turretHealth = turretHealth - 2;
                Bullet.Velocity = new Vector2(0, 0);
                Bullet.Expired = true;
            }

            //Timer for "Weakness" Window
            if (specialHit == true)
            {
                specialHitTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (specialHitTimer > specialTimerHitLength)
                    specialHit = false;
                forceField = false;
            }
            else
            {
                forceField = true;
                specialHitTimer = 0;
            }

            //Check if Won
            if (turretHealth <= 0)
            {
                dead = false;
                won = true;
                roundOverTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (roundOverTimer > roundOverSpeed)
                {
                    turretHealth = 100 + round * 8;
                    lives = 50;
                    specialCounter = 0;
                    Tank.WorldLocation = new Vector2(20, ScreenHeight - TileMap.TileHeight * 4);
                    Turret.WorldLocation = new Vector2(13, ScreenHeight - TileMap.TileHeight * 4 + 2);
                    won = false;
                    round++;
                    roundOverTimer = 0f;
                    TileMap.InitializeTileMap();
                    CastleShootSpeed = 4.5f * (.8f - round / 10);
                }

            }

            // Check for All Specials Hit
            if (specialCounter >= 5)
            {
                forceField = false;
                CastleShootSpeed = 1.9f;
                specialHit = true;
            }

            //End Game
            if (gameOverTimer > gameOverSpeed)
                this.Exit();

            if (forceField == false)
            {
                TileMap.mapSquare[30, 10].tileType = TileMap.TileType.Empty;
                TileMap.mapSquare[34, 10].tileType = TileMap.TileType.Empty;
                TileMap.mapSquare[30, 9].tileType = TileMap.TileType.Empty;
                TileMap.mapSquare[34, 9].tileType = TileMap.TileType.Empty;
                TileMap.mapSquare[30, 8].tileType = TileMap.TileType.Empty;
                TileMap.mapSquare[34, 8].tileType = TileMap.TileType.Empty;
                TileMap.mapSquare[31, 8].tileType = TileMap.TileType.Empty;
                TileMap.mapSquare[33, 8].tileType = TileMap.TileType.Empty;
                TileMap.mapSquare[32, 8].tileType = TileMap.TileType.Empty;
            }
            else
            {
                TileMap.mapSquare[30, 10].tileType = TileMap.TileType.forceField;
                TileMap.mapSquare[34, 10].tileType = TileMap.TileType.forceField;
                TileMap.mapSquare[30, 9].tileType = TileMap.TileType.forceField;
                TileMap.mapSquare[34, 9].tileType = TileMap.TileType.forceField;
                TileMap.mapSquare[30, 8].tileType = TileMap.TileType.forceField;
                TileMap.mapSquare[34, 8].tileType = TileMap.TileType.forceField;
                TileMap.mapSquare[31, 8].tileType = TileMap.TileType.forceField;
                TileMap.mapSquare[33, 8].tileType = TileMap.TileType.forceField;
                TileMap.mapSquare[32, 8].tileType = TileMap.TileType.forceField;
            }

            // Check for Castle Missing Row


            // Update Sprites
            Tank.Update(gameTime);
            Turret.Update(gameTime);
            Bullet.Update(gameTime);
            CastleBullet.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();

            //*******************************************************************************************************************************************************************************
            // More about the safe area of the screen and stuff: https://developer.xamarin.com/guides/cross-platform/game_development/monogame/platforms/uwp/#Safe_Area_on_Xbox_One
            //*******************************************************************************************************************************************************************************

            //Draw the stupid background that I made
            spriteBatch.Draw(backgroundtexture, new Rectangle(0, 0, ScreenWidth, ScreenHeight), Color.White);            

            // Draw gameboard
            TileMap.Draw(spriteBatch);
            Tank.Draw(spriteBatch);
            Turret.Draw(spriteBatch);
            Bullet.Draw(spriteBatch);
            CastleTurret.Draw(spriteBatch);
            CastleTurretBase.Draw(spriteBatch);
            CastleBullet.Draw(spriteBatch);

            spriteBatch.DrawString(Andy40, won.ToString(), new Vector2(20, 60), Color.White);
            spriteBatch.DrawString(Andy40, tankLoc.ToString(), new Vector2(20, 100), Color.White);
            spriteBatch.DrawString(Andy40, specialCounter.ToString(), new Vector2(20, 140), Color.White);
            spriteBatch.DrawString(Andy40, lives.ToString(), new Vector2(20, 180), Color.White);
            spriteBatch.DrawString(Andy40, turretHealth.ToString(), new Vector2(20, 220), Color.White);

            //Temp
            Drawing.DrawBox(Tank.BoundingBoxRect, Color.Yellow, spriteBatch);
            Drawing.DrawBox(CastleTurretBase.BoundingBoxRect, Color.Yellow, spriteBatch);

            //game over
            if (dead == true)
                spriteBatch.DrawString(Andy20, "Game over. You did " + score.ToString() + " % Damage to the Enemy", new Vector2(200, 70), Color.White);

            //Next round
            if (won == true)
                spriteBatch.DrawString(Andy20, "Good Job! You killed the Enemy. Prepare for The next Round", new Vector2(160, 70), Color.White);

            //Draw Turret Health Bar
            Drawing.DrawBox(new Rectangle((492), 120, 50, 3), Color.Red, spriteBatch);
            for (int i = 0; i < turretHealth; i++)
                Drawing.DrawVLine((492 + i), (120), (123), Color.Red, spriteBatch);

            //Draw Tank Health Bar
            Drawing.DrawBox(new Rectangle(((int)Tank.WorldLocation.X), 330, 50, 3), Color.Red, spriteBatch);
            for (int i = 0; i < lives; i++)
                Drawing.DrawVLine(((int)Tank.WorldLocation.X + i), (330), (333), Color.Red, spriteBatch);


            //Draw a box for the safe area of the screen to put ALL important things in
            Drawing.DrawBox(new Rectangle((1920 - SafeArea.Width)/2, (1080 - SafeArea.Height)/2, SafeArea.Width, SafeArea.Height), Color.Red, spriteBatch);

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
