using Craftable.Core.interfaces.CQRS;
using Craftable.SharedKernel.interfaces;

namespace Craftable.Core.interfaces.queries
{
    public interface IQueryHandler<TQuery, TResult> : IHandlerAsync<TQuery, TResult>
        where TResult : IResult
        where TQuery : IQuery
    {
    }
}
