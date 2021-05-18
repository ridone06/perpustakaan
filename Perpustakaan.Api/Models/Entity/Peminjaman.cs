using System;
using System.ComponentModel.DataAnnotations;

namespace Perpustakaan.Api.Models.Entity
{
    public class Peminjaman : BaseEntity
    {
        [Required]
        public DateTime TanggalPinjam { get; set; }

        public DateTime TanggalKembali { get; set; }

        [Required]
        public int AnggotaId { get; set; }

        [Required]
        public int PetugasId { get; set; }
    }
}
