using System.ComponentModel.DataAnnotations;

namespace Perpustakaan.Api.Models.Entity
{
    public class PengembalianDetail
    {
        [Required]
        public int PengembalianId { get; set; }

        [Required]
        public int BukuId { get; set; }
    }
}
