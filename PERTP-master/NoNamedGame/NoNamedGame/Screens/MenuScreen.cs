using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace NoNamedGame.Screens
{
    public class MenuScreen : Screen
    {

        public NoNamedGame.Image image;

        public MenuScreen() 
        {
            base.Type = this.GetType();
        }

        public override void Initializate()
        {

        }

        public override void LoadContent(ContentManager Content)
        {
            image.Loadcontent(Content);
        }

        public override void UnloadContent(ContentManager Content)
        {

        }

        public override void Update(GameTime gameTime)
        {

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            image.Draw(spriteBatch);
        }
    }
}
