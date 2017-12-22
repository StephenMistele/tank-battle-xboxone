﻿using Microsoft.Xna.Framework;
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

        // Textures & Sprites
        Texture2D TankTexture;
        Texture2D BulletTexture;
        Texture2D CastleBulletTexture;
        Texture2D TurretTexture;
        Texture2D CastleTurretTexture;
        Texture2D CastleTurretTextureForShow;
        Texture2D CastleTurretBaseTexture;
        Texture2D dot;
        SpriteFont Andy40, Andy20;

        // Global Variables
        int ScreenWidth = TileMap.TileWidth * TileMap.MapWidth;
        int ScreenHeight = TileMap.TileHeight * TileMap.MapHeight;
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
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            Texture2D TileTexture;
            int x = rand.Next(-2, 3);

        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
