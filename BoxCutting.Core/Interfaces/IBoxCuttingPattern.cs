using BoxCutting.Core.Models;

namespace BoxCutting.Core.Interfaces
{
    /// <summary>
    /// BoxCuttingPattern represent BoxCutting which can place several BoxCuttings efficiently
    ///
    /// The <see cref="Build"/> method allows to build pattern for given sheet
    /// </summary>
    public interface IBoxCuttingPattern : IBoxCutting
    {
        /// <summary>
        /// The type of growth
        /// </summary>
        BoxCuttingPatternType Type { get; }


        /// <summary>
        /// Build BoxCuttingPattern for given sheet
        /// This method changes the Width and Height of this BoxCutting,
        /// because it can pack as many items as possible for given sheet efficiently
        /// </summary>
        /// <param name="sheet"></param>
        IBoxCutting Build(Sheet sheet);
    }
}