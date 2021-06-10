using Craftable.SharedKernel.DTO;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Craftable.Core.interfaces.services
{
    public interface IPostcodeServiceAsync
    {
        Task<ResultDTO<PostcodeRangedDTO>> GetPostcodeRangedAsync(string postcode, CancellationToken cancellationToken);

        Task<ResultDTO<IReadOnlyList<PostcodeDTO>>> GetPostcodesRangedsAsync(CancellationToken cancellationToken);
    }
}