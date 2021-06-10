using System.Collections.Generic;

namespace Craftable.SharedKernel.DTO
{
    public class ResultDTO<T>
    {
        public ResultDTO(T data, bool success, IReadOnlyList<string> errors)
        {
            Data = data;
            Success = success;
            Errors = errors;
        }

        public T Data { get; private set; }
        public bool Success { get; private set; }
        public IReadOnlyList<string> Errors { get; set; }
    }
}