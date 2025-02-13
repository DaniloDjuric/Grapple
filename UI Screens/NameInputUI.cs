using Grapple.General;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grapple.UI_Screens
{       
    internal class NameInputUI

        {
            private string playerName = "";
            private SpriteFont font;
            private Rectangle inputBox;
            private bool isActive = false;
            private Keys? lastPressedKey = null; // Track the last pressed key

            public NameInputUI()
            {
                font = Globals.Content.Load<SpriteFont>("LeaderboardFont"); // Load a font
                inputBox = new Rectangle(380, 290, 200, 40); // Example position & size
            }

            public void Activate()
            {
                isActive = true;
                playerName = "";
            }

            public void Deactivate()
            {
                isActive = false;
            }

            public bool IsActive() => isActive;

            public string GetPlayerName() => playerName;

            public void Update()
            {
                if (!isActive) return;

                KeyboardState keyboardState = Keyboard.GetState();
                Keys[] keys = keyboardState.GetPressedKeys();

                if (keys.Length > 0)
                {
                    Keys key = keys[0]; // Get first key pressed

                    if (key != lastPressedKey) // Process only on new key press
                    {
                        if (key == Keys.Back && playerName.Length > 0)
                        {
                            playerName = playerName[..^1]; // Remove last character
                        }
                        else if (key == Keys.Enter)
                        {
                            isActive = false; // Submit name
                        }
                        else if (playerName.Length < 10 && IsValidCharacter(key))
                        {
                            playerName += ConvertKeyToString(key);
                        }
                    }

                    lastPressedKey = key; // Store current key as last pressed
                }
                else
                {
                    lastPressedKey = null; // Reset when no keys are pressed
                }
            }

            private bool IsValidCharacter(Keys key)
            {
                return (key >= Keys.A && key <= Keys.Z) || (key >= Keys.D0 && key <= Keys.D9);
            }

            private string ConvertKeyToString(Keys key)
            {
                if (key >= Keys.A && key <= Keys.Z)
                {
                    return key.ToString(); // Convert to string (A-Z)
                }
                if (key >= Keys.D0 && key <= Keys.D9)
                {
                    return key.ToString().Remove(0, 1); // Convert D0-D9 to "0"-"9"
                }
                return "";
            }

            public void Draw()
            {
                if (!isActive) return;

                Globals.DrawRect(Globals.SpriteBatch, inputBox, Color.White); // Draw input box (requires a DrawRectangle helper)
                Globals.SpriteBatch.DrawString(font, playerName, new Vector2(inputBox.X + 10, inputBox.Y + 3), Color.White);
            }
        }

}
