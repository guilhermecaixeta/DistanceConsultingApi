using Craftable.Core.valueObjects;
using Craftable.Infrastructure.client.postcode.api;
using Craftable.Infrastructure.client.postcode.models;
using Craftable.SharedKernel.exceptions;
using Geolocation;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Craftable.Infrastructure.client.postcode
{
    public class PostCodeClient : IPostCodeClient
    {
        private readonly IPostCodeApi _postCodeApi;

        public PostCodeClient(IPostCodeApi postCodeApi)
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
                throw new Exception($"Error to retrieve the data \n Status Code: {response.Status} \n Message: {response.Result}");
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
