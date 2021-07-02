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
        private readonly IPostcodeService _addressService;

        public PostCodeController(
            IPostcodeService addressService)
        {
            _addressService = addressService;
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

            var response = await _addressService.SaveDistanceFromPostCode(request.Code, cancellationToken);
            return ValidateResponse(response, nameof(GetLastPostcodesHistoric));
        }

        [HttpGet]
        [ProducesResponseType(typeof(ResponseDTO<IReadOnlyList<PostalcodeDTO>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseDTO<IReadOnlyList<PostalcodeDTO>>), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ResponseDTO<IReadOnlyList<PostalcodeDTO>>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetLastPostcodesHistoric(CancellationToken cancellationToken)
        {
            
            var response = await _addressService.GetPostcodeList(cancellationToken);
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