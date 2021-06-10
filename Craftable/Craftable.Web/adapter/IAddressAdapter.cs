using Craftable.Web.DTO;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Craftable.Web.services
{
    public interface IAddressAdapter
    {
        Task<ResponseDTO<IReadOnlyList<PostalcodeDTO>>> GetPostcodeHistoric(CancellationToken cancellationToken);

        Task<ResponseDTO<PostcodeDistanceDTO>> GetAddressByPostalCode(string code, CancellationToken cancellationToken);
    }
}