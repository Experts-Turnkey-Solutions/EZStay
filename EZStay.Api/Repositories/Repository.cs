using Microsoft.EntityFrameworkCore;
using EZStay.Api.Data;

namespace EZStay.Api.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly AppDbContext _context;
        private readonly DbSet<T> _dbSet;

        public Repository(AppDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
            => await _dbSet.AsNoTracking().ToListAsync();

        public async Task<IEnumerable<T>> GetAllWithIncludeAsync(Func<IQueryable<T>, IQueryable<T>> include)
            => await include(_dbSet).AsNoTracking().ToListAsync();

        public async Task<T?> GetByIdAsync(Guid id)
            => await _dbSet.FindAsync(id);

        public async Task<T> AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<T?> UpdateAsync(Guid id, T entity)
        {
            var existing = await _dbSet.FindAsync(id);
            if (existing == null) return null;

            _context.Entry(existing).CurrentValues.SetValues(entity);
            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity == null) return false;

            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public IQueryable<T> Query()
        {
            return _dbSet.AsNoTracking();
        }
    }
}