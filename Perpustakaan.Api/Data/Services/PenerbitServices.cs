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
    public class PenerbitServices : IPenerbitServices
    {
        private readonly ILogger _logger;
        private readonly IPenerbitRepository _repository;

        public PenerbitServices(IPenerbitRepository repository,
            ILogger<PenerbitServices> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<PenerbitDTO> Get(int id)
        {
            try
            {
                var result = (await _repository.Get(id)).ToModel();

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError($"error get Penerbit by id {id}", ex);
                throw new ExceptionExtension($"error get Penerbit by id {id}. detail ", ex);
            }
        }

        public async Task<List<PenerbitDTO>> GetAll()
        {
            try
            {
                var result = await _repository.GetAll().ToListAsync();

                return result.ToModel(); ;
            }                                              
            catch (Exception ex)
            {
                _logger.LogError("error get all Penerbit", ex);
                throw new ExceptionExtension("error get all Penerbit. detail", ex);
            }
        }

        public async Task<PenerbitDTO> Add(PenerbitDTO model)
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
                _logger.LogError("error create Penerbit.", ex);
                throw new ExceptionExtension("error create Penerbit.", ex);
            }
        }

        public async Task<PenerbitDTO> Update(PenerbitDTO model)
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
                _logger.LogError("error update Penerbit.", ex);
                throw new ExceptionExtension("error update Penerbit.", ex);
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
                _logger.LogError($"error delete Penerbit by id {Id}.", ex);
                throw new ExceptionExtension($"error delete Penerbit by id {Id}.", ex);
            }
        }
    }
}
