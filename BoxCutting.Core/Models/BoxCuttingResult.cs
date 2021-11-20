using System.Collections.Generic;
using BoxCutting.Core.Interfaces;

namespace BoxCutting.Core.Models
{
    public class BoxCuttingResult
    {
        public bool Success => string.IsNullOrEmpty(Error);

        public string Error { get; set; }

        public int Amount { get; set; }

        public List<ICommand> Program { get; set; }

        public BoxCuttingResult()
        {
            Program = new List<ICommand>();
        }

    }
}