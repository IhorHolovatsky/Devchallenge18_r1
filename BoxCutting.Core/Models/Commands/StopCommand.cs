using BoxCutting.Core.Interfaces;

namespace BoxCutting.Core.Models.Commands
{
    /// <summary>
    /// This commands is a normal exit behavior
    /// It lifts Cutter into Idle position and move to (0, 0) coordinate
    /// </summary>
    public class StopCommand : ICommand
    {
        /// <inheritdoc cref="ICommand.Type"/>
        public CommandType Type => CommandType.Stop;
    }
}