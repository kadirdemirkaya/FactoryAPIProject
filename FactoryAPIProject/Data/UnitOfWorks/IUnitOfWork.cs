using FactoryAPIProject.Data.Repositories.Abstractions;
using FactoryAPIProject.Models.BaseEntity;

namespace FactoryAPIProject.Data.UnitOfWorks
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        IRepository<T> GetRepository<T>() where T : class, EntityBase, new();

        Task<int> SaveAsync();
    }
}
