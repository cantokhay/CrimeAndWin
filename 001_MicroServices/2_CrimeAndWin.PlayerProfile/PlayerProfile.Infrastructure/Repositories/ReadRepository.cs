using Microsoft.EntityFrameworkCore;
using PlayerProfile.Infrastructure.Persistance.Context;
using Shared.Domain;
using Shared.Domain.Repository;
using System.Linq.Expressions;

namespace PlayerProfile.Infrastructure.Repositories
{
    public class ReadRepository<T> : BaseRepository<T>, IReadRepository<T> where T : BaseEntity
    {
        public ReadRepository(PlayerProfileDbContext ctx) : base(ctx) { }

        public IQueryable<T> Query() => _ctx.Set<T>().AsNoTracking();

        public IQueryable<T> GetAll(bool tracking = true)
            => tracking ? Table.AsQueryable() : Table.AsNoTracking().AsQueryable();

        public IQueryable<T> GetWhere(Expression<Func<T, bool>> method, bool tracking = true)
            => tracking ? Table.Where(method) : Table.Where(method).AsNoTracking();

        public async Task<T> GetSingleAsync(Expression<Func<T, bool>> method, bool tracking = true)
        {
            if (tracking)
                return await Table.FirstOrDefaultAsync(method) ?? default!;
            return await Table.AsNoTracking().FirstOrDefaultAsync(method) ?? default!;
        }

        public async Task<T> GetByIdAsync(string id, bool tracking = true)
        {
            if (!Guid.TryParse(id, out var guid))
                throw new ArgumentException("Geçersiz id formatı (Guid bekleniyor).", nameof(id));

            var query = tracking ? Table : Table.AsNoTracking();
            return await query.FirstOrDefaultAsync(e => e.Id == guid) ?? default!;
        }
    }
}
