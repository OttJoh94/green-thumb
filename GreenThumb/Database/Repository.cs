using Microsoft.EntityFrameworkCore;

namespace GreenThumb.Database
{
    internal class Repository<T>(GreenDbContext context) where T : class
    {
        private readonly GreenDbContext _context = context;
        private readonly DbSet<T> _dbSet = context.Set<T>();

        public async Task<List<T>> GetAllAsync() => await _dbSet.ToListAsync();
        public async Task<T?> GetByIdAsync(int id) => await _dbSet.FindAsync(id);
        public async Task AddAsync(T entity) => await _dbSet.AddAsync(entity);
        //Gör den void för att det inte finns någon RemoveAsync i entity framework
        public void Remove(T entity) => _dbSet.Remove(entity);
    }
}
