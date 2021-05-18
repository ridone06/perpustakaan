using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Perpustakaan.Api.Models.Entity
{
    public class Buku : BaseEntity
    {
        [Required]
        [Column(TypeName = "NVARCHAR(100)")]
        public string Judul { get; set; }

        [Required]
        public int TahunTerbit { get; set; }

        [Required]
        public int PengarangId { get; set; }

        [Required]
        public int PenerbitId { get; set; }

        [Required]
        [Column(TypeName = "NVARCHAR(10)")]
        public string KodeRak { get; set; }
    }
}
