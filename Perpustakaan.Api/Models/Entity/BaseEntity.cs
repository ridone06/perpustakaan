using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Perpustakaan.Api.Models.Entity
{
    public class BaseEntity : IEntity
    {
        public int Id { get; set; }

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
