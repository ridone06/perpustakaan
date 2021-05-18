using Perpustakaan.Api.Models.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Perpustakaan.Api.Data.Repository.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class, IEntity
    {
        IQueryable<TEntity> GetAll();
        Task<TEntity> Get(int id);
        Task<TEntity> Add(TEntity entity);
        Task<TEntity> Update(TEntity entity);
        Task<TEntity> Delete(int id);
        Task DeleteAll(string tableName);
    }
}
