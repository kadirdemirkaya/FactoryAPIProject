using System.ComponentModel.DataAnnotations;

namespace FactoryAPIProject.Models
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "User Name is required")]
        public string? Username { get; set; }

        [EmailAddress]
        [Required(ErrorMessage = "Email is required")]
        public string? Email { get; set; }

        public string? PhoneNumber { get; set; }
        public string? Gender { get; set; }
        public string? Address { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string? Password { get; set; }
    }
}
