using System.ComponentModel.DataAnnotations;

namespace ZentekLabs.Models.Dtos.Request
{
    public class CreateProductRequestDto
    {
        [Required]
        public string Name { get; set; } = default!;

        [Required]
        public string Colour { get; set; } = default!;


        public string? Description { get; set; }

        [Range(0.01, double.MaxValue)]
        public decimal Price { get; set; }
    }
}
