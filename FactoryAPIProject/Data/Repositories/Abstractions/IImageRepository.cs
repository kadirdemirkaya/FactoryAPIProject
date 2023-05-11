using FactoryAPIProject.Models;
using System.Collections;

namespace FactoryAPIProject.Data.Repositories.Abstractions
{
    public interface IImageRepository
    {
        Task<IEnumerable<string>> GetPathByImageAsync();

        Task<Image> GetImageByPathAsync(string path);
    }
}
