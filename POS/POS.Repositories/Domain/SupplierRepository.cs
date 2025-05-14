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
    public class SupplierRepository : BaseRepository<SupplierEntity>, ISupplierRepository
    {
        private readonly POSDbContext _dbContext;

        public SupplierRepository(POSDbContext dbContext) : base(dbContext)
        {
            this._dbContext = dbContext;
        }
    }
}
