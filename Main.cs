﻿using Grapple.General;
using Grapple.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Diagnostics;
using System.Linq;

namespace Grapple
{
    // Main script. Loads in the controller, initializes and handles beginning and exiting.
    public class Main : Game
    {
        private GraphicsDeviceManager graphics;
        private GameController gameController;
        
        //  Screen size : 800 x 480

        public Main()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            Globals.Camera = new Camera();
            Globals.TotalSeconds = 0;
            Globals.Content = Content;
            Globals.GameRunning = false;
            Globals.HighScoreManager = new HighScoreManager();
            Globals.GameInstance = this;
            base.Initialize();
        }

        protected override void LoadContent()
        {
            gameController = new GameController(Content);
            Globals.GameControllerReference = gameController;
            Globals.GraphicsDevice = GraphicsDevice;
            Globals.SpriteBatch = new SpriteBatch(GraphicsDevice);
        }

        // Where the controller gets updated. 
        // Last step is updating the base game.

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }

            gameController.Update(gameTime);
            Globals.Update(gameTime);

            base.Update(gameTime);
        }

        // Where all the visual changes start.
        // Last step is drawing the base game.
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // SpriteBatch initializing before being passed into the controller, and ending after use.
            Globals.SpriteBatch.Begin(transformMatrix: Globals.Camera.GetViewMatrix());
            gameController.Draw(Globals.SpriteBatch);
            Globals.SpriteBatch.End();

            base.Draw(gameTime);
        }
    }
}