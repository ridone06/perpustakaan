using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Perpustakaan.Api.Data.Repository.Interfaces;
using Perpustakaan.Api.Data.Services.Interfaces;
using Perpustakaan.Api.Mappers;
using Perpustakaan.Api.Models.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Perpustakaan.Api.Data.Services
{
    public class PengarangServices : IPengarangServices
    {
        private readonly ILogger _logger;
        private readonly IPengarangRepository _repository;

        public PengarangServices(IPengarangRepository repository,
            ILogger<PengarangServices> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<PengarangDTO> Get(int id)
        {
            try
            {
                var result = (await _repository.Get(id)).ToModel();

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError($"error get Pengarang by id {id}", ex);
                throw new ExceptionExtension($"error get Pengarang by id {id}. detail ", ex);
            }
        }

        public async Task<List<PengarangDTO>> GetAll()
        {
            try
            {
                var result = await _repository.GetAll().ToListAsync();

                return result.ToModel(); ;
            }                                              
            catch (Exception ex)
            {
                _logger.LogError("error get all Pengarang", ex);
                throw new ExceptionExtension("error get all Pengarang. detail", ex);
            }
        }

        public async Task<PengarangDTO> Add(PengarangDTO model)
        {
            try
            {
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
                _logger.LogError("error create Pengarang.", ex);
                throw new ExceptionExtension("error create Pengarang.", ex);
            }
        }

        public async Task<PengarangDTO> Update(PengarangDTO model)
        {
            try
            {
                var entity = await _repository.Get(model.Id);

                entity.Alamat = model.Alamat;
                entity.Nama = model.Nama;
                entity.NoTlp = model.NoTlp;
                
                entity.IsActive = true;
                entity.UpdatedAt = DateTime.Now;
                entity.UpdatedBy = "admin";

                var result = await _repository.Update(entity);

                return result.ToModel();
            }
            catch (Exception ex)
            {
                _logger.LogError("error update Pengarang.", ex);
                throw new ExceptionExtension("error update Pengarang.", ex);
            }
        }

        public async Task<int> Delete(int Id)
        {
            try
            {
                var result = await _repository.Delete(Id);

                return result.Id;
            }
            catch (Exception ex)
            {
                _logger.LogError($"error delete Pengarang by id {Id}.", ex);
                throw new ExceptionExtension($"error delete Pengarang by id {Id}.", ex);
            }
        }
    }
}
