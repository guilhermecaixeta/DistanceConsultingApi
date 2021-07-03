using Craftable.Web.DTO;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Craftable.Web.services
{
    public interface IPostcodeService
    {
        Task<ResponseDTO<IReadOnlyList<PostalcodeDTO>>> GetPostcodeList(CancellationToken cancellationToken);

        Task<ResponseDTO<PostcodeDistanceDTO>> SaveDistanceFromPostCode(string code, CancellationToken cancellationToken);
    }
}