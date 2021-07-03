using Craftable.Infrastructure.client.postcode.models;
using Refit;
using System.Threading;
using System.Threading.Tasks;

namespace Craftable.Infrastructure.client.postcode.api
{
    public interface IPostCodeApi
    {
        [Get("/postcodes/{postalCode}")]
        Task<PostcodeResponse<PostcodeAddress>> GetAddressFromPostalCode(string postalCode, CancellationToken cancellationToken);

        [Get("/postcodes/{postalCode}/validate")]
        Task<PostcodeResponse<bool>> ValidatePostalCode(string postalCode, CancellationToken cancellationToken);
    }
}