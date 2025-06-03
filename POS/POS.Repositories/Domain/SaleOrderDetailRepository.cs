using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using POS.Domain.DAO;
using POS.Domain.Models.DataModels;
using POS.Repositories.Common;
using System.Data;
using System.Linq.Expressions;

namespace POS.Repositories.Domain
{
    public class SaleOrderDetailRepository : BaseRepository<SaleOrderDetailEntity>, ISaleOrderDetailRepository
    {
        private readonly POSDbContext _dbContext;

        public SaleOrderDetailRepository(POSDbContext dbContext) : base(dbContext)
        {
            this._dbContext = dbContext;
        }
        public async Task<int> Execute_SaleDetl_Procedure(string cmd,string id,int lineId,string grpId,string itemId,decimal qnty,string rmk,string crdBy)
        {
            try
            {
                var outputParam = new SqlParameter
                {
                    ParameterName = "@RETVAL",
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Output
                };

                var Param1 = new SqlParameter("@CMD", cmd);
                var Param2 = new SqlParameter("@ID", id);
                var Param3 = new SqlParameter("@LINE_ID", lineId);
                var Param4 = new SqlParameter("@STOCKGRP_ID", grpId);
                var Param5 = new SqlParameter("@STOCKITEM_ID", itemId);
                var Param6 = new SqlParameter("@QUANTITY", qnty);
                var Param7 = new SqlParameter("@REMARKS", string.IsNullOrEmpty(rmk)? DBNull.Value : rmk);
                var Param8 = new SqlParameter("@CREATEDBY", crdBy);

                await _dbContext.Database.ExecuteSqlRawAsync("EXEC dbo.Pro_SALE_ORDER_DETL @RETVAL OUTPUT, @CMD, @ID, @LINE_ID, @STOCKGRP_ID, @STOCKITEM_ID, @QUANTITY, @REMARKS, @CREATEDBY", 
                    outputParam, Param1, Param2, Param3, Param4, Param5, Param6, Param7, Param8);

                return (int)outputParam.Value;
            }
            catch (Exception ex)
            {
                return 1;
            }
        }
    }
}
