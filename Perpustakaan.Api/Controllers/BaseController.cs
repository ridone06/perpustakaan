using AutoWrapper.Wrappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SerilogTimings;
using System;
using System.Threading.Tasks;

namespace Perpustakaan.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ApiException), StatusCodes.Status400BadRequest)]
    public class BaseController : ControllerBase
    {
        protected readonly ILogger logger;

        public BaseController(ILogger _logger)
        {
            logger = _logger;
        }

        protected async Task<IActionResult> ApiResult<TResult>(Func<Task<TResult>> method, string logMsg = "")
        {
            try
            {
                using (var op = Operation.Begin(logMsg))
                {
                    var data = await method();
                    op.Complete();

                    logger.LogDebug($"{logMsg} data : {data}");

                    return Ok(data);
                }
            }
            catch (Exception ex)
            {
                logger.LogError(logMsg, ex);
                throw new ApiException(ex, StatusCodes.Status400BadRequest);
            }
        }
    }
}
