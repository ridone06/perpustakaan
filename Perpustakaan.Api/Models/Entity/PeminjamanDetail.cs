using System.ComponentModel.DataAnnotations;

namespace Perpustakaan.Api.Models.Entity
{
    public class PeminjamanDetail
    {
        [Required]
        public int PeminjamanId { get; set; }

        [Required]
        public int BukuId { get; set; }
    }
}
