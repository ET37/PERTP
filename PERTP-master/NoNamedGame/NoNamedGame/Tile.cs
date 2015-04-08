using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace NoNamedGame
{
    public class Tile
    {
        Vector2 position;
        Rectangle sourceRect;
        int state;

        public Rectangle SourceRect
        {
            get { return sourceRect; }
        }

        public Vector2 Position
        {
            get { return position;  }
        }

        public void LoadContent(Vector2 position, Rectangle sourceRect, int state)
        {
            this.position = position;
            this.sourceRect = sourceRect;
            this.state = state;
        }


        public void Update(GameTime gameTime)
        {
            if (state == 1)
            {
                Rectangle tileRect = new Rectangle((int)position.X, (int)position.Y, sourceRect.Width, sourceRect.Height);
                Rectangle playerRect = new Rectangle((int)Player.Instance.Image.position.X, (int)Player.Instance.Image.position.Y,
                                                     (int)Player.Instance.Image.sourceRect.Width, (int)Player.Instance.Image.sourceRect.Height);

                if (playerRect.Intersects(tileRect))
                {
                    Player.Instance.Image.position.Y = tileRect.Y - Player.Instance.Image.texture.Height/2 - 1;
                    Player.Instance.Falling = false;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {

        }
    }
}
