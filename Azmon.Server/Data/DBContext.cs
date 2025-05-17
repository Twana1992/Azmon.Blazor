using Azmon.Core;
using Microsoft.EntityFrameworkCore;

namespace Azmon.Server.Data
{
    public class DBContext : DbContext
    {
        public DBContext(DbContextOptions<DBContext> options) : base(options)
        {

        }

        public DbSet<Users> Users { get; set; }
        public DbSet<Suppliers> Suppliers { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<Buy_Detail> Buy_Detail { get; set; }
        public DbSet<Buy> Buy { get; set; }
        public DbSet<Sell> Sell { get; set; }
        public DbSet<Sell_Detail> Sell_Detail { get; set; }
        public DbSet<Customer> Customer { get; set; }
        public DbSet<SystemRecords> SystemRecords { get; set; }
        public DbSet<Sup_Pay> Sup_Pay { get; set; }
        public DbSet<Cus_Pay> Cus_Pay { get; set; }
    }
}
