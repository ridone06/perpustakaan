using System.Linq;
using System.Threading.Tasks;

namespace Perpustakaan.Api.Data.Repository.Interfaces
{
    public interface IRepositoryWithoutId<TEntity> where TEntity : class
    {
        IQueryable<TEntity> GetAll();
        Task<TEntity> Add(TEntity entity);
        Task<TEntity> Update(TEntity entity);
    }
}
