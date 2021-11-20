using BoxCutting.Core.Interfaces;

namespace BoxCutting.Core.Models.Commands
{
    /// <summary>
    /// Lower Cutter into working position
    /// If Cutter already lowered, then this command do nothing
    /// </summary>
    public class DownCommand : ICommand
    {
        /// <inheritdoc cref="ICommand.Type"/>
        public CommandType Type => CommandType.Down;
    }
}