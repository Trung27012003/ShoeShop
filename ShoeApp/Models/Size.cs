using System.ComponentModel.DataAnnotations;

namespace ShoeApp.Models
{
    public class Size
    {
        [Key]
        public Guid Id { get; set; }
        public string? SizeName { get; set; }
        public virtual List<ProductDetail>? ProductDetails { get; set; }
    }
}
