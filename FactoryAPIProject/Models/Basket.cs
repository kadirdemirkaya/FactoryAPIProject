namespace FactoryAPIProject.Models
{
    public class Basket
    {
        public int BasketId { get; set; }
        public DateTime CreatedDate { get; set; }


        public Product Product { get; set; }
    }
}
