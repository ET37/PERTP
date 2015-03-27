using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

namespace NoNamedGame.ImageEffects
{
    public class SpriteSheetEffect : ImageEffect
    {
        public int FrameCounter;
        public int SwitchFrame;

        public Vector2 currentFrame;
        public Vector2 amountOfFrames;

        public int FrameWidth
        {
            get
            {
                if (image.texture != null)
                    return image.texture.Width / (int)amountOfFrames.X;
                return 0;
            }
        }

        public int FrameHeight
        {
            get
            {
                if (image.texture != null)
                    return image.texture.Height / (int)amountOfFrames.Y;
                return 0;
            }
        }

        public SpriteSheetEffect()
        {
            amountOfFrames = new Vector2(4, 1);
            currentFrame = new Vector2(1, 0);
            FrameCounter = 3;
            SwitchFrame = 18;
        }

        public override void LoadContent(ref Image image)
        {
            base.LoadContent(ref image);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (image.isActive)
            {
                FrameCounter += (int)gameTime.ElapsedGameTime.TotalMilliseconds;
                if (FrameCounter >= SwitchFrame)
                {
                    FrameCounter = 0;
                    currentFrame.X++;

                    if (currentFrame.X * FrameWidth >= image.texture.Width)
                        currentFrame.X = 0;
                }
            }

            image.sourceRect = new Rectangle((int)currentFrame.X * FrameWidth, (int)currentFrame.Y * FrameHeight,
                                            FrameWidth, FrameHeight);
        }

        public override void Draw()
        {
            base.Draw();
        }
    }
}
