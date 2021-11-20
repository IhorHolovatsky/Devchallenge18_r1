using System.Text.Json.Serialization;

namespace BoxCutting.Core.Dtos
{
    public class SheetDto
    {
        /// <example>800</example>
        [JsonPropertyName("w")]
        public int Width { get; set; }

        /// <example>600</example>
        [JsonPropertyName("l")]
        public int Length { get; set; }
    }
}