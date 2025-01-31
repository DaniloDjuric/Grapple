using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace Grapple.Managers
{
    public class HighScoreManager
    {
        private const string HighScoreFileName = "HighScores.json";
        public List<KeyValuePair<string, int>> HighScores { get; private set; } = new List<KeyValuePair<string, int>>();
        private readonly string highScoreFilePath;

        public bool scoresUpdated = false;

        public HighScoreManager()
        {
            highScoreFilePath = Path.Combine(Environment.CurrentDirectory, HighScoreFileName);

            // Initialize the file if it doesn't exist
            if (!File.Exists(highScoreFilePath))
            {
                File.WriteAllText(highScoreFilePath, "[]"); // Initialize with an empty array
            }

            LoadHighScores();
        }

        // Load high scores from the file
        private void LoadHighScores()
        {
            try
            {
                string json = File.ReadAllText(highScoreFilePath);
                HighScores = JsonSerializer.Deserialize<List<KeyValuePair<string, int>>>(json) ?? new List<KeyValuePair<string, int>>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to load high scores: {ex.Message}");
                HighScores = new List<KeyValuePair<string, int>>();
            }
        }

        public void SaveHighScores()
        {
            try
            {
                string json = JsonSerializer.Serialize(HighScores, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(highScoreFilePath, json);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to save high scores: {ex.Message}");
            }
        }

        public void UpdateHighScores(string playerName, int finalScore)
        {
            // Add the new score
            HighScores.Add(new KeyValuePair<string, int>(playerName, finalScore));

            // Keep only the top 3 scores, sorted in descending order
            HighScores = HighScores
                .OrderByDescending(x => x.Value) // Sort by score
                .Take(3)                        // Take the top 3
                .ToList();


            SaveHighScores();
        }

        public void AddScore(string playerName, int score)
        {
            UpdateHighScores(playerName, score);
        }

        // For debugging or displaying high scores
        public string GetHighScoresAsString()
        {
            return string.Join("\n", HighScores.Select(x => $"{x.Key}: {x.Value}"));
        }
    }
}
