using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Grapple.Models;
using Grapple.General;
using Grapple.UI_Screens;

namespace Grapple.Level
{

    public class LevelModel
    {
        public static int Score { get; set; }
        public static float Time = 60000;
        public static bool isPaused = false;
        public static bool isLeaderboardOpen = false;
        public static bool isSettingsOpen = false;

        private static string currentSceneFile = "";

        public PlayerModel Player { get; set; }
        public List<BalloonModel> Balloons { get; set; }
        public List<PlatformModel> Platforms { get; set; }

        public LevelModel()
        {
            Player = new PlayerModel();
            Balloons = new List<BalloonModel>();
            Platforms = new List<PlatformModel>();
        }
        public void LoadObjectsFromJson(string jsonFilePath)
        {
            currentSceneFile = jsonFilePath;

            if (!File.Exists(jsonFilePath))
            {
                throw new FileNotFoundException($"JSON file not found: {jsonFilePath}");
            }

            try
            {
                string jsonData = File.ReadAllText(jsonFilePath);

                LevelModel loadedLevel = JsonSerializer.Deserialize<LevelModel>(jsonData);

                Player = loadedLevel.Player;
                Balloons = loadedLevel.Balloons;
                Platforms = loadedLevel.Platforms;
            }
            catch (Exception ex)
            {
                // Handle other potential exceptions, such as JSON parsing errors
                throw new Exception($"Error loading objects from JSON file: {jsonFilePath}", ex);
            }

        }

        public void RestartLevel()
        {
            Score = 0;  // Reset score
            Time = 60000; // Reset time
            isPaused = false;
            isLeaderboardOpen = false;
            isSettingsOpen = false;

            Player = new PlayerModel(); // Reset player
            Balloons.Clear();
            Platforms.Clear();

            LevelController.autoAim = false;
            LoadObjectsFromJson(currentSceneFile); // Reload level data

            Globals.GameRunning = true;
        }

    }
}
