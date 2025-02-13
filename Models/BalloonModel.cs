using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Grapple.General;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Grapple.Models
{
    public class BalloonModel
    {
        [JsonPropertyName("X")]
        public float X { get; set; }

        [JsonPropertyName("Y")]
        public float Y { get; set; }

        [JsonPropertyName("Width")]
        public float Width { get; set; }

        [JsonPropertyName("Height")]
        public float Height { get; set; }

        public Vector2 Position
        {
            get { return new Vector2(X, Y); }
            set
            {
                X = value.X;
                Y = value.Y;
            }
        }
        public void MoveAway(Vector2 playerPosition)
        {
            Vector2 position = new Vector2(X, Y);
            Vector2 direction = position - playerPosition;
            Vector2 newPosition = position + direction * 0.1f * Globals.TotalSeconds;
            Vector2 newPositionBottomRight = new Vector2(newPosition.X + Width, newPosition.Y + Height);
            if (Globals.PlayableArea.Contains(newPosition) &&
                Globals.PlayableArea.Contains(newPositionBottomRight))
            {
                X = newPosition.X;
                Y = newPosition.Y;
            }
        }
    }
}
