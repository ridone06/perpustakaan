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
    [Route("api/buku")]
    [ApiExplorerSettings(GroupName = "Buku")]
    public class BukuController : BaseController
    {
        private readonly IBukuServices _services;

        public BukuController(ILogger<BukuController> logger,
          IBukuServices services) : base(logger)
        {
            _services = services;
        }

        /// <summary>
        ///  Get all Buku
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(List<BukuDTO>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get()
        {
            return await ApiResult(async () => await _services.GetAll(), "Buku");
        }

        /// <summary>
        /// Get a specific Buku
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("{id}")]
        [HttpGet]
        [ProducesResponseType(typeof(BukuDTO), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            return await ApiResult(async () => await _services.Get(id), "Buku");
        }

        [HttpPost]
        [ProducesResponseType(typeof(BukuDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiProblemDetailsException), StatusCodes.Status422UnprocessableEntity)]
        public async Task<IActionResult> Post([FromBody] BukuDTO model)
        {
            if (!ModelState.IsValid)
                throw new ApiProblemDetailsException(ModelState);

            return await ApiResult(async () => await _services.Add(model), "Buku");
        }

        [Route("{id}")]
        [HttpPut]
        [ProducesResponseType(typeof(BukuDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiProblemDetailsException), StatusCodes.Status422UnprocessableEntity)]
        public async Task<IActionResult> Put([FromRoute] int id, [FromBody] BukuDTO model)
        {
            if (!ModelState.IsValid)
                throw new ApiProblemDetailsException(ModelState);

            model.Id = id;

            return await ApiResult(async () => await _services.Update(model), "Buku");
        }

        /// <summary>
        /// Delete Buku
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("{id}")]
        [HttpDelete]
        [ProducesResponseType(typeof(BukuDTO), StatusCodes.Status200OK)]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            return await ApiResult(async () => await _services.Delete(id), "Buku");
        }
    }
}
