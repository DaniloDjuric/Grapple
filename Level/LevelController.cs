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
                ChangeTarget();
            }
            BalloonCollissionCheck(gameTime);

            // Should move in a direction until a collision with a wall. Not to a specific position
            Vector2 direction = Vector2.Normalize(targetPosition - levelModel.Player.Position);
            physics.MoveTowards(direction, gameTime);

            levelView.Update(gameTime);
        }

        // Avoid accidental multi-clicks
        private void ChangeTarget()
        {
            if (new Vector2(Mouse.GetState().X, Mouse.GetState().Y) != targetPosition)
            {
                targetPosition = new Vector2(Mouse.GetState().X, Mouse.GetState().Y);
            }
        }

        private void BalloonCollissionCheck(GameTime gameTime)
        {
            /*
            if ((Mouse.GetState().X > levelModel.Balloons[0].X &&
                Mouse.GetState().X < (levelModel.Balloons[0].X + levelModel.Balloons[0].Width)) &&
                (Mouse.GetState().Y > levelModel.Balloons[0].Y &&
                Mouse.GetState().Y < (levelModel.Balloons[0].Y + levelModel.Balloons[0].Height)))
            */

            if (physics.CheckCollision_p_b(levelModel.Balloons[0]))
            {
                levelModel.Balloons[0].X = new Random().NextInt64(750);
                levelModel.Balloons[0].Y = new Random().NextInt64(400);

                LevelModel.Score++;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            // Tells the View to draw, with the atributes (position, size, ...) stored in the Model
            levelView.Draw(spriteBatch, levelModel);
        }
    }

}
