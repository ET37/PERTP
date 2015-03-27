using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using NoNamedGame.Managers;

namespace NoNamedGame
{
    public class Player
    {
        //Atributos
        public Image Image;
        public Vector2 Velocity;

        private Vector2 InitialPos;
        private Vector2 TextureSize;
        private string texturePath;
        private float jumpHeight = 50; //Cuanto de Y subir
        private float initialY, finalY;
        private bool lookLeft;

        public bool Jumping;

        /*
         * Constructor
         * 
         */
        public Player()
        {
            Velocity = new Vector2(100, 400);
            texturePath = "Gameplay/player";
            InitialPos = new Vector2(0, 0);
            TextureSize = new Vector2(1, 1);
            Image = new Image(texturePath, InitialPos, TextureSize, 100);
            Image.effects.Add("SpriteSheetEffect");
            Jumping = false;
            lookLeft = false;
        }

        public void LoadContent()
        {
            Image.Loadcontent();
            Image.spriteSheetEffect.amountOfFrames = new Vector2(4, 2);
            Image.spriteSheetEffect.currentFrame = new Vector2(0);
        }

        /*
         * Usté lo dijo mijo con razón, no lo vamos a usar pero
         * lo dejo porsi.
         * 
         * TODO: Ver la posibilidad de eliminar este metodo.
         */
        public void UnloadContent()
        {

        }

        public void Update(GameTime gameTime)
        {
            Image.isActive = true;
            /* En esta parte contolo el movimiento del personaje según la tecla
             * que se presiona
             * 
             */
            if (InputManager.Instance.KeyPressed(Keys.Space) && !Jumping)
            {
                Jumping = true;
                initialY = Image.position.Y;
                finalY = initialY - jumpHeight;
            }

            saltar(gameTime);

            if (InputManager.Instance.KeyDown(Keys.Left))
            {
                lookLeft = true;
                Image.position.X -= Velocity.X * (float)gameTime.ElapsedGameTime.TotalSeconds;

                if (Image.spriteSheetEffect.currentFrame.X == 1)
                    Image.spriteSheetEffect.currentFrame.X = 2;
                else
                    Image.spriteSheetEffect.currentFrame.X = 1;

                Image.spriteSheetEffect.currentFrame.Y = (lookLeft) ? 1 : 0;
            }

            else if (InputManager.Instance.KeyDown(Keys.Right))
            {
                lookLeft = false;
                Image.position.X += Velocity.X * (float)gameTime.ElapsedGameTime.TotalSeconds;

                if (Image.spriteSheetEffect.currentFrame.X == 1)
                    Image.spriteSheetEffect.currentFrame.X = 2;
                else
                    Image.spriteSheetEffect.currentFrame.X = 1;

                Image.spriteSheetEffect.currentFrame.Y = (lookLeft) ? 1 : 0;
            }

            //quieto
            else
            {
                Image.isActive = false;
                Image.spriteSheetEffect.currentFrame.X = 0;
                Image.spriteSheetEffect.currentFrame.Y = (lookLeft) ? 1 : 0;
            }

            Image.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Image.Draw(spriteBatch);
        }

        private void saltar(GameTime gameTime)
        {
            //Animacion


            if (Jumping)
            {
                Image.isActive = false;
                Image.spriteSheetEffect.currentFrame.X = 4;
                Image.spriteSheetEffect.currentFrame.Y = (lookLeft) ? 1 : 0;
                Image.position.Y -= Velocity.Y * (float)gameTime.ElapsedGameTime.TotalSeconds;

                //Si La posición en Y es mayor o igual a la Y inicial + lo que salta...
                if (Image.position.Y <= finalY)
                    Jumping = false;
            }
        }
    }
}
