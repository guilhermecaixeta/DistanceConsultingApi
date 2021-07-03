using Craftable.Core.commands;
using Craftable.SharedKernel.DTO;
using Craftable.SharedKernel.interfaces;

namespace Craftable.Core.interfaces.command
{
    public interface IGetDistanceFromPostCodeUseCaseAsync : IUseCase<PostcodeCommand, IResult<PostcodeRangedDTO>>
    {
    }
}