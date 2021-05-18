using System.Collections.Generic;
using System.Threading.Tasks;

namespace Perpustakaan.Api.Data.Services.Interfaces
{
    public interface IServices<TEntity>
    {
        Task<List<TEntity>> GetAll();
        Task<TEntity> Get(int id);
        Task<TEntity> Add(TEntity model);
        Task<TEntity> Update(TEntity model);
        Task<int> Delete(int id);
    }
}
