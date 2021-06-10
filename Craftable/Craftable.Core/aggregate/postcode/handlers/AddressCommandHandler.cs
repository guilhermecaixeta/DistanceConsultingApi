using Craftable.Core.aggregate.postcode.commands;
using Craftable.Core.eventsResult;
using Craftable.Core.interfaces;
using Craftable.Core.interfaces.Repository;
using Craftable.Core.valueObjects;
using System.Threading;
using System.Threading.Tasks;

namespace Craftable.Core.aggregate.postcode.handlers
{
    public class AddressRangedCommandHandler :
        IRequestHandlerAsync<AddressRangedCommand, ICommandResult>
    {
        private readonly IPostalCodeRepository _consultedPostCodeRepository;

        public AddressRangedCommandHandler(IPostalCodeRepository consultedPostCodeRepository)
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
            var addressRanged = new AddressRegister(handler.Postcode, handler.Country, coordinates, distance);

            await _consultedPostCodeRepository.CreateAsync(addressRanged, cancellationToken);

            return new CommandResult(true, default);
        }
    }
}