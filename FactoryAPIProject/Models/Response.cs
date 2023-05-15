using FactoryAPIProject.Models.BaseEntity;

namespace FactoryAPIProject.Models
{
    public class Response : EntityBase
    {
        public string? Status { get; set; }
        public string? Message { get; set; }
        public bool isSuccess { get; set; }
    }
}
