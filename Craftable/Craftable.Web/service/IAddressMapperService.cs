using Craftable.Core.interfaces.CQRS;
using Craftable.SharedKernel.DTO;
using Craftable.Web.DTO;
using System.Collections.Generic;

namespace Craftable.Web.services
{
    public interface IAddressMapperService
    {
        ResponseDTO<IReadOnlyList<PostalcodeDTO>> MappingPostcodeList(IQueryResult<IReadOnlyList<PostcodeDTO>> queryResult);

        ResponseDTO<PostcodeDistanceDTO> MappingPostcode(ResultDTO<PostcodeRangedDTO> resultDTO);
    }
}