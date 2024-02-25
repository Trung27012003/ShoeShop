using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShoeApp.Models
{
    public class ProductImage
    {
        [Key]
        public Guid Id { get; set; }
        public Guid? ProductDetailId { get; set; }
        public string? ImageUrl { get; set; }
        [ForeignKey("ProductDetailId")]
        public ProductDetail? Product { get; set; }
    }
}
