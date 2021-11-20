using System.Collections.Generic;
using System.Linq;
using BoxCutting.Core;
using BoxCutting.Core.Constants;
using BoxCutting.Core.Interfaces;
using BoxCutting.Core.Models;
using BoxCutting.Core.Models.Commands;
using Moq;
using NetTopologySuite.Geometries;
using NUnit.Framework;

namespace BoxCutting.UnitTests
{
    [TestFixture]
    public class BoxCutterTests
    {
        /// <summary>
        /// Test whole solution (no mocks)
        /// </summary>
        [Test]
        [TestCase(800, 600, 200, 200, 200, 1)]
        [TestCase(1600, 600, 200, 200, 200, 2)]
        [TestCase(800, 1000, 200, 200, 200, 2)]
        [TestCase(1600, 1000, 200, 200, 200, 4)]
        public void TestSuccessResults(
            int sheetWidth,
            int sheetLength,
            int boxWidth,
            int boxDepth,
            int boxHeight,
            int expectedBoxCount)
        {
            // Arrange
            var sheet = new Sheet(sheetWidth, sheetLength);

            var boxCutter = new BoxCutter(sheet);

            // Act
            var result = boxCutter.GetCuttingProgram(boxWidth, boxDepth, boxHeight);

            // Assert
            Assert.IsTrue(result.Success);
            Assert.AreEqual(expectedBoxCount, result.Amount);
        }

        [Test(
            Description = "Test that BoxCutter return error when it can't make any box from Sheet")]
        public void TestPackerCantPlaceAnyBoxCutting_SheetSizeTooSmall()
        {
            // Arrange
            var sheet = new Sheet(1000, 1000);
            var packerMock = new Mock<IPacker>();

            packerMock.Setup(x => x.PackBoxCuttings(It.IsAny<Sheet>(), It.IsAny<List<IBoxCutting>>()))
                .Returns(new List<BoxPackingResult>());

            var boxCutter = new BoxCutter(sheet,
                packerMock.Object);

            var boxWidth = 100;
            var boxDepth = 100;
            var boxHeight = 100;

            // Act
            var result = boxCutter.GetCuttingProgram(boxWidth, boxDepth, boxHeight);

            // Assert
            Assert.IsFalse(result.Success);
            Assert.AreEqual(ErrorMessages.SheetSizeTooSmall, result.Error);
        }

        [Test(
            Description = "Test that BoxCutter always starts with 'Start' command and ends with 'Stop' command")]
        public void TestProgram_AlwaysHasStartStopCommands()
        {
            // Arrange
            var sheet = new Sheet(1000, 1000);

            var boxCuttingMock = CreateBoxCuttingMock();

            // Mock IBoxCuttingFactory to always return BoxCuttingMock
            var boxCuttingFactoryMock = new Mock<IBoxCuttingFactory>();
            boxCuttingFactoryMock.Setup(x => x.Create(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>()))
                .Returns(new List<IBoxCutting> { boxCuttingMock.Object });

            // Mock IPacker to always return just one result
            var packerMock = new Mock<IPacker>();
            packerMock.Setup(x => x.PackBoxCuttings(It.IsAny<Sheet>(), It.IsAny<List<IBoxCutting>>()))
                .Returns(new List<BoxPackingResult>
                {
                    new BoxPackingResult
                    {
                        Cutting = boxCuttingMock.Object,
                        Position = new Point(0, 0)
                    }
                });

            var boxCutter = new BoxCutter(sheet,
                packerMock.Object,
                boxCuttingFactoryMock.Object);

            // Act
            var result = boxCutter.GetCuttingProgram(100, 100, 100);

            // Assert
            Assert.IsTrue(result.Success);
            Assert.GreaterOrEqual(result.Program.Count, 2);
            Assert.IsTrue(result.Program.First().Type == CommandType.Start,
                $"First Command in Program should be always {CommandType.Start}");

            Assert.IsTrue(result.Program.Last().Type == CommandType.Stop,
                $"Last Command in Program should be always {CommandType.Stop}");
        }

