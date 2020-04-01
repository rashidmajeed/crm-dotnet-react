using Microsoft.AspNetCore.Http;

namespace Domain.DataTransferObjects
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
        public IFormFile Image { get; set; }
        public decimal Price { get; set; }
        public string Available { get; set; }

    }
}