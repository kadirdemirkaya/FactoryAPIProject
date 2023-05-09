using FactoryAPIProject.Models.BaseEntity;

namespace FactoryAPIProject.Models
{
    public class ProductModel : EntityBase
    {
        public string ProductName { get; set; }
        public float Price { get; set; }
    }
}
