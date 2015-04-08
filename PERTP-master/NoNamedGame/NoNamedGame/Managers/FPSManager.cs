﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace NoNamedGame.Managers
{
    //SINGLETON
    public class FPSManager
    {
        //Frames por segundo
        private float fps;
        //Contador de frames
        private float frameCounter;
        //Controla el tiempo entre frame y frame
        private float milisecondsElapsed;
        //instancia estática
        private static FPSManager instance;
        private SpriteFont myfont;
        private Boolean showingFps;
        private Vector2 position;

        //constructor privado
        private FPSManager() 
        {
            fps = 0;
            frameCounter = 0;
            milisecondsElapsed = 0;
            showingFps = true;
            position = new Vector2(3);
            LoadContent(Game1.Instance.Content);
        }

        //Instancia geteable
        public static FPSManager Instance 
        {
            get 
            {
                if (instance == null)
                    instance = new FPSManager();

                return instance;
            }
        }

        private void LoadContent(ContentManager Content) 
        {
            myfont = Content.Load<SpriteFont>("Fonts/MyFont");
        }

        public void Update(GameTime gameTime) 
        {
            if (InputManager.Instance.KeyPressed(Keys.P))
            {
                if (!showingFps)
                    showingFps = true;
                else
                    showingFps = false;
            }

            if (showingFps)
            {
                milisecondsElapsed += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            }
        }

        public void Draw(SpriteBatch spriteBatch) 
        {
            if (showingFps)
            {
                if (milisecondsElapsed < 1000)
                {
                    frameCounter++;
                }

                else 
                {
                    fps = frameCounter;
                    frameCounter = 0;
                    milisecondsElapsed = 0;
                }

                spriteBatch.DrawString(myfont, ((int)fps).ToString(), position, Color.DarkRed);
            }
        }
    }
}
