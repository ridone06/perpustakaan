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
    [Route("api/peminjaman")]
    [ApiExplorerSettings(GroupName = "Peminjaman")]
    public class PeminjamanController : BaseController
    {
        private readonly IPeminjamanServices _services;

        public PeminjamanController(ILogger<PeminjamanController> logger,
          IPeminjamanServices services) : base(logger)
        {
            _services = services;
        }

        /// <summary>
        ///  Get all Peminjaman
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(List<PeminjamanDTO>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get()
        {
            return await ApiResult(async () => await _services.GetAll(), "Peminjaman");
        }

        /// <summary>
        /// Get a specific Peminjaman
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("{id}")]
        [HttpGet]
        [ProducesResponseType(typeof(PeminjamanDTO), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            return await ApiResult(async () => await _services.Get(id), "Peminjaman");
        }

        /// <summary>
        /// Create Peminjaman
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST api/peminjaman
        ///     {
        ///        "TanggalPinjam": "2021-05-17",
        ///        "TanggalKembali": "2021-05-20",
        ///        "AnggotaId": 1,
        ///        "PetugasId": 1,
        ///        "Details": [{ BukuId: 1 }, { BukuId: 2 }]
        ///     }
        ///
        /// </remarks>
        [HttpPost]
        [ProducesResponseType(typeof(PeminjamanDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiProblemDetailsException), StatusCodes.Status422UnprocessableEntity)]
        public async Task<IActionResult> Post([FromBody] PeminjamanDTO model)
        {
            if (!ModelState.IsValid)
                throw new ApiProblemDetailsException(ModelState);

            return await ApiResult(async () => await _services.Add(model), "Peminjaman");
        }

        /// <summary>
        /// Update Peminjaman
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT api/peminjaman/1
        ///     {
        ///        "TanggalPinjam": "2021-05-17",
        ///        "TanggalKembali": "2021-05-20",
        ///        "AnggotaId": 1,
        ///        "PetugasId": 1,
        ///        "Details": [{ BukuId: 2 }, { BukuId: 3 }]
        ///     }
        ///
        /// </remarks>
        [Route("{id}")]
        [HttpPut]
        [ProducesResponseType(typeof(PeminjamanDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiProblemDetailsException), StatusCodes.Status422UnprocessableEntity)]
        public async Task<IActionResult> Put([FromRoute] int id, [FromBody] PeminjamanDTO model)
        {
            if (!ModelState.IsValid)
                throw new ApiProblemDetailsException(ModelState);

            model.Id = id;

            return await ApiResult(async () => await _services.Update(model), "Peminjaman");
        }

        /// <summary>
        /// Delete Peminjaman
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("{id}")]
        [HttpDelete]
        [ProducesResponseType(typeof(PeminjamanDTO), StatusCodes.Status200OK)]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            return await ApiResult(async () => await _services.Delete(id), "Peminjaman");
        }
    }
}
