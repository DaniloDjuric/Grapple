using Grapple.General;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Grapple.Managers
{
    internal static class AudioManager
    {
        private static SoundEffect popSoundEffect;
        private static SoundEffect buttonSoundEffect;
        private static Song mainSong;

        public static bool muteSong = false;
        public static bool muteSoundEffects = false;
        private static float SoundEffectVolume = 0.6f;
        private static float musicVolume = 0.25f;

        public static void LoadContent()
        {
            popSoundEffect = Globals.Content.Load<SoundEffect>(@"Sounds\\pop-sound");
            buttonSoundEffect = Globals.Content.Load<SoundEffect>(@"Sounds\\button-sound");
            mainSong = Globals.Content.Load<Song>(@"Sounds\\main-song");

            MediaPlayer.IsRepeating = true;
            MediaPlayer.Volume = musicVolume;
            MediaPlayer.Play(mainSong);
        }

        public static void PlayPopSound()
        {
            if(!muteSoundEffects) popSoundEffect?.Play(SoundEffectVolume, 0.0f, 0.0f);
        }
        public static void PlayButtonSound()
        {
            if (!muteSoundEffects) buttonSoundEffect?.Play(SoundEffectVolume, 0.0f, 0.0f);
        }

        public static void AdjustMusicVolume(bool isPaused)
        { 
            MediaPlayer.Volume = muteSong ? 0f : isPaused ? 0.05f : musicVolume;
        }
    }
}
