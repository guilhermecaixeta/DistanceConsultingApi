using Craftable.SharedKernel.interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace Craftable.Core.interfaces.CQRS
{
    public interface IHandlerAsync<in TEvent, TResult>
        where TEvent : IEvent
        where TResult : IResult
    {
        Task<TResult> HandlerAsync(TEvent handler, CancellationToken cancellationToken);
    }
}