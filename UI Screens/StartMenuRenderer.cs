using Grapple.General;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grapple.UI_Screens
{
    internal class StartMenuRenderer
    {
        Button playButton;
        Button quitButton;

        Texture2D playButtonTexture;

        public StartMenuRenderer()
        {
            playButtonTexture = Globals.Content.Load<Texture2D>("balloon");

            playButton = new Button("Play", playButtonTexture, 340, 150);
            quitButton = new Button("Quit", 100, 50, 375, 400);
        }

        public void Draw()
        {
            SpriteFont SpriteFont = Globals.Content.Load<SpriteFont>("galleryFont");

            // UI Write
            Globals.SpriteBatch.DrawString(SpriteFont, "Grapple Gang", new Vector2(200, 50), Color.White, 0f, Vector2.One, 2f, SpriteEffects.None, 1f) ;
            Globals.SpriteBatch.DrawString(SpriteFont, "Play", new Vector2(375, 200), Color.White);
            playButton.Display();
            quitButton.Display();

        }
    }
}
