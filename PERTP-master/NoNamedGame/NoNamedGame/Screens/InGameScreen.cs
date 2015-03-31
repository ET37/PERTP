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

        private List<Image> drawings;

        public InGameScreen()
        {
            drawings = new List<Image>();
        }

        public override void LoadContent(ContentManager Content)
        {
            Player.Instance.LoadContent();

            foreach (Image image in drawings)
                image.Loadcontent();
        }

        

        public override void Update(GameTime gameTime)
        {
            Player.Instance.Update(gameTime);
            if (!Player.Instance.Jumping)
            {
                if (Player.Instance.Image.position.Y <= ScreenManager.Instance.dimensions.Y - 100)
                    Player.Instance.Image.position.Y += GRAVITY * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            Player.Instance.Draw(spriteBatch);
        }
    }
}
