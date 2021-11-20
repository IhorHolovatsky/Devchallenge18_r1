using System.Linq;
using BoxCutting.Core.Models;
using BoxCutting.Core.Models.BoxCuttings;
using NetTopologySuite.Geometries;
using NUnit.Framework;

namespace BoxCutting.UnitTests.BoxCuttingsTests
{
    [TestFixture]
    public class BoxTetrisCuttingPatternTests
    {
        [Test]
        public void TestBoxTetrisCutting_ShouldBuildPattern()
        {
            // Arrange
            var crossBoxCutting = new BoxTetrisCuttingPattern(200, 200, 200);
            var sheet = new Sheet(1600, 1000);

            // Act
            var boxCutting = crossBoxCutting.Build(sheet);

            // Assert
            Assert.AreNotSame(crossBoxCutting, boxCutting, 
                "IBoxCuttingPattern.Build should return new BoxCutting, but not itself");
            Assert.IsTrue(boxCutting.BoxCount == 2);
        }


        [Test]
        public void TestBoxTetrisCutting_EndsWithUpCommand()
        {
            // Arrange
            var crossBoxCutting = new BoxTetrisCuttingPattern(200, 200, 200);
            var initialPoint = new Point(0, 0);

            // Act
            var commands = crossBoxCutting.GetProgram(initialPoint);

            // Assert
            Assert.IsTrue(commands.Last().Type == CommandType.Up,
                "CrossBoxCutting.GetProgram should end with CommandType.Up command");
        }
    }
}