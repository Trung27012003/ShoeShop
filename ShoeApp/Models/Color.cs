using System.ComponentModel.DataAnnotations;

namespace ShoeApp.Models
{
    public class Color
    {
        [Key]
        public Guid ColorId { get; set; }
        public string? ColorName { get; set; }
        public string? ColorCode { get; set; }
        public virtual List<ProductDetail>? ProductDetails { get; set; }
    }
}
