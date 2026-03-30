using Moderation.Infrastructure.Persistance.Context;
using Microsoft.EntityFrameworkCore;
using Shared.Domain;
using Shared.Domain.Repository;

namespace Moderation.Infrastructure.Repositories
{
    public class BaseRepository<T> : IRepository<T> where T : BaseEntity
    {
        protected readonly ModerationDbContext _ctx;
        public BaseRepository(ModerationDbContext ctx) => _ctx = ctx;

        public DbSet<T> Table => _ctx.Set<T>();
    }
}
