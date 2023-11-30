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
        private Physics physics;

        private Vector2 targetPosition;
        //private int inputsAllowed = 3;
        //private int inputsLeft = 2;

        public LevelController(LevelModel model, LevelView view)
        {
            levelModel = model;
            levelView = view;
            physics = new Physics(levelModel);
        }

        // TouchPanel.GetState();    =>      For mobile funcionality
        // (The rest of the code stays mostly the same, Mouse will be used for easier testing) 

        public void Update(GameTime gameTime)
        {
            //if (TouchPanel.GetState().Count > 0)
            if (Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                BalloonCollissionCheck(gameTime);

                ChangeTarget(gameTime);
            }

            physics.MoveTowards(targetPosition, gameTime);            
        }

        private void HandleInput()
        {
            if (Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                // Call the Raycast method from Physics and update targetPosition
                Vector2 playerPos = new Vector2(levelModel.Player.X, levelModel.Player.Y);
                Vector2 intersectionPoint = physics.Raycast(playerPos, new Vector2(Mouse.GetState().X, Mouse.GetState().Y));

                if (intersectionPoint != Vector2.Zero)
                {
                    targetPosition = intersectionPoint;
                }
                else
                {
                    targetPosition = new Vector2(Mouse.GetState().X, Mouse.GetState().Y);
                }
            }
        }

        private void ChangeTarget(GameTime gameTime)
        {
            // The ninja can only change his possition 2 times, pop ballons to get more moves
            if (new Vector2(Mouse.GetState().X, Mouse.GetState().Y) != targetPosition)
            {
                targetPosition = new Vector2(Mouse.GetState().X, Mouse.GetState().Y);
            }
        }

        private void BalloonCollissionCheck(GameTime gameTime)
        {
            // Simple TEPORARY collision detection for clicking on the balloon
            if ((Mouse.GetState().X > levelModel.Balloons[0].X &&
                Mouse.GetState().X < (levelModel.Balloons[0].X + levelModel.Balloons[0].Width)) &&
                (Mouse.GetState().Y > levelModel.Balloons[0].Y &&
                Mouse.GetState().Y < (levelModel.Balloons[0].Y + levelModel.Balloons[0].Height)))
            {
                levelModel.Balloons[0].X = new Random().NextInt64(750);
                levelModel.Balloons[0].Y = new Random().NextInt64(400);

                // The ninja can keep moving after popping a balloon
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            // Tells the View to draw the Model
            levelView.Draw(spriteBatch, levelModel);
        }
    }

}
