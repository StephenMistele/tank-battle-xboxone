using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Tank_Game
{
    public class TileMap
    {
        #region Declarations
        // Public Properties
        public const int TileWidth = 16;
        public const int TileHeight = 16;
        public const int MapWidth = 55;
        public const int MapHeight = 25;
        public enum TileType { Empty, Border, Grass, Water, Castle, Special, forceField}
        public struct square
        {
            public TileType tileType;
            public Color tileColor;
        }
        public static square[,] mapSquare = new square[MapWidth, MapHeight];

        // Private Variables
        private static Texture2D texture;

        #endregion

        #region Initialization
        // Initialize Class
        public static void Initialize(Texture2D tileTexture)
        {
            texture = tileTexture;

            InitializeTileMap();
        }
        #endregion

        #region Public Methods
        // Public Methods
        public static void InitializeTileMap()
        {
            for (int x = 0; x < MapWidth; x++)
                for (int y = 0; y < MapHeight; y++)
                    mapSquare[x, y].tileType = TileType.Empty;

            // Borders
            for (int x = 0; x < MapWidth; x++)
                mapSquare[x, 0].tileType = TileType.Border;

            for (int y = 0; y < MapHeight; y++)
                mapSquare[0, y].tileType = TileType.Border;

            for (int x = 0; x < MapWidth; x++)
                mapSquare[x, MapHeight - 1].tileType = TileType.Border;

            for (int y = 0; y < MapHeight; y++)
                mapSquare[MapWidth - 1, y].tileType = TileType.Border;

            // Grass
            for (int x = 1; x < MapWidth - 1; x++)
                mapSquare[x, MapHeight - 2].tileType = TileType.Grass;

            for (int x = 20; x < 25; x++)
                mapSquare[x, MapHeight - 3].tileType = TileType.Grass;

            for (int x = 21; x < 24; x++)
                mapSquare[x, MapHeight - 4].tileType = TileType.Grass;

            for (int x = 22; x < 23; x++)
                mapSquare[x, MapHeight - 4].tileType = TileType.Grass;


            // Moat
            for (int x = 25; x < 30; x++)
                mapSquare[x, MapHeight - 2].tileType = TileType.Water;


            // Castle
            for (int y = 16; y < MapWidth - 31; y++)
            {
                for (int x = 30; x < MapWidth - 2; x++)
                    mapSquare[x, y].tileType = TileType.Castle;
            }
            for (int y = 11; y < MapWidth - 31; y++)
            {
                for (int x = 30; x < MapWidth - 20; x++)
                    mapSquare[x, y].tileType = TileType.Castle;
            }
            for (int y = 11; y < MapWidth - 31; y++)
            {
                for (int x = MapWidth - 7; x < MapWidth - 2; x++)
                    mapSquare[x, y].tileType = TileType.Castle;
            }

            
            // Battlement 2
            mapSquare[48, 10].tileType = TileType.Castle;
            mapSquare[50, 10].tileType = TileType.Castle;
            mapSquare[52, 10].tileType = TileType.Castle;

            // Battlement middle
            mapSquare[36, 15].tileType = TileType.Castle;
            mapSquare[38, 15].tileType = TileType.Castle;
            mapSquare[40, 15].tileType = TileType.Castle;
            mapSquare[42, 15].tileType = TileType.Castle;
            mapSquare[44, 15].tileType = TileType.Castle;
            mapSquare[46, 15].tileType = TileType.Castle;

            // Special blocks
            mapSquare[33, 14].tileType = TileType.Special;
            mapSquare[33, 19].tileType = TileType.Special;
            mapSquare[41, 19].tileType = TileType.Special;
            mapSquare[49, 19].tileType = TileType.Special;
            mapSquare[49, 14].tileType = TileType.Special;

            // Turret protector
            mapSquare[30, 10].tileType = TileType.forceField;
            mapSquare[34, 10].tileType = TileType.forceField;
            mapSquare[30, 9].tileType = TileType.forceField;
            mapSquare[34, 9].tileType = TileType.forceField;
            mapSquare[30, 8].tileType = TileType.forceField;
            mapSquare[34, 8].tileType = TileType.forceField;
            mapSquare[31, 8].tileType = TileType.forceField;
            mapSquare[33, 8].tileType = TileType.forceField;
            mapSquare[32, 8].tileType = TileType.forceField;
        }



        #endregion

        #region Information about Map Squares
        static public int GetSquareByPixelX(int pixelX)
        {
            return pixelX / TileWidth;
        }

        static public int GetSquareByPixelY(int pixelY)
        {
            return pixelY / TileHeight;
        }

        static public Vector2 GetSquareAtPixel(Vector2 pixelLocation)
        {
            return new Vector2(
                GetSquareByPixelX((int)pixelLocation.X),
                GetSquareByPixelY((int)pixelLocation.Y));
        }

        static public Vector2 GetSquareCenter(int squareX, int squareY)
        {
            return new Vector2(
                (squareX * TileWidth) + (TileWidth / 2),
                (squareY * TileHeight) + (TileHeight / 2));
        }

        static public Vector2 GetSquareCorner(Vector2 square)
        {
            return new Vector2(
                (square.X * TileWidth),
                (square.Y * TileHeight));
        }

        static public Vector2 GetSquareCenter(Vector2 square)
        {
            return GetSquareCenter(
                (int)square.X,
                (int)square.Y);
        }

        static public Vector2 GetSquareCorner(int squareX, int squareY)
        {
            return new Vector2(
                (squareX * TileWidth),
                (squareY * TileHeight));
        }

        static public Rectangle SquareToWorldRectangle(int x, int y)
        {
            return new Rectangle(
                x * TileWidth,
                y * TileHeight,
                TileWidth,
                TileHeight);
        }

        static public Rectangle SquareToWorldRectangle(Vector2 square)
        {
            return SquareToWorldRectangle(
                (int)square.X,
                (int)square.Y);
        }

        #endregion

        #region Information about Map Tiles
        private static bool IsValidTile(Vector2 tile)
        {
            if ((tile.X >= 0) && (tile.Y >= 0) &&
                (tile.X < MapWidth) && (tile.Y < MapHeight))
                return true;
            else
                return false;
        }

        public static bool TilesAreAdjacent(Vector2 tile1, Vector2 tile2)
        {
            if ((tile1.X == tile2.X) && ((tile1.Y == tile2.Y + 1) || (tile1.Y == tile2.Y - 1)))
                return true;
            if ((tile1.Y == tile2.Y) && ((tile1.X == tile2.X + 1) || (tile1.X == tile2.X - 1)))
                return true;

            return false;

        }
        #endregion

        #region Update & Draw
        static public void Draw(SpriteBatch spriteBatch)
        {

            for (int x = 0; x < MapWidth; x++)
                for (int y = 0; y < MapHeight; y++)
                {
                    switch (mapSquare[x, y].tileType)
                    {
                        case TileType.Empty:
                            {
                                spriteBatch.Draw(texture, SquareToWorldRectangle(x, y), Color.Black);
                            }
                            break;
                        case TileType.Border:
                            {
                                spriteBatch.Draw(texture, SquareToWorldRectangle(x, y), Color.Gray);
                            }
                            break;
                        case TileType.Grass:
                            {
                                spriteBatch.Draw(texture, SquareToWorldRectangle(x, y), Color.Green);
                            }
                            break;
                        case TileType.Water:
                            {
                                spriteBatch.Draw(texture, SquareToWorldRectangle(x, y), Color.Blue);
                            }
                            break;
                        case TileType.Castle:
                            {
                                spriteBatch.Draw(texture, SquareToWorldRectangle(x, y), Color.DarkGray);
                            }
                            break;
                        case TileType.Special:
                            {
                                spriteBatch.Draw(texture, SquareToWorldRectangle(x, y), Color.Red);
                            }
                            break;
                        case TileType.forceField:
                            {
                                spriteBatch.Draw(texture, SquareToWorldRectangle(x, y), Color.Orange);
                            }
                            break;

                    }
                }

        }
        #endregion
    }
}
