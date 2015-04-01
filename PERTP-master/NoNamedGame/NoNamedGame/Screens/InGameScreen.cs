using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using NoNamedGame.Managers;

namespace NoNamedGame.Screens
{
    public class InGameScreen : Screen
    {
        private const float GRAVITY = 300;

        private Map map;
        private List<Image> drawings;

        public InGameScreen()
        {
            drawings = new List<Image>();
        }

        public override void LoadContent(ContentManager Content)
        {
            //Cargar el player (singleton ftw)
            Player.Instance.LoadContent();

            //Inicializar el serializador para la clase Map
            XmlManager<Map> mapLoader = new XmlManager<Map>();

            //Cargar el archivo xml con el map y lo inicializo
            map = mapLoader.Load("Maps/Map1.xml");
            map.LoadContent();

            //Cargar las imágenes
            foreach (Image image in drawings)
                image.Loadcontent();

        }

        

        public override void Update(GameTime gameTime)
        {
            Player.Instance.Update(gameTime);
            if (!Player.Instance.Jumping)
            {
                if (Player.Instance.Image.position.Y <= ScreenManager.Instance.dimensions.Y)
                    Player.Instance.Image.position.Y += GRAVITY * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }

            map.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            Player.Instance.Draw(spriteBatch);
            map.Draw(spriteBatch);
        }
    }
}
