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
        SpriteFont spriteFont;

        public LeaderboardRenderer(ContentManager content)
        {
            pauseButtonTexture = content.Load<Texture2D>("pause_button");
            pauseButton = new Button("pause", pauseButtonTexture, 700, 35);
            spriteFont = content.Load<SpriteFont>("galleryFont");

        }

        public void Draw(SpriteBatch spriteBatch)
        {


            pauseButton.Update();
            pauseButton.Draw(spriteBatch, spriteFont);
           
            spriteBatch.DrawString(spriteFont, "Leaderboard", new Vector2(100, 50), Color.White);

            spriteBatch.DrawString(spriteFont, "1. Unknown", new Vector2(100, 150), Color.White);

            spriteBatch.DrawString(spriteFont, "2. Unknown", new Vector2(100, 200), Color.White);

            spriteBatch.DrawString(spriteFont, "3. Unknown", new Vector2(100, 250), Color.White);

        }
    }
}
