using Perpustakaan.Api.Models.Entity;
using System.Threading.Tasks;

namespace Perpustakaan.Api.Data.Repository.Interfaces
{
    public interface IPengembalianDetailRepository : IRepositoryWithoutId<PengembalianDetail>
    {
        Task Delete(int PengembalianId);
    }
}
