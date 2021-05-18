using Perpustakaan.Api.Data.Repository.Interfaces;
using Perpustakaan.Api.Models.Entity;

namespace Perpustakaan.Api.Data.Repository
{
    public class PengembalianRepository : BaseRepository<Pengembalian>, IPengembalianRepository
    {
        public PengembalianRepository(PerpustakaanContext context) : base(context) { }
    }
}
