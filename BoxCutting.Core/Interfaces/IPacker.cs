using System.Collections.Generic;
using BoxCutting.Core.Models;

namespace BoxCutting.Core.Interfaces
{
    /// <summary>
    /// Represents a logic which can efficiently position BoxCuttings into given Sheet
    /// </summary>
    public interface IPacker
    {
        /// <param name="sheet">A Sheet to where we need place BoxCuttings</param>
        /// <param name="boxCuttings">A list of BoxCuttings variants which we want to place into Sheet</param>
        /// <returns>List of <see cref="BoxPackingResult"/> which can be placed into Sheet </returns>
        List<BoxPackingResult> PackBoxCuttings(Sheet sheet, List<IBoxCutting> boxCuttings);
    }
}