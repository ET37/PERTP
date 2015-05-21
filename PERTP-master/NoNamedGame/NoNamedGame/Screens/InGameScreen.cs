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

        public Map map;
        private List<Image> drawings;

        public InGameScreen()
        {
            Console.WriteLine("> inGameScreen iniciado");
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

            //TODO: Encontrarle utilidad a esto, no sirve.
            //Cargar las imágenes
            foreach (Image image in drawings)
                image.Loadcontent();

        }

        public override void Update(GameTime gameTime)
        {
            //Updateo el mapa y despues el jugador
            map.Update(gameTime);
            Player.Instance.Update(gameTime, map);
            
            //Si el jugador saltó y ahora está cayendo
            if (!Player.Instance.Jumping && Player.Instance.Falling)
            {
                if (Player.Instance.Image.position.Y <= ScreenManager.Instance.dimensions.Y)
                    Player.Instance.Image.position.Y += GRAVITY * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }

            //Si el jugador cayó al vacio (se sale del mapa, osea que su pos. es la del final en Y de la pantalla + tamaño del sprite)
            if (Player.Instance.Image.position.Y > ScreenManager.Instance.dimensions.Y - Player.Instance.Image.texture.Height)
                Player.Instance.Image.position = Player.Instance.InitialPos;

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            //Mapa primero ^^
            map.Draw(spriteBatch);

            //Y ahora el jugador :3
            Player.Instance.Draw(spriteBatch);
            
        }
    }
}
