using System.Collections.Generic;
using System.Linq;
using BoxCutting.Core.Constants;
using BoxCutting.Core.Factories;
using BoxCutting.Core.Interfaces;
using BoxCutting.Core.Models;
using BoxCutting.Core.Models.Commands;
using BoxCutting.Core.Packers;

namespace BoxCutting.Core
{
    public class BoxCutter
    {
        protected Sheet Sheet { get; }
        protected IPacker Packer { get; }
        protected IBoxCuttingFactory BoxCuttingFactory { get; }

        public BoxCutter(Sheet sheet)
        {
            Sheet = sheet;
            Packer = new BinaryTreePacker();
            BoxCuttingFactory = new DefaultBoxCuttingFactory();
        }

        /// <summary>
        /// For unit tests
        /// </summary>
        public BoxCutter(Sheet sheet, 
            IPacker packer = null, 
            IBoxCuttingFactory boxCuttingFactory = null)
        {
            Sheet = sheet;
            Packer = packer ?? new BinaryTreePacker();
            BoxCuttingFactory = boxCuttingFactory ?? new DefaultBoxCuttingFactory();
        }

        public BoxCuttingResult GetCuttingProgram(int boxWidth, int boxDepth, int boxHeight)
        {
            var result = new BoxCuttingResult();

            if (boxWidth <= 0
                || boxDepth <= 0
                || boxHeight <= 0
                || Sheet.Width <= 0
                || Sheet.Length <= 0)
            {
                result.Error = ErrorMessages.InvalidInputFormat;
                return result;
            }


            // Create all possible BoxCuttings variants
            var boxCuttings = BoxCuttingFactory.Create(boxWidth, boxDepth, boxHeight);

            var boxPackingResults = Packer.PackBoxCuttings(Sheet, boxCuttings);

            if (!boxPackingResults.Any())
            {
                result.Error = ErrorMessages.SheetSizeTooSmall;
                return result;
            }


            var commands = new List<ICommand>
            {
                new StartCommand()
            };

            foreach (var boxPacking in boxPackingResults)
            {
                commands.AddRange(boxPacking.Cutting.GetProgram(boxPacking.Position));
            }

            commands.Add(new StopCommand());

            result.Program = commands;
            result.Amount = boxPackingResults.Sum(x => x.Cutting.BoxCount);

            return result;
        }
    }
}