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
        public DbSet<MemberEntity> Members { get; set; }
        public DbSet<CashierEntity> Cashiers { get; set; }
        public DbSet<StockIncomeEntity> StockIncome { get; set; }        
        public DbSet<MemberDiscountEntity> MemberDisc { get; set; }
        public DbSet<SaleOrderHaderEntity> SaleHader { get; set; }
        public DbSet<SaleOrderDetailEntity> SaleDetail { get; set; }
        
        //for composit primary key
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StockIncomeEntity>()
                .HasKey(o => new { o.Id, o.LINE_ID });

            modelBuilder.Entity<MemberDiscountEntity>()
                .HasKey(o => new { o.Id, o.LINE_ID });
        }
    }
}
