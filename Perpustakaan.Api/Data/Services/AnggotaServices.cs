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
    public class AnggotaServices : IAnggotaServices
    {
        private readonly ILogger _logger;
        private readonly IAnggotaRepository _repository;

        public AnggotaServices(IAnggotaRepository repository,
            ILogger<AnggotaServices> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<AnggotaDTO> Get(int id)
        {
            try
            {
                var result = (await _repository.Get(id)).ToModel();

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError($"error get Anggota by id {id}", ex);
                throw new ExceptionExtension($"error get Anggota by id {id}. detail ", ex);
            }
        }

        public async Task<List<AnggotaDTO>> GetAll()
        {
            try
            {
                var result = await _repository.GetAll().ToListAsync();

                return result.ToModel(); ;
            }                                              
            catch (Exception ex)
            {
                _logger.LogError("error get all Anggota", ex);
                throw new ExceptionExtension("error get all Anggota. detail", ex);
            }
        }

        public async Task<AnggotaDTO> Add(AnggotaDTO model)
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
                _logger.LogError("error create Anggota.", ex);
                throw new ExceptionExtension("error create Anggota.", ex);
            }
        }

        public async Task<AnggotaDTO> Update(AnggotaDTO model)
        {
            try
            {
                var entity = await _repository.Get(model.Id);

                entity.Alamat = model.Alamat;
                entity.Email = model.Email;
                entity.JenisKelamin = model.JenisKelamin;
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
                _logger.LogError("error update Anggota.", ex);
                throw new ExceptionExtension("error update Anggota.", ex);
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
                _logger.LogError($"error delete Anggota by id {Id}.", ex);
                throw new ExceptionExtension($"error delete Anggota by id {Id}.", ex);
            }
        }
    }
}
