using Craftable.Core.aggregate.postcode;
using Craftable.Core.interfaces.Repository;
using Craftable.Infrastructure.data;

namespace Craftable.Infrastructure.repositories
{
    public class PostalCodeRepositoryAsync : RepositoryAsync<AddressRegister>, IPostalCodeRepository
    {
        public PostalCodeRepositoryAsync(CraftableContext context) : base(context)
        {
        }
    }
}
