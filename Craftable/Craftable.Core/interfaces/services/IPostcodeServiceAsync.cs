using Craftable.SharedKernel.DTO;
using System.Threading;
using System.Threading.Tasks;

namespace Craftable.Core.interfaces.services
{
    public interface IPostcodeServiceAsync
    {
        Task<ResultDTO<PostcodeRangedDTO>> GetPostcodeRangedAsync(string postcode, CancellationToken cancellationToken);
    }
}