using Microsoft.EntityFrameworkCore;
using MongoDB.EntityFrameworkCore.Extensions;
using Orders.Core.Entities;

namespace Orders.Infrastructure.Persistence.Contexts
{
    public class ReadDbContext(DbContextOptions<ReadDbContext> options) : DbContext(options)
    {
        public DbSet<Order> Orders { get; set; }
        public DbSet<Voucher> Vouchers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>().ToCollection("Orders");
            modelBuilder.Entity<Voucher>().ToCollection("Vouchers");
        }
    }
}
