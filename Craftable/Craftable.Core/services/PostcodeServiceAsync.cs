using Craftable.Core.commands;
using Craftable.Core.interfaces.CQRS;
using Craftable.Core.interfaces.CQRS.commands;
using Craftable.Core.interfaces.CQRS.queries;
using Craftable.Core.interfaces.services;
using Craftable.Core.queries;
using Craftable.SharedKernel.DTO;
using System.Threading;
using System.Threading.Tasks;

namespace Craftable.Core.services
{
    public class PostcodeServiceAsync : IPostcodeServiceAsync
    {
        private readonly IPostalcodeQueryHandler _postalCodeQuery;
        private readonly IPostalcodeCommandHandler _addressRangedCommand;

        public PostcodeServiceAsync(
            IPostalcodeQueryHandler postalCodeQuery,
            IPostalcodeCommandHandler addressRangedCommand)
        {
            _postalCodeQuery = postalCodeQuery;
            _addressRangedCommand = addressRangedCommand;
        }

        public async Task<ResultDTO<PostcodeRangedDTO>> GetPostcodeRangedAsync(string postcode, CancellationToken cancellationToken)
        {
            var postcodeResponse = await PostcodeQueryAsync(postcode, cancellationToken);

            if (!postcodeResponse.Success)
            {
                return new ResultDTO<PostcodeRangedDTO>(default, false, postcodeResponse.Errors);
            }

            var address = postcodeResponse.Data;
            var commandResponse = await AddressRangedCommandAsync(address, cancellationToken);

            if (!commandResponse.Success)
            {
                return new ResultDTO<PostcodeRangedDTO>(default, false, commandResponse.Errors);
            }

            var postcodeRangeDTO = new PostcodeRangedDTO
            {
                Code = address.Postcode,
                DistanceFromHeathrowAirportInKilometer = address.DistanceFromHeathrowAirportInKilometers,
                DistanceFromHeathrowAirportInMiles = address.DistanceFromHeathrowAirportInMiles
            };
            return new ResultDTO<PostcodeRangedDTO>(postcodeRangeDTO, true, default);
        }

        private Task<ICommandResult> AddressRangedCommandAsync(PostcodeAddressRangedDTO address, CancellationToken cancellationToken)
        {
            var addressRangedCommand = new AddressRangedCommand
            {
                Postcode = address.Postcode,
                Country = address.Country,
                Latitude = address.Latitude,
                Longitude = address.Longitude,
                DistanceInMiles = address.DistanceFromHeathrowAirportInMiles,
                DistanceInKilometer = address.DistanceFromHeathrowAirportInKilometers
            };

            return _addressRangedCommand.HandleAsync(addressRangedCommand, cancellationToken);
        }

        private Task<IQueryResult<PostcodeAddressRangedDTO>> PostcodeQueryAsync(string postcode, CancellationToken cancellationToken)
        {
            var query = new PostalCodeQuery
            {
                Code = postcode
            };

            return _postalCodeQuery.HandleAsync(query, cancellationToken);
        }
    }
}