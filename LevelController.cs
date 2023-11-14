using Grapple.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Grapple
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
        private Vector2 targetPosition;
        private float movementSpeed = 120f;
        private int inputsAllowed = 2;
        private int inputsLeft = 2;

        public LevelController(LevelModel model, LevelView view)
        {
            levelModel = model;
            levelView = view;
        }

        public void Update(GameTime gameTime)
        {
            HandleInput();

            UpdateSmoothMovement(gameTime);
        }

        private void HandleInput()
        {
            // TouchPanel.GetState();    =>      For mobile funcionality
            // (The rest of the code stays mostly the same, Mouse will be used for easier testing) 

            if (Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                targetPosition = new Vector2(Mouse.GetState().X, Mouse.GetState().Y);
                // The ninja can only change his possition 2 times, pop ballons to get more moves
                inputsLeft -= 1;
            }
        }

        private void UpdateSmoothMovement(GameTime gameTime)
        {
            // Simple collision detection for clicking on the balloon
            if ((targetPosition.X > levelModel.Balloons[0].X &&
                targetPosition.X < (levelModel.Balloons[0].X + levelModel.Balloons[0].Width)) &&
                (targetPosition.Y > levelModel.Balloons[0].Y &&
                targetPosition.Y < (levelModel.Balloons[0].Y + levelModel.Balloons[0].Height)))
            {
                levelModel.Balloons[0].X = new Random().NextInt64(750);
                levelModel.Balloons[0].Y = new Random().NextInt64(400);

                // The ninja can keep moving after popping a balloon
                inputsLeft = inputsAllowed;
            }


            // Moving the player to the pressed possition if he haves available moves
            if (inputsLeft > 0)
            {
                Vector2 direction = Vector2.Normalize(targetPosition - new Vector2(levelModel.Player.X, levelModel.Player.Y));

                Vector2 newPosition = new Vector2(
                    levelModel.Player.X + direction.X * movementSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds,
                    levelModel.Player.Y + direction.Y * movementSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds
                );

                levelModel.Player.X = newPosition.X;
                levelModel.Player.Y = newPosition.Y;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            // Tells the View to draw the Model
            levelView.Draw(spriteBatch, levelModel);
        }
    }

}
