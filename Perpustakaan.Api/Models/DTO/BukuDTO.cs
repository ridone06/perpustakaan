using System.ComponentModel.DataAnnotations;

namespace Perpustakaan.Api.Models.DTO
{
    public class BukuDTO : BaseDTO
    {
        [Required]
        [StringLength(100, ErrorMessage = "Judul can't be greather than 100 character.")]
        public string Judul { get; set; }

        [Required]
        public int TahunTerbit { get; set; }

        [Required]
        public int PengarangId { get; set; }

        public string NamaPengarang { get; set; }

        [Required]
        public int PenerbitId { get; set; }

        public string NamaPenerbit { get; set; }

        [Required]
        [StringLength(10, ErrorMessage = "Kode Rak can't be greather than 10 character.")]
        public string KodeRak { get; set; }
    }
}
