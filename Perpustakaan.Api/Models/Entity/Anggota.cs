using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Perpustakaan.Api.Models.Entity
{
    public class Anggota : BaseEntity
    {
        [Required]
        [Column(TypeName = "NVARCHAR(100)")]
        public string Nama { get; set; }

        [Required]
        [Column(TypeName = "NVARCHAR(1)")]
        public string JenisKelamin { get; set; }

        [Required]
        [Column(TypeName = "NVARCHAR(MAX)")]
        public string Alamat { get; set; }

        [Required]
        [Column(TypeName = "NVARCHAR(100)")]
        public string Email { get; set; }

        [Required]
        [Column(TypeName = "NVARCHAR(15)")]
        public string NoTlp { get; set; }
    }
}
