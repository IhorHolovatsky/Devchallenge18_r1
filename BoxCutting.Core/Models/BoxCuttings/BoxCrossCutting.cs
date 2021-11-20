using System.Collections.Generic;
using System.Linq;
using BoxCutting.Core.Interfaces;
using BoxCutting.Core.Models.Commands;
using NetTopologySuite;
using NetTopologySuite.Geometries;

namespace BoxCutting.Core.Models.BoxCuttings
{
    /// <summary>
    /// Representation of BoxCrossCutting, see `./resources/BoxCrossCutting.jpg` in root folder
    /// </summary>
    public class BoxCrossCutting : IBoxCutting
    {
        public int BoxCount => 1;

        public virtual int Width { get; protected set; }
        public virtual int Height { get; protected set; }
        public int Area => Height * Width;

        public double Efficiency => Polygon.Area / Area;

        /// <summary>
        /// Polygon figure which describes Box Cutting.
        /// It consist of set of Coordinates
        /// </summary>
        public Polygon Polygon { get; set; }

        protected BoxCrossCutting() {}

        public BoxCrossCutting(int boxWidth, int boxDepth, int boxHeight)
        {
            var geometryFactory = NtsGeometryServices.Instance.CreateGeometryFactory();

            Width = 2 * boxHeight + 2 * boxWidth;
            Height = 2 * boxHeight + boxDepth;

            // See `./resources/BoxCrossCutting.jpg` in root folder for Visual representation of this Polygon
            Polygon = geometryFactory.CreatePolygon(new[]
            {
                // Point #1
                new Coordinate(0, boxHeight),
                // Point #2
                new Coordinate(boxHeight, boxHeight),
                // Point #3
                new Coordinate(boxHeight, 0),
                // Point #4
                new Coordinate(boxHeight + boxWidth, 0),
                // Point #5
                new Coordinate(boxHeight + boxWidth, boxHeight),
                // Point #6
                new Coordinate(2 * boxHeight + boxWidth, boxHeight),
                // Point #7
                new Coordinate(2 * boxHeight + 2 * boxWidth, boxHeight),
                // Point #8
                new Coordinate(2 * boxHeight + 2 * boxWidth, boxHeight + boxDepth),
                // Point #9
                new Coordinate(2 * boxHeight + boxWidth, boxHeight + boxDepth),
                // Point #10
                new Coordinate(boxHeight + boxWidth, boxHeight + boxDepth),
                // Point #11
                new Coordinate(boxHeight + boxWidth, 2 * boxHeight + boxDepth),
                // Point #12
                new Coordinate(boxHeight, 2 * boxHeight + boxDepth),
                // Point #13
                new Coordinate(boxHeight, boxHeight + boxDepth),
                // Point #14
                new Coordinate(0, boxHeight + boxDepth),
                // Point #1
                new Coordinate(0, boxHeight),
            });
        }

        /// <inheritdoc cref="IBoxCutting.GetProgram"/>
        public IReadOnlyList<ICommand> GetProgram(Point initialPoint)
        {
            var firstCoordinate = Polygon.Coordinates.First();

            // Initialize cutter, move to first polygon coordinate and make cutter down
            var commands = new List<ICommand>
            {
                new GotoCommand((int)(initialPoint.X + firstCoordinate.X), (int)(initialPoint.Y + firstCoordinate.Y)),
                new DownCommand()
            };

            commands.AddRange(Polygon.Coordinates
                // Skip first coordinate because we pre-moved cutter to that coordinate before
                .Skip(1)
                .Select(coordinate => new GotoCommand(
                        (int)(initialPoint.X + coordinate.X),
                        (int)(initialPoint.Y + coordinate.Y))
                )
                .ToList());

            commands.Add(new UpCommand());

            return commands;
        }
    }
}