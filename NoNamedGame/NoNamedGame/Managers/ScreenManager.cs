using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace NoNamedGame.Managers
{
    public class ScreenManager
    {
        //Atributos
        private static ScreenManager instance;
        private GraphicsDeviceManager graphics;
        private Vector2 dimensions;
        //private Screen currentScreen;

        //Constructor privado
        private ScreenManager()
        {
            //Dimensiones de la pantalla
            dimensions = new Vector2();
            dimensions.X = 800;
            dimensions.Y = 600;
            //currentScreen = menuScreen? / presentationScreen?
        }

        public static ScreenManager Instance
        {
            get
            {
                //Si instance es null, crea una nueva instancia usando el constructor privado
                if (instance == null)
                {
                    instance = new ScreenManager();
                }
                return instance;
            }
        }

        // |-----------------Métodos comunes-------------------|

        public void Initializate(GraphicsDeviceManager graphics) 
        {
            this.graphics = graphics;

            /* Esto se hace en éste método y no en el constructor 
             * porque todavía no tendríamos los graphics del Game1*/
            graphics.PreferredBackBufferWidth = Convert.ToInt32(dimensions.X);
            graphics.PreferredBackBufferHeight = Convert.ToInt32(dimensions.Y);
            graphics.ApplyChanges();
        }

        public void LoadContent(ContentManager Content) 
        {
            //currentScreen.Loadcontent(Content);
        }

        public void UnloadContent(ContentManager Content)
        {
            //currentScreen.UnloadContent(Content);
        }

        public void Update(GameTime gameTime)
        {
            //currentScreen.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            //currentScreen.Draw(spriteBatch);
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
