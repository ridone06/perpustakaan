using System;
using System.ComponentModel.DataAnnotations;

namespace Perpustakaan.Api.Models.DTO
{
    public class RakDTO
    {
        [Required]
        [StringLength(10, ErrorMessage = "Kode can't be greather than 10 character.")]
        public string Kode { get; set; }

        public string Lokasi { get; set; }

        public string CreatedBy { get; set; }

        public DateTime CreatedAt { get; set; }

        public string UpdatedBy { get; set; }

        public DateTime UpdatedAt { get; set; }

        public bool IsActive { get; set; }
    }
}
