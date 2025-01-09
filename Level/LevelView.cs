using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using static System.Formats.Asn1.AsnWriter;

namespace Grapple.Level
{
    /*  Level View tasks:
     *  - Load in all the textures and animations
     *  - Call the draw funcion to loop through the Model
     *  - Display GUI elements (score, time, etc.)                 
     */

    internal class LevelView
    {
        UIRenderer uiRenderer;
        MenuRenderer menuRenderer;
        LeaderboardRenderer leaderboardRenderer;

        Texture2D balloonSprite;
        Texture2D platformSprite;

        Texture2D ninjaIdle;
        Texture2D ninjaAttack;

        public static float time = 60000;
        public static bool paused = false;
        public static bool leaderboard = false;

        private Animation attackAnimation;
        private Animation flyAnimation;
        private Animation wallAnimation;

        public LevelView(ContentManager content)
        {
            uiRenderer = new UIRenderer(content);
            menuRenderer = new MenuRenderer(content);
            leaderboardRenderer = new LeaderboardRenderer(content);

            ninjaIdle = content.Load<Texture2D>("IDLE");
            ninjaAttack = content.Load<Texture2D>("ATTACK 1");
            balloonSprite = content.Load<Texture2D>("balloon");
            platformSprite = content.Load<Texture2D>("platform");

            flyAnimation = new(ninjaIdle, 10, 0.1f);
            attackAnimation = new(ninjaAttack, 7, 0.1f);
        }

        public void Update(GameTime gametime) {
            if (!paused)
            {
                time -= (float)gametime.ElapsedGameTime.TotalMilliseconds;
            }
        }
        public void Draw(SpriteBatch spriteBatch, LevelModel levelModel)
        {
            if (!paused)
            {

                // Draw platforms
                foreach (var platform in levelModel.Platforms)
                {
                    spriteBatch.Draw(platformSprite,
                        new Rectangle((int)platform.X, (int)platform.Y, (int)platform.Width, (int)platform.Height),
                        new Rectangle(130, 70, 170, 800),
                        Color.White);
                }

                // Draw balloons
                foreach (var enemy in levelModel.Balloons)
                {
                    spriteBatch.Draw(balloonSprite,
                        new Rectangle((int)enemy.X, (int)enemy.Y, (int)enemy.Width, (int)enemy.Height),
                        Color.Lime);
                }

                // Draw player
                attackAnimation.Update();
                attackAnimation.Draw(levelModel.Player.Position, spriteBatch);

                // 
                uiRenderer.Draw(spriteBatch, time, levelModel);
            }
            else if (!leaderboard && paused)
            {
                menuRenderer.Draw(spriteBatch);
            }
            else if (leaderboard && paused)
            {
                leaderboardRenderer.Draw(spriteBatch);
            }
        }
    }
}
