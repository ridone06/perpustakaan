using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Perpustakaan.Api.Data.Repository.Interfaces;
using Perpustakaan.Api.Data.Services.Interfaces;
using Perpustakaan.Api.Mappers;
using Perpustakaan.Api.Models.DTO;
using Perpustakaan.Api.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;

namespace Perpustakaan.Api.Data.Services
{
    public class PengembalianServices : IPengembalianServices
    {
        private readonly ILogger _logger;
        private readonly IPengembalianRepository _repository;
        private readonly IPengembalianDetailRepository _detailRepository;
        private readonly IAnggotaRepository _anggotaRepository;
        private readonly IPetugasRepository _petugasRepository;
        private readonly IBukuRepository _bukuRepository;

        public PengembalianServices(IPengembalianRepository repository,
            IPengembalianDetailRepository detailRepository,
            IAnggotaRepository anggotaRepository,
            IPetugasRepository petugasRepository,
            IBukuRepository bukuRepository,
            ILogger<PengembalianServices> logger)
        {
            _repository = repository;
            _detailRepository = detailRepository;
            _anggotaRepository = anggotaRepository;
            _petugasRepository = petugasRepository;
            _bukuRepository = bukuRepository;
            _logger = logger;
        }

        public async Task<PengembalianDTO> Get(int id)
        {
            try
            {
                var quaryDetail = _detailRepository.GetAll();
                var quaryBuku = _bukuRepository.GetAll();

                var result = (await _repository.GetAll().Where(w => w.PeminjamanId == id).FirstOrDefaultAsync()).ToModel();

                if (result != null)
                {
                    result.NamaAnggota = _anggotaRepository.Get(result.AnggotaId).Result.Nama;
                    result.NamaPetugas = _petugasRepository.Get(result.PetugasId).Result.Nama;
                    result.Details = (
                                    from p in quaryDetail.Where(w => w.PengembalianId == result.Id)
                                    join q in quaryBuku on p.BukuId equals q.Id
                                    select new PengembalianDetailDTO
                                    {
                                        PengembalianId = p.PengembalianId,
                                        BukuId = p.BukuId,
                                        JudulBuku = q.Judul
                                    }
                                  ).ToList();
                }

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError($"error get Pengembalian by id {id}", ex);
                throw new ExceptionExtension($"error get Pengembalian by id {id}. detail ", ex);
            }
        }

        public async Task<List<PengembalianDTO>> GetAll()
        {
            try
            {
                var quaryDetail = _detailRepository.GetAll();
                var quaryBuku = _bukuRepository.GetAll();

                var result = await _repository.GetAll().ToListAsync();

                return result.Select(s => new PengembalianDTO
                {
                    Id = s.Id,
                    AnggotaId = s.AnggotaId,
                    NamaAnggota = _anggotaRepository.Get(s.AnggotaId).Result.Nama,
                    PetugasId = s.PetugasId,
                    NamaPetugas = _petugasRepository.Get(s.PetugasId).Result.Nama,
                    TanggalPengembalian = s.TanggalPengembalian,
                    PeminjamanId = s.PeminjamanId,
                    Details = (
                                from p in quaryDetail.Where(w => w.PengembalianId == s.Id)
                                join q in quaryBuku on p.BukuId equals q.Id
                                select new PengembalianDetailDTO
                                {
                                    PengembalianId = p.PengembalianId,
                                    BukuId = p.BukuId,
                                    JudulBuku = q.Judul
                                }
                              ).ToList(),
                    IsActive = s.IsActive,
                    CreatedAt = s.CreatedAt,
                    CreatedBy = s.CreatedBy,
                    UpdatedAt = s.UpdatedAt,
                    UpdatedBy = s.UpdatedBy
                }).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError("error get all Pengembalian", ex);
                throw new ExceptionExtension("error get all Pengembalian. detail", ex);
            }
        }

        public async Task<PengembalianDTO> Add(PengembalianDTO model)
        {
            try
            {
                using (var scope = new TransactionScope(TransactionScopeOption.Required,
                    TimeSpan.FromMinutes(1),
                    TransactionScopeAsyncFlowOption.Enabled))
                {
                    var entity = model.ToEntity();

                    entity.IsActive = true;
                    entity.CreatedAt = DateTime.Now;
                    entity.CreatedBy = "admin";
                    entity.UpdatedAt = DateTime.Now;
                    entity.UpdatedBy = "admin";

                    var result = await _repository.Add(entity);

                    if (model.Details != null)
                    {
                        foreach (var detail in model.Details)
                        {
                            await _detailRepository.Add(new PengembalianDetail
                            {
                                BukuId = detail.BukuId,
                                PengembalianId = result.Id
                            });
                        }
                    }

                    scope.Complete();

                    return result.ToModel();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("error create Pengembalian.", ex);
                throw new ExceptionExtension("error create Pengembalian.", ex);
            }
        }

        public async Task<PengembalianDTO> Update(PengembalianDTO model)
        {
            try
            {
                using (var scope = new TransactionScope(TransactionScopeOption.Required,
                    TimeSpan.FromMinutes(1),
                    TransactionScopeAsyncFlowOption.Enabled))
                {
                    var entity = await _repository.Get(model.Id);

                    entity.AnggotaId = model.AnggotaId;
                    entity.PetugasId = model.PetugasId;
                    entity.PeminjamanId = model.PeminjamanId;
                    entity.TanggalPengembalian = model.TanggalPengembalian;
                    entity.IsActive = true;
                    entity.UpdatedAt = DateTime.Now;
                    entity.UpdatedBy = "admin";

                    var result = await _repository.Update(entity);

                    await _detailRepository.Delete(result.Id);

                    if (model.Details != null)
                    {
                        foreach (var detail in model.Details)
                        {
                            await _detailRepository.Add(new PengembalianDetail
                            {
                                BukuId = detail.BukuId,
                                PengembalianId = result.Id
                            });
                        }
                    }

                    scope.Complete();

                    return result.ToModel();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("error update Pengembalian.", ex);
                throw new ExceptionExtension("error update Pengembalian.", ex);
            }
        }

        public async Task<int> Delete(int Id)
        {
            try
            {
                using (var scope = new TransactionScope(TransactionScopeOption.Required,
                   TimeSpan.FromMinutes(1),
                   TransactionScopeAsyncFlowOption.Enabled))
                {
                    var result = await _repository.Delete(Id);
                    await _detailRepository.Delete(Id);

                    scope.Complete();

                    return result.Id;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"error delete Pengembalian by id {Id}.", ex);
                throw new ExceptionExtension($"error delete Pengembalian by id {Id}.", ex);
            }
        }
    }
}
