using Craftable.Core.aggregate.postcode.commands;
using Craftable.Core.aggregate.postcode.queries;
using Craftable.Core.interfaces;
using Craftable.Core.interfaces.services;
using Craftable.SharedKernel.DTO;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Craftable.Core.services
{
    public class PostcodeServiceAsync : IPostcodeServiceAsync
    {
        private readonly IRequestHandlerAsync<AddressesQuery, IQueryResult<IReadOnlyList<PostcodeDTO>>> _addressRangedQuery;
        private readonly IRequestHandlerAsync<PostalCodeQuery, IQueryResult<PostcodeAddressRangedDTO>> _postalCodeQuery;
        private readonly IRequestHandlerAsync<AddressRangedCommand, ICommandResult> _addressRangedCommand;

        public PostcodeServiceAsync(
            IRequestHandlerAsync<AddressesQuery, IQueryResult<IReadOnlyList<PostcodeDTO>>> addressRangedQuery,
            IRequestHandlerAsync<PostalCodeQuery, IQueryResult<PostcodeAddressRangedDTO>> postalCodeQuery,
            IRequestHandlerAsync<AddressRangedCommand, ICommandResult> addressRangedCommand)
        {
            _addressRangedQuery = addressRangedQuery;
            _postalCodeQuery = postalCodeQuery;
            _addressRangedCommand = addressRangedCommand;
        }

        public async Task<ResultDTO<IReadOnlyList<PostcodeDTO>>> GetPostcodesRangedsAsync(CancellationToken cancellationToken)
        {
            var query = new AddressesQuery();
            var response = await _addressRangedQuery.HandleAsync(query, cancellationToken);
            if (!response.Success)
            {
                return new ResultDTO<IReadOnlyList<PostcodeDTO>>(default, false, response.Errors);
            }
            return new ResultDTO<IReadOnlyList<PostcodeDTO>>(response.Data, response.Success, response.Errors);
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