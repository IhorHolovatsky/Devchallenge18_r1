namespace BoxCutting.Core.Models
{
    public enum BoxCuttingPatternType
    {
        /// <summary>
        /// Whether pattern growth to right (width based), so height is fixed
        /// </summary>
        WidthBased,

        /// <summary>
        /// Whether pattern growth to up (height based), so width is fixed
        /// </summary>
        HeightBased
    }
}