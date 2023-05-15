using FactoryAPIProject.Models.BaseEntity;
using Microsoft.AspNetCore.Identity;

namespace FactoryAPIProject.Models
{
    public class AppUserToken : IdentityUserToken<int> , EntityBase
    {
    }
}
