using Microsoft.AspNetCore.Http;

namespace Domain.DataTransferObjects
{
    public class CreateProductDto
    {
        public string Name { get; set; }
        public IFormFile Image { get; set; }
        public decimal Price { get; set; }
    }
}