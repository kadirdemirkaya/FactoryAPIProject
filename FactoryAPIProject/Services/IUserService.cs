using FactoryAPIProject.Models;
using Microsoft.AspNetCore.Identity;

namespace FactoryAPIProject.Services
{
    public interface IUserService
    {
        Task<List<IdentityUser>> GetUsers();

        string GetUserName();
    }
}
