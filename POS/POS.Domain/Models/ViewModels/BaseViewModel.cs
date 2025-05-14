namespace POS.Domain.Models.ViewModels
{
    public class BaseViewModel
    {
        public string Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string? UpdatedBy { get; set; }
        public string Ip { get; set; }
        public bool IsActive { get; set; }
        public string? Remarks { get; set; }
    }
}
