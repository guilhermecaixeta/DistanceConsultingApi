using Craftable.Core.interfaces.CQRS.queries;
using Craftable.Core.interfaces.services;
using Craftable.Core.queries;
using Craftable.Web.DTO;
using Craftable.Web.services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Craftable.Web.Controllers
{
    [Route("api/post-code")]
    [ApiController]
    public class PostCodeController : ControllerBase
    {
        private readonly IAddressMapperService _addressMapperService;
        private readonly IPostcodeServiceAsync _postcodeServiceAsync;
        private readonly IPostalcodeListQueryHandler _postalcodeListQueryHandler;

        public PostCodeController(
            IAddressMapperService addressMapperService,
            IPostcodeServiceAsync postcodeServiceAsync,
            IPostalcodeListQueryHandler postalcodeListQueryHandler)
        {
            _addressMapperService = addressMapperService;
            _postcodeServiceAsync = postcodeServiceAsync;
            _postalcodeListQueryHandler = postalcodeListQueryHandler;
        }

        [HttpPost]
        [ProducesResponseType(typeof(ResponseDTO<PostcodeDistanceDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseDTO<PostcodeDistanceDTO>), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ResponseDTO<PostcodeDistanceDTO>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAddress([FromBody] PostcodeRequestDTO request, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var resultDTO = await _postcodeServiceAsync.GetPostcodeRangedAsync(request.Code, cancellationToken);
            var response = _addressMapperService.MappingPostcode(resultDTO);
            return ValidateResponse(response, nameof(GetLastPostcodesHistoric));
        }

        [HttpGet]
        [ProducesResponseType(typeof(ResponseDTO<IReadOnlyList<PostalcodeDTO>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseDTO<IReadOnlyList<PostalcodeDTO>>), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ResponseDTO<IReadOnlyList<PostalcodeDTO>>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetLastPostcodesHistoric(CancellationToken cancellationToken)
        {
            var query = new AddressesQuery();
            var queryResult = await _postalcodeListQueryHandler.HandleAsync(query, cancellationToken);
            var response = _addressMapperService.MappingPostcodeList(queryResult);
            return ValidateResponse(response);
        }

        private IActionResult ValidateResponse<T>(ResponseDTO<T> response, string actionNameAttribute = null) where T : class =>
            response.StatusCode switch
            {
                HttpStatusCode.Created => CreatedAtAction(actionNameAttribute, response),
                HttpStatusCode.BadRequest => BadRequest(response),
                HttpStatusCode.NoContent => NoContent(),
                HttpStatusCode.OK => Ok(response),
                _ => StatusCode((int)HttpStatusCode.InternalServerError, default)
            };

    }
}