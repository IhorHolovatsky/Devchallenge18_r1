using System.Collections.Generic;
using System.Text.Json.Serialization;
using BoxCutting.Core.Interfaces;

namespace BoxCutting.Core.Dtos
{
    public class SimpleBoxResponseDto
    {
        [JsonPropertyName("success")]
        public bool Success => string.IsNullOrEmpty(Error);

        [JsonPropertyName("error")]
        public string Error { get; set; }

        [JsonPropertyName("amount")]
        public int Amount { get; set; }

        [JsonPropertyName("program")]
        //[JsonConverter()]
        public List<ICommand> Program { get; set; }

        public SimpleBoxResponseDto()
        {
            Program = new List<ICommand>();
        }
    }
}