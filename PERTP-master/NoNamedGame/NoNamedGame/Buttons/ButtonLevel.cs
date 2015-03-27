using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace NoNamedGame.Buttons
{

    public class ButtonLevel : Button
    {
        private string texturePath;

        public ButtonLevel(string texturePath, Vector2 position)
            : base(texturePath, position)
        {
            this.texturePath = texturePath;
            this.position = position;
        }

        //Métodos del botón

        public override void LoadContent(ContentManager Content)
        {
            texture = Content.Load<Texture2D>(texturePath);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }

        public override void OnClick()
        {
            //TODO: Ir al nivel seleccionado
            //Managers.ScreenManager.Instance.ChangeScreen("NombreDelNivel");
        }

        
    }
}
