using FactoryAPIProject.Models;
using FactoryAPIProject.Statics;
using System.Collections;
using static FactoryAPIProject.Controllers.ImageController;

namespace FactoryAPIProject.Services
{
    public interface IImageService
    {
        Task CreateImageAsync(Image image);

        Task DeleteImage(int id);

        Task<Image> ImageGetByIdAsync(int id);

        Task<IEnumerable<string>> GetAllImagesPathAsync();

        Task<List<Image>> GetAllImage();

        Task<Image> GetImageByPathAsync(string path);

    }
}
