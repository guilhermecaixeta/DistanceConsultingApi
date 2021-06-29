using System.Threading;
using System.Threading.Tasks;

namespace Craftable.Core.interfaces.CQRS
{
    public interface IRequestHandlerAsync<in TEvent, TResult>
        where TEvent : IEvent
        where TResult : IResult
    {
        Task<TResult> HandleAsync(TEvent handler, CancellationToken cancellationToken);
    }
}