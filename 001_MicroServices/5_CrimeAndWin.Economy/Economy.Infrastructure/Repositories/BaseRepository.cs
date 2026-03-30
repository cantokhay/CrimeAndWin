using Economy.Infrastructure.Persistance.Context;
using Microsoft.EntityFrameworkCore;
using Shared.Domain;
using Shared.Domain.Repository;

namespace Economy.Infrastructure.Repositories
{
    public class BaseRepository<T> : IRepository<T> where T : BaseEntity
    {
        protected readonly EconomyDbContext _ctx;
        public BaseRepository(EconomyDbContext ctx) => _ctx = ctx;

        public DbSet<T> Table => _ctx.Set<T>();
    }
}
