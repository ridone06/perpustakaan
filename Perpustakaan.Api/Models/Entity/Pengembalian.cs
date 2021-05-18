using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Perpustakaan.Api.Models.Entity
{
    public class Pengembalian : BaseEntity
    {
        [Required]
        public DateTime TanggalPengembalian { get; set; }

        [Column(TypeName = "DECIMAL(18,2)")]
        public decimal Denda { get; set; }

        [Required]
        public int PeminjamanId { get; set; }

        [Required]
        public int AnggotaId { get; set; }

        [Required]
        public int PetugasId { get; set; }
    }
}
