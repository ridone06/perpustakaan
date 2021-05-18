using System.ComponentModel.DataAnnotations;

namespace Perpustakaan.Api.Models.DTO
{
    public class AnggotaDTO : BaseDTO
    {
        [Required]
        [StringLength(100, ErrorMessage = "Nama can't be greather than 100 character.")]
        public string Nama { get; set; }

        [Required]
        [StringLength(1, ErrorMessage = "Jenis Kelamin can't be greather than 1 character.")]
        public string JenisKelamin { get; set; }

        [Required]
        public string Alamat { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Email can't be greather than 100 character.")]
        public string Email { get; set; }

        [Required]
        [StringLength(15, ErrorMessage = "No. Telepone can't be greather than 15 character.")]
        public string NoTlp { get; set; }
    }
}
