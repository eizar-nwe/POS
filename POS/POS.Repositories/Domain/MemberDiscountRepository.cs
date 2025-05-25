using POS.Domain.DAO;
using POS.Domain.Models.DataModels;
using POS.Repositories.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Repositories.Domain
{
    public class MemberDiscountRepository : BaseRepository<MemberDiscountEntity>, IMemberDiscountRepository
    {
        private readonly POSDbContext _dbContext;

        public MemberDiscountRepository(POSDbContext dbContext) : base(dbContext)
        {
            this._dbContext = dbContext;
        }
        //to filter composit primary key
        public bool UpdateAsync(MemberDiscountEntity entity)
        {
            try
            {
                _dbContext.MemberDisc.Update(entity);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool DeleteAsync(MemberDiscountEntity entity)
        {
            try
            {
                _dbContext.MemberDisc.Remove(entity);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
