using Action.Infrastructure.Persistance.Context;
using Microsoft.EntityFrameworkCore;
using Shared.Domain;
using Shared.Domain.Repository;

namespace Action.Infrastructure.Repositories
{
    public class BaseRepository<T> : IRepository<T> where T : BaseEntity
    {
        protected readonly ActionDbContext _ctx;
        public BaseRepository(ActionDbContext ctx) => _ctx = ctx;

        public DbSet<T> Table => _ctx.Set<T>();
    }
}
