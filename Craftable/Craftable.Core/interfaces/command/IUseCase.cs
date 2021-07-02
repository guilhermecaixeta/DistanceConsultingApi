using Craftable.Core.interfaces.CQRS;
using Craftable.SharedKernel.interfaces;

namespace Craftable.Core.interfaces.command
{
    public interface IUseCase<TIn, TResult> : IHandlerAsync<TIn, TResult>
        where TResult : IResult
        where TIn : ICommand
    {
    }
}
