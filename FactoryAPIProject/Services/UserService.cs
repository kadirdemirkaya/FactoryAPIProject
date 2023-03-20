using FactoryAPIProject.Models;
using Microsoft.AspNetCore.Identity;

namespace FactoryAPIProject.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<IdentityUser> _userManager;

        public UserService(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<List<IdentityUser>> GetUsers()
        {
            var users = await _userManager.GetUsersInRoleAsync("User");
            return users.ToList();
        }
    }
}
