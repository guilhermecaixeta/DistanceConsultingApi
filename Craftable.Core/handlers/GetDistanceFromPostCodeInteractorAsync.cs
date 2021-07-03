using Craftable.Core.commands;
using Craftable.Core.entities;
using Craftable.Core.interfaces.command;
using Craftable.Core.interfaces.queries;
using Craftable.Core.queries;
using Craftable.Core.valueObjects;
using Craftable.SharedKernel.DTO;
using Craftable.SharedKernel.interfaces;
using Craftable.SharedKernel.interfaces.Repository;
using System.Threading;
using System.Threading.Tasks;

namespace Craftable.Core.services
{
    public class GetDistanceFromPostCodeInteractorAsync : IGetDistanceFromPostCodeUseCaseAsync
    {
        private readonly IPostalcodeQueryHandler _postalCodeQuery;
        private readonly IRepositoryAsync<AddressRegister> _consultedPostCodeRepository;

        public GetDistanceFromPostCodeInteractorAsync(
            IPostalcodeQueryHandler postalCodeQuery,
            IRepositoryAsync<AddressRegister> consultedPostCodeRepository)
        {
            _postalCodeQuery = postalCodeQuery;
            _consultedPostCodeRepository = consultedPostCodeRepository;
        }

        public async Task<IResult<PostcodeRangedDTO>> HandlerAsync(PostcodeCommand handler, CancellationToken cancellationToken)
        {

            var postcodeResponse = await PostcodeQueryAsync(handler.Postcode, cancellationToken);

            if (!postcodeResponse.Success)
            {
                return new ResultDTO<PostcodeRangedDTO>(false, postcodeResponse.Errors, default);
            }

            var address = postcodeResponse.Data;

            await SaveAddressRegister(address, cancellationToken);

            var postcodeRangeDTO = new PostcodeRangedDTO
            {
                Code = address.Postcode,
                DistanceFromHeathrowAirportInKilometer = address.DistanceFromHeathrowAirportInKilometers,
                DistanceFromHeathrowAirportInMiles = address.DistanceFromHeathrowAirportInMiles
            };

            return new ResultDTO<PostcodeRangedDTO>(true, default, postcodeRangeDTO);
        }

        private Task SaveAddressRegister(PostcodeAddressRangedDTO address, CancellationToken cancellationToken)
        {
            var coordinates = new Coordinates(address.Longitude, address.Latitude);
            var distance = new Distance(address.DistanceFromHeathrowAirportInKilometers, address.DistanceFromHeathrowAirportInMiles);
            var postcode = address.Postcode;
            var country = address.Country;

            var addressRanged = new AddressRegister(postcode, country, coordinates, distance);

            return _consultedPostCodeRepository.CreateAsync(addressRanged, cancellationToken);
        }

        private Task<IResult<PostcodeAddressRangedDTO>> PostcodeQueryAsync(string postcode, CancellationToken cancellationToken)
        {
            var query = new PostalCodeQuery
            {
                Code = postcode
            };

            return _postalCodeQuery.HandlerAsync(query, cancellationToken);
        }
    }
}