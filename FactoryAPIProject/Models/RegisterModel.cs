using FactoryAPIProject.Models.BaseEntity;
using System.ComponentModel.DataAnnotations;

namespace FactoryAPIProject.Models
{
    public class RegisterModel : EntityBase
    {
        public string? Username { get; set; }
        public string? Fullname { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Gender { get; set; }
        public string? Address { get; set; }
        public int imageId { get; set; }

        public string? Password { get; set; }
        public string? ConfirmPassword { get; set; }
    }
}
