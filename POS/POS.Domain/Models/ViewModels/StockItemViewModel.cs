namespace POS.Domain.Models.ViewModels
{
    public class StockItemViewModel:BaseViewModel
    {        
        public string Code { get; set; }
        public string? Description { get; set; }
        public string StockGrp_Id { get; set; }
        public string? BarCode { get; set; }
        public string? Disc_Type { get; set; }
        public decimal? Disc_Amount { get; set; }
        public decimal? Disc_Percent { get; set; }
        public DateTime? From_Date { get; set; }
        public DateTime? To_Date { get; set; }
        public string? StockGrpCode { get; set; }
        public IList<StockGroupViewModel> StockGrpVMs { get; set; }
    }
}
