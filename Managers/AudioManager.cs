using Grapple.General;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grapple.Managers
{
    internal static class AudioManager
    {
        private static SoundEffect popSoundEffect;
        private static Song mainSong;

        private static float popSoundVolume = 0.6f;
        private static float musicVolume = 0.2f;

        public static void LoadContent()
        {
            popSoundEffect = Globals.Content.Load<SoundEffect>(@"Sounds\\pop-sound");
            mainSong = Globals.Content.Load<Song>(@"Sounds\\main-song");

            MediaPlayer.IsRepeating = true;
            MediaPlayer.Volume = musicVolume;
            MediaPlayer.Play(mainSong);
        }

        public static void PlayPopSound()
        {
            popSoundEffect?.Play(popSoundVolume, 0.0f, 0.0f);
        }

        public static void AdjustMusicVolume(bool isPaused)
        {
            MediaPlayer.Volume = isPaused ? 0.05f : musicVolume;
        }
    }
}
