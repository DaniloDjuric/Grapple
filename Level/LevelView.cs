using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using static System.Formats.Asn1.AsnWriter;

namespace Grapple.Level
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

        Texture2D UIArt;
        SpriteFont spriteFont;
        int time = 60;

        public LevelView(ContentManager content)
        {
            ninjaSprite = content.Load<Texture2D>("ninja");
            balloonSprite = content.Load<Texture2D>("balloon");
            platformSprite = content.Load<Texture2D>("platform");
            UIArt = content.Load<Texture2D>("Grapple_Concept_Art");
            spriteFont = content.Load<SpriteFont>("galleryFont");
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
                new Rectangle((int)levelModel.Player.Position.X, (int)levelModel.Player.Position.Y, (int)levelModel.Player.Width, (int)levelModel.Player.Height),
                new Rectangle(135, 135, 500, 960),
                Color.DarkGray);


            // UI Write
            spriteBatch.DrawString(spriteFont, $"Score: {levelModel.Score}", new Vector2(100, 30), Color.White);
            spriteBatch.DrawString(spriteFont, $"Time: {time} s", new Vector2(300, 30), Color.White);

            spriteBatch.Draw(UIArt,
                    new Rectangle(250, 30, 40, 40),
                    new Rectangle(1720, 1650, 700, 700),
                    Color.White);

            spriteBatch.Draw(UIArt,
                    new Rectangle(50, 30, 50, 50),
                    new Rectangle(2350, 200, 1100, 1100),
                    Color.White);
        }
    }
}
