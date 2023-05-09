using Microsoft.AspNetCore.Identity;

namespace FactoryAPIProject.Models
{
    public class AppRole : IdentityRole<int>
    {
        public bool? isActive { get; set; }
    }
}
