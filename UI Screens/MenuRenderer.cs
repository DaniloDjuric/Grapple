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
    internal class MenuRenderer
    {
        Button pauseButton;
        Button restartButton;
        Button leaderBoardButton;
        Button lelevSelectButton;
        Button settingsButton;
        Button quitButton;

        Texture2D pauseButtonTexture;
        Texture2D leaderBoardTexture;
        Texture2D UIArt;

        public MenuRenderer()
        {
            leaderBoardTexture = Globals.Content.Load<Texture2D>("Leaderboard");
            pauseButtonTexture = Globals.Content.Load<Texture2D>("pause_button");
            UIArt = Globals.Content.Load<Texture2D>("Grapple_Concept_Art");
            
            pauseButton = new Button("Pause", pauseButtonTexture, 700, 35);
            restartButton = new Button("Restart level", 200, 50, 100, 200);
            lelevSelectButton = new Button("Select Level", 200, 50, 100, 250);
            settingsButton = new Button("Settings", 150, 50, 100, 300);
            quitButton = new Button("Quit", 100, 50, 100, 350);
            leaderBoardButton = new Button("Leaderboard", leaderBoardTexture, 500, 200);
        }

        public void Draw()
        {
            SpriteFont SpriteFont = Globals.Content.Load<SpriteFont>("galleryFont");

            // UI Write
            Globals.SpriteBatch.DrawString(SpriteFont, "Paused", new Vector2(100, 150), Color.White);
            Globals.SpriteBatch.DrawString(SpriteFont, "Leaderboard", new Vector2(440, 250), Color.Black);

            pauseButton.Display();
            lelevSelectButton.Display();
            settingsButton.Display();
            quitButton.Display();
            restartButton.Display();
            leaderBoardButton.Display();
        }
    }
}
