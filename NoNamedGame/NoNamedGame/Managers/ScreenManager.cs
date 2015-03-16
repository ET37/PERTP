using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using NoNamedGame.Screens;

namespace NoNamedGame.Managers
{
    public class ScreenManager
    {
        //Atributos
        [XmlIgnore]
        private static ScreenManager instance;
        [XmlIgnore]
        private GraphicsDeviceManager graphics;
        public Vector2 dimensions;
        [XmlIgnore]
        private Screen currentScreen;
        [XmlIgnore]
        private Screen oldScreen;
        [XmlIgnore]
        private XmlManager<Screen> xmlManager;
        public Boolean isTransitioning;

        //Constructor privado
        private ScreenManager()
        {
            //La primera pantalla del juego van a ser las imágenes de presentación (SplashScreens)
            currentScreen = new SplashScreen();
            oldScreen = currentScreen;

            //Se instancia el xmlManager
            xmlManager = new XmlManager<Screen>();
            xmlManager.Type = currentScreen.GetType();
            currentScreen = xmlManager.Load("Load/SplashScreen.xml");
        }

        public static ScreenManager Instance
        {
            get
            {
                //Si instance es null, crea una nueva instancia usando el constructor privado
                if (instance == null)
                {
                    //Se instancia desde el constructor
                    instance = new ScreenManager();
                    //Y desde ScreenManager.xml
                    XmlManager<ScreenManager> xml = new XmlManager<ScreenManager>();
                    instance = xml.Load("Load/ScreenManager.xml");
                }
                return instance;
            }
        }

        // |-----------------Métodos comunes-------------------|

        public void Initializate(GraphicsDeviceManager graphics) 
        {
            this.graphics = graphics;

            SetDimensions(dimensions);

            Game1.Instance.IsMouseVisible = true;

            currentScreen.Initializate();
        }

        public void LoadContent(ContentManager Content) 
        {
            currentScreen.LoadContent(Content);
        }

        public void UnloadContent(ContentManager Content)
        {
            currentScreen.UnloadContent(Content);
        }

        public void Update(GameTime gameTime)
        {
            currentScreen.Update(gameTime);
            InputManager.Instance.Update();
            Transition(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            currentScreen.Draw(spriteBatch);
        }

        // |------------Métodos propios-----------------------|

        public void ChangeScreen(String screenName) 
        {
            currentScreen = (Screen)Activator.CreateInstance(Type.GetType("NoNamedGame.Screens." + screenName));
            isTransitioning = true;
        }

        private void Transition(GameTime gameTime) 
        {
            if (isTransitioning)
            {
                //Acá iría el fade in-out en Image.Update()
                oldScreen = currentScreen;
                xmlManager.Type = currentScreen.Type;
                currentScreen = xmlManager.Load("Load/" + currentScreen.GetType().ToString().Replace("NoNamedGame.Screens.","") + ".xml");
                currentScreen.LoadContent(Game1.Instance.Content);
            }
        }

        // |-------------Getters & Setters-------------------|

        public Vector2 GetDimensions()
        {
            return this.dimensions;
        }

        public void SetDimensions(Vector2 dimensions)
        {
            this.dimensions = dimensions;

            //Si sólo cambiamos las dimensiones no pasa nada, hay que aplicar cambios y cambiar el PreferedBackBuffer
            graphics.PreferredBackBufferWidth = Convert.ToInt32(dimensions.X);
            graphics.PreferredBackBufferHeight = Convert.ToInt32(dimensions.Y);
            graphics.ApplyChanges();
        }
    }
}
