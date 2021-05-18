using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Perpustakaan.Api.Models.DTO
{
    public class PengembalianDTO : BaseDTO
    {
        [Required]
        public DateTime TanggalPengembalian { get; set; }

        public decimal Denda { get; set; }

        [Required]
        public int PeminjamanId { get; set; }

        [Required]
        public int AnggotaId { get; set; }

        public string NamaAnggota { get; set; }

        [Required]
        public int PetugasId { get; set; }

        public string NamaPetugas { get; set; }

        [Required]
        public IList<PengembalianDetailDTO> Details { get; set; }
    }
}
