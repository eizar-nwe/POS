using System.ComponentModel.DataAnnotations;

namespace POS.Domain.Models.DataModels
{
    public class BaseEntity
    {
        [Key]
        public required string Id { get; set; }
        public required DateTime CreatedAt { get; set; }
        public required string CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string? UpdatedBy { get; set; }
        public required string Ip { get; set; }
        public required bool IsActive { get; set; }
        public string? Remarks { get; set; }
    }
}
