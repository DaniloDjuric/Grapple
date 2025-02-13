using Grapple.General;
using Grapple.Managers;
using Grapple.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;

namespace Grapple.Level
{
    internal class LevelController
    {
        private LevelModel levelModel;
        private LevelView levelView;

        public static Vector2 targetPosition;
        private bool Moving = false;
        private Vector2 clickPosition;
        private int clickCounter; 
        private int totalClicksAllowed = 1;

        public static bool autoAim;

        public LevelController(LevelModel model, LevelView view)
        {
            AudioManager.LoadContent();

            levelModel = model;
            levelView = view;
        }

        public void Update(GameTime gameTime)
        {
            AudioManager.AdjustMusicVolume(LevelModel.isPaused);
            Globals.Camera.Update(gameTime);

            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                autoAim = true;
                ChangeTarget();
            }


            if (Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                ChangeTarget();
            }

            BalloonCollissionCheck(gameTime);
            
            Physics.MoveTowards(targetPosition, gameTime, ref Moving, levelModel.Player);

            if (!Moving)
            {
                clickCounter = totalClicksAllowed;
            }
            if (!Globals.GameRunning && !Globals.HighScoreManager.scoresUpdated && !levelView.nameInputUI.IsActive())
            {
                string playerName = levelView.nameInputUI.GetPlayerName();
                if (!string.IsNullOrEmpty(playerName))
                {
                    Globals.HighScoreManager.UpdateHighScores(playerName, LevelModel.Score);
                    Globals.HighScoreManager.scoresUpdated = true;
                }
            }
            foreach (BalloonModel balloon in levelModel.Balloons)
            {
                balloon.MoveAway(levelModel.Player.Position);
            }

            levelView.Update(gameTime);
        }

        // Avoid accidental multi-clicks
        private void ChangeTarget()
        {
            if (!Globals.GameRunning || clickCounter <= 0 || LevelModel.isPaused) return;

            Vector2 newClickPosition = new Vector2(Mouse.GetState().X, Mouse.GetState().Y);
            
            if (autoAim)
            {
                newClickPosition = Globals.LevelModelInstance.Balloons[0].Position;
            }

            if (newClickPosition != clickPosition)
            {
                clickPosition = newClickPosition;
                Vector2 calculatedTarget = Physics.CalculateTargetPosition(levelModel.Player.Center, clickPosition);
                if (calculatedTarget != Vector2.Zero)
                {
                    targetPosition = calculatedTarget;
                    clickCounter--; // Decrement clicks
                    Moving = true; // Enable movement
                }
                else
                {
                    Debug.WriteLine("Invalid target position calculated");
                }
            }
            if (!Moving) autoAim = false;
        }

        private void BalloonCollissionCheck(GameTime gameTime)
        {
            foreach (BalloonModel balloon in levelModel.Balloons)
            if (Physics.CheckCollision_p_b(balloon))
            {
                balloon.X = new Random().NextInt64(750);
                balloon.Y = new Random().NextInt64(400);

                Globals.Camera.StartShake(1f, 0.2f); // Shake with intensity 1 for 0.2 seconds
                AudioManager.PlayPopSound();
                LevelModel.Score++;
            }
        }

        public void Draw()
        {
            // Tells the View to draw, with the atributes (position, size, ...) stored in the Model
            levelView.Draw(levelModel);
        }
    }

}
