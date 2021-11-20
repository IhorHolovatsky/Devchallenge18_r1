using System.Collections.Generic;
using NetTopologySuite.Geometries;

namespace BoxCutting.Core.Interfaces
{
    /// <summary>
    /// Describes specific Box cutting from which we can make a box.
    /// The most simple BoxCutting will produce 1 box.
    /// Complex BoxCutting can produce more than 1 box (to more efficiently use area)
    /// </summary>
    public interface IBoxCutting
    {
        /// <summary>
        /// The number of boxes which could be made from this Cutting.
        /// </summary>
        int BoxCount { get; }

        /// <summary>
        /// Bounding Box width
        /// </summary>
        int Width { get; }

        /// <summary>
        /// Bounding Box height
        /// </summary>
        int Height { get; }

        /// <summary>
        /// Efficiency of cutting. 
        /// UsedArea divided by TotalArea of bounding box
        ///
        /// 1 - is the max value, it's considered as perfect cutting since all area is used
        /// </summary>
        double Efficiency { get; }
        
        /// <summary>
        /// Generate commands of this specific cutting for Cutting Machine
        /// </summary>
        /// <param name="initialPoint">The left-bottom point where cutting will be presented on sheet</param>
        /// <returns>List of commands for Cutting Machine</returns>
        IReadOnlyList<ICommand> GetProgram(Point initialPoint);
    }
}