using Shared.Domain;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Economy.Domain.Entities;
using Economy.Domain.Enums;

namespace Economy.Infrastructure.Persistance.Context
{
    public class EconomyDbContext : DbContext
    {
        public EconomyDbContext(DbContextOptions<EconomyDbContext> options) : base(options) { }

        public DbSet<Wallet> Wallets => Set<Wallet>();
        public DbSet<Transaction> WalletTransactions => Set<Transaction>(); // Fix: Use 'Transaction' entity
        public DbSet<GangWallet> GangWallets => Set<GangWallet>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(EconomyDbContext).Assembly);
            base.OnModelCreating(modelBuilder);

            // 1. Soft Delete Filter
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                if (typeof(BaseEntity).IsAssignableFrom(entityType.ClrType))
                {
                    var parameter = Expression.Parameter(entityType.ClrType, "e");
                    var property = Expression.Property(parameter, "IsDeleted");
                    var body = Expression.Not(property);
                    var lambda = Expression.Lambda(body, parameter);

                    modelBuilder.Entity(entityType.ClrType).HasQueryFilter(lambda);
                }
            }

            // 2. Global OnDeleteBehavior (Restrict)
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }
    }
}
