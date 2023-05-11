using FactoryAPIProject.Data.Repositories.Abstractions;
using FactoryAPIProject.Data.UnitOfWorks;
using FactoryAPIProject.Models;
using FactoryAPIProject.Statics;
using Microsoft.AspNetCore.Hosting;
using System.Collections;
using static FactoryAPIProject.Controllers.ImageController;

namespace FactoryAPIProject.Services
{
    public class ImageService : IImageService
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IUnitOfWork unitOfWork;
        private readonly IImageRepository imageRepository;

        public ImageService(IWebHostEnvironment webHostEnvironment, IUnitOfWork unitOfWork, IImageRepository imageRepository)
        {
            _webHostEnvironment = webHostEnvironment;
            this.unitOfWork = unitOfWork;
            this.imageRepository = imageRepository;
        }

        public async Task CreateImageAsync(Image image)
        {
            await unitOfWork.GetRepository<Image>().AddAsync(image);
        }

        public async Task DeleteImage(int id)
        {
            await unitOfWork.GetRepository<Image>().DeleteAsync(id);
        }

        public async Task<List<Image>> GetAllImage()
        {
            var images = await unitOfWork.GetRepository<Image>().GetAllAsync();
            return images;
        }

        public async Task<IEnumerable<string>> GetAllImagesPathAsync()
        {
            var images = await imageRepository.GetPathByImageAsync();
            return images;
        }

        public async Task<Image> GetImageByPathAsync(string path)
        {
            var image = await imageRepository.GetImageByPathAsync(path);
            return image;
        }

        public async Task<Image> ImageGetByIdAsync(int id)
        {
            var image = await unitOfWork.GetRepository<Image>().GetByIdAsync(id);
            return image;
        }
    }
}
