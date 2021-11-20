using NetTopologySuite;
using NetTopologySuite.Geometries;

namespace BoxCutting.Core.Models.BoxCuttings
{
    /// <summary>
    /// Representation of BoxCrossCutting, see `./resources/BoxCrossRotatedCutting.jpg` in root folder
    /// </summary>
    public class BoxCrossRotatedCutting : BoxCrossCutting
    {
        public override int Width { get; protected set; }
        public override int Height { get; protected set; }

        public BoxCrossRotatedCutting(int boxWidth, int boxDepth, int boxHeight)
        {
            var geometryFactory = NtsGeometryServices.Instance.CreateGeometryFactory();

            Width = 2 * boxHeight + boxDepth;
            Height = 2 * boxHeight + 2 * boxWidth;

            // See `./resources/BoxCrossRotatedCutting.jpg` in root folder for Visual representation of this Polygon
            Polygon = geometryFactory.CreatePolygon(new[]
            {
                // Point #1
                new Coordinate(0, boxHeight),
                // Point #2
                new Coordinate(boxHeight, boxHeight),
                // Point #3
                new Coordinate(boxHeight, 0),
                // Point #4
                new Coordinate(boxHeight + boxDepth, 0),
                // Point #5
                new Coordinate(boxHeight + boxDepth, boxHeight),
                // Point #6
                new Coordinate(2 * boxHeight + boxDepth, boxHeight),
                // Point #7
                new Coordinate(2 * boxHeight + boxDepth, boxHeight + boxWidth),
                // Point #8
                new Coordinate(boxHeight + boxDepth, boxHeight + boxWidth),
                // Point #9
                new Coordinate(boxHeight + boxDepth, 2 * boxHeight + 2 * boxWidth),
                // Point #10
                new Coordinate(boxHeight, 2 * boxHeight + 2 * boxWidth),
                // Point #11
                new Coordinate(boxHeight, boxHeight + boxWidth),
                // Point #12
                new Coordinate(0, boxHeight + boxWidth),
                // Point #1
                new Coordinate(0, boxHeight),
            });
        }
    }
}