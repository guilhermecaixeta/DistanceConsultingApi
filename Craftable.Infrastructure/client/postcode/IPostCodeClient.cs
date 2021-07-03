using Craftable.Core.valueObjects;
using Craftable.Infrastructure.client.postcode.models;
using System.Threading;
using System.Threading.Tasks;

namespace Craftable.Infrastructure.client.postcode
{
    public interface IPostCodeClient
    {
        Task<Distance> GetDistanceFromCoordinatesAsync(Coordinates source, Coordinates destination, CancellationToken cancellationToken);

        Task<PostcodeAddress> GetAddressByPostalCodeAsync(string postalCode, CancellationToken cancellationToken);

        Task<bool> ValidatePostalCodeAsync(string postalCode, CancellationToken cancellationToken);
    }
}
