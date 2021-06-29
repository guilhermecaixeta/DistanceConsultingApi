using Craftable.Core.commands;
using Craftable.Core.eventsResult;
using Craftable.Core.interfaces.CQRS;
using Craftable.Core.interfaces.CQRS.commands;
using Craftable.Core.interfaces.Repository;
using Craftable.Core.valueObjects;
using System.Threading;
using System.Threading.Tasks;

namespace Craftable.Core.entities.handlers
{
    public class AddressRangedCommandHandler : IPostalcodeCommandHandler
    {
        private readonly IPostCodeAddressRepository _consultedPostCodeRepository;

        public AddressRangedCommandHandler(IPostCodeAddressRepository consultedPostCodeRepository)
        {
            _consultedPostCodeRepository = consultedPostCodeRepository;
        }

        public async Task<ICommandResult> HandleAsync(AddressRangedCommand handler, CancellationToken cancellationToken)
        {
            var notificator = handler.ValidateEvent();

            if (!notificator.IsValid)
            {
                return new CommandResult(false, notificator.Errors);
            }

            var coordinates = new Coordinates(handler.Longitude, handler.Latitude);
            var distance = new Distance(handler.DistanceInKilometer, handler.DistanceInMiles);
            var postcode = handler.Postcode;
            var country = handler.Country;

            var addressRanged = new AddressRegister(postcode, country, coordinates, distance);

            await _consultedPostCodeRepository.CreateAsync(addressRanged, cancellationToken);

            return new CommandResult(true, default);
        }
    }
}