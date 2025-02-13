using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace Grapple.Models
{
    public class PlatformModel
    {
        [JsonPropertyName("X")]
        public float X { get; set; }

        [JsonPropertyName("Y")]
        public float Y { get; set; }

        [JsonPropertyName("Width")]
        public float Width { get; set; }

        [JsonPropertyName("Height")]
        public float Height { get; set; }
    }
}
