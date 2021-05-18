using System;

namespace Perpustakaan.Api.Models.Entity
{
    public interface IEntity
    {
        int Id { get; set; }
        string CreatedBy { get; set; }
        DateTime CreatedAt { get; set; }
        string UpdatedBy { get; set; }
        DateTime UpdatedAt { get; set; }
        bool IsActive { get; set; }
    }
}
