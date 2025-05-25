using POS.Domain.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Services
{
    public interface IMemberDiscountService
    {
        string Create(MemberDiscountViewModel MemDiscVM);
        IEnumerable<MemberDiscountViewModel> GetAll();
        IEnumerable<MemberDiscountViewModel> GetById(string id);
        string Update(MemberDiscountViewModel MemDiscVM);
        string DeleteRec(string id);
        string DeleteItem(string id, int line_id);
    }
}
