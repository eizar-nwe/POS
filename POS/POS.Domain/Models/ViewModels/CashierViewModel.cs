﻿namespace POS.Domain.Models.ViewModels
{
    public class CashierViewModel:BaseViewModel
    {
        public string Name { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Address { get; set; }
        public string Gender { get; set; }
    }
}
