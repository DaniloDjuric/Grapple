using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Grapple.General;
using Grapple.Level;
using Grapple.UI_Screens;
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

    public class GameController
    {
        private LevelController levelController;
        private LevelModel levelModel;
        private LevelView levelView;
        private StartMenuRenderer startMenu;

        public static int currentLevel = 1;
        public GameController(ContentManager content)
        {
            levelModel = new LevelModel();
            startMenu = new StartMenuRenderer();
            levelModel.LoadObjectsFromJson($"..\\..\\..\\scene_{currentLevel}.json");
            levelView = new LevelView(levelModel);
            levelController = new LevelController(levelModel, levelView);
            
            Globals.LevelModelInstance = levelModel;
            Physics.levelModel = levelModel;

            //menuController = new MenuController(menuModel, menuView);
        }
        public void LoadLevel()
        {
            levelModel = new LevelModel();
            levelModel.LoadObjectsFromJson($"..\\..\\..\\scene_{currentLevel}.json");
            levelView = new LevelView(levelModel);
            levelController = new LevelController(levelModel, levelView);

            Globals.LevelModelInstance = levelModel;
            Physics.levelModel = levelModel;
        }
        // These two are being called from "Main"
        public void Update(GameTime gameTime)
        {
            if (Globals.GameRunning) levelController.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {

            if (Globals.GameRunning) levelController.Draw();
            else startMenu.Draw();
        }
    }

}
