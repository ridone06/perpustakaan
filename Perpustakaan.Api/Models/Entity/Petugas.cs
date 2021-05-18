using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Perpustakaan.Api.Models.Entity
{
    public class Petugas : BaseEntity
    {
        [Required]
        [Column(TypeName = "NVARCHAR(50)")]
        public string Username { get; set; }

        [Required]
        [Column(TypeName = "NVARCHAR(50)")]
        public string Password { get; set; }

        [Required]
        [Column(TypeName = "NVARCHAR(50)")]
        public string Nama { get; set; }

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
