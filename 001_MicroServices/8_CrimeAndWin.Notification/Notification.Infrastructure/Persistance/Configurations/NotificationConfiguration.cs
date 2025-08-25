using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace Notification.Infrastructure.Persistance.Configurations
{
    public class NotificationConfiguration : IEntityTypeConfiguration<Domain.Entities.Notification>
    {
        public void Configure(EntityTypeBuilder<Domain.Entities.Notification> builder)
        {

            builder.ToTable("Notifications");
            builder.HasKey(x => x.Id);
            builder.OwnsOne(x => x.Content, cb =>
            {
                cb.Property(c => c.Title).HasColumnName("Title").HasMaxLength(100);
                cb.Property(c => c.Message).HasColumnName("Message").HasMaxLength(500);
                cb.Property(c => c.Type).HasColumnName("Type").HasMaxLength(50);
            });
            builder.Property(x => x.PlayerId).IsRequired();
            builder.HasIndex(x => x.PlayerId);
        }
    }
}
