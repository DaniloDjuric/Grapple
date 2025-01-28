using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Grapple.Models
{
    internal class PlayerModel
    {
        [JsonPropertyName("X")]
        public float X { get; set; }

        [JsonPropertyName("Y")]
        public float Y { get; set; }

        [JsonPropertyName("Width")]
        public float Width { get; set; }

        [JsonPropertyName("Height")]
        public float Height { get; set; }

        [JsonPropertyName("Speed")]
        public int Speed { get; set; }

        public Vector2 Center
        {
            get { return new Vector2(X+40, Y+50); }
        }

        // Conbines the position into a Vector2 for easier calculations and use
        public Vector2 Position
        {
            get { return new Vector2(X, Y); }
            set
            {
                X = value.X;
                Y = value.Y;
            }
        }

    }
}
