using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grapple.Level;
using Microsoft.Xna.Framework.Audio;
using Grapple.General;
using Grapple.Managers;

namespace Grapple
{
    public class Button
    {
        private int ButtonX, ButtonY;
        private readonly Rectangle Rect;
        private Texture2D Texture;
        private readonly string Name;
        private Color Color;
        private MouseState previousMouseState;

        public Button(string name, int width, int height, int buttonX, int buttonY)
        {
            this.Name = name;
            this.ButtonX = buttonX;
            this.ButtonY = buttonY;
            this.Rect = new(buttonX, buttonY, width, height);
            this.Color = Color.Red;

            previousMouseState = Mouse.GetState();
        }
        public Button(string name, Texture2D texture, int buttonX, int buttonY)
        {
            this.Name = name;
            this.Texture = texture;
            this.ButtonX = buttonX;
            this.ButtonY = buttonY;
            this.Color = Color.DarkGray;
            if(name == "Play")
            {
                this.Rect = new(buttonX, buttonY, (int)Math.Round(texture.Width * 0.2), (int)Math.Round(texture.Height * 0.2));
            }
            else
            {
                this.Rect = new(buttonX, buttonY, (int)Math.Round(texture.Width* 0.05), (int)Math.Round(texture.Height * 0.05));
            }

            previousMouseState = Mouse.GetState();
        }

        /**
         * @return true: If a player enters the button with mouse
         */
        public bool enterButton()
        {
            MouseState ms = Mouse.GetState();
            Rectangle cursor = new(ms.Position.X, ms.Position.Y, 1, 1);

            if (cursor.Intersects(Rect))
            {
                if (Name != "Mute Song" && Name != "Mute Effects")
                {
                    this.Color = Color.LightGray;
                }
                return true;

            }
            else
            {
                if(Name != "Mute Song" && Name != "Mute Effects")
                {
                    this.Color = Color.DarkGray;
                }
                return false;
            }
        }

        public void Display()
        {
            SpriteFont SpriteFont = Globals.Content.Load<SpriteFont>("galleryFont");

            MouseState currentMouseState = Mouse.GetState();

            if (enterButton() && previousMouseState.LeftButton == ButtonState.Released && currentMouseState.LeftButton == ButtonState.Pressed)
            {
                if (Name == "Play") AudioManager.PlayPopSound();

                AudioManager.PlayButtonSound();

                switch (Name)
                {
                    case "Play":
                        Globals.GameRunning = true;
                        break;
                    case "Pause":
                        LevelModel.isPaused = !LevelModel.isPaused;
                        LevelModel.isLeaderboardOpen = false;
                        LevelModel.isSettingsOpen = false;
                        break;
                    case "Restart level":
                        Globals.LevelModelInstance.RestartLevel();
                        break;
                    case "Leaderboard":
                        LevelModel.isLeaderboardOpen = true;
                        break;
                    case "Quit":
                        Globals.GameInstance.Exit();
                        break;
                    case "Settings":
                        LevelModel.isSettingsOpen = true;
                        break;
                    case "Next level":
                        if (GameController.currentLevel >= 4)
                        {
                            GameController.currentLevel = 1;
                        }
                        else
                        {
                            GameController.currentLevel += 1;
                        }
                        Globals.GameControllerReference.LoadLevel();
                        Globals.LevelModelInstance.RestartLevel();
                        break;
                    case "Mute Song":
                        AudioManager.muteSong = !AudioManager.muteSong;
                        if (AudioManager.muteSong) {
                            this.Color = Color.Green;
                        }else this.Color = Color.Red;
                        break;
                    case "Mute Effects":
                        AudioManager.muteSoundEffects = !AudioManager.muteSoundEffects;
                        if (AudioManager.muteSoundEffects)
                        {
                            this.Color = Color.Green;
                        }
                        else this.Color = Color.Red;

                        break;
                    default:
                        break;
                }
            }

            // Drawing the button texture/text
            if (Texture != null)
            {
                if (Name == "Play")
                {
                    Globals.SpriteBatch.Draw(Texture, new Vector2(ButtonX, ButtonY), new Rectangle(0, 0, Texture.Width, Texture.Height), this.Color, 0f, Vector2.One, 0.2f, SpriteEffects.None, 0f);
                }
                else Globals.SpriteBatch.Draw(Texture, new Vector2(ButtonX, ButtonY), new Rectangle(0, 0, Texture.Width, Texture.Height), this.Color, 0f, new Vector2(), 0.05f, new SpriteEffects(), 0f);
            }
            else
            {
                Globals.SpriteBatch.DrawString(SpriteFont, Name, new Vector2(ButtonX, ButtonY), this.Color);
            }

            previousMouseState = currentMouseState;
        }
    }
}
