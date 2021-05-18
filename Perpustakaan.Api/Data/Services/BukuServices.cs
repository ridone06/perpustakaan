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
    public class BukuServices : IBukuServices
    {
        private readonly ILogger _logger;
        private readonly IBukuRepository _repository;
        private readonly IPengarangRepository _pengarangRepository;
        private readonly IPenerbitRepository _penerbitRepository;

        public BukuServices(IBukuRepository repository,
            IPengarangRepository pengarangRepository,
            IPenerbitRepository penerbitRepository,
            ILogger<BukuServices> logger)
        {
            _repository = repository;
            _pengarangRepository = pengarangRepository;
            _penerbitRepository = penerbitRepository;
            _logger = logger;
        }

        public async Task<BukuDTO> Get(int id)
        {
            try
            {
                var result = (await _repository.Get(id)).ToModel();

                if (result != null)
                {
                    result.NamaPengarang = _pengarangRepository.Get(result.PengarangId).Result.Nama;
                    result.NamaPenerbit = _penerbitRepository.Get(result.PenerbitId).Result.Nama;
                }

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError($"error get Buku by id {id}", ex);
                throw new ExceptionExtension($"error get Buku by id {id}. detail ", ex);
            }
        }

        public async Task<List<BukuDTO>> GetAll()
        {
            try
            {
                var result = await _repository.GetAll().ToListAsync();

                return result.Select(s => new BukuDTO
                {
                    Id = s.Id,
                    PengarangId = s.PengarangId,
                    NamaPengarang = _pengarangRepository.Get(s.PengarangId).Result.Nama,
                    PenerbitId = s.PenerbitId,
                    NamaPenerbit = _penerbitRepository.Get(s.PenerbitId).Result.Nama,
                    Judul = s.Judul,
                    KodeRak = s.KodeRak,
                    TahunTerbit = s.TahunTerbit,
                    IsActive = s.IsActive,
                    CreatedAt = s.CreatedAt,
                    CreatedBy = s.CreatedBy,
                    UpdatedAt = s.UpdatedAt,
                    UpdatedBy = s.UpdatedBy
                }).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError("error get all Buku", ex);
                throw new ExceptionExtension("error get all Buku. detail", ex);
            }
        }

        public async Task<BukuDTO> Add(BukuDTO model)
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
                _logger.LogError("error create Buku.", ex);
                throw new ExceptionExtension("error create Buku.", ex);
            }
        }

        public async Task<BukuDTO> Update(BukuDTO model)
        {
            try
            {
                var entity = await _repository.Get(model.Id);

                entity.Judul = model.Judul;
                entity.KodeRak = model.KodeRak;
                entity.TahunTerbit = model.TahunTerbit;
                entity.PenerbitId = model.PenerbitId;
                entity.PengarangId = model.PengarangId;
                entity.IsActive = true;
                entity.UpdatedAt = DateTime.Now;
                entity.UpdatedBy = "admin";

                var result = await _repository.Update(entity);

                return result.ToModel();
            }
            catch (Exception ex)
            {
                _logger.LogError("error update Buku.", ex);
                throw new ExceptionExtension("error update Buku.", ex);
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
                _logger.LogError($"error delete Buku by id {Id}.", ex);
                throw new ExceptionExtension($"error delete Buku by id {Id}.", ex);
            }
        }
    }
}
