using Craftable.Core.valueObjects;
using Craftable.Infrastructure.models;
using Craftable.Infrastructure.refitClients;
using Craftable.SharedKernel.exceptions;
using Geolocation;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Craftable.Infrastructure.facade
{
    public class PostCodeFacade : IPostCodeFacade
    {
        private readonly IPostCodeApi _postCodeApi;

        public PostCodeFacade(IPostCodeApi postCodeApi)
        {
            _postCodeApi = postCodeApi;
        }

        public async Task<PostcodeAddress> GetAddressByPostalCodeAsync(string postalCode, CancellationToken cancellationToken)
        {
            IsPostalCodeValid(postalCode);

            var response = await _postCodeApi.GetAddressFromPostalCode(postalCode, cancellationToken);
            IsResponseValid(response);

            var address = response.Result;

            return address;
        }

        public async Task<bool> ValidatePostalCodeAsync(string postalCode, CancellationToken cancellationToken)
        {
            IsPostalCodeValid(postalCode);
            var response = await _postCodeApi.ValidatePostalCode(postalCode, cancellationToken);
            IsResponseValid(response);
            return response.Result;
        }

        public Task<Distance> GetDistanceFromCoordinatesAsync(Coordinates source, Coordinates destination, CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                var distanceInKm = GeoCalculator.GetDistance(source.Latitude, source.Longitude, destination.Latitude, destination.Longitude, 1, DistanceUnit.Kilometers);
                var distanceInMl = GeoCalculator.GetDistance(source.Latitude, source.Longitude, destination.Latitude, destination.Longitude);

                return new Distance(distanceInKm, distanceInMl);
            }, cancellationToken);
        }

        private static void IsResponseValid<T>(PostcodeResponse<T> response)
        {
            if (response.Status != 200)
            {
                throw new Exception("Error to retrieve the data");
            }
        }

        private static void IsPostalCodeValid(string postalCode)
        {
            if (string.IsNullOrEmpty(postalCode))
            {
                throw new PostalCodeInvalidException();
            }
        }
    }
}
