using Craftable.Core.queries;
using Craftable.SharedKernel.DTO;
using System.Collections.Generic;

namespace Craftable.Core.interfaces.CQRS.queries
{
    public interface IPostalcodeListQueryHandler : IRequestHandlerAsync<AddressesQuery, IQueryResult<IReadOnlyList<PostcodeDTO>>>
    {
    }
}
