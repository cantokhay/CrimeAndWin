using Microsoft.EntityFrameworkCore;

namespace Shared.Domain.Repository
{
    public interface IRepository<T> where T : BaseEntity
    {
        DbSet<T> Table { get; }
    }
}
