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
    internal class LeaderboardRenderer
    {
        Button pauseButton;
        Texture2D pauseButtonTexture;
        SpriteFont leaderboardFont;
        
        public LeaderboardRenderer()
        {
            pauseButtonTexture = Globals.Content.Load<Texture2D>("pause_button");
            pauseButton = new Button("Pause", pauseButtonTexture, 700, 35);
            leaderboardFont = Globals.Content.Load<SpriteFont>("LeaderboardFont");
        }

        public void Draw()
        {
            pauseButton.Display();

            Globals.SpriteBatch.DrawString(leaderboardFont, "Leaderboard", new Vector2(100, 50), Color.White);

            Globals.SpriteBatch.DrawString(leaderboardFont, $"{Globals.HighScoreManager.HighScores[0].Key,-12} - {Globals.HighScoreManager.HighScores[0].Value, 4}", new Vector2(100, 160), Color.LightGray);

            Globals.SpriteBatch.DrawString(leaderboardFont, $"{Globals.HighScoreManager.HighScores[1].Key,-12} - {Globals.HighScoreManager.HighScores[1].Value, 4}", new Vector2(100, 200), Color.LightGray);

            Globals.SpriteBatch.DrawString(leaderboardFont, $"{Globals.HighScoreManager.HighScores[2].Key,-12} - {Globals.HighScoreManager.HighScores[2].Value, 4}", new Vector2(100, 240), Color.LightGray);

        }
    }
}
