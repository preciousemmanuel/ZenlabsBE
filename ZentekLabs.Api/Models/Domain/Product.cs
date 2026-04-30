namespace ZentekLabs.Models.Domain
{
    public class Product
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = default!;

        public string Description { get; set; } = default!;

        public string Colour { get; set; } = default!;

        public decimal Price { get; set; }

        public int? StockQuantity { get; set; }

        public bool IsActive { get; set; } = true;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedAt { get; set; }= DateTime.UtcNow;
    }
}
