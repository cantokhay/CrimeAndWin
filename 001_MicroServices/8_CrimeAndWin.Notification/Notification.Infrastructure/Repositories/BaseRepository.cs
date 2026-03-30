using Notification.Infrastructure.Persistance.Context;
using Microsoft.EntityFrameworkCore;
using Shared.Domain;
using Shared.Domain.Repository;

namespace Notification.Infrastructure.Repositories
{
    public class BaseRepository<T> : IRepository<T> where T : BaseEntity
    {
        protected readonly NotificationDbContext _ctx;
        public BaseRepository(NotificationDbContext ctx) => _ctx = ctx;

        public DbSet<T> Table => _ctx.Set<T>();
    }
}
