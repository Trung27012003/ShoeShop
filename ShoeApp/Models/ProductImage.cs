using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShoeApp.Models
{
    public class ProductImage
    {
        [Key]
        public Guid Id { get; set; }
        public Guid? ProductId { get; set; }
        public string? ImageUrl { get; set; }
        [ForeignKey("ProductId")]
        public Product? Product { get; set; }
    }
}
