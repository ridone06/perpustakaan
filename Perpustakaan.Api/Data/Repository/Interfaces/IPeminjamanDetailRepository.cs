using Perpustakaan.Api.Models.Entity;
using System.Threading.Tasks;

namespace Perpustakaan.Api.Data.Repository.Interfaces
{
    public interface IPeminjamanDetailRepository : IRepositoryWithoutId<PeminjamanDetail>
    {
        Task Delete(int PeminjamanId);
    }
}
