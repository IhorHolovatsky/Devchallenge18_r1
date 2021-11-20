using BoxCutting.Core.Interfaces;

namespace BoxCutting.Core.Models.Commands
{
    /// <summary>
    /// Lift Cutter to Idle position
    /// If Cutter already in Idle position, then this command do nothing
    /// </summary>
    public class UpCommand : ICommand
    {
        /// <inheritdoc cref="ICommand.Type"/>
        public CommandType Type => CommandType.Up;
    }
}