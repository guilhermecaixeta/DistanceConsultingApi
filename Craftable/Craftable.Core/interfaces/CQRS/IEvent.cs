using Craftable.Core.validators;

namespace Craftable.Core.interfaces
{
    public interface IEvent
    {
        Notificator ValidateEvent();
    }
}