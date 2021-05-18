using Microsoft.EntityFrameworkCore;
using Perpustakaan.Api.Data.Repository.Interfaces;
using Perpustakaan.Api.Models.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Perpustakaan.Api.Data.Repository
{
    public abstract class BaseRepository<TEntity> : IRepository<TEntity>
       where TEntity : class, IEntity
    {
        public readonly PerpustakaanContext context;
        public BaseRepository(PerpustakaanContext context)
        {
            this.context = context;
        }
        public virtual async Task<TEntity> Add(TEntity entity)
        {
            context.Set<TEntity>().Add(entity);
            await context.SaveChangesAsync();
            return entity;
        }

        public virtual async Task<TEntity> Delete(int id)
        {
            var entity = await context.Set<TEntity>().FindAsync(id);
            if (entity == null)
            {
                return entity;
            }

            entity.IsActive = false;
            context.Entry(entity).State = EntityState.Modified;
            await context.SaveChangesAsync();

            return entity;
        }

        public virtual async Task<TEntity> DeleteForce(int id)
        {
            var entity = await context.Set<TEntity>().FindAsync(id);
            if (entity == null)
            {
                return entity;
            }

            context.Set<TEntity>().Remove(entity);
            await context.SaveChangesAsync();

            return entity;
        }

        public virtual async Task<TEntity> Get(int id)
        {
            return await context.Set<TEntity>().Where(w => w.IsActive && w.Id == id).FirstOrDefaultAsync();
        }

        public virtual async Task<TEntity> Update(TEntity entity)
        {
            context.Entry(entity).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return entity;
        }

        public virtual IQueryable<TEntity> GetAll()
        {
            return context.Set<TEntity>().Where(w => w.IsActive == true).AsNoTracking();
        }

        public async Task DeleteAll(string tableName)
        {
            var query = $"TRUNCATE TABLE {tableName}";

            await context.Database.ExecuteSqlRawAsync(query);
        }
    }
}
