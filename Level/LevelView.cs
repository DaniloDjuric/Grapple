using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Grapple.General;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
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

        public LevelView()
        {
            uiRenderer = new UIRenderer();
            menuRenderer = new MenuRenderer();
            leaderboardRenderer = new LeaderboardRenderer();

            ninjaIdle = Globals.Content.Load<Texture2D>("DASH");
            ninjaAttack = Globals.Content.Load<Texture2D>("ATTACK 1");
            balloonSprite = Globals.Content.Load<Texture2D>("balloon");
            platformSprite = Globals.Content.Load<Texture2D>("platform");

            flyAnimation = new(ninjaIdle, 8, 0.1f);
            attackAnimation = new(ninjaAttack, 7, 0.1f);
        }

        public void Update(GameTime gametime) {
            if (!paused)
            {
                time -= Globals.TotalSeconds*1000; // Convert to miliseconds for better precission
            }
        }
        public void Draw(LevelModel levelModel)
        {
            if (time <= 0)
            {
                Globals.GameRunning = false;
            }

            if (!paused)
            {
                // Draw platforms
                foreach (var platform in levelModel.Platforms)
                {
                    Globals.SpriteBatch.Draw(platformSprite,
                        new Rectangle((int)platform.X, (int)platform.Y, (int)platform.Width, (int)platform.Height),
                        new Rectangle(130, 70, 170, 800),
                        Color.White);
                }

                // Draw balloons
                foreach (var enemy in levelModel.Balloons)
                {
                    Globals.SpriteBatch.Draw(balloonSprite,
                        new Rectangle((int)enemy.X, (int)enemy.Y, (int)enemy.Width, (int)enemy.Height),
                        Color.Lime);

                    // Baloon hitbox - debug
                    Globals.DrawRect(Globals.SpriteBatch, new Rectangle((int)enemy.X, (int)enemy.Y, (int)enemy.Width, (int)enemy.Height), Color.DarkRed);
                }

                // Draw player
                flyAnimation.Update();
                flyAnimation.Draw(levelModel.Player.Position, LevelController.targetPosition.X >= levelModel.Player.X);

                // Player hitbox - debug
                Globals.DrawRect(Globals.SpriteBatch, new Rectangle((int)levelModel.Player.X, (int)levelModel.Player.Y, (int)levelModel.Player.Width, (int)levelModel.Player.Height), Color.Green);

                Globals.DrawLine(Globals.SpriteBatch, levelModel.Player.Center, Physics.CalculateTargetPosition(levelModel.Player.Center, LevelController.targetPosition), Color.Black, 3);

                // UI  
                uiRenderer.Draw(time);
            }
            else if (!leaderboard && paused)
            {
                menuRenderer.Draw();
            }
            else if (leaderboard && paused)
            {
                leaderboardRenderer.Draw();
            }
        }
    }
}
