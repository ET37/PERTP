using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using NoNamedGame.Buttons;

namespace NoNamedGame.Screens
{
    public class LevelSelectScreen : Screen
    {
        private String imagesPath;

        private Image logoImage;
        private Image backgroundImage;

        private List<Image> drawings;
        
        //Botones con los niveles
        //private List<ButtonLevel> niveles;

        public LevelSelectScreen()
        {
            base.Type = this.GetType();
            drawings = new List<Image>();
            imagesPath = "MenuScreen/";

            //Imágenes
            logoImage = new Image();
            backgroundImage = new Image();
        }

        public override void LoadContent(ContentManager Content)
        {
            logoImage.path = this.imagesPath + "Logo";
            backgroundImage.path = this.imagesPath + "Background";
            
        }

        public override void Update(GameTime gameTime)
        {

        }

        public override void Draw(SpriteBatch spriteBatch)
        {

        }

        private void cargarNiveles()
        {

        }
    }
}
