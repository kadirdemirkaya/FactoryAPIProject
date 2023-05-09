using FactoryAPIProject.Data.UnitOfWorks;
using FactoryAPIProject.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FactoryAPIProject.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IUnitOfWork unitOfWork;

        public UserService(UserManager<AppUser> userManager, IHttpContextAccessor httpContextAccessor, IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            this.httpContextAccessor = httpContextAccessor;
            this.unitOfWork = unitOfWork;
        }

        public async Task<List<AppUser>> GetUsers()
        {
            var users = await unitOfWork.GetRepository<AppUser>().GetAllAsync();
            return users;
        }

        public string GetUserName()
        {
            string? userName = httpContextAccessor.HttpContext.User.Identity.Name;
            return userName;
        }
    }
}
