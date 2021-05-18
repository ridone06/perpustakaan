using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Perpustakaan.Api.Models.DTO
{
    public class PeminjamanDTO : BaseDTO
    {
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        [Required]
        public DateTime TanggalPinjam { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        [Required]
        public DateTime TanggalKembali { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime? TanggalPengembalian { get; set; }

        [Required]
        public int AnggotaId { get; set; }

        public string NamaAnggota { get; set; }

        [Required]
        public int PetugasId { get; set; }

        public string NamaPetugas { get; set; }

        public string Status
        {
            get
            {
                if (TanggalPengembalian != null && TanggalPengembalian.Value.Year != 1)
                    return "Be returned";

                if (TanggalKembali.Year + TanggalKembali.Month + TanggalKembali.Day < DateTime.Now.Year + DateTime.Now.Month + DateTime.Now.Day)
                    return "Overdue";

                return "On progress";
            }
        }

        [Required]
        public IList<PeminjamanDetailDTO> Details { get; set; }
    }
}
