using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Domain.Models.ViewModels
{
    public class MemberViewModel:BaseViewModel
    {
        public string Name { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Address { get; set; }
        public DateTime MemberDate { get; set; }
    }
}
