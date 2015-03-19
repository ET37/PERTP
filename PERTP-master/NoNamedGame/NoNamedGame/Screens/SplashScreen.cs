using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using NoNamedGame.Managers;

namespace NoNamedGame.Screens
{
    /*Aclaración 1: Los atributos se crean públicos para que 
     * el XmlSerializator los pueda instanciar, de lo contrario
     * no funciona. */
    /*Aclaración 2: Para que el juego cargue correctamente las
     * SplashImages se tienen que llamar como archivo 'SplashImageN',
     * siendo la última letra N el número de splash. (Se empieza desde 0)
     *  Ejemplo: SplashImage0 - SplashImage1 - SplashImage2*/
    public class SplashScreen : Screen
    {
        //Path base de las SplashImages
        public String imagesPath;
        private int splashImageNumber;
        private int splashImageNumberLimit;

        //Para que no se busque instanciar con XmlSerialization
        [XmlIgnore]

        //Implementación Image
        private List<Image> splashImages;


        public override void Initializate()
        {
            splashImages = new List<Image>();
        }

        /* Se cargan todas las SplashImages de la carpeta en un while(true)
         * y cuando se genera un ContentLoadException significará que ya no hay imágenes
         * que cargar, por lo tanto finaliza la carga de contenido.
         * 
         * ------------------------------------------------------
         * 
         * Ahora en vez de cargar el contenido como Texture2D y guardarlo en un ArrayList
         * de texture2Ds se crean instancias de Image y se llama a su método LoadContent()
         * para que image se haga cargo. Y al final, en el Draw de ésta clase
         * se llama al Draw de la image, ya no se dibuja la textura manualmente desde 
         * esta clase para poder así tener la posibilidad de manejar cosas como
         * el alpha, el scale, etc.
         * */
        public override void LoadContent(ContentManager Content)
        {
            try
            {
                splashImageNumber = 0;
                while (true)
                {
                    Image image = new Image(imagesPath + splashImageNumber.ToString(), Vector2.Zero, Vector2.One, 1.0F);
                    //No se pone solo FadeEffect, se pone las subcarpetas con puntos y el efecto al final
                    //Estuve como 25 min para darme cuenta -.-" t-t En el video no lo dice porque no lo tiene en subfolder
                    image.effects.Add("ImageEffects.FadeEffect");
                    image.isActive = true;
                    image.Loadcontent(Content);
                    image.fadeEffect.fadeSpeed = 0.5F;
                    image.fadeEffect.pauseTimeSeconds = 2.0F;
                    splashImages.Add(image);
                    splashImageNumber++;
                }
            }

            catch (ContentLoadException)
            {
                splashImageNumberLimit = splashImageNumber - 1;
                splashImageNumber = 0;
            }
        }

        public override void UnloadContent(ContentManager Content)
        {

        }

        //Se encarga del fadeIn y fadeOut de las splashImages
        public override void Update(GameTime gameTime)
        {
            if (splashImages.Count == 0)
                ChangeScreen();

            splashImages[splashImageNumber].Update(gameTime);

            //Cambia de splashImage al tocar enter
            if (InputManager.Instance.KeyReleased(Keys.Enter))
            {
                ChangeScreen();
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            //Llama al método Draw de la Image correspondiente
            if (splashImages.Count != 0)
                splashImages[splashImageNumber].Draw(spriteBatch);
        }

        private void ChangeScreen() 
        {
            splashImageNumber++;

            //Si el índice se pasa del límite, cambia de SplashScreen a MenuScreen
            if (splashImageNumber > splashImageNumberLimit)
            {
                ScreenManager.Instance.ChangeScreen("MenuScreen");
            }
        }
    }
}
