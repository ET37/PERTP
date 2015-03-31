using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace NoNamedGame
{
    public class Layer
    {

        public class TileMap
        {
            public List<String> Row;

            public TileMap()
            {
                Row = new List<String>();
            }
        }

        public TileMap Tile;
        public List<Tile> tiles;

        public Layer()
        {

        }

        public void LoadContent(Vector2 tileDimensions) 
        {
            foreach (string row in Tile.Row)
            {
                string[] split = row.Split(']');
                foreach (string s in split)
                {
                    if (s != String.Empty)
                    {
                        string str = s.Replace("[", String.Empty);
                        int value1 = int.Parse(str.Substring(0, str.IndexOf(':')));
                        int value2 = int.Parse(str.Substring(str.IndexOf(':') + 1));
                    }

                }
            }
        }


        public void Update(GameTime gametime)
        {

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            
        }
    }
}
