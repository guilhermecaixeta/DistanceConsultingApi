using Craftable.Core.aggregate.postcode;
using Craftable.Core.aggregate.postcode.queries;
using Craftable.Core.eventsResult;
using Craftable.Core.extensions;
using Craftable.Core.interfaces;
using Craftable.Core.valueObjects;
using Craftable.Infrastructure.data;
using Craftable.Infrastructure.facade;
using Craftable.SharedKernel.DTO;
using Craftable.SharedKernel.exceptions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Craftable.Infrastructure.queries
{
    public class AddressRangedQueryHandler :
        IRequestHandlerAsync<AddressesQuery, IQueryResult<IReadOnlyList<PostcodeDTO>>>,
        IRequestHandlerAsync<PostalCodeQuery, IQueryResult<PostcodeAddressRangedDTO>>
    {
        private readonly IPostCodeFacade _postcodeFacade;
        private readonly DbSet<AddressRegister> _addressRangedContext;

        public AddressRangedQueryHandler(IPostCodeFacade postcodeFacade, CraftableContext context)
        {
            _postcodeFacade = postcodeFacade;
            _addressRangedContext = context.Addresses;
        }

        public async Task<IQueryResult<IReadOnlyList<PostcodeDTO>>> HandleAsync(AddressesQuery handler, CancellationToken cancellationToken)
        {
            var adresses = _addressRangedContext.AsQueryable();
            var lastAddresses = await adresses
                .Reverse()
                .Take(3)
                .Select(a => new PostcodeDTO
                {
                    Code = a.Postcode,
                    Date = a.Date
                }).ToListAsync(cancellationToken);

            var addressResult = new QueryResult<IReadOnlyList<PostcodeDTO>>(true, default, lastAddresses);

            return addressResult;
        }

        public async Task<IQueryResult<PostcodeAddressRangedDTO>> HandleAsync(PostalCodeQuery handler, CancellationToken cancellationToken)
        {
            var noticator = handler.ValidateEvent();
            if (!noticator.IsValid)
            {
                return new QueryResult<PostcodeAddressRangedDTO>(false, noticator.Errors, default);
            }

            var isPostalCodeValid = await _postcodeFacade.ValidatePostalCodeAsync(handler.Code, cancellationToken);
            if (!isPostalCodeValid)
            {
                throw new PostalCodeInvalidException();
            }

            var hasPostCode = await _addressRangedContext.AnyAsync(address => address.Postcode == handler.Code, cancellationToken);
            var code = handler.Code;
            var addressDTO = hasPostCode switch
            {
                true => await GetAddressFromRepository(code, cancellationToken),
                false => await GetAddressFromApi(code, cancellationToken)
            };

            var response = new QueryResult<PostcodeAddressRangedDTO>(true, default, addressDTO);
            return response;
        }

        private async Task<PostcodeAddressRangedDTO> GetAddressFromRepository(string code, CancellationToken cancellationToken)
        {
            var addressRegister = await _addressRangedContext.LastAsync(address => address.Postcode == code, cancellationToken);
            return new PostcodeAddressRangedDTO
            {
                Postcode = addressRegister.Postcode,
                Country = addressRegister.Country,
                Longitude = addressRegister.Coordinates.Longitude,
                Latitude = addressRegister.Coordinates.Latitude,
                DistanceFromHeathrowAirportInKilometers = addressRegister.Distance.DistanceInKilometer,
                DistanceFromHeathrowAirportInMiles = addressRegister.Distance.DistanceInMiles
            };
        }
        private async Task<PostcodeAddressRangedDTO> GetAddressFromApi(string code, CancellationToken cancellationToken)
        {
            var address = await _postcodeFacade.GetAddressByPostalCodeAsync(code, cancellationToken);

            var source = new Coordinates(address.Longitude, address.Latitude);
            var distance = await _postcodeFacade.GetDistanceFromCoordinatesAsync(source, HeathrowAirpot.Coordinates, cancellationToken);

            var addressDTO = new PostcodeAddressRangedDTO
            {
                Postcode = address.Postcode,
                Country = address.Country,
                Longitude = address.Longitude,
                Latitude = address.Latitude,
                DistanceFromHeathrowAirportInKilometers = distance.DistanceInKilometer,
                DistanceFromHeathrowAirportInMiles = distance.DistanceInMiles
            };
            return addressDTO;
        }
    }
}