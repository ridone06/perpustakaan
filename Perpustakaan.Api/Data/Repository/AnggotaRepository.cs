using Perpustakaan.Api.Data.Repository.Interfaces;
using Perpustakaan.Api.Models.Entity;

namespace Perpustakaan.Api.Data.Repository
{
    public class AnggotaRepository : BaseRepository<Anggota>, IAnggotaRepository
    {
        public AnggotaRepository(PerpustakaanContext context) : base(context) { }
    }
}
