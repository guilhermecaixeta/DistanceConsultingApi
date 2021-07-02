using Craftable.Core.interfaces.CQRS;
using Craftable.Core.queries;
using Craftable.SharedKernel.DTO;
using Craftable.SharedKernel.interfaces;

namespace Craftable.Core.interfaces.queries
{
    public interface IPostalcodeQueryHandler : IQueryHandler<PostalCodeQuery, IResult<PostcodeAddressRangedDTO>>
    {
    }
}
