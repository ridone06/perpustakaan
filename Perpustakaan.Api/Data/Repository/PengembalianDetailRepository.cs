using Microsoft.EntityFrameworkCore;
using Perpustakaan.Api.Data.Repository.Interfaces;
using Perpustakaan.Api.Models.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Perpustakaan.Api.Data.Repository
{
    public class PengembalianDetailRepository : BaseRepositoryWithoutId<PengembalianDetail>, IPengembalianDetailRepository
    {
        public PengembalianDetailRepository(PerpustakaanContext context) : base(context) { }

        public async Task Delete(int PeminjamanId)
        {
            var details = await context.PeminjamanDetail.Where(w => w.PeminjamanId == PeminjamanId).ToListAsync();
            if (details.Count() > 0)
            {
                context.PeminjamanDetail.RemoveRange(details);
                context.SaveChanges();
            }
        }
    }
}
