using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Tank_Game
{
    class Drawing
    {
        private static Texture2D dot;

        public static void Initialize(Texture2D dotTexture)
        {
            dot = dotTexture;
        }


        public static void DrawBox(int x1, int y1, int x2, int y2, Color color, SpriteBatch spriteBatch)
        {
            DrawVLine(x1, y1, y2, color, spriteBatch);
            DrawVLine(x2, y1, y2, color, spriteBatch);
            DrawHLine(x1, x2, y1, color, spriteBatch);
            DrawHLine(x1, x2, y2, color, spriteBatch);
        }

        public static void DrawBox(Rectangle rect, Color color, SpriteBatch spriteBatch)
        {
            DrawBox(rect.X, rect.Y, rect.X + rect.Width, rect.Y + rect.Height,
                color, spriteBatch);
        }

        public static void DrawVLine(int x, int y1, int y2, Color color, SpriteBatch spriteBatch)
        {

            spriteBatch.Draw(dot, new Rectangle(x, y1, 1, 1 + y2 - y1), color);
        }

        public static void DrawHLine(int x1, int x2, int y, Color color, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(dot, new Rectangle(x1, y, 1 + x2 - x1, 1), color);
        }

        public static void DrawProgressBar(int x, int y, int width, int height, int percent, Color color, SpriteBatch spriteBatch)
        {
            DrawVLine(x, y, y + height, color, spriteBatch);
            DrawVLine(x + width, y, y + height, color, spriteBatch);
            DrawHLine(x, x + width, y, color, spriteBatch);
            DrawHLine(x, x + width, y + height, color, spriteBatch);

            for (int i = 0; i < width * ((float)percent / 100); i++)
            {
                DrawVLine(x + i, y, y + height, color, spriteBatch);
            }
        }


        public static void DrawProgressBar(Vector2 xy, int width, int height, int percent, Color color, SpriteBatch spriteBatch)
        {
            int x = (int)xy.X;
            int y = (int)xy.Y;
            DrawVLine(x, y, y + height, color, spriteBatch);
            DrawVLine(x + width, y, y + height, color, spriteBatch);
            DrawHLine(x, x + width, y, color, spriteBatch);
            DrawHLine(x, x + width, y + height, color, spriteBatch);

            for (int i = 0; i < width * ((float)percent / 100); i++)
            {
                DrawVLine(x + width - i, y, y + height, color, spriteBatch);
            }

        }
    }
}
