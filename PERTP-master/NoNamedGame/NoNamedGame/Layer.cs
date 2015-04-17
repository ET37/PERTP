using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using NoNamedGame.Managers;

namespace NoNamedGame
{
    public class Layer
    {

        public class TileMap
        {
            [XmlElement("Rows")]
            public List<String> Rows;
            
            public TileMap()
            {
                Rows = new List<String>();
            }
        }

        [XmlElement("TileMap")]
        public TileMap Tile;
        public Image Image;

        public List<Tile> tiles;

        public Layer()
        {
            Image = new Image();
            tiles = new List<Tile>();
        }

        public void LoadContent(Vector2 tileDimensions) 
        {
            Image.Loadcontent();
            //?
            Vector2 position = -tileDimensions;
            int i = 0;
            position.X = -tileDimensions.X;
            position.Y = ScreenManager.Instance.dimensions.Y - tileDimensions.Y;

            foreach (string row in Tile.Rows)
            {
                string[] split = row.Split(']');
                position.X = -tileDimensions.X;
                position.Y -= tileDimensions.Y * i;
                foreach (string s in split)
                {
                    if (s != String.Empty)
                    {
                        position.X += tileDimensions.X;
                        tiles.Add(new Tile());

                        string str = s.Replace("[", String.Empty);
                        int value1 = int.Parse(str[0].ToString());
                        int value2 = int.Parse(str[2].ToString());
                        int state  = int.Parse(str[4].ToString());

                        tiles[tiles.Count - 1].LoadContent(position, new Rectangle(
                            value1 * (int)tileDimensions.X, value2 * (int)tileDimensions.Y,
                            (int)tileDimensions.X, (int)tileDimensions.Y), state);
                    }
                }

                i++;
            }
        }


        public void Update(GameTime gameTime)
        {
            foreach (Tile tile in tiles)
            {
                tile.Update(gameTime);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Tile tile in tiles)
            {
                Image.position = tile.Position;
                Image.sourceRect = tile.SourceRect;
                Image.Draw(spriteBatch);
            }
        }
    }
}
