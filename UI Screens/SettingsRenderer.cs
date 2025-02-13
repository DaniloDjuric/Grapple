using Grapple.General;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grapple.UI_Screens
{
    internal class SettingsRenderer
    {
        Texture2D pauseButtonTexture;

        Button pauseButton;
        Button muteSongButton;
        Button muteSoundEffectsButton;

        public SettingsRenderer()
        {
            pauseButtonTexture = Globals.Content.Load<Texture2D>("pause_button");
            pauseButton = new Button("Pause", pauseButtonTexture, 700, 35);

            muteSongButton = new Button("Mute Song", 200, 50, 100, 200);
            muteSoundEffectsButton = new Button("Mute Effects", 200, 50, 100, 250);
        }

        public void Draw()
        {
            pauseButton.Display();
            muteSongButton .Display();
            muteSoundEffectsButton.Display();
        }
    }
}
