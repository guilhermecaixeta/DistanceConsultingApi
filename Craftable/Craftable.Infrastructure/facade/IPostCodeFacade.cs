using Craftable.Core.valueObjects;
using Craftable.Infrastructure.models;
using System.Threading;
using System.Threading.Tasks;

namespace Craftable.Infrastructure.facade
{
    public interface IPostCodeFacade
    {
        Task<Distance> GetDistanceFromCoordinatesAsync(Coordinates source, Coordinates destination, CancellationToken cancellationToken);

        Task<PostcodeAddress> GetAddressByPostalCodeAsync(string postalCode, CancellationToken cancellationToken);

        Task<bool> ValidatePostalCodeAsync(string postalCode, CancellationToken cancellationToken);
    }
}
