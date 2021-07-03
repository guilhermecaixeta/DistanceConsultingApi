using Craftable.SharedKernel.DTO;
using Craftable.SharedKernel.exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;

namespace Craftable.Web.filters
{
    public class HttpResponseExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            var status = context.Exception switch
            {
                ArgumentNullException or
                PostalCodeInvalidException => StatusCodes.Status400BadRequest,
                _ => StatusCodes.Status500InternalServerError
            };
            var response = new ResultDTO<string>(false, new List<string> { context.Exception.Message }, context.Exception.Message);

            context.Result = new ObjectResult(response)
            {
                StatusCode = status,
            };

            context.ExceptionHandled = true;
        }
    }
}