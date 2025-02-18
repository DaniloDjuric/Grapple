﻿using Grapple.Level;
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
using Microsoft.Xna.Framework.Audio;
using Grapple.General;
using Grapple.UI_Screens;

namespace Grapple
{
    internal class UIRenderer
    {
        Button pauseButton;
        Texture2D pauseButtonTexture;
        Texture2D UIArt;

        public UIRenderer()
        {
            pauseButtonTexture = Globals.Content.Load<Texture2D>("pause_button");
            UIArt = Globals.Content.Load<Texture2D>("Grapple_Concept_Art");
            pauseButton = new Button("Pause", pauseButtonTexture, 700, 35);
        }

        public void Draw(float time)
        {
            SpriteFont SpriteFont = Globals.Content.Load<SpriteFont>("galleryFont");

            pauseButton.Display();

            if (LevelController.autoAim)
            {
                Globals.DrawRect(Globals.SpriteBatch, new Rectangle(10, 10, 780, 460), Color.MediumVioletRed, 5);
            }

            if (Globals.GameRunning)
            {
                // In Level UI
                Globals.SpriteBatch.DrawString(SpriteFont, $"Score: {LevelModel.Score}", new Vector2(100, 30), Color.White);
                Globals.SpriteBatch.DrawString(SpriteFont, $"Time: {(time / 1000).ToString("0.00")} s", new Vector2(300, 30), Color.White);
                Globals.SpriteBatch.Draw(UIArt,
                        new Rectangle(250, 30, 40, 40),
                        new Rectangle(1720, 1650, 700, 700),
                        Color.White);
                Globals.SpriteBatch.Draw(UIArt,
                        new Rectangle(50, 30, 50, 50),
                        new Rectangle(2350, 200, 1100, 1100),
                        Color.White);
            }
            else
            {
                // Game Over UI
                Globals.SpriteBatch.DrawString(SpriteFont, "Game Over", new Vector2(300, 50), Color.White);
                Globals.SpriteBatch.DrawString(SpriteFont, $"Your name:", new Vector2(200, 290), Color.White);
                Globals.SpriteBatch.DrawString(SpriteFont, $"Your final score is: {LevelModel.Score}", new Vector2(200, 340), Color.White);
            }
        }
    }
}
