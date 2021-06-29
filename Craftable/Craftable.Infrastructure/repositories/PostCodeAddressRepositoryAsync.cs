using Craftable.Core.entities;
using Craftable.Core.interfaces.Repository;
using Craftable.Infrastructure.data;

namespace Craftable.Infrastructure.repositories
{
    public class PostCodeAddressRepositoryAsync : RepositoryAsync<AddressRegister>, IPostCodeAddressRepository
    {
        public PostCodeAddressRepositoryAsync(CraftableContext context) : base(context)
        {
        }
    }
}
