using FactoryAPIProject.Models;
using Microsoft.AspNetCore.Identity;

namespace FactoryAPIProject.Services
{
    public interface IUserService
    {
        Task<List<AppUser>> GetUsers();

        string GetUserName();

        Task DeleteUser(int id);

        void UpdateUser(AppUser appUser);

    }
}
