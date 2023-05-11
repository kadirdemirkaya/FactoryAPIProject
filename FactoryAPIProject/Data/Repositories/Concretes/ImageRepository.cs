using FactoryAPIProject.Data.Repositories.Abstractions;
using FactoryAPIProject.Models;
using FactoryAPIProject.Models.BaseEntity;
using Microsoft.EntityFrameworkCore;
using System.Collections;

namespace FactoryAPIProject.Data.Repositories.Concretes
{
    public class ImageRepository : IImageRepository
    {
        private readonly ApplicationDbContext context;
        public ImageRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        private DbSet<Image> Table { get => context.Set<Image>(); }

        public async Task<Image> GetImageByPathAsync(string path)
        {
            var image = await Table.FirstOrDefaultAsync(x => x.Path == path);
            return image;
        }

        public async Task<IEnumerable<string>> GetPathByImageAsync()
        {
            var paths = await Table.Select(i => i.Path).ToListAsync();
            return paths;
        }
    }
}
