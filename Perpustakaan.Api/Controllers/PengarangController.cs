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
    [Route("api/pengarang")]
    [ApiExplorerSettings(GroupName = "Pengarang")]
    public class PengarangController : BaseController
    {
        private readonly IPengarangServices _services;

        public PengarangController(ILogger<PengarangController> logger,
          IPengarangServices services) : base(logger)
        {
            _services = services;
        }

        /// <summary>
        ///  Get all Pengarang
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(List<PengarangDTO>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get()
        {
            return await ApiResult(async () => await _services.GetAll(), "Pengarang");
        }

        /// <summary>
        /// Get a specific Pengarang
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("{id}")]
        [HttpGet]
        [ProducesResponseType(typeof(PengarangDTO), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            return await ApiResult(async () => await _services.Get(id), "Pengarang");
        }

        [HttpPost]
        [ProducesResponseType(typeof(PengarangDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiProblemDetailsException), StatusCodes.Status422UnprocessableEntity)]
        public async Task<IActionResult> Post([FromBody] PengarangDTO model)
        {
            if (!ModelState.IsValid)
                throw new ApiProblemDetailsException(ModelState);

            return await ApiResult(async () => await _services.Add(model), "Pengarang");
        }

        [Route("{id}")]
        [HttpPut]
        [ProducesResponseType(typeof(PengarangDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiProblemDetailsException), StatusCodes.Status422UnprocessableEntity)]
        public async Task<IActionResult> Put([FromRoute] int id, [FromBody] PengarangDTO model)
        {
            if (!ModelState.IsValid)
                throw new ApiProblemDetailsException(ModelState);

            model.Id = id;

            return await ApiResult(async () => await _services.Update(model), "Pengarang");
        }

        /// <summary>
        /// Delete Pengarang
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("{id}")]
        [HttpDelete]
        [ProducesResponseType(typeof(PengarangDTO), StatusCodes.Status200OK)]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            return await ApiResult(async () => await _services.Delete(id), "Pengarang");
        }
    }
}
