namespace Domain.DataTransferObjects
{
    public class ListProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
        public decimal Price { get; set; }
        public int QuantityInStock { get; set; }

    }
}