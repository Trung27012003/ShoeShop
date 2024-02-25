using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShoeApp.Models
{
    public class Rate
    {
        [Key]
        public Guid Id { get; set; }
        public Guid OrderItemId { get; set; }
        public string? Content { get; set; }
        public string? Reply { get; set; }
        public string? ImageUrl { get; set; }
        public float? Rating { get; set; }
        public int Status { get; set; }
        [ForeignKey("OrderItemId")]
        public OrderItem? OrderItem { get; set; }
    }
}
