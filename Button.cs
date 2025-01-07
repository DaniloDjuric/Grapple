using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grapple
{
    public class Button
    {
        private int ButtonX, ButtonY;
        private readonly Rectangle Rect;
        private Texture2D Texture;
        private readonly string Name;
        private Color Color;
        
        public Button(string name, Texture2D texture, int buttonX, int buttonY)
        {
            this.Name = name;
            this.Texture = texture;
            this.ButtonX = buttonX;
            this.ButtonY = buttonY;
            this.Rect = new(buttonX, buttonY, texture.Width, texture.Height);
            this.Color = Color.CornflowerBlue;
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
                this.Color = Color.DarkRed;
                return true;
            }
            else
            {
                this.Color = Color.CornflowerBlue;
                return false;
            }
        }

        public void Update()
        {
            if (enterButton() && Mouse.GetState().LeftButton == ButtonState.Released && Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                switch (Name)
                {
                    case "pause":
                        // Code for the pause screen
                        break;
                    default:
                        break;
                }
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, new Vector2(ButtonX, ButtonY), new Rectangle(0, 0, Texture.Width, Texture.Height), this.Color, 0f, new Vector2(), new Vector2(0.05f, 0.05f), new SpriteEffects(), 0f);
        }
    }
}
