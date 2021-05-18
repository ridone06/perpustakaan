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
    public class PeminjamanServices : IPeminjamanServices
    {
        private readonly ILogger _logger;
        private readonly IPengembalianRepository _pengembalianRepository;
        private readonly IPeminjamanRepository _repository;
        private readonly IPeminjamanDetailRepository _detailRepository;
        private readonly IAnggotaRepository _anggotaRepository;
        private readonly IPetugasRepository _petugasRepository;
        private readonly IBukuRepository _bukuRepository;

        public PeminjamanServices(IPeminjamanRepository repository,
            IPeminjamanDetailRepository detailRepository,
            IPengembalianRepository pengembalianRepository,
            IAnggotaRepository anggotaRepository,
            IPetugasRepository petugasRepository,
            IBukuRepository bukuRepository,
            ILogger<PeminjamanServices> logger)
        {
            _repository = repository;
            _detailRepository = detailRepository;
            _anggotaRepository = anggotaRepository;
            _petugasRepository = petugasRepository;
            _bukuRepository = bukuRepository;
            _pengembalianRepository = pengembalianRepository;
            _logger = logger;
        }

        public async Task<PeminjamanDTO> Get(int id)
        {
            try
            {
                var quaryDetail = _detailRepository.GetAll();
                var quaryBuku = _bukuRepository.GetAll();

                var result = (await _repository.Get(id)).ToModel();

                if (result != null)
                {
                    result.NamaAnggota = _anggotaRepository.Get(result.AnggotaId).Result.Nama;
                    result.NamaPetugas = _petugasRepository.Get(result.PetugasId).Result.Nama;
                    result.TanggalPengembalian = _pengembalianRepository.GetAll().Where(w => w.PeminjamanId == result.Id).Select(s => s.TanggalPengembalian).FirstOrDefault();
                    result.Details = (
                                    from p in quaryDetail.Where(w => w.PeminjamanId == result.Id)
                                    join q in quaryBuku on p.BukuId equals q.Id
                                    select new PeminjamanDetailDTO
                                    {
                                        PeminjamanId = p.PeminjamanId,
                                        BukuId = p.BukuId,
                                        JudulBuku = q.Judul
                                    }
                                  ).ToList();
                }

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError($"error get Peminjaman by id {id}", ex);
                throw new ExceptionExtension($"error get Peminjaman by id {id}. detail ", ex);
            }
        }

        public async Task<List<PeminjamanDTO>> GetAll()
        {
            try
            {
                var quaryDetail = _detailRepository.GetAll();
                var quaryBuku = _bukuRepository.GetAll();

                var result = await _repository.GetAll().ToListAsync();

                return result.Select(s => new PeminjamanDTO
                {
                    Id = s.Id,
                    AnggotaId = s.AnggotaId,
                    NamaAnggota = _anggotaRepository.Get(s.AnggotaId).Result.Nama,
                    PetugasId = s.PetugasId,
                    NamaPetugas = _petugasRepository.Get(s.PetugasId).Result.Nama,
                    TanggalKembali = s.TanggalKembali,
                    TanggalPinjam = s.TanggalPinjam,
                    TanggalPengembalian = _pengembalianRepository.GetAll().Where(w => w.PeminjamanId == s.Id).Select(s => s.TanggalPengembalian).FirstOrDefault(),
                    Details = (
                                from p in quaryDetail.Where(w => w.PeminjamanId == s.Id)
                                join q in quaryBuku on p.BukuId equals q.Id
                                select new PeminjamanDetailDTO
                                {
                                    PeminjamanId = p.PeminjamanId,
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
                _logger.LogError("error get all Peminjaman", ex);
                throw new ExceptionExtension("error get all Peminjaman. detail", ex);
            }
        }

        public async Task<PeminjamanDTO> Add(PeminjamanDTO model)
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
                            await _detailRepository.Add(new PeminjamanDetail
                            {
                                BukuId = detail.BukuId,
                                PeminjamanId = result.Id
                            });
                        }
                    }

                    scope.Complete();

                    return result.ToModel();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("error create Peminjaman.", ex);
                throw new ExceptionExtension("error create Peminjaman.", ex);
            }
        }

        public async Task<PeminjamanDTO> Update(PeminjamanDTO model)
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
                    entity.TanggalKembali = model.TanggalKembali;
                    entity.TanggalPinjam = model.TanggalPinjam;
                    entity.IsActive = true;
                    entity.UpdatedAt = DateTime.Now;
                    entity.UpdatedBy = "admin";

                    var result = await _repository.Update(entity);

                    await _detailRepository.Delete(result.Id);

                    if (model.Details != null)
                    {
                        foreach (var detail in model.Details)
                        {
                            await _detailRepository.Add(new PeminjamanDetail
                            {
                                BukuId = detail.BukuId,
                                PeminjamanId = result.Id
                            });
                        }
                    }

                    scope.Complete();

                    return result.ToModel();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("error update Peminjaman.", ex);
                throw new ExceptionExtension("error update Peminjaman.", ex);
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
                _logger.LogError($"error delete Peminjaman by id {Id}.", ex);
                throw new ExceptionExtension($"error delete Peminjaman by id {Id}.", ex);
            }
        }
    }
}
