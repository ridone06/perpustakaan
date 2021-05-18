using System;

namespace Perpustakaan.Api.Models.DTO
{
    public class BaseDTO
    {
        public int Id { get; set; }

        public string CreatedBy { get; set; }

        public DateTime CreatedAt { get; set; }

        public string UpdatedBy { get; set; }

        public DateTime UpdatedAt { get; set; }

        public bool IsActive { get; set; }
    }
}
