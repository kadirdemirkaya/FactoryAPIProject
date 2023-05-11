using FactoryAPIProject.Models.BaseEntity;
using FactoryAPIProject.Statics;

namespace FactoryAPIProject.Models
{
    public class Image : EntityBase
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public string Path { get; set; }
        public string imagesFolderName { get; set; }
        public byte[]? Data { get; set; }


        public AppUser AppUser { get; set; }


        public Product Product { get; set; }
    }
}
