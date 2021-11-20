using BoxCutting.Core.Interfaces;

namespace BoxCutting.Core.Models.Commands
{
    /// <summary>
    /// Moves Cutter to (X, Y) position.
    /// If Cutter lowered, it makes a cut (X0, Y0) -> (X, Y), where X0 and Y0 - previous Cutter coordinates
    /// </summary>
    public class GotoCommand : ICommand
    {
        /// <inheritdoc cref="ICommand.Type"/>
        public CommandType Type => CommandType.Goto;

        /// <summary>
        /// The X coordinate to where move Cutter
        /// </summary>
        public int X { get; set; }

        /// <summary>
        /// The Y coordinate to where move Cutter
        /// </summary>
        public int Y { get; set; }

        public GotoCommand(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}