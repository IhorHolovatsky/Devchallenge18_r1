using System.Text.Json.Serialization;

namespace BoxCutting.Core.Dtos
{
    public class SimpleBoxRequestDto
    {
        [JsonPropertyName("sheetSize")]
        public SheetDto Sheet { get; set; }

        [JsonPropertyName("boxSize")]
        public BoxSizeDto BoxSize { get; set; }
    }
}