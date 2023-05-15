using FactoryAPIProject.Models.BaseEntity;
using Microsoft.AspNetCore.Identity;

namespace FactoryAPIProject.Models
{
    public class AppRole : IdentityRole<int> , EntityBase
    {
        public bool? isActive { get; set; }
    }
}
