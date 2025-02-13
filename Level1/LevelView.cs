using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Grapple.General;
using Grapple.UI_Screens;
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
        SettingsRenderer settingsRenderer;
        public NameInputUI nameInputUI;

        Texture2D spriteMap;
        Texture2D background;
        Texture2D ninjaIdle;
        Texture2D ninjaAttack;

        private Animation attackAnimation;
        private Animation flyAnimation;
        private Animation wallAnimation;

        public LevelView(LevelModel levelModel)
        {
            nameInputUI = new NameInputUI();
            uiRenderer = new UIRenderer();
            menuRenderer = new MenuRenderer();
            settingsRenderer = new SettingsRenderer();
            leaderboardRenderer = new LeaderboardRenderer();

            background = GameController.currentLevel == 1 ? 
                Globals.Content.Load<Texture2D>("dojo_bg") :
                Globals.Content.Load<Texture2D>($"dojo_bg_{GameController.currentLevel}");
            ninjaIdle = Globals.Content.Load<Texture2D>("DASH");
            ninjaAttack = Globals.Content.Load<Texture2D>("ATTACK 1");
            spriteMap = Globals.Content.Load<Texture2D>("Grapple_Concept_Art"); // 210, 230
            flyAnimation = new(ninjaIdle, 8, 0.1f);
            attackAnimation = new(ninjaAttack, 7, 0.1f);
        }

        private Vector2 CalculateBackgroundScale()
        {
            float screenWidth = Globals.GraphicsDevice.Viewport.Width;
            float screenHeight = Globals.GraphicsDevice.Viewport.Height;

            float scaleX = screenWidth / (float)background.Width;
            float scaleY = screenHeight / (float)background.Height;

            // Choose the larger scale to ensure full coverage
            return new Vector2(scaleX, scaleY);
        }

        public void Update(GameTime gametime) {
            if (!LevelModel.isPaused)
            {
                LevelModel.Time -= Globals.TotalSeconds*1000; // Convert to miliseconds for better precission
            }
            
            // Name input
            if (Globals.GameRunning && nameInputUI.IsActive()) nameInputUI.Deactivate();
            if (!Globals.GameRunning && LevelModel.isPaused) nameInputUI.Deactivate();
            if (!Globals.GameRunning && !LevelModel.isPaused && !nameInputUI.IsActive()) nameInputUI.Activate();
            if (nameInputUI.IsActive()) nameInputUI.Update();
        }
        public void Draw(LevelModel levelModel)
        {
            if (LevelModel.Time <= 0)
            {
                Globals.GameRunning = false;
            }

            Globals.SpriteBatch.Draw(background, new Vector2(0, 0), new Rectangle(0, 0, background.Width, background.Height), Globals.darkBG, 0f, new Vector2(), CalculateBackgroundScale(), SpriteEffects.None, 0);
            
            if (nameInputUI.IsActive())
            {
                nameInputUI.Draw();
            }

            if (!LevelModel.isPaused)
            {
                // Draw platforms
                for (int i = 0; i < levelModel.Platforms.Count; i++)
                {
                    var platform = levelModel.Platforms[i];

                    if (i < 4)
                    {
                        Globals.SpriteBatch.Draw(spriteMap,
                            new Rectangle((int)platform.X, (int)platform.Y, (int)platform.Width, (int)platform.Height),
                            new Rectangle(1250, 75, 170, 800),
                            Color.White);
                    }
                    else
                    {
                        Globals.SpriteBatch.Draw(spriteMap,
                            new Rectangle((int)platform.X, (int)platform.Y, (int)platform.Width, (int)platform.Height),
                            new Rectangle(1750, 125, 160, 200),
                            Color.White);
                        if(platform.Width > platform.Height)
                        {

                            Globals.SpriteBatch.Draw(spriteMap,
                                new Rectangle((int)platform.X, (int)platform.Y, 10, 40),
                                new Rectangle(1180, 1630, 400, 600),
                                Color.White);
                            Globals.SpriteBatch.Draw(spriteMap,
                                new Rectangle((int)platform.X + (int)platform.Width - 10,
                                (int)platform.Y,
                                10, 40),
                                new Rectangle(1180, 1630, 400, 600),
                                Color.White);
                        }
                        else
                        {
                            Globals.SpriteBatch.Draw(spriteMap,
                                new Rectangle((int)platform.X, (int)platform.Y + 10, 10, 40),
                                new Rectangle(1180, 1630, 400, 600),
                                Color.White, (float)-Math.PI/2, new Vector2(), SpriteEffects.None, 0);
                            Globals.SpriteBatch.Draw(spriteMap,
                                new Rectangle((int)platform.X,
                                (int)platform.Y + (int)platform.Height,
                                10, 40),
                                new Rectangle(1180, 1630, 400, 600),
                                Color.White, (float)-Math.PI / 2, new Vector2(), SpriteEffects.None, 0);
                        }
                    }
                }

                // Draw balloons
                foreach (var enemy in levelModel.Balloons)
                {
                    Globals.SpriteBatch.Draw(spriteMap,
                        new Rectangle((int)enemy.X, (int)enemy.Y, (int)enemy.Width, (int)enemy.Height),
                        new Rectangle(210, 230, 645, 1060),
                        Color.Lime);

                    // Baloon hitbox - debug
                   // Globals.DrawRect(Globals.SpriteBatch, new Rectangle((int)enemy.X, (int)enemy.Y, (int)enemy.Width, (int)enemy.Height), Color.DarkRed);
                }

                // Draw player
                flyAnimation.Update();
                flyAnimation.Draw(levelModel.Player.Position, LevelController.targetPosition.X >= levelModel.Player.X);

                // Player hitbox - debug
               // Globals.DrawRect(Globals.SpriteBatch, new Rectangle((int)levelModel.Player.X, (int)levelModel.Player.Y, (int)levelModel.Player.Width, (int)levelModel.Player.Height), Color.Green);

                // Grapple line
                Globals.DrawLine(Globals.SpriteBatch, levelModel.Player.Center, Physics.CalculateTargetPosition(levelModel.Player.Center, LevelController.targetPosition), Color.Black, 3);

                // UI  
                uiRenderer.Draw(LevelModel.Time);
            }

            else if (!LevelModel.isLeaderboardOpen && !LevelModel.isSettingsOpen && LevelModel.isPaused)
            {
                menuRenderer.Draw();
            }
            else if (!LevelModel.isLeaderboardOpen && LevelModel.isSettingsOpen && LevelModel.isPaused)
            {
                settingsRenderer.Draw();
            }
            else if (LevelModel.isLeaderboardOpen && !LevelModel.isSettingsOpen && LevelModel.isPaused)
            {
                leaderboardRenderer.Draw();
            }
        }
    }
}
