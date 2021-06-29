using Craftable.Core.extensions;
using Craftable.Core.interfaces;
using Craftable.Core.interfaces.CQRS;
using Craftable.Core.validators;

namespace Craftable.Core.queries
{
    public class PostalCodeQuery : IQuery
    {
        public string Code { get; set; }

        public Notificator ValidateEvent()
        {
            var validator = new PostalCodeQueryValidator();

            var validationResult = validator.Validate(this);

            return validationResult.ToNotificator();
        }
    }
}