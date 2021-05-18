using Perpustakaan.Api.Models.Entity;
using System.Threading.Tasks;

namespace Perpustakaan.Api.Data.Repository.Interfaces
{
    public interface IRakRepository : IRepositoryWithoutId<Rak>
    {
        Task<Rak> Delete(string kode);
    }
}
