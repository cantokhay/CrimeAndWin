using Inventory.Infrastructure.Persistance.Context;
using Microsoft.EntityFrameworkCore;
using Shared.Domain;
using Shared.Domain.Repository;

namespace Inventory.Infrastructure.Repositories
{
    public class BaseRepository<T> : IRepository<T> where T : BaseEntity
    {
        protected readonly InventoryDbContext _ctx;
        public BaseRepository(InventoryDbContext ctx) => _ctx = ctx;

        public DbSet<T> Table => _ctx.Set<T>();
    }
}