        [Test(
            Description = "Test whether BoxCutter includes all commands from each IBoxCutting used in calculations")]
        public void TestProgram_IncludesBoxCuttingCommands()
        {
            // Arrange
            var sheet = new Sheet(1000, 1000);

            var boxCuttingCommands = new List<ICommand>
            {
                new DownCommand(),
                new GotoCommand(10, 10),
                new UpCommand()
            };

            var boxCuttingMock = CreateBoxCuttingMock(program: boxCuttingCommands);

            var boxCuttingFactoryMock = new Mock<IBoxCuttingFactory>();
            boxCuttingFactoryMock.Setup(x => x.Create(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>()))
                .Returns(new List<IBoxCutting> { boxCuttingMock.Object });

            var packerMock = new Mock<IPacker>();
            packerMock.Setup(x => x.PackBoxCuttings(It.IsAny<Sheet>(), It.IsAny<List<IBoxCutting>>()))
                .Returns(new List<BoxPackingResult>
                {
                    new BoxPackingResult
                    {
                        Cutting = boxCuttingMock.Object,
                        Position = new Point(0, 0)
                    }
                });

            var boxCutter = new BoxCutter(sheet,
                packerMock.Object,
                boxCuttingFactoryMock.Object);

            // Act
            var result = boxCutter.GetCuttingProgram(100, 100, 100);

            // Assert
            Assert.IsTrue(result.Success);

            foreach (var boxCuttingCommand in boxCuttingCommands)
            {
                // Assert if program contains all commands from what IBoxCutting needs (compare by reference, because commandTypes can be duplicated)
                var programCommand = result.Program.FirstOrDefault(p => p == boxCuttingCommand);

                Assert.IsNotNull(programCommand);
            }
        }


        [Test(
            Description = "Test whether BoxCutter sums all amount of boxes which could be made from each IBoxCutting")]
        public void TestBoxCount_AreIncludedInResult()
        {
            // Arrange
            var sheet = new Sheet(1000, 1000);
            var boxCutting1BoxCount = 10;
            var boxCutting2BoxCount = 1;

            var boxCutting1Mock = CreateBoxCuttingMock(boxCutting1BoxCount);
            var boxCutting2Mock = CreateBoxCuttingMock(boxCutting2BoxCount);

            var boxCuttingFactoryMock = new Mock<IBoxCuttingFactory>();
            boxCuttingFactoryMock.Setup(x => x.Create(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>()))
                .Returns(new List<IBoxCutting>
                {
                    boxCutting1Mock.Object,
                    boxCutting2Mock.Object
                });

            var packerMock = new Mock<IPacker>();
            packerMock.Setup(x => x.PackBoxCuttings(It.IsAny<Sheet>(), It.IsAny<List<IBoxCutting>>()))
                .Returns(new List<BoxPackingResult>
                {
                    new BoxPackingResult
                    {
                        Cutting = boxCutting1Mock.Object,
                        Position = new Point(0, 0)
                    },
                    new BoxPackingResult
                    {
                        Cutting = boxCutting2Mock.Object,
                        Position = new Point(0, 0)
                    }
                });

            var boxCutter = new BoxCutter(sheet,
                packerMock.Object,
                boxCuttingFactoryMock.Object);

            // Act
            var result = boxCutter.GetCuttingProgram(100, 100, 100);

            // Assert
            Assert.IsTrue(result.Success);
            Assert.AreEqual(boxCutting1BoxCount + boxCutting2BoxCount, result.Amount);
        }


        private Mock<IBoxCutting> CreateBoxCuttingMock(int boxCount = 1, List<ICommand> program = null)
        {
            var boxCuttingMock = new Mock<IBoxCutting>();
            boxCuttingMock.Setup(x => x.GetProgram(It.IsAny<Point>()))
                .Returns(program ?? new List<ICommand>());
            boxCuttingMock.Setup(x => x.BoxCount)
                .Returns(boxCount);

            return boxCuttingMock;
        }
    }
}