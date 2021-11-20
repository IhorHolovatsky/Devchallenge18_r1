using System.Collections.Generic;
using BoxCutting.Core.Interfaces;
using BoxCutting.Core.Models.BoxCuttings;

namespace BoxCutting.Core.Factories
{
    public class DefaultBoxCuttingFactory : IBoxCuttingFactory
    {
        public List<IBoxCutting> Create(int boxWidth, int boxDepth, int boxHeight)
        {
            return new List<IBoxCutting>
            {
                new BoxCrossCutting(boxWidth, boxDepth, boxHeight),
                new BoxCrossRotatedCutting(boxWidth, boxDepth, boxHeight),
                new BoxTetrisCuttingPattern(boxWidth, boxDepth, boxHeight)
            };
        }
    }
}