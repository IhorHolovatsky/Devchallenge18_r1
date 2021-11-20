using BoxCutting.Core.Models;

namespace BoxCutting.Core.Interfaces
{
    /// <summary>
    /// Represent a command for Cutting Machine
    /// </summary>
    public interface ICommand
    {
        CommandType Type { get; }
    }
}