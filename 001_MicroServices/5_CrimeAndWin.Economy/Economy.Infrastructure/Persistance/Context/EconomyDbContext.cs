using Economy.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Economy.Infrastructure.Persistance.Context
{
    public class EconomyDbContext : DbContext
    {
        public EconomyDbContext(DbContextOptions<EconomyDbContext> options) : base(options) { }

        public DbSet<Wallet> Wallets { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(EconomyDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
