using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Tank_Game
{
    public static class Camera
    {
        #region Declarations
        public static Vector2 position = Vector2.Zero;
        private static Vector2 viewPortSize = Vector2.Zero;
        private static Rectangle worldRectangle = new Rectangle(0, 0, 0, 0);
        private const float CameraSpeed = 400f;
        #endregion

        #region Properties
        public static Vector2 Position
        {
            get { return position; }
            set
            {
                position = new Vector2(
                    MathHelper.Clamp(value.X,
                        worldRectangle.X,
                        worldRectangle.Width - ViewPortWidth),
                    MathHelper.Clamp(value.Y,
                        worldRectangle.Y,
                        worldRectangle.Height - ViewPortHeight));
            }
        }

        public static Rectangle WorldRectangle
        {
            get { return worldRectangle; }
            set { worldRectangle = value; }
        }

        public static int ViewPortWidth
        {
            get { return (int)viewPortSize.X; }
            set { viewPortSize.X = value; }
        }

        public static int ViewPortHeight
        {
            get { return (int)viewPortSize.Y; }
            set { viewPortSize.Y = value; }
        }

        public static Rectangle ViewPort
        {
            get
            {
                return new Rectangle(
                    (int)Position.X, (int)Position.Y,
                    ViewPortWidth, ViewPortHeight);
            }
        }
        #endregion

        #region Public Methods
        public static void Move(Vector2 offset)
        {
            Position += offset;
        }

        public static bool ObjectIsVisible(Rectangle bounds)
        {
            return (ViewPort.Intersects(bounds));
        }

        public static Vector2 ConvertWorldToScreenLocation(Vector2 point)
        {
            return point - position;
        }

        public static Rectangle ConvertWorldToScreenLocation(Rectangle rectangle)
        {
            return new Rectangle(
                rectangle.Left - (int)position.X,
                rectangle.Top - (int)position.Y,
                rectangle.Width,
                rectangle.Height);
        }
        public static Vector2 ConvertScreenToWorldLocation(Vector2 point)
        {
            return point + position;
        }
        #endregion

        #region Input Handling
        private static void repositionCamera(GameTime gameTime, Vector2 moveAngle)
        {
            float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
            float moveScale = CameraSpeed * elapsed;

            if (moveAngle.X < 0)
            {
                Camera.Move(new Vector2(moveAngle.X, 0) * moveScale);
            }

            if (moveAngle.X > 0)
            {
                Camera.Move(new Vector2(moveAngle.X, 0) * moveScale);
            }

            if (moveAngle.Y < 0)
            {
                Camera.Move(new Vector2(0, moveAngle.Y) * moveScale);
            }

            if (moveAngle.Y > 0)
            {
                Camera.Move(new Vector2(0, moveAngle.Y) * moveScale);
            }
        }

        private static void HandleKeyboardInput(GameTime gameTime)
        {
            KeyboardState keyState = Keyboard.GetState();
            Vector2 cameraDirection = new Vector2(0, 0);

            if (keyState.IsKeyDown(Keys.NumPad1))
                cameraDirection = new Vector2(-1, 1);

            if (keyState.IsKeyDown(Keys.NumPad2) || (keyState.IsKeyDown(Keys.S)))
                cameraDirection = new Vector2(0, 1);

            if (keyState.IsKeyDown(Keys.NumPad3))
                cameraDirection = new Vector2(1, 1);

            if ((keyState.IsKeyDown(Keys.NumPad4)) || (keyState.IsKeyDown(Keys.A)))
                cameraDirection = new Vector2(-1, 0);

            if ((keyState.IsKeyDown(Keys.NumPad6)) || (keyState.IsKeyDown(Keys.D)))
                cameraDirection = new Vector2(1, 0);

            if (keyState.IsKeyDown(Keys.NumPad7))
                cameraDirection = new Vector2(-1, -1);

            if ((keyState.IsKeyDown(Keys.NumPad8)) || (keyState.IsKeyDown(Keys.W)))
                cameraDirection = new Vector2(0, -1);

            if (keyState.IsKeyDown(Keys.NumPad9))
                cameraDirection = new Vector2(1, -1);

            cameraDirection.Normalize();
            repositionCamera(gameTime, cameraDirection);

        }

        private static void HandleGamePadInput(GameTime gameTime)
        {
            Vector2 cameraDirection = new Vector2(0, 0);
            GamePadState gamepadState = GamePad.GetState(PlayerIndex.One);




            if (gamepadState.ThumbSticks.Left.X > 0 && gamepadState.ThumbSticks.Left.Y > 0)
                cameraDirection = new Vector2(1, -1);

            if (gamepadState.ThumbSticks.Left.X < 0 && gamepadState.ThumbSticks.Left.Y > 0)
                cameraDirection = new Vector2(-1, -1);

            if (gamepadState.ThumbSticks.Left.X < 0 && gamepadState.ThumbSticks.Left.Y < 0)
                cameraDirection = new Vector2(-1, 1);

            if (gamepadState.ThumbSticks.Left.X > 0 && gamepadState.ThumbSticks.Left.Y < 0)
                cameraDirection = new Vector2(1, 1);


            if (gamepadState.ThumbSticks.Left.X == 0 && gamepadState.ThumbSticks.Left.Y > 0)
                cameraDirection = new Vector2(0, -1);

            if (gamepadState.ThumbSticks.Left.X == 0 && gamepadState.ThumbSticks.Left.Y < 0)
                cameraDirection = new Vector2(0, 1);

            if (gamepadState.ThumbSticks.Left.X > 0 && gamepadState.ThumbSticks.Left.Y == 0)
                cameraDirection = new Vector2(1, 0);

            if (gamepadState.ThumbSticks.Left.X < 0 && gamepadState.ThumbSticks.Left.Y == 0)
                cameraDirection = new Vector2(-1, 0);


            cameraDirection.Normalize();
            repositionCamera(gameTime, cameraDirection);

        }
        #endregion

        #region Update
        public static void Update(GameTime gameTime)
        {
            HandleKeyboardInput(gameTime);
            HandleGamePadInput(gameTime);
        }
        #endregion
    }
}
