﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Grapple.Level;
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
        private LevelModel levelModel;
        private LevelView levelView;

        public GameController(ContentManager content)
        {
            levelModel = new LevelModel();
            levelModel.LoadObjectsFromJson("..\\..\\..\\scene.json");
            levelView = new LevelView();
            levelController = new LevelController(levelModel, levelView);
            
            Physics.levelModel = levelModel;

            //menuController = new MenuController(menuModel, menuView);
        }

        // These two are being called from "Main"
        public void Update(GameTime gameTime)
        {
            // Logic + menuController.Update();
            levelController.Update(gameTime); 
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            // Later this can call "MenuController.Draw" when I have a Menu
            // and have logic for knowing what scene should be drawn
            levelController.Draw();
        }
    }

}
