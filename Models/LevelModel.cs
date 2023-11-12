using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Grapple.Models
{
    /*  Level Model tasks:
     *  - Load in all the elements of the level from a JSON file
     *  - Keep all the objects in organized lists
     *  - Update based on changes in the level                
     */

    internal class LevelModel
    {
        public List<PlatformModel> Platforms { get; set; }
        public PlayerModel Player { get; set; }
        public List<BalloonModel> Balloons { get; set; }

        public LevelModel()
        {
            Platforms = new List<PlatformModel>();
            Player = new PlayerModel();
            Balloons = new List<BalloonModel>();
        }
        public void LoadObjectsFromJson(string jsonFilePath)
        {
            if (!File.Exists(jsonFilePath))
            {
                throw new FileNotFoundException($"JSON file not found: {jsonFilePath}");
            }

            try
            {
                string jsonData = File.ReadAllText(jsonFilePath);

                LevelModel loadedLevel = JsonSerializer.Deserialize<LevelModel>(jsonData);

                Platforms = loadedLevel.Platforms;
                Player = loadedLevel.Player;
                Balloons = loadedLevel.Balloons;
            }
            catch (Exception ex)
            {
                // Handle other potential exceptions, such as JSON parsing errors
                throw new Exception($"Error loading objects from JSON file: {jsonFilePath}", ex);
            }
            
        }

    }
}
