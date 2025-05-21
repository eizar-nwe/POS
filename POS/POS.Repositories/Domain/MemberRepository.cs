using POS.Domain.DAO;
using POS.Domain.Models.DataModels;
using POS.Repositories.Common;

namespace POS.Repositories.Domain
{
    public class MemberRepository : BaseRepository<MemberEntity>, IMemberRepository
    {
        private readonly POSDbContext _dbContext;

        public MemberRepository(POSDbContext dbContext) : base(dbContext)
        {
            this._dbContext = dbContext;
        }
    }
}
