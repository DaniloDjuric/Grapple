using Grapple.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
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

        public LevelController(LevelModel model, LevelView view)
        {
            levelModel = model;
            levelView = view;
        }

        public void Update(GameTime gameTime)
        {
            // Update the current level
                
            // Handle game logic 
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            // Tells the View to draw the Model
            levelView.Draw(spriteBatch, levelModel);
        }
    }

}
