using System.Linq;
using BoxCutting.Core.Models;
using BoxCutting.Core.Models.BoxCuttings;
using NetTopologySuite.Geometries;
using NUnit.Framework;

namespace BoxCutting.UnitTests.BoxCuttingsTests
{
    [TestFixture]
    public class BoxCrossCuttingTests
    {
        [Test]
        public void TestBoxCutting_ValidPolygon()
        {
            // Arrange
            // Act
            var crossBoxCutting = new BoxCrossCutting(200, 200, 200);

            // Assert
            Assert.IsTrue(crossBoxCutting.Polygon.IsValid, 
                "CrossBoxCutting.Polygon should be valid figure");
            Assert.IsTrue(!crossBoxCutting.Polygon.Holes.Any(),
                "CrossBoxCutting.Polygon should not have holes");
        }

        [Test]
        public void TestBoxCutting_EndsWithUpCommand()
        {
            // Arrange
            var crossBoxCutting = new BoxCrossCutting(200, 200, 200);
            var initialPoint = new Point(0, 0);

            // Act
            var commands = crossBoxCutting.GetProgram(initialPoint);

            // Assert
            Assert.IsTrue(commands.Last().Type == CommandType.Up,
                "CrossBoxCutting.GetProgram should end with CommandType.Up command");
        }
    }
}