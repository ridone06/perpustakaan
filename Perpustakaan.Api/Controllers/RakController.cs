using AutoWrapper.Wrappers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Perpustakaan.Api.Data.Services.Interfaces;
using Perpustakaan.Api.Models.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Perpustakaan.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/rak")]
    [ApiExplorerSettings(GroupName = "Rak")]
    public class RakController : BaseController
    {
        private readonly IRakServices _services;

        public RakController(ILogger<RakController> logger,
          IRakServices services) : base(logger)
        {
            _services = services;
        }

        /// <summary>
        ///  Get all Rak
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(List<RakDTO>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get()
        {
            return await ApiResult(async () => await _services.GetAll(), "Rak");
        }

        /// <summary>
        /// Get a specific Rak
        /// </summary>
        /// <param name="kode"></param>
        /// <returns></returns>
        [Route("{kode}")]
        [HttpGet]
        [ProducesResponseType(typeof(RakDTO), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get([FromRoute] string kode)
        {
            return await ApiResult(async () => await _services.GetByKode(kode), "Rak");
        }

        [HttpPost]
        [ProducesResponseType(typeof(RakDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiProblemDetailsException), StatusCodes.Status422UnprocessableEntity)]
        public async Task<IActionResult> Post([FromBody] RakDTO model)
        {
            if (!ModelState.IsValid)
                throw new ApiProblemDetailsException(ModelState);

            return await ApiResult(async () => await _services.Add(model), "Rak");
        }

        [Route("{kode}")]
        [HttpPut]
        [ProducesResponseType(typeof(RakDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiProblemDetailsException), StatusCodes.Status422UnprocessableEntity)]
        public async Task<IActionResult> Put([FromRoute] string kode, [FromBody] RakDTO model)
        {
            if (!ModelState.IsValid)
                throw new ApiProblemDetailsException(ModelState);

            model.Kode = kode;

            return await ApiResult(async () => await _services.Update(model), "Rak");
        }

        /// <summary>
        /// Delete Rak
        /// </summary>
        /// <param name="kode"></param>
        /// <returns></returns>
        [Route("{kode}")]
        [HttpDelete]
        [ProducesResponseType(typeof(RakDTO), StatusCodes.Status200OK)]
        public async Task<IActionResult> Delete([FromRoute] string kode)
        {
            return await ApiResult(async () => await _services.DeleteByKode(kode), "Rak");
        }
    }
}
