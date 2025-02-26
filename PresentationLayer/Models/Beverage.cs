using System;

namespace PresentationLayer.Models
{
    public class Beverage
    {
        public string Id { get; set; }
        public string CategoryId { get; set; }
        public string? Name { get; set; }
        public string? Size { get; set; }
        public decimal Price { get; set; }
        public string? ImageUrl { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}
