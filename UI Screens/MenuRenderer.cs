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
        SpriteFont spriteFont;

        public MenuRenderer(ContentManager content)
        {
            leaderBoardTexture = content.Load<Texture2D>("Leaderboard");
            pauseButtonTexture = content.Load<Texture2D>("pause_button");
            UIArt = content.Load<Texture2D>("Grapple_Concept_Art");
            spriteFont = content.Load<SpriteFont>("galleryFont");
            
            pauseButton = new Button("pause", pauseButtonTexture, 700, 35);
            restartButton = new Button("Restart level", 200, 50, 100, 200);
            lelevSelectButton = new Button("Select Level", 200, 50, 100, 250);
            settingsButton = new Button("Settings", 150, 50, 100, 300);
            quitButton = new Button("Quit", 100, 50, 100, 350);
            leaderBoardButton = new Button("Leaderboard", leaderBoardTexture, 500, 200);
        }

        public void Draw(SpriteBatch spriteBatch)
        {

            // UI Write
            spriteBatch.DrawString(spriteFont, "Paused", new Vector2(100, 150), Color.White);
            spriteBatch.DrawString(spriteFont, "Leaderboard", new Vector2(440, 250), Color.Black);

            pauseButton.Update();
            pauseButton.Draw(spriteBatch, spriteFont);

            lelevSelectButton.Update();
            lelevSelectButton.Draw(spriteBatch, spriteFont);
            settingsButton.Update();
            settingsButton.Draw(spriteBatch, spriteFont);
            quitButton.Update();
            quitButton.Draw(spriteBatch, spriteFont);
            restartButton.Update();
            restartButton.Draw(spriteBatch, spriteFont);
            leaderBoardButton.Update();
            leaderBoardButton.Draw(spriteBatch, spriteFont);
        }
    }
}
