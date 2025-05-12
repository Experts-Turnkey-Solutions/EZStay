namespace EZStay.Api.Repositories
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> GetAllWithIncludeAsync(Func<IQueryable<T>, IQueryable<T>> include);
        Task<T?> GetByIdAsync(Guid id);
        Task<T> AddAsync(T entity);
        Task<T?> UpdateAsync(Guid id, T entity);
        Task<bool> DeleteAsync(Guid id);
        IQueryable<T> Query();
    }
}
