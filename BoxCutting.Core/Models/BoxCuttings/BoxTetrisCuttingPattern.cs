using System.Collections.Generic;
using BoxCutting.Core.Interfaces;
using NetTopologySuite.Geometries;

namespace BoxCutting.Core.Models.BoxCuttings
{
    public class BoxTetrisCuttingPattern : IBoxCuttingPattern
    {
        public BoxCuttingPatternType Type => BoxCuttingPatternType.HeightBased;


        public int BoxCount => RepentanceFactor * 2;
        public int Width => 2 * _boxWidth + 2 * _boxHeight;
        public int Height => RepentanceFactor * (3 * _boxHeight + 2 * _boxDepth);

        //TODO: calculate efficiency
        public double Efficiency => 1;

        /// <summary>
        /// How many times pattern was applied,
        /// this is not ideal, since it apply whole pattern, it will be good to have
        /// ability to apply only single BoxCutting, but not whole pattern
        /// </summary>
        protected int RepentanceFactor = 1;

        private readonly int _boxWidth;
        private readonly int _boxDepth;
        private readonly int _boxHeight;

        public BoxTetrisCuttingPattern(int boxWidth, int boxDepth, int boxHeight)
        {
            _boxWidth = boxWidth;
            _boxDepth = boxDepth;
            _boxHeight = boxHeight;
        }

        public IReadOnlyList<ICommand> GetProgram(Point initialPoint)
        {
            var commands = new List<ICommand>();
           
            var halfCrossBoxCutting = new BoxHalfCrossCutting(_boxWidth, _boxDepth, _boxHeight);
            var crossBoxCutting = new BoxCrossCutting(_boxWidth, _boxDepth, _boxHeight);

            for (var i = 0; i < RepentanceFactor; i++)
            {
                commands.AddRange(halfCrossBoxCutting.GetProgram(initialPoint));

                initialPoint.Y += halfCrossBoxCutting.Height;

                commands.AddRange(crossBoxCutting.GetProgram(initialPoint));

                initialPoint.Y += crossBoxCutting.Height;
            }

            return commands;
        }


        public IBoxCutting Build(Sheet sheet)
        {
            return new BoxTetrisCuttingPattern(_boxWidth, _boxDepth, _boxHeight)
            {
                RepentanceFactor = sheet.Length / Height
            };
        }
    }
}