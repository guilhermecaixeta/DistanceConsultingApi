using Craftable.Core.extensions;
using Craftable.Core.interfaces.CQRS;
using Craftable.Core.validators;

namespace Craftable.Core.commands
{
    public class AddressRangedCommand : ICommand
    {
        public Notificator ValidateEvent()
        {
            var addressValidator = ValidatorFactory<AddressRangedCommand, AddressRangedCommandValidator>.Create();
            var notificator = addressValidator.Validate(this).ToNotificator();
            return notificator;
        }

        public string Postcode { get; set; }
        public string Country { get; set; }
        public double DistanceInKilometer { get; set; }
        public double DistanceInMiles { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}