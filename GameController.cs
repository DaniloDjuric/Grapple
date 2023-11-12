using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grapple.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Grapple
{

    /*  Game Controller tasks:
     *  - Switch currently active screen (Gameplay, Menu, Shop...)
     *  - Save / Load System
     *  - Keep track of high scores and currency                
     */
     
    internal class GameController
    {
        private LevelController levelController;
        private ContentManager contentManager;
        private LevelModel levelModel;
        private LevelView levelView;

        public GameController(ContentManager content)
        {
            contentManager = content;
            levelModel = new LevelModel();
            levelModel.LoadObjectsFromJson("C:\\Users\\dadal\\source\\repos\\Grapple\\scene.json");

            levelView = new LevelView(contentManager);
            levelController = new LevelController(levelModel, levelView);
            //menuController;
            //shopController;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            // Later this can call "MenuController.Draw" when I have a Menu
            // and have logic for knowing what scene should be drawn
            levelController.Draw(spriteBatch);
        }
    }

}
