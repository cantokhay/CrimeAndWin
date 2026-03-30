using Microsoft.EntityFrameworkCore;
using PlayerProfile.Infrastructure.Persistance.Context;
using Shared.Domain;
using Shared.Domain.Repository;

namespace PlayerProfile.Infrastructure.Repositories
{
    public class BaseRepository<T> : IRepository<T> where T : BaseEntity
    {
        protected readonly PlayerProfileDbContext _ctx;
        public BaseRepository(PlayerProfileDbContext ctx) => _ctx = ctx;

        public DbSet<T> Table => _ctx.Set<T>();
    }
}
