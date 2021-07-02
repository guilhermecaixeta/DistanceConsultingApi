using Craftable.SharedKernel.interfaces;
using System.Collections.Generic;

namespace Craftable.SharedKernel.DTO
{
    public class ResultDTO<T> : ResultDTO, IResult<T>
    {
        public ResultDTO(bool success, IReadOnlyList<string> errors, T data) : base(success, errors)
        {
            Data = data;
        }

        public T Data { get; private set; }
    }

    public class ResultDTO : IResult
    {
        public ResultDTO(bool success, IReadOnlyList<string> errors)
        {
            Success = success;
            Errors = errors;
        }

        public bool Success { get; private set; }
        public IReadOnlyList<string> Errors { get; set; }
    }
}