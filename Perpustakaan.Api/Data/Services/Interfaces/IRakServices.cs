using Perpustakaan.Api.Models.DTO;
using System.Threading.Tasks;

namespace Perpustakaan.Api.Data.Services.Interfaces
{
    public interface IRakServices : IServices<RakDTO>
    {
        Task<RakDTO> GetByKode(string kode);
        Task<RakDTO> DeleteByKode(string kode);
    }
}
