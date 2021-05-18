using Microsoft.EntityFrameworkCore;
using Perpustakaan.Api.Data.Repository.Interfaces;
using Perpustakaan.Api.Models.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Perpustakaan.Api.Data.Repository
{
    public class RakRepository : BaseRepositoryWithoutId<Rak>, IRakRepository
    {
        public RakRepository(PerpustakaanContext context) : base(context) { }

        public async Task<Rak> Delete(string kode)
        {
            var entity = await context.Set<Rak>().Where(w => w.Kode == kode).FirstOrDefaultAsync();
            if (entity == null)
            {
                return entity;
            }

            entity.IsActive = false;
            context.Entry(entity).State = EntityState.Modified;
            await context.SaveChangesAsync();

            return entity;
        }
    }
}
