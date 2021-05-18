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
    [Route("api/pengembalian")]
    [ApiExplorerSettings(GroupName = "Pengembalian")]
    public class PengembalianController : BaseController
    {
        private readonly IPengembalianServices _services;

        public PengembalianController(ILogger<PengembalianController> logger,
          IPengembalianServices services) : base(logger)
        {
            _services = services;
        }

        /// <summary>
        ///  Get all Pengembalian
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(List<PengembalianDTO>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get()
        {
            return await ApiResult(async () => await _services.GetAll(), "Pengembalian");
        }

        /// <summary>
        /// Get a specific Pengembalian
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("{id}")]
        [HttpGet]
        [ProducesResponseType(typeof(PengembalianDTO), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            return await ApiResult(async () => await _services.Get(id), "Pengembalian");
        }

        /// <summary>
        /// Create Pengembalian
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST api/Pengembalian
        ///     {
        ///        "PeminjamanId": 1,
        ///        "TanggalPengembalian": "2021-05-20",
        ///        "Denda": 20000,
        ///        "AnggotaId": 1,
        ///        "PetugasId": 1,
        ///        "Details": [{ BukuId: 2 }]
        ///     }
        ///
        /// </remarks>
        [HttpPost]
        [ProducesResponseType(typeof(PengembalianDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiProblemDetailsException), StatusCodes.Status422UnprocessableEntity)]
        public async Task<IActionResult> Post([FromBody] PengembalianDTO model)
        {
            if (!ModelState.IsValid)
                throw new ApiProblemDetailsException(ModelState);

            return await ApiResult(async () => await _services.Add(model), "Pengembalian");
        }

        /// <summary>
        /// Update Pengembalian
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT api/Pengembalian/1
        ///     {
        ///        "PeminjamanId": 1,
        ///        "TanggalPengembalian": "2021-05-22",
        ///        "Denda": 25000,
        ///        "AnggotaId": 1,
        ///        "PetugasId": 1,
        ///        "Details": [{ BukuId: 3 }]
        ///     }
        ///
        /// </remarks>
        [Route("{id}")]
        [HttpPut]
        [ProducesResponseType(typeof(PengembalianDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiProblemDetailsException), StatusCodes.Status422UnprocessableEntity)]
        public async Task<IActionResult> Put([FromRoute] int id, [FromBody] PengembalianDTO model)
        {
            if (!ModelState.IsValid)
                throw new ApiProblemDetailsException(ModelState);

            model.Id = id;

            return await ApiResult(async () => await _services.Update(model), "Pengembalian");
        }

        /// <summary>
        /// Delete Pengembalian
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("{id}")]
        [HttpDelete]
        [ProducesResponseType(typeof(PengembalianDTO), StatusCodes.Status200OK)]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            return await ApiResult(async () => await _services.Delete(id), "Pengembalian");
        }
    }
}
