using System.ComponentModel.DataAnnotations;

namespace POS.Domain.Models.ViewModels
{
    public class StockGroupViewModel: BaseViewModel
    {
        public string Code { get; set; }
        public string? Description { get; set; }
        
    }
}
