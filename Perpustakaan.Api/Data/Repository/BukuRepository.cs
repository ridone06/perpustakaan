using Perpustakaan.Api.Data.Repository.Interfaces;
using Perpustakaan.Api.Models.Entity;

namespace Perpustakaan.Api.Data.Repository
{
    public class BukuRepository : BaseRepository<Buku>, IBukuRepository
    {
        public BukuRepository(PerpustakaanContext context) : base(context) { }
    }
}
