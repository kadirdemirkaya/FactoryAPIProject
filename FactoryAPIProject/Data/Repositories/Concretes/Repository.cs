using FactoryAPIProject.Data.Repositories.Abstractions;
using FactoryAPIProject.Models.BaseEntity;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace FactoryAPIProject.Data.Repositories.Concretes
{
    public class Repository<T> : IRepository<T> where T : class, EntityBase, new()
    {
        private readonly ApplicationDbContext _context;

        public Repository(ApplicationDbContext context)
        {
            _context = context;
        }

        private DbSet<T> Table { get => _context.Set<T>(); }

        public async Task AddAsync(T entity)
        {
            await Table.AddAsync(entity);
        }

        public async Task AddRangeAsync(List<T> entity)
        {
            await Table.AddRangeAsync(entity);
        }

        public async Task<List<T>> GetAllAsync(System.Linq.Expressions.Expression<Func<T, bool>> predicate = null)
        {
            return await Table.ToListAsync();
        }

        public async Task<T> GetByIdAsync(int? id)
        {
            return await Table.FindAsync(id);
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await GetByIdAsync(id);
            var response = Table.Remove(entity);
        }

        public void Update(T entity)
        {
            Table.Update(entity);
        }
    }
}
