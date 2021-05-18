using Perpustakaan.Api.Data.Repository.Interfaces;
using Perpustakaan.Api.Models.Entity;

namespace Perpustakaan.Api.Data.Repository
{
    public class PeminjamanRepository : BaseRepository<Peminjaman>, IPeminjamanRepository
    {
        public PeminjamanRepository(PerpustakaanContext context) : base(context) { }
    }
}
