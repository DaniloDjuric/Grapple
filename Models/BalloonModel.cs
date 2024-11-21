using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Grapple.Models
{
    internal class BalloonModel
    {
        [JsonPropertyName("X")]
        public float X { get; set; }

        [JsonPropertyName("Y")]
        public float Y { get; set; }

        [JsonPropertyName("Width")]
        public float Width { get; set; }

        [JsonPropertyName("Height")]
        public float Height { get; set; }

        //public Color Color {
        //  get { return new Color(new Random().NextInt64(255), new Random().NextInt64(255), new Random().NextInt64(255)); }
        //}
}
}
