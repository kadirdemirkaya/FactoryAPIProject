namespace FactoryAPIProject.Models
{
    public class FileModel
    {
        public string FileName { get; set; }
        public IFormFile? file { get; set; }
        public string imagesFolderName { get; set; }

        public int? productId { get; set; }
    }
}
