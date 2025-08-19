using GameWorld.Infrastructure.Persistance.Context;
using Microsoft.EntityFrameworkCore;
using Shared.Domain;
using Shared.Domain.Repository;

namespace GameWorld.Infrastructure.Repositories
{
    public class WriteRepository<T> : BaseRepository<T>, IWriteRepository<T> where T : BaseEntity
    {
        public WriteRepository(GameWorldDbContext ctx) : base(ctx) { }

        public async Task<bool> AddAsync(T entity)
        {
            await Table.AddAsync(entity);
            return true;
        }

        public async Task<bool> AddRangeAsync(List<T> entityList)
        {
            await Table.AddRangeAsync(entityList);
            return true;
        }

        public bool Update(T entity)
        {
            Table.Update(entity);
            return true;
        }

        public bool UpdateRange(List<T> entityList)
        {
            Table.UpdateRange(entityList);
            return true;
        }

        public bool Remove(T entity)
        {
            Table.Remove(entity);
            return true;
        }

        public bool RemoveRange(List<T> entityList)
        {
            Table.RemoveRange(entityList);
            return true;
        }

        public async Task<bool> RemoveAsync(string id)
        {
            if (!Guid.TryParse(id, out var guid))
                throw new ArgumentException("Geçersiz id formatı (Guid bekleniyor).", nameof(id));

            var entity = await Table.FirstOrDefaultAsync(e => e.Id == guid);
            if (entity is null) return false;

            Table.Remove(entity);
            return true;
        }

        public Task<int> SaveAsync() => _ctx.SaveChangesAsync();
    }
}
