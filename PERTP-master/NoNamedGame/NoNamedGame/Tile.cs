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
            get { return position; }
        }

        public void LoadContent(Vector2 position, Rectangle sourceRect, int state)
        {
            this.position = position;
            this.sourceRect = sourceRect;
            this.state = state;
        }


        public void Update(GameTime gameTime)
        {
            Rectangle tileRect = new Rectangle((int)position.X, (int)position.Y, sourceRect.Width, sourceRect.Height);
            Rectangle playerRect = new Rectangle((int)Player.Instance.Image.position.X, (int)Player.Instance.Image.position.Y,
                                                 (int)Player.Instance.Image.sourceRect.Width, (int)Player.Instance.Image.sourceRect.Height);

            //Ya que estamos en la clase tile, cambiemos la forma de verlo(?)
            if (tileRect.Intersects(playerRect))
            {
                if (state == 1)
                {
                    //Desde abajo
                    if (playerRect.Y >= tileRect.Y)
                    {
                        Player.Instance.Jumping = false;
                        Player.Instance.Falling = true;
                    }
                    //Desde arriba
                    else if (playerRect.Y <= tileRect.Y)
                    {
                        Player.Instance.Jumping = false;
                        Player.Instance.Falling = false;
                    }
                    //Colision de lado
                    else
                    {

                    }
                }
                else
                {
                    Player.Instance.Falling = true;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {

        }
    }
}
