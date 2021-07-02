using Craftable.Core.queries;
using Craftable.SharedKernel.DTO;
using Craftable.SharedKernel.interfaces;
using System.Collections.Generic;

namespace Craftable.Core.interfaces.queries
{
    public interface IPostalcodeListQueryHandler : IQueryHandler<AddressesQuery, IResult<IReadOnlyList<PostcodeDTO>>>
    {
    }
}
