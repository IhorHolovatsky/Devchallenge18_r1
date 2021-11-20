using System.Collections.Generic;

namespace BoxCutting.Core.Interfaces
{
    public interface IBoxCuttingFactory
    {
        List<IBoxCutting> Create(int boxWidth, int boxDepth, int boxHeight);
    }
}