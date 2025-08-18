namespace Shared.Domain.Repository
{
    public interface IWriteRepository<T> : IRepository<T> where T : BaseEntity
    {
        Task<bool> AddAsync(T entity);
        Task<bool> AddRangeAsync(List<T> entityList);
        bool Update(T entity);
        bool UpdateRange(List<T> entityList);
        bool Remove(T entity);
        bool RemoveRange(List<T> entityList);
        Task<bool> RemoveAsync(string id);

        Task<int> SaveAsync();
    }
}
