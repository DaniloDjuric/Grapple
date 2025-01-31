using Grapple.General;
using Grapple.Managers;
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
    /*  Level Controller tasks:
     *  - Handle Balloon spawning and other game logic
     *  - Handle player input for Ninja's movement
     *  - Calls for collision detection, AI, etc.              
     */

    internal class LevelController
    {
        private LevelModel levelModel;
        private LevelView levelView;

        private SoundEffect popSoundEffect;
        private float popSoundVolume = 0.6f;
        private Song mainSong;

        public static Vector2 targetPosition;
        private bool Moving = false;
        private Vector2 clickPosition;
        private int clickCounter; 
        private int totalClicksAllowed = 1; 


        public LevelController(LevelModel model, LevelView view)
        {
            AudioManager.LoadContent();

            levelModel = model;
            levelView = view;
        }

        // TouchPanel.GetState();    =>      For mobile funcionality
        // (The rest of the code stays mostly the same, Mouse will be used for easier testing) 

        public void Update(GameTime gameTime)
        {
            AudioManager.AdjustMusicVolume(LevelView.paused);


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

            if (!Globals.GameRunning && Globals.HighScoreManager.scoresUpdated == false)
            {
                Globals.HighScoreManager.UpdateHighScores("Danilo", LevelModel.Score);
                Globals.HighScoreManager.scoresUpdated = true;
            }

            levelView.Update(gameTime);
        }

        // Avoid accidental multi-clicks
        private void ChangeTarget()
        {
            if (!Globals.GameRunning) return;

            if (clickCounter <= 0) return;

            Vector2 newClickPosition = new Vector2(Mouse.GetState().X, Mouse.GetState().Y);

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
        }

        private void BalloonCollissionCheck(GameTime gameTime)
        {
            if (Physics.CheckCollision_p_b(levelModel.Balloons[0]))
            {
                levelModel.Balloons[0].X = new Random().NextInt64(750);
                levelModel.Balloons[0].Y = new Random().NextInt64(400);


                AudioManager.PlayPopSound();
                LevelModel.Score++;
            }
        }

        public void Draw()
        {
            // Tells the View to draw, with the atributes (position, size, ...) stored in the Model
            levelView.Draw(levelModel);

            Globals.DrawLine(Globals.SpriteBatch, levelModel.Player.Center, Physics.CalculateTargetPosition(levelModel.Player.Center, targetPosition), Color.Black, 3);

        }
    }

}
