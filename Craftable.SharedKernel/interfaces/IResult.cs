using System.Collections.Generic;

namespace Craftable.SharedKernel.interfaces
{
    public interface IResult<out T> : IResult
    {
        T Data { get; }
    }

    public interface IResult
    {
        bool Success { get; }

        IReadOnlyList<string> Errors { get; }
    }
}