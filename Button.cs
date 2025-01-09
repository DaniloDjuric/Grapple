using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grapple.Level;

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
            this.Color = Color.CornflowerBlue;

            previousMouseState = Mouse.GetState();
        }
        public Button(string name, Texture2D texture, int buttonX, int buttonY)
        {
            this.Name = name;
            this.Texture = texture;
            this.ButtonX = buttonX;
            this.ButtonY = buttonY;
            this.Rect = new(buttonX, buttonY, (int)Math.Round(texture.Width* 0.05), (int)Math.Round(texture.Height * 0.05));
            this.Color = Color.Black;

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
                this.Color = Color.LightGray;
                return true;
            }
            else
            {
                this.Color = Color.Black;
                return false;
            }
        }

        public void Update()
        {
            MouseState currentMouseState = Mouse.GetState();

            if (enterButton() && previousMouseState.LeftButton == ButtonState.Released && currentMouseState.LeftButton == ButtonState.Pressed)
            {
                switch (Name)
                {
                    case "pause":
                        LevelView.paused = !LevelView.paused;
                        LevelView.leaderboard = false;
                        break;
                    case "Restart level":
                        LevelView.paused = false;
                        LevelView.time = 60000;
                        LevelModel.Score = 0;
                        break;
                    case "Leaderboard":
                        LevelView.leaderboard = true;
                        break;
                    default:
                        // Remove after testing

                        break;
                }
            }

            previousMouseState = currentMouseState;
        }
        public void Draw(SpriteBatch spriteBatch, SpriteFont spriteFont)
        {
            if (Texture != null)
            {  
                spriteBatch.Draw(Texture, new Vector2(ButtonX, ButtonY), new Rectangle(0, 0, Texture.Width, Texture.Height), this.Color, 0f, new Vector2(), new Vector2(0.05f, 0.05f), new SpriteEffects(), 0f);
            }
            else
            {
                spriteBatch.DrawString(spriteFont, Name, new Vector2(ButtonX, ButtonY), this.Color);
            }
        }
    }
}
