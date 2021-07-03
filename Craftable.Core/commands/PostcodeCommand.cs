using Craftable.Core.extensions;
using Craftable.Core.interfaces.CQRS;
using Craftable.Core.validators;

namespace Craftable.Core.commands
{
    public class PostcodeCommand : ICommand
    {
        public Notificator ValidateEvent()
        {
            var addressValidator = ValidatorFactory<PostcodeCommand, PostcodeCommandValidator>.Create();
            var notificator = addressValidator.Validate(this).ToNotificator();
            return notificator;
        }

        public string Postcode { get; set; }
    }
}