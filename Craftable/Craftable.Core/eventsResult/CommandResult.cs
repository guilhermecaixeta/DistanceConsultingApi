using Craftable.Core.interfaces;
using System.Collections.Generic;

namespace Craftable.Core.eventsResult
{
    public class CommandResult : ICommandResult
    {
        public CommandResult(bool success, IReadOnlyList<string> errors)
        {
            Success = success;
            Errors = errors;
        }

        public bool Success { get; private set; }

        public IReadOnlyList<string> Errors { get; private set; }
    }
}