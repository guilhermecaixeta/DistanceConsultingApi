using Craftable.Core.validators;

namespace Craftable.Core.interfaces.CQRS
{
    public interface IEvent
    {
        Notificator ValidateEvent();
    }
}