using Perpustakaan.Api.Data.Repository.Interfaces;
using Perpustakaan.Api.Models.Entity;

namespace Perpustakaan.Api.Data.Repository
{
    public class PengarangRepository : BaseRepository<Pengarang>, IPengarangRepository
    {
        public PengarangRepository(PerpustakaanContext context) : base(context) { }
    }
}
