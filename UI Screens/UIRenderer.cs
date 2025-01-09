using Grapple.Level;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;

namespace Grapple
{
    internal class UIRenderer
    {
        Button pauseButton;
        Texture2D pauseButtonTexture;
        Texture2D UIArt;
        SpriteFont spriteFont;

        public UIRenderer(ContentManager content)
        {
            pauseButtonTexture = content.Load<Texture2D>("pause_button");
            UIArt = content.Load<Texture2D>("Grapple_Concept_Art");
            spriteFont = content.Load<SpriteFont>("galleryFont");
            
            pauseButton = new Button("pause", pauseButtonTexture, 700, 35);
        }

        public void Draw(SpriteBatch spriteBatch, float time, LevelModel levelModel)
        {

            // UI Write
            spriteBatch.DrawString(spriteFont, $"Score: {LevelModel.Score}", new Vector2(100, 30), Color.White);
            spriteBatch.DrawString(spriteFont, $"Time: {(time / 1000).ToString("0.00")} s", new Vector2(300, 30), Color.White);
            spriteBatch.Draw(UIArt,
                    new Rectangle(250, 30, 40, 40),
                    new Rectangle(1720, 1650, 700, 700),
                    Color.White);

            spriteBatch.Draw(UIArt,
                    new Rectangle(50, 30, 50, 50),
                    new Rectangle(2350, 200, 1100, 1100),
                    Color.White);

            pauseButton.Update();
            pauseButton.Draw(spriteBatch, spriteFont);
        }
    }
}
