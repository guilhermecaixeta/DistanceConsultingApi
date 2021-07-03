using System;

namespace Craftable.SharedKernel.DTO
{
    public record PostcodeDTO
    {
        public string Code { get; init; }
        public DateTime Date { get; init; }
    }
}