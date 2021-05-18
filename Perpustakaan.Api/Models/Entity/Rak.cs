using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Perpustakaan.Api.Models.Entity
{
    public class Rak
    {
        [Key]
        [Required]
        [Column(TypeName = "NVARCHAR(10)")]
        public string Kode { get; set; }

        [Column(TypeName = "NVARCHAR(MAX)")]
        public string Lokasi { get; set; }

        [Required]
        [Column(TypeName = "NVARCHAR(30)")]
        public string CreatedBy { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        [Required]
        [Column(TypeName = "NVARCHAR(30)")]
        public string UpdatedBy { get; set; }

        [Required]
        public DateTime UpdatedAt { get; set; }

        [Required]
        public bool IsActive { get; set; }
    }
}
