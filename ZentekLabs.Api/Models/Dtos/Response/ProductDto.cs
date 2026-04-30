namespace ZentekLabs.Models.Dtos.Response
{
    public class ProductDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string? Description { get; set; } 

        public string Colour { get; set; } 

        public decimal Price { get; set; }

        public int? StockQuantity { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}
