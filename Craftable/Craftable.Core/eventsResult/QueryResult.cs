using Craftable.Core.interfaces.CQRS;
using System.Collections.Generic;

namespace Craftable.Core.eventsResult
{
    public class QueryResult<T> : IQueryResult<T>
    {
        public QueryResult(bool success, IReadOnlyList<string> errors, T data)
        {
            Success = success;
            Errors = errors;
            Data = data;
        }

        public bool Success { get; private set; }

        public IReadOnlyList<string> Errors { get; private set; }

        public T Data { get; private set; }
    }
}