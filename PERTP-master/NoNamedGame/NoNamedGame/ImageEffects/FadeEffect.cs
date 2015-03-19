using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

namespace NoNamedGame.ImageEffects
{
    public class FadeEffect : NoNamedGame.ImageEffects.ImageEffect
    {
        //Atributos
        public float fadeSpeed;
        public float pauseTimeSeconds;
        private Boolean increasing;
        private Boolean onPause;
        private float onPauseCounter;

        public FadeEffect()
        {
            //Default
            fadeSpeed = 1.0F;
            pauseTimeSeconds = 0;
            increasing = false;
            onPause = false;
        }

        public override void LoadContent(ref Image image)
        {
            base.image = image;
        }

        public override void UnloadContent()
        {

        }

        /* Este método lleva a cabo los cambios de valores
         * en alpha para el efecto Fade.
         * */
        public override void Update(GameTime gameTime)
        {
            // Lo hace sólo si la imagen está activa. Sino, alpha = 1.0
            if (image.isActive)
            {
                //Si no está en la pausa, incrementa-decrementa el alpha
                if (!onPause)
                {
                    if (increasing && !onPause)
                        image.alpha += fadeSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    else
                        image.alpha -= fadeSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;

                    //Si alcanza 1.0 0 0.0, cambia el increasing
                    if (image.alpha >= 1.0F)
                    {
                        increasing = false;
                        image.alpha = 1.0F;
                        onPause = true;
                    }

                    else if (image.alpha <= 0.0F)
                    {
                        increasing = true;
                        image.alpha = 0.0F;
                    }
                }

                //Si está en pausa
                else 
                {
                    //El contador de pausa se va sumando hasta alcanzar el tiempo límite de pausa (pauseTimeSeconds)
                    onPauseCounter += (float)gameTime.ElapsedGameTime.TotalSeconds;

                    if(onPauseCounter >= pauseTimeSeconds)
                    {
                        onPause = false;
                        onPauseCounter = 0;
                    }
                }
            }


            else
                image.alpha = 1.0F;
        }
    }
}
