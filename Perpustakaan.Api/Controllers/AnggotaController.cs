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
    [Route("api/anggota")]
    [ApiExplorerSettings(GroupName = "Anggota")]
    public class AnggotaController : BaseController
    {
        private readonly IAnggotaServices _services;

        public AnggotaController(ILogger<AnggotaController> logger,
          IAnggotaServices services) : base(logger)
        {
            _services = services;
        }

        /// <summary>
        ///  Get all Anggota
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(List<AnggotaDTO>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get()
        {
            return await ApiResult(async () => await _services.GetAll(), "Anggota");
        }

        /// <summary>
        /// Get a specific Anggota
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("{id}")]
        [HttpGet]
        [ProducesResponseType(typeof(AnggotaDTO), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            return await ApiResult(async () => await _services.Get(id), "Anggota");
        }

        [HttpPost]
        [ProducesResponseType(typeof(AnggotaDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiProblemDetailsException), StatusCodes.Status422UnprocessableEntity)]
        public async Task<IActionResult> Post([FromBody] AnggotaDTO model)
        {
            if (!ModelState.IsValid)
                throw new ApiProblemDetailsException(ModelState);

            return await ApiResult(async () => await _services.Add(model), "Anggota");
        }

        [Route("{id}")]
        [HttpPut]
        [ProducesResponseType(typeof(AnggotaDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiProblemDetailsException), StatusCodes.Status422UnprocessableEntity)]
        public async Task<IActionResult> Put([FromRoute] int id, [FromBody] AnggotaDTO model)
        {
            if (!ModelState.IsValid)
                throw new ApiProblemDetailsException(ModelState);

            model.Id = id;

            return await ApiResult(async () => await _services.Update(model), "Anggota");
        }

        /// <summary>
        /// Delete Anggota
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("{id}")]
        [HttpDelete]
        [ProducesResponseType(typeof(AnggotaDTO), StatusCodes.Status200OK)]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            return await ApiResult(async () => await _services.Delete(id), "Anggota");
        }
    }
}
