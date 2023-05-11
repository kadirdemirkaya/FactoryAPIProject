using FactoryAPIProject.Data.Repositories.Abstractions;
using FactoryAPIProject.Data.Repositories.Concretes;
using FactoryAPIProject.Data.UnitOfWorks;
using FactoryAPIProject.Models;
using FactoryAPIProject.Services;
using FactoryAPIProject.Statics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using System.IO;

namespace FactoryAPIProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ImageController : ControllerBase
    {
        private readonly IImageService imageService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly UserManager<AppUser> userManager;
        private readonly IUnitOfWork unitOfWork;
        private readonly IUserService userService;
        private readonly IProductService productService;
        private readonly IRepository<Image> repository;

        public ImageController(IImageService imageService, IWebHostEnvironment webHostEnvironment, IHttpContextAccessor httpContextAccessor, UserManager<AppUser> userManager, IUnitOfWork unitOfWork, IUserService userService, IProductService productService, IRepository<Image> repository)
        {
            this.imageService = imageService;
            _webHostEnvironment = webHostEnvironment;
            this.httpContextAccessor = httpContextAccessor;
            this.userManager = userManager;
            this.unitOfWork = unitOfWork;
            this.userService = userService;
            this.productService = productService;
            this.repository = repository;
        }

        [HttpPost]
        [Route("UploadFile")]
        public async Task<IActionResult> UploadFile([FromForm] FileModel fileModel)
        {
            try
            {
                if (fileModel.imagesFolderName == ImagesFolderName.ProductImages)
                {
                    string path2 = @$"C:\Users\Casper\Desktop\GitHub Projects\FactoryAPIProject\FactoryAPIProject\wwwroot\Images\{ImagesFolderName.ProductImages}\";

                    string timestamp = DateTime.Now.ToString("yyyyMMddHHmmss");
                    string fileNameWithTimestamp = $"{fileModel.FileName}_{timestamp}.jpg";
                    string path = Path.Combine(path2, fileNameWithTimestamp);

                    using (Stream stream = new FileStream(path, FileMode.Create))
                    {
                        fileModel.file.CopyTo(stream);
                    }

                    var product = await productService.GetProductById((int)fileModel.productId);//product

                    var productImageId = product.ImageId;

                    var imageToProduct = imageService.ImageGetByIdAsync(productImageId);//image


                    if (imageToProduct.Result.imagesFolderName == ImagesFolderName.DefaultImageProduct)
                    {
                        Image image = new()
                        {
                            FileName = fileModel.FileName,
                            Path = path,
                            imagesFolderName = ImagesFolderName.ProductImages
                        };
                        await imageService.CreateImageAsync(image);
                        await unitOfWork.SaveAsync();

                        product.ImageId = image.Id; // ???
                        await unitOfWork.SaveAsync();
                        return Ok(new
                        {
                            Message = "File Saved"
                        });
                    }
                    else
                    {
                        string imagePath = imageToProduct.Result.Path;
                        try
                        {
                            FileInfo fileInfo = new FileInfo(imagePath);
                            fileInfo.Delete();

                            imageToProduct.Result.Path = path;
                            imageToProduct.Result.imagesFolderName = ImagesFolderName.ProductImages;
                            imageToProduct.Result.FileName = fileModel.FileName;

                            await unitOfWork.SaveAsync();
                            return Ok(new
                            {
                                Message = "File Saved"
                            });
                        }
                        catch (Exception ex)
                        {
                            return Ok(new
                            {
                                Message = "File NOT SAVED"
                            });
                            throw ex;
                        }
                    }
                }
                else if (fileModel.imagesFolderName == ImagesFolderName.DefaultImageUser)
                {
                    string path = @$"C:\Users\Casper\Desktop\GitHub Projects\FactoryAPIProject\FactoryAPIProject\wwwroot\Images\DefaultImages\{ImagesFolderName.DefaultImageUser}\user.png";
                    //TODO: sonradan default resim atamaları yapabilirsin
                    return Ok(new
                    {
                        Message = "File Saved"
                    });

                }
                else if (fileModel.imagesFolderName == ImagesFolderName.UserImages)
                {
                    string path2 = @$"C:\Users\Casper\Desktop\GitHub Projects\FactoryAPIProject\FactoryAPIProject\wwwroot\Images\{ImagesFolderName.UserImages}\";

                    string timestamp = DateTime.Now.ToString("yyyyMMddHHmmss");
                    string fileNameWithTimestamp = $"{fileModel.FileName}_{timestamp}.jpg";
                    string path = Path.Combine(path2, fileNameWithTimestamp);

                    using (Stream stream = new FileStream(path, FileMode.Create))
                    {
                        fileModel.file.CopyTo(stream);
                    }

                    var currentUserName = httpContextAccessor.HttpContext.User.Identity.Name;
                    var user = await userManager.FindByNameAsync(currentUserName);//user 

                    var userImageId = user.imageId;

                    var imageToUser = imageService.ImageGetByIdAsync(userImageId);//image

                    #region user.png
                    //user.png
                    //C:\Users\Casper\Desktop\GitHub Projects\FactoryAPIProject\FactoryAPIProject\wwwroot\Images\DefaultImages\DefaultImageUser\user.png
                    //DefaultImageUser
                    #endregion

                    #region carpi.png
                    //carpi.png
                    //C:\Users\Casper\Desktop\GitHub Projects\FactoryAPIProject\FactoryAPIProject\wwwroot\Images\DefaultImages\DefaultImageProduct\carpi.png
                    //DefaultImageProduct
                    #endregion

                    if (imageToUser.Result.imagesFolderName == ImagesFolderName.DefaultImageUser)
                    {
                        Image image = new()
                        {
                            FileName = fileModel.FileName,
                            Path = path,
                            imagesFolderName = ImagesFolderName.UserImages,
                        };
                        await imageService.CreateImageAsync(image);
                        await unitOfWork.SaveAsync();

                        user.imageId = image.Id; // ???
                        await unitOfWork.SaveAsync();
                        return Ok(new
                        {
                            Message = "File Saved"
                        });
                    }
                    else
                    {
                        string imagePath = imageToUser.Result.Path;
                        try
                        {
                            FileInfo fileInfo = new FileInfo(imagePath);
                            fileInfo.Delete();

                            imageToUser.Result.Path = path;
                            imageToUser.Result.imagesFolderName = ImagesFolderName.UserImages;
                            imageToUser.Result.FileName = fileModel.FileName;

                            await unitOfWork.SaveAsync();
                            return Ok(new
                            {
                                Message = "File Saved"
                            });
                        }
                        catch (Exception ex)
                        {
                            return Ok(new
                            {
                                Message = "File NOT SAVED"
                            });
                            throw ex;
                        }
                    }
                }
            }
            catch (Exception)
            {
                return Ok(new
                {
                    Message = "File NOT SAVED !"
                });
            }
            return Ok(new
            {
                Message = "File NOT SAVED !"
            });
        }

        [HttpGet]
        [Route("GetImagesToPath")]
        public async Task<IEnumerable<string>> GetImagesToPath()
        {
            var paths = await imageService.GetAllImagesPathAsync();
            return paths;
        }

        [HttpGet]
        [Route("GetImages")]
        public async Task<IActionResult> GetImages()
        {
            var images = await imageService.GetAllImage();

            if (images == null || images.Count == 0)
            {
                return NotFound(new
                {
                    Message = "Picture is empty",
                    isSuccess = false
                });
            }

            List<FileContentResult> results = new List<FileContentResult>();
            var provider = new FileExtensionContentTypeProvider();

            foreach (var image in images)
            {
                var imagePath = image.Path;

                if (!System.IO.File.Exists(imagePath))
                {
                    continue;
                }

                if (!provider.TryGetContentType(imagePath, out var contentType))
                {
                    contentType = "application/octet-stream";
                }

                var file = System.IO.File.ReadAllBytes(imagePath);

                var result = new FileContentResult(file, contentType)
                {
                    FileDownloadName = Path.GetFileName(imagePath)
                };

                results.Add(result);
            }

            if (results.Count == 0)
            {
                return NotFound(new
                {
                    Message = "Picture is null",
                    isSuccess = false
                });
            }

            return new JsonResult(new
            {
                Images = results,
                isSuccess = true
            });
        }
    }
}
