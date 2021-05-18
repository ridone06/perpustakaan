using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Perpustakaan.Api.Data.Repository.Interfaces;
using Perpustakaan.Api.Data.Services.Interfaces;
using Perpustakaan.Api.Mappers;
using Perpustakaan.Api.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Perpustakaan.Api.Data.Services
{
    public class RakServices : IRakServices
    {
        private readonly ILogger _logger;
        private readonly IRakRepository _repository;

        public RakServices(IRakRepository repository,
            ILogger<RakServices> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<RakDTO> GetByKode(string kode)
        {
            try
            {
                var result = (await _repository.GetAll().Where(w => w.Kode == kode).FirstOrDefaultAsync()).ToModel();

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError($"error get Rak by kode {kode}", ex);
                throw new ExceptionExtension($"error get Rak by kode {kode}. detail ", ex);
            }
        }

        public async Task<List<RakDTO>> GetAll()
        {
            try
            {
                var result = await _repository.GetAll().Where(w => w.IsActive).ToListAsync();

                return result.ToModel(); ;
            }
            catch (Exception ex)
            {
                _logger.LogError("error get all Rak", ex);
                throw new ExceptionExtension("error get all Rak. detail", ex);
            }
        }

        public async Task<RakDTO> Add(RakDTO model)
        {
            try
            {
                var dataExist = await _repository.GetAll().Where(w => w.Kode == model.Kode).FirstOrDefaultAsync();

                if (dataExist != null)
                    return await Update(model);

                var entity = model.ToEntity();

                entity.IsActive = true;
                entity.CreatedAt = DateTime.Now;
                entity.CreatedBy = "admin";
                entity.UpdatedAt = DateTime.Now;
                entity.UpdatedBy = "admin";

                var result = await _repository.Add(entity);

                return result.ToModel();
            }
            catch (Exception ex)
            {
                _logger.LogError("error create Rak.", ex);
                throw new ExceptionExtension("error create Rak.", ex);
            }
        }

        public async Task<RakDTO> Update(RakDTO model)
        {
            try
            {
                var entity = await _repository.GetAll().Where(w => w.Kode == model.Kode).FirstOrDefaultAsync();

                if (entity == null)
                    throw new Exception($"Kode Rak {model.Kode} not found.");

                entity.Kode = model.Kode;
                entity.Lokasi = model.Lokasi;

                entity.IsActive = true;
                entity.UpdatedAt = DateTime.Now;
                entity.UpdatedBy = "admin";

                var result = await _repository.Update(entity);

                return result.ToModel();
            }
            catch (Exception ex)
            {
                _logger.LogError("error update Rak.", ex);
                throw new ExceptionExtension("error update Rak.", ex);
            }
        }

        public async Task<RakDTO> DeleteByKode(string kode)
        {
            try
            {
                var entity = await _repository.GetAll().Where(w => w.Kode == kode).FirstOrDefaultAsync();

                if (entity == null)
                    throw new Exception($"Kode Rak {kode} not found.");

                var result = await _repository.Delete(kode);

                return result.ToModel();
            }
            catch (Exception ex)
            {
                _logger.LogError($"error delete Rak by kode {kode}.", ex);
                throw new ExceptionExtension($"error delete Rak by kode {kode}.", ex);
            }
        }

        public Task<RakDTO> Get(int id)
        {
            throw new NotImplementedException();
        }

        public Task<int> Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
