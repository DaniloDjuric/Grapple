using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Grapple.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Grapple
{
    /*  Level View tasks:
     *  - Load in all the textures and animations
     *  - Call the draw funcion to loop through the Model
     *  - Display GUI elements (score, time, etc.)                 
     */

    internal class LevelView
    {
        Texture2D ninjaSprite;
        Texture2D balloonSprite;
        Texture2D platformSprite;

        public LevelView(ContentManager content)
        {
            ninjaSprite = content.Load<Texture2D>("ninja");
            balloonSprite = content.Load<Texture2D>("balloon");
            platformSprite = content.Load<Texture2D>("platform");
        }

        public void Draw(SpriteBatch spriteBatch, LevelModel levelModel)
        {

            // Dictionary {objectType ; Sprite}
            // objectType: Player, Platform, Baloon
            // Sprite(new script) +texture +crop +color;

            // Draw platforms
            foreach (var platform in levelModel.Platforms)
            {
                spriteBatch.Draw(platformSprite,
                    new Rectangle((int)platform.X, (int)platform.Y, (int)platform.Width, (int)platform.Height),
                    new Rectangle(130, 70, 170, 800),
                    Color.White);
            }

            // Draw balloons
            foreach (var enemy in levelModel.Balloons)
            {
                spriteBatch.Draw(balloonSprite,
                    new Rectangle((int)enemy.X, (int)enemy.Y, (int)enemy.Width, (int)enemy.Height),
                    Color.Lime);
            }

            // Draw player
            spriteBatch.Draw(ninjaSprite,
                new Rectangle((int)levelModel.Player.X, (int)levelModel.Player.Y, (int)levelModel.Player.Width, (int)levelModel.Player.Height),
                new Rectangle(135, 135, 500, 960),
                Color.DarkGray);
        }
    }
}
