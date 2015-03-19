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

        //Transparencia de las splashImages
        public float alpha;
        public float fadeSpeed;
        //Timepo en que la splashImage se mostrará en pantalla
        public float pauseTime;

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
                    image.Loadcontent(Content);
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
