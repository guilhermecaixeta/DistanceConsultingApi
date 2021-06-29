using Craftable.Core.commands;

namespace Craftable.Core.interfaces.CQRS.commands
{
    public interface IPostalcodeCommandHandler : IRequestHandlerAsync<AddressRangedCommand, ICommandResult>
    {
    }
}
