using System.Text.Json.Serialization;

namespace BoxCutting.Core.Dtos
{
    public class BoxSizeDto
    {
        /// <example>200</example>
        [JsonPropertyName("w")]
        public int Width { get; set; }


        /// <example>200</example>
        [JsonPropertyName("d")]
        public int Depth { get; set; }


        /// <example>200</example>
        [JsonPropertyName("l")]
        public int Height { get; set; }
    }
}