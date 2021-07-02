using Craftable.Core.commands;
using Craftable.Core.interfaces.command;
using Craftable.Core.interfaces.queries;
using Craftable.Core.queries;
using Craftable.Web.DTO;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Craftable.Web.services
{
    public class PostcodeService : IPostcodeService
    {
        private const string DATE_FORMAT = "MM/dd/yyyy HH:mm:ss";

        private readonly IGetDistanceFromPostCodeUseCaseAsync _postcodeUseCase;
        private readonly IPostalcodeListQueryHandler _postalcodeListQueryHandler;

        public PostcodeService(
            IGetDistanceFromPostCodeUseCaseAsync postcodeUseCase,
            IPostalcodeListQueryHandler postalcodeListQueryHandler)
        {
            _postcodeUseCase = postcodeUseCase;
            _postalcodeListQueryHandler = postalcodeListQueryHandler;
        }

        public async Task<ResponseDTO<PostcodeDistanceDTO>> SaveDistanceFromPostCode(string code, CancellationToken cancellationToken)
        {
            var command = new PostcodeCommand { Postcode = code };
            var result = await _postcodeUseCase.HandlerAsync(command, cancellationToken);
            if (!result.Success)
            {
                return GetErrorResponse<PostcodeDistanceDTO>(result.Errors);
            }
            var postcode = result.Data;
            var data = new PostcodeDistanceDTO
            {
                PostalCode = postcode.Code,
                DistanceInKilometer = postcode.DistanceFromHeathrowAirportInKilometer,
                DistanceInMiles = postcode.DistanceFromHeathrowAirportInMiles
            };

            var ResponseDTO = GetSuccessResponse(data, HttpStatusCode.Created);

            return ResponseDTO;
        }

        public async Task<ResponseDTO<IReadOnlyList<PostalcodeDTO>>> GetPostcodeList(CancellationToken cancellationToken)
        {
            var query = new AddressesQuery();
            var queryResult = await _postalcodeListQueryHandler.HandlerAsync(query, cancellationToken);

            if (!queryResult.Success)
            {
                return GetErrorResponse<IReadOnlyList<PostalcodeDTO>>(queryResult.Errors);
            }

            var addresses = queryResult.Data.Select(address => new PostalcodeDTO
            {
                Code = address.Code,
                Date = address.Date.ToString(DATE_FORMAT, CultureInfo.InvariantCulture)
            });

            if (!addresses.Any())
            {
                return GetSuccessResponse<IReadOnlyList<PostalcodeDTO>>(default, HttpStatusCode.NoContent);
            }

            return GetSuccessResponse<IReadOnlyList<PostalcodeDTO>>(addresses.ToList(), HttpStatusCode.OK);
        }

        private static ResponseDTO<T> GetErrorResponse<T>(IReadOnlyList<string> errors) where T : class =>
            new ResponseDTO<T>
            {
                Data = default,
                Success = false,
                StatusCode = HttpStatusCode.BadRequest,
                Errors = errors
            };

        private static ResponseDTO<T> GetSuccessResponse<T>(T data, HttpStatusCode statusCode = HttpStatusCode.OK) where T : class =>
            new ResponseDTO<T>
            {
                Data = data,
                Success = true,
                StatusCode = statusCode,
                Errors = default
            };
    }
}