using FactoryAPIProject.Models.BaseEntity;
using System.ComponentModel.DataAnnotations;

namespace FactoryAPIProject.Models
{
    public class LoginModel : EntityBase
    {
        public string Username { get; set; }

        public string Password { get; set; }
    }
}
