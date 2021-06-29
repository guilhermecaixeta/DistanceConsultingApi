using Craftable.Core.queries;
using Craftable.SharedKernel.DTO;

namespace Craftable.Core.interfaces.CQRS.queries
{
    public interface IPostalcodeQueryHandler : IRequestHandlerAsync<PostalCodeQuery, IQueryResult<PostcodeAddressRangedDTO>>
    {
    }
}
