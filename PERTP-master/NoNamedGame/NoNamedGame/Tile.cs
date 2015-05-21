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
                Console.WriteLine(">>Colisión tileRect y PlayerRect");
                if (state == 1)
                {
                    Console.WriteLine(">>>Tile sólido");
                    //Desde arriba
                    if (playerRect.Y <= tileRect.Y)
                    {
                        Console.WriteLine(">>>>Colisión desde arriba");
                        Player.Instance.Jumping = false;
                        Player.Instance.Falling = false;
                        Player.Instance.Image.position.Y -= tileRect.Y - playerRect.Y;
                    }
                    //desde abajo
                    if (playerRect.Y >= tileRect.Y)
                    {
                        Console.WriteLine(">>>>Colisión desde abajo");
                        Player.Instance.Falling = true;
                    }
                    //Desde izquierda
                    if (playerRect.X <= tileRect.X &&
                           (playerRect.Y < tileRect.Y && playerRect.Y + playerRect.Width > tileRect.Y + tileRect.Width))
                    {
                        Console.WriteLine(">>>>Colisión desde la izquierda");
                        Player.Instance.Image.position = new Vector2(tileRect.X - 10, Player.Instance.Image.position.Y);
                    }
                    //Desde derecha
                    if (playerRect.X >= tileRect.X &&
                          (playerRect.Y < tileRect.Y && playerRect.Y + playerRect.Width > tileRect.Y + tileRect.Width))
                    {
                        Console.WriteLine(">>>>Colisión desde la derecha");
                        Player.Instance.Image.position = new Vector2(tileRect.Width + 10, Player.Instance.Image.position.Y);
                    }
                }
                else
                {
                    Console.WriteLine(">> Tile pasivo, no hay colisión. Falling = true");
                    Player.Instance.Falling = true;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {

        }
    }
}
