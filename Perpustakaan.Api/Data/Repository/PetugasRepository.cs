using Perpustakaan.Api.Data.Repository.Interfaces;
using Perpustakaan.Api.Models.Entity;

namespace Perpustakaan.Api.Data.Repository
{
    public class PetugasRepository : BaseRepository<Petugas>, IPetugasRepository
    {
        public PetugasRepository(PerpustakaanContext context) : base(context) { }
    }
}
