using FactoryAPIProject.Models.BaseEntity;
using Microsoft.AspNetCore.Identity;

namespace FactoryAPIProject.Models
{
    public class AppUser : IdentityUser<int> , EntityBase
    {
        public string FullName { get; set; }
        public string Gender { get; set; }
        public string Address { get; set; }
    }
}
