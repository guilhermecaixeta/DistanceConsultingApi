using Craftable.Core.interfaces;
using Craftable.Core.validators;

namespace Craftable.Core.aggregate.postcode.queries
{
    public class AddressesQuery : IQuery
    {
        public Notificator ValidateEvent()
        {
            return new Notificator(true, default);
        }
    }
}