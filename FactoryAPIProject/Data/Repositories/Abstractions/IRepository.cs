using FactoryAPIProject.Models.BaseEntity;
using System.Linq.Expressions;

namespace FactoryAPIProject.Data.Repositories.Abstractions
{
    public interface IRepository<T> where T : class, EntityBase, new()
    {
        Task AddAsync(T entity);
        Task AddRangeAsync(List<T> entity);
        Task<List<T>> GetAllAsync(Expression<Func<T, bool>> predicate = null);
        Task<T> GetByIdAsync(int id);
    }
}
