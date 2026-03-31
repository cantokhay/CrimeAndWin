using MassTransit;
using Microsoft.EntityFrameworkCore;
using CrimeAndWin.Saga.States;

namespace CrimeAndWin.Saga.Data;

public class SagaDbContext : DbContext
{
    public SagaDbContext(DbContextOptions<SagaDbContext> options) : base(options)
    {
    }

    public DbSet<CrimeRewardState> CrimeRewardStates { get; set; } = null!;
    public DbSet<CrimeActionState> CrimeActionStates { get; set; } = null!;
    public DbSet<PurchaseState> PurchaseStates { get; set; } = null!;
    public DbSet<RankUpdateState> RankUpdateStates { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Inbox / Outbox state tables for MassTransit atomicity
        modelBuilder.AddInboxStateEntity();
        modelBuilder.AddOutboxMessageEntity();
        modelBuilder.AddOutboxStateEntity();

        modelBuilder.Entity<CrimeRewardState>(entity =>
        {
            entity.ToTable("CrimeRewardStates");
            entity.HasKey(x => x.CorrelationId);
            entity.Property(x => x.CurrentState).HasMaxLength(64).IsRequired();
            entity.Property(x => x.FailReason).HasMaxLength(500);
            entity.Property(x => x.MoneyReward).HasPrecision(18, 2);
        });

        modelBuilder.Entity<CrimeActionState>(entity =>
        {
            entity.ToTable("CrimeActionStates");
            entity.HasKey(x => x.CorrelationId);
            entity.Property(x => x.CurrentState).HasMaxLength(64).IsRequired();
            entity.Property(x => x.FailReason).HasMaxLength(500);
        });

        modelBuilder.Entity<PurchaseState>(entity =>
        {
            entity.ToTable("PurchaseStates");
            entity.HasKey(x => x.CorrelationId);
            entity.Property(x => x.CurrentState).HasMaxLength(64).IsRequired();
            entity.Property(x => x.FailReason).HasMaxLength(500);
            entity.Property(x => x.Price).HasPrecision(18, 2);
        });

        modelBuilder.Entity<RankUpdateState>(entity =>
        {
            entity.ToTable("RankUpdateStates");
            entity.HasKey(x => x.CorrelationId);
            entity.Property(x => x.CurrentState).HasMaxLength(64).IsRequired();
            entity.Property(x => x.FailReason).HasMaxLength(500);
        });
    }
}
