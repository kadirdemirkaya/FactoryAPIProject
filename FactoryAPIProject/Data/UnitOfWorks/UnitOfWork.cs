using FactoryAPIProject.Data.Repositories.Abstractions;
using FactoryAPIProject.Data.Repositories.Concretes;
using System.Security.Cryptography.Xml;

namespace FactoryAPIProject.Data.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        public async ValueTask DisposeAsync()
        {
            await _context.DisposeAsync();
        }

        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }

        IRepository<T> IUnitOfWork.GetRepository<T>()
        {
            return new Repository<T>(_context);
        }
    }
}
