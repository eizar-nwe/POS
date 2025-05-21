using POS.Domain.Models.ViewModels;

namespace POS.Services
{
    public interface IMemberService
    {
        bool Create(MemberViewModel memberVM);
        IEnumerable<MemberViewModel> GetAll();
        MemberViewModel GetById(string id);
        bool Update(MemberViewModel memberVM);
        bool Delete(String id);
    }
}
