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
        private Player player;
        private const float GRAVITY = 300;

        private List<Image> drawings;

        public InGameScreen()
        {
            player = new Player();
            drawings = new List<Image>();
        }

        public override void LoadContent(ContentManager Content)
        {
            player.LoadContent();

            foreach (Image image in drawings)
                image.Loadcontent();
        }

        

        public override void Update(GameTime gameTime)
        {
            player.Update(gameTime);
            if (!player.Jumping)
            {
                if (player.Image.position.Y <= ScreenManager.Instance.dimensions.Y - 100)
                    player.Image.position.Y += GRAVITY * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            player.Draw(spriteBatch);
        }
    }
}
