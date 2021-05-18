using Perpustakaan.Api.Data.Repository.Interfaces;
using Perpustakaan.Api.Models.Entity;

namespace Perpustakaan.Api.Data.Repository
{
    public class PenerbitRepository : BaseRepository<Penerbit>, IPenerbitRepository
    {
        public PenerbitRepository(PerpustakaanContext context) : base(context) { }
    }
}
