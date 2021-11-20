using BoxCutting.Core.Interfaces;
using NetTopologySuite.Geometries;

namespace BoxCutting.Core.Models
{
    public class BoxPackingResult
    {
        public IBoxCutting Cutting { get; set; }
        
        /// <summary>
        /// Represents left-bottom position on Sheet for BoxCutting
        /// </summary>
        public Point Position { get; set; }
    }
}