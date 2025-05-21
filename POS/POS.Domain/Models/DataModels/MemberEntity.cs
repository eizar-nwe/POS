using System.ComponentModel.DataAnnotations.Schema;

namespace POS.Domain.Models.DataModels
{
    [Table("Member")]
    public class MemberEntity:BaseEntity
    {
        public required string Name { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Address { get; set; }
        public DateTime MemberDate { get; set; }
    }
}
