using FactoryAPIProject.Models.BaseEntity;

namespace FactoryAPIProject.Models
{
    public class Product : EntityBase
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public float Price { get; set; }
    }
}
