using System.ComponentModel.DataAnnotations;

namespace Perpustakaan.Api.Models.DTO
{
    public class PengarangDTO : BaseDTO
    {
        [Required]
        [StringLength(100, ErrorMessage = "Nama can't be greather than 100 character.")]
        public string Nama { get; set; }

        [Required]
        public string Alamat { get; set; }

        [Required]
        [StringLength(15, ErrorMessage = "No. Telepone can't be greather than 15 character.")]
        public string NoTlp { get; set; }
    }
}
