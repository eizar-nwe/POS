using Microsoft.EntityFrameworkCore;
using POS.Domain.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Domain.DAO
{
    public class POSDbContext:DbContext
    {
        public POSDbContext(DbContextOptions<POSDbContext> o) : base(o)
        {

        }

        public DbSet<StockGroupEntity> StockGroup { get; set; }
        public DbSet<StockItemEntity> StockItem { get; set; }
        public DbSet<SupplierEntity> Suppliers { get; set; }
    }
}
