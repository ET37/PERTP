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
        [XmlElement("alpha")]
        public float alpha;
        public float fadeSpeed;
        //Timepo en que la splashImage se mostrará en pantalla
        public float pauseTime;

        //Para que no se busque instanciar con XmlSerialization
        [XmlIgnore]
        private ArrayList splashImages;


        public override void Initializate()
        {
            splashImages = new ArrayList();
        }

        /*Se cargan todas las SplashImages de la carpeta en un while(true)
         * y cuando se genera la Exception va a significar que ya no hay imágenes
         * que cargar, por lo tanto finaliza la carga de contenido
         * */
        public override void LoadContent(ContentManager Content)
        {
            try
            {
                splashImageNumber = 0;
                while (true)
                {
                    splashImages.Add(Content.Load<Texture2D>(imagesPath + splashImageNumber.ToString()));
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
            if (splashImages.Count != 0)
                spriteBatch.Draw((Texture2D)splashImages[splashImageNumber], Vector2.Zero, Color.White * alpha);
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
