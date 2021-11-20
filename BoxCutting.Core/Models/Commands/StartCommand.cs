using BoxCutting.Core.Interfaces;

namespace BoxCutting.Core.Models.Commands
{
    /// <summary>
    /// Insert new Sheet into Cutting Machine.
    /// Moves Cutter to (0, 0) coordinate and enforce idle position
    /// </summary>
    public class StartCommand : ICommand
    {
        /// <inheritdoc cref="ICommand.Type"/>
        public CommandType Type => CommandType.Start;
    }
}