using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using POS.Domain.DAO;
using POS.Domain.Models.DataModels;
using POS.Domain.Models.ViewModels;
using POS.Repositories.Common;
using System;
using System.Data;
using System.Linq.Expressions;

namespace POS.Repositories.Domain
{
    public class PaymentRepository : BaseRepository<PaymentEntity>, IPaymentRepository
    {
        private readonly POSDbContext _dbContext;

        public PaymentRepository(POSDbContext dbContext) : base(dbContext)
        {
            this._dbContext = dbContext;
        }

        public async Task<int> Execute_Payment_Procedure(string id, string paymethod, string cardNo, string crdBy, SaleOrderViewModel saleTotal,string ordrtype)
        {
            try
            {
                var outputParam = new SqlParameter
                {
                    ParameterName = "@RETVAL",
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Output
                };

                decimal sub_tot = saleTotal.SUB_TOT;
                decimal tot_disc = saleTotal.DISC_TOT;
                decimal tot_amt = saleTotal.ALL_TOT;

                var Param1 = new SqlParameter("@ID", id);
                var Param2 = new SqlParameter("@PAY_METHOD", paymethod);
                var Param3 = new SqlParameter("@SUB_TOT", sub_tot);
                var Param4 = new SqlParameter("@TOT_DISC", tot_disc);
                var Param5 = new SqlParameter("@TOT_AMT", tot_amt);
                var Param6 = new SqlParameter("@CARD_NUM", string.IsNullOrEmpty(cardNo) ? DBNull.Value : cardNo);                
                var Param7 = new SqlParameter("@CREATEDBY", crdBy);
                var Param8 = new SqlParameter("@ORDR_TYPE", ordrtype);

                await _dbContext.Database.ExecuteSqlRawAsync("EXEC dbo.Pro_Add_Payment @RETVAL OUTPUT, @ID, @PAY_METHOD, @SUB_TOT, @TOT_DISC, @TOT_AMT, @CARD_NUM, @CREATEDBY, @ORDR_TYPE",
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
