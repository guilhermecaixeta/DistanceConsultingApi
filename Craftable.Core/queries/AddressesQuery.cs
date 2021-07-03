using Craftable.Core.interfaces.CQRS;
using Craftable.Core.validators;

namespace Craftable.Core.queries
{
    public class AddressesQuery : IQuery
    {
        public Notificator ValidateEvent()
        {
            return new Notificator(true, default);
        }
    }
}