using Microsoft.EntityFrameworkCore;
using Perpustakaan.Api.Data.Repository.Interfaces;
using Perpustakaan.Api.Models.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Perpustakaan.Api.Data.Repository
{
    public abstract class BaseRepositoryWithoutId<TEntity> : IRepositoryWithoutId<TEntity>
       where TEntity : class
    {
        public readonly PerpustakaanContext context;
        public BaseRepositoryWithoutId(PerpustakaanContext context)
        {
            this.context = context;
        }
        public virtual async Task<TEntity> Add(TEntity entity)
        {
            context.Set<TEntity>().Add(entity);
            await context.SaveChangesAsync();
            return entity;
        }

        public virtual async Task<TEntity> Update(TEntity entity)
        {
            context.Entry(entity).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return entity;
        }

        public virtual IQueryable<TEntity> GetAll()
        {
            return context.Set<TEntity>().AsNoTracking();
        }
    }
}
